(function ($) {
    $.fn.loadDropdownData = function (url, data = {}, valueField = "id", textField = "name", customSuccessCallback = null) {
        const $dropdown = this;
        const deferred = $.Deferred();

        $.ajax({
            type: 'GET',
            url: url,
            data: data,
            dataType: 'json',
            success: function (response) {

                if (typeof customSuccessCallback === 'function') {
                    customSuccessCallback(response, $dropdown);
                } else {
                    $dropdown.empty().append('<option value="">Select</option>');
                    $.each(response.data, function (index, item) {
                        $dropdown.append(`<option value="${item[valueField]}">${item[textField]}</option>`);
                    });
                }
                deferred.resolve(); // ✅ resolve when done
            },
            error: function (xhr, status, error) {
                console.error("Dropdown load error:", error);
                deferred.reject(error); // ❌ reject on failure
            }
        });

        return deferred.promise(); // Return promise
    };
})(jQuery);
