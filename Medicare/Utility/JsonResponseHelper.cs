using Microsoft.AspNetCore.Mvc;

namespace Medicare.Utility
{
    public static class JsonResponseHelper
    {
        public static JsonResult CreateFailureResponse(string message = "An Error Occurred", object? data = null)
        {
            return new JsonResult(new
            {
                success = false,
                message = message,
                data = data
            });
        }

        public static JsonResult CreateSuccessResponse(object? data = null, string message = "Operation Successful")
        {
            bool isSuccess = false;

            if (data != null)
            {
                // Check if data is an integer and > 0
                if (data is int intValue)
                {
                    isSuccess = intValue > 0;
                }
                // If it's a non-empty string
                else if (data is string strValue)
                {
                    isSuccess = !string.IsNullOrWhiteSpace(strValue);
                }
                else
                {
                    // For any other non-null data (e.g., complex objects, lists, etc.)
                    isSuccess = true;
                }
            }

            return new JsonResult(new
            {
                success = isSuccess,
                message = message,
                data = data
            });
        }
    }
}
