document.addEventListener('DOMContentLoaded', function () {
    const AjaxLoader = (function () {
        // Initialize AJAX Loader
        const ajaxLoaderEl = document.createElement('div');
        ajaxLoaderEl.className = 'ajax-loader-overlay';
        ajaxLoaderEl.style.cssText = 'display:none;'
        ajaxLoaderEl.innerHTML = `
            <div class="loader-pulse"></div>
            <div class="loader-text">Please Wait</div>
        `;
        document.body.appendChild(ajaxLoaderEl);

        return {
            show: function (message = 'Please Wait') {
                const textEl = ajaxLoaderEl.querySelector('.ajax-loader-text');
                if (textEl) textEl.textContent = message;
                ajaxLoaderEl.style.display = 'flex';
            },
            hide: function () {
                ajaxLoaderEl.style.display = 'none';
            }
        };
    })();

  
    const TableLoader = (function () {
        // Create loader styles (only once)
        const styleId = 'table-loader-styles';
        if (!document.getElementById(styleId)) {
            const style = document.createElement('style');
            style.id = styleId;
            document.head.appendChild(style);
        }

        return {
            /**
             * Initialize the loader for a table
             * @param {jQuery} $table - The table jQuery element
             * @param {Object} options - Optional configuration
             */
            init: function ($table, options = {}) {
                const defaults = {
                    minHeight: '200px',  // Minimum container height
                    emptyTableHeight: '150px', // Height when table has no rows
                    messages: {
                        default: 'Loading',
                        sorting: 'Sorting data',
                        searching: 'Searching',
                        pagination: 'Loading page',
                        refreshing: 'Refreshing'
                    }
                };
                options = $.extend({}, defaults, options);

                // Wrap table in a container if not already wrapped
                if (!$table.parent().hasClass('table-loader-container')) {
                    $table.wrap('<div class="table-loader-container"></div>');
                }

                // Apply minimum height
                const $container = $table.parent();
                $container.css('min-height', options.minHeight);

                // Create loader element if it doesn't exist
                if (!$table.siblings('.table-loader-overlay').length) {
                    const $overlay = $(`
                    <div class="table-loader-overlay" style="display: none;">
                        <div class="table-loader-spinner"></div>
                        <div class="table-loader-text">Loading...</div>
                    </div>
                `);
                    $table.before($overlay);
                }

                // Ensure table has minimum height when empty
                if ($table.find('tbody tr').length === 0) {
                    $table.css('min-height', options.emptyTableHeight);
                }

                const $overlay = $container.find('.table-loader-overlay');
                const $text = $overlay.find('.table-loader-text');

                return {
                    /**
                     * Show the loader with custom text
                     * @param {string} text - Loading text to display
                     */
                    show: function (action = 'default') {
                        const message = options.messages[action] || options.messages.default;
                        $text.text(message);
                        // Ensure container has minimum height
                        $container.css('min-height', options.minHeight);
                        $table.addClass('table-loader-blur');
                        $overlay.fadeIn(200);
                    },

                    /**
                     * Hide the loader
                     */
                    hide: function () {
                        $table.removeClass('table-loader-blur');
                        $overlay.fadeOut(200);

                        // Reset min-height if table has content
                        if ($table.find('tbody tr').length > 0) {
                            $table.css('min-height', '');
                        }
                    },

                    /**
                     * Update loader text
                     * @param {string} text - New loading text
                     */
                    setText: function (text) {
                        $table.parent().find('.table-loader-text').text(text);
                    }
                };
            }
        };
    })();

    // Alert System
    const AlertSystem = (function () {
        // Shared config for both alert types
        const baseConfig = {
            background: 'white',
            width: '380px',
            padding: '1rem',
            showConfirmButton: true,
            confirmButtonText: 'OK',
            customClass: {
                container: 'compact-swal',
                popup: 'compact-swal-popup',
                title: 'compact-swal-title',
                htmlContainer: 'compact-swal-content',
                confirmButton: 'btn btn-sm'
            }
        };

        // Success Alert
        function showSuccess(title = "Success!", message = "Operation Successful", duration = 3000) {
            Swal.fire({
                title: title,
                text: message,
                icon: 'success',
                //timer: duration,
                //timerProgressBar: true,
                showConfirmButton: true,
                background: 'white',
                iconColor: '#28a745',
                customClass: {
                    title: 'swal-title-success',
                    content: 'swal-text-success'
                }
            });
        }

        // Error Alert
        function showError(title = "Error!", message = "Operation Failed", duration = 5000) {
            Swal.fire({
                title: title,
                text: message,
                icon: 'error',
                timer: duration,
                timerProgressBar: true,
                showConfirmButton: true,
                confirmButtonColor: '#dc3545',
                background: 'white',
                iconColor: '#dc3545',
                customClass: {
                    title: 'swal-title-error',
                    content: 'swal-text-error'
                }
            });
        }

        return {
            success: showSuccess,
            error: showError
        };
    })();

    // Make available globally
    window.AjaxLoader = AjaxLoader;
    window.TableLoader = TableLoader;
    window.AlertSystem = AlertSystem;
});