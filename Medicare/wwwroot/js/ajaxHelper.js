function postData(url, data, settings = {}) {
    const {
        loader = true,
        loaderText = 'Please Wait',
        showSuccess = true,
        showError = true,
        success,
        error
    } = settings;

    $.ajax({
        type: 'POST',
        url: url,
        data: data,
        dataType: 'json',
        beforeSend: function () {
            if (loader) {
                AjaxLoader.show(loaderText);
            }
        },
        success: function (response) {
            if (response.success) {
                if (showSuccess) {
                    AlertSystem.success('Success!', response.message);
                }

                if (typeof success === 'function') {
                    success(response);
                }
            }
            else if (showError) {
                AlertSystem.error('Error!', response.message);
            }
        },
        error: function (xhr, status, err) {
            if (showError)
                AlertSystem.error('Error', err)
            console.error('AJAX error:', err);
            error(xhr, status, err); // call your error callback
        },
        complete: function () {
            AjaxLoader.hide();
        }
    });
}

function deleteData(url, data, settings = {}) {
    //debugger;
    const {
        loader = true,
        loaderText = 'Deleting...',
        showSuccess = true,
        showError = true,
        confirmTitle = "Are you sure?",
        confirmText = "This action cannot be undone!",
        confirmButtonText = "Yes, delete it!",
        success,
        error
    } = settings;

    Swal.fire({
        title: confirmTitle,
        text: confirmText,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: confirmButtonText
    }).then((result) => {
        //debugger
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: url,
                data: data,
                dataType: 'json',
                beforeSend: function () {
                    if (loader) {
                        AjaxLoader.show(loaderText);
                    }
                },
                success: function (response) {
                    //debugger
                    if (response.success) {
                        if (showSuccess) {
                            AlertSystem.success("Deleted!", response.message);
                        }
                        if (typeof success === "function") {
                            success(response);
                        }
                    } else if (showError) {
                        AlertSystem.error("Error!", response.message);
                    }
                },
                error: function (xhr, status, err) {
                    //debugger
                    if (showError) {
                        AlertSystem.error("Error", err);
                    }
                    console.error("AJAX error:", err);
                    if (typeof error === "function") {
                        error(xhr, status, err);
                    }
                },
                complete: function () {
                    //debugger;
                    AjaxLoader.hide();
                }
            });
        }
    });
}


(function ($) {
    $.fn.populateData = function (url, requestData = {}, settings = {}) {
        const {
            loader = true,
            loaderText = 'Please Wait',
            showSuccess = true,
            showError = true,
            success,
            error
        } = settings;

        const $form = this;
        const deferred = $.Deferred();

        if (!$form.length || !$form.is('form')) {
            console.warn('populateData should be called on a form element');
            return;
        }

        $.ajax({
            url: url,
            method: 'GET',
            data: requestData,
            success: function (response) {
                //console.log(response);
                // Assuming response is a flat JSON object
                for (const key in response.data) {
                    let value = response.data[key];

                    // Find input/select/textarea by name
                    const $field = $form.find(`[name="${key}"]`);

                    if ($field.length) {
                        const type = $field.attr('type');

                        // Handle Microsoft JSON Date format
                        if (typeof value === 'string' && /\/Date\((\d+)\)\//.test(value)) {
                            debugger;
                            const timestamp = parseInt(value.match(/\/Date\((\d+)\)\//)[1], 10);
                            const date = new Date(timestamp);

                            if (type === 'datetime-local') {
                                value = date.toISOString().slice(0, 16); // yyyy-MM-ddTHH:mm
                            } else if (type === 'date') {
                                value = date.toISOString().slice(0, 10); // yyyy-MM-dd
                            } else if (type === 'time') {
                                value = date.toISOString().slice(11, 16); // HH:mm
                            }
                        }

                        if (type === 'checkbox') {
                            $field.prop('checked', !!value);
                        } else if (type === 'radio') {
                            $form.find(`[name="${key}"][value="${value}"]`).prop('checked', true);
                        } else {
                            $field.val(value);
                        }
                    }
                }

                deferred.resolve(response); // Return data for chaining
            },
            error: function (xhr, status, error) {
                console.error('Failed to load data:', error);
                if (showError) {
                    AlertSystem.error('Error!', response.message);
                }
                deferred.reject(error);
            },
            beforeSend: function () {
                if (loader) {
                    AjaxLoader.show(loaderText);
                }
            },
            complete: function () {
                AjaxLoader.hide();
            }
        });

        return deferred.promise();
    };
})(jQuery);

