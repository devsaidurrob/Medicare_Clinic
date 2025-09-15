(function ($) {
    $.fn.customedatatable = function (ajaxUrl, data = {}, settings = {}, searchBoxId = null) {
        const $table = this, $thead = $table.find('thead'), $tbody = $table.find('tbody');
        const $pagination = $('ul.pagination'), $paginationInfo = $('.pagination-info');
        let currentPage = 1, pageSize = settings.length || 10, totalRecords = 0;
        let sortColumn = '', sortDir = 'asc', searchTerm = '';

        // Initialize table loader
        // Initialize table loader with custom messages
        const tableLoader = TableLoader.init($table, {
            messages: {
                default: 'Loading data',
                sorting: 'Sorting records',
                searching: 'Searching',
                pagination: 'Loading page',
                refreshing: 'Refreshing'
            }
        });

        // Extract column keys from <th data-*>
        const columnMap = [];
        $thead.find('th').each(function () {
            const $th = $(this);
            const dataKey = Object.keys(this.dataset)[0]; // e.g., data-name => "name"
            columnMap.push(dataKey ? dataKey.toLowerCase() : null); // May be null for 'Actions'
        });

        const loadData = (action = 'default') => {
            const tableData = $table.data('customedatatable-data') || {};
            const filterData = tableData.Data || {};

            $.ajax({
                url: ajaxUrl,
                method: 'GET',
                data: {
                    ...data,                 // plugin's default top-level data
                    ...filterData,           // dynamically add all custom filters
                    start: (currentPage - 1) * pageSize,
                    length: pageSize,
                    sortColumn, sortDir,
                    search: searchTerm
                },
                success: res => {
                    console.log(res);
                    // Since API sends { total, records }
                    const records = (res.data.records || []).map(normalizeDataKeys);
                    totalRecords = res.data.total || records.length;
                    $tbody.empty();

                    if (records.length === 0)
                        return $tbody.append('<tr><td colspan="100%">No records found</td></tr>');

                    records.forEach(row => {
                        
                        const $tr = $('<tr>');
                        columnMap.forEach(key => {
                            if (key) {
                                $tr.append(`<td>${row[key] ?? ''}</td>`);
                            } else {
                                if (typeof settings.rowActionBuilder === 'function') {
                                    
                                    const tdHtml = settings.rowActionBuilder(row); // Return complete <td>...</td>
                                    $tr.append(tdHtml);
                                } else {
                                    $tr.append('<td></td>'); // fallback
                                }
                            }
                        });
                        $tbody.append($tr);
                    });

                    renderPagination();
                    const start = totalRecords === 0 ? 0 : (currentPage - 1) * pageSize + 1;
                    const end = Math.min(currentPage * pageSize, totalRecords);
                    $paginationInfo.text(`Showing ${start}-${end} of ${totalRecords} records`);

                    // ✅ Call callback on successful data load
                    if (typeof settings.onDataLoaded === 'function') {
                        settings.onDataLoaded({
                            filtersApplied: filterData,   // current filters
                            totalRecords,
                            currentPage,
                            pageSize
                        });
                    }
                },
                error: () => $tbody.html('<tr><td colspan="100%">Failed to load data</td></tr>'),
                beforeSend: function () {

                    //console.log('AJAX data being sent:', {
                    //    ...data,
                    //    ...filterData,
                    //    start: (currentPage - 1) * pageSize,
                    //    length: pageSize,
                    //    sortColumn,
                    //    sortDir,
                    //    search: searchTerm
                    //});

                    tableLoader.show(action);
                },
                complete: function () {
                    tableLoader.hide();
                }
            });
        };

        const renderPagination = () => {
            const totalPages = Math.ceil(totalRecords / pageSize);
            $pagination.empty().append(
                `<li class="page-item ${currentPage === 1 ? 'disabled' : ''}">
                    <a class="page-link" href="#"><i class="bi bi-chevron-left"></i></a>
                </li>`
            );

            for (let i = 1; i <= totalPages; i++) {
                $pagination.append(
                    `<li class="page-item ${i === currentPage ? 'active' : ''}">
                        <a class="page-link" href="#">${i}</a>
                    </li>`
                );
            }

            $pagination.append(
                `<li class="page-item ${currentPage === totalPages ? 'disabled' : ''}">
                    <a class="page-link" href="#"><i class="bi bi-chevron-right"></i></a>
                </li>`
            );
        };

        const initEvents = () => {
            $thead.find('th.sortable').css('cursor', 'pointer').on('click', function () {
                const key = Object.keys(this.dataset)[0];
                if (sortColumn === key) sortDir = sortDir === 'asc' ? 'desc' : 'asc';
                else { sortColumn = key; sortDir = 'asc'; }
                $thead.find('th.sortable').removeClass('asc desc');
                $(this).addClass(sortDir);
                currentPage = 1;
                loadData('sorting');
            });

            $pagination.on('click', 'li.page-item:not(.disabled):not(.active) a', function (e) {
                e.preventDefault();
                const txt = $(this).text().trim();
                const totalPages = Math.ceil(totalRecords / pageSize);
                if ($(this).find('.bi-chevron-left').length) currentPage = Math.max(1, currentPage - 1);
                else if ($(this).find('.bi-chevron-right').length) currentPage = Math.min(totalPages, currentPage + 1);
                else currentPage = parseInt(txt);
                loadData('pagination');
            });

            if (searchBoxId && $(searchBoxId).length) {
                $(searchBoxId).on('input', function () {
                    searchTerm = $(this).val().trim();
                    currentPage = 1;
                    loadData('searching');
                });
            }
        };

        const normalizeDataKeys = row => {
            const result = {};
            for (let key in row) if (row.hasOwnProperty(key)) result[key.toLowerCase()] = row[key];
            return result;
        };

        initEvents();
        loadData();

        // Add refresh method to the table element for external calls
        $table.data('refresh', () => {
            loadData('refreshing');
        });

        return this;
    };
})(jQuery);


function normalizeDataKeys(row) {
    const normalized = {};
    for (let key in row) {
        if (row.hasOwnProperty(key)) {
            normalized[key.toLowerCase()] = row[key];
        }
    }
    return normalized;
}
