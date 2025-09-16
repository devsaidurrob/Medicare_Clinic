using AutoMapper;
using Medicare.Repository.Entity;
using Medicare.Repository.Interfaces;
using Medicare.Repository.Repository;
using Medicare.Repository.Utility;
using Medicare.Utility;
using Medicare.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Medicare.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        public UserController(IUserRepository repo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetUsers(UserFilter filter)
        {
            try
            {
                var users = await _repo.GetAllAsync(filter);
                var viewModels = _mapper.Map<PagingResult<UserViewModel>>(users);
                return JsonResponseHelper.CreateSuccessResponse(viewModels);
            }
            catch (Exception ex)
            {
                return JsonResponseHelper.CreateFailureResponse(ex.Message);
            }
        }
        [HttpPost]
        public async Task<JsonResult> AddUser(CreateUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return JsonResponseHelper.CreateFailureResponse("Validation failed", errors);
            }
            try
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword("Medicare@123");

                var user = _mapper.Map<User>(viewModel);
                user.Id = Guid.NewGuid();
                user.PasswordHash = hashedPassword;
                user.Roles = viewModel.UserRoles.Select(x => new UserRole()
                {
                     RoleId = x,
                     UserId = user.Id
                }).ToList();
                var userResult = await _repo.AddAsync(user);

                int retVal = await _unitOfWork.SaveChangesAsync();
                if (retVal > 0)
                    return JsonResponseHelper.CreateSuccessResponse(_mapper.Map<UserViewModel>(userResult), "User Created successfully");
                else
                    return JsonResponseHelper.CreateFailureResponse("Failed to Create User");
            }
            catch (Exception e)
            {
                return JsonResponseHelper.CreateFailureResponse(e.Message);
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteUser(Guid id)
        {
            try
            {
                var success = await _repo.DeleteAsync(id);
                if (success)
                {
                    return JsonResponseHelper.CreateSuccessResponse(success, "Doctor deleted successfully");
                }
                else
                {
                    return JsonResponseHelper.CreateFailureResponse("Doctor not found");
                }
            }
            catch (Exception e)
            {
                return JsonResponseHelper.CreateFailureResponse(e.Message);
            }
        }
    }
}
