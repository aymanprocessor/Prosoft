$(document).ready(function () {
    // Get data from server (these should be passed from the Razor view)
    var priceLists =  [];
    var priceListDetails = [];
    var mainClinic = [];
    var subClinic = [];
    var services = [];

    // Price List Type options
    var priceListTypes = [
        { value: 1, text: 'خاص' },
        { value: 2, text: 'تعاقد' }
    ];

    var coveredOptions = [
        { value: 1, text: "نعم" },
        { value: 2, text: "لا" }
    ];

    // Load dropdown data via AJAX
    loadDropdownData();

    if ($.fn.DataTable.isDataTable('#PriceList')) {
        $('#PriceList').DataTable().destroy();
    }

    // Initialize Price List DataTable
    var priceListTable = $('#PriceList').DataTable({
        ajax: {
            url: '/Medical/PriceListNew/GetPriceList',
            type: "GET",
            dataSrc: function (json) {
                console.log('🔄 Raw AJAX response:', json);

                if (!json) {
                    console.warn('⚠️ No data returned from server.');
                    return [];
                }

                if (Array.isArray(json)) {
                    console.log('✅ Response is a raw array.');
                    return json;
                }

                if (json.data && Array.isArray(json.data)) {
                    console.log('✅ Response has a "data" array property.');
                    return json.data;
                }

                console.error('❌ Unexpected response format. Returning empty array.');
                return [];
            }
        },
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.13.7/i18n/ar.json'
        },
        columns: [
            {
                data: 'plId',
                title: 'كود',
                width: '50px'
            },
            {
                data: 'plDesc',
                title: 'اسم القائمة',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="text" class="form-control editable-field" data-field="plDesc" data-id="' + row.plId + '" value="' + (data || '') + '" required>';
                    }
                    return data;
                }
            },
            {
                data: 'flag1',
                title: 'نوع القائمة',
                width: '100px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        var options = '';
                        priceListTypes.forEach(function (option) {
                            var selected = option.value == data ? 'selected' : '';
                            options += '<option value="' + option.value + '" ' + selected + '>' + option.text + '</option>';
                        });
                        return '<select class="form-control editable-field" data-field="flag1" data-id="' + row.plId + '" required>' + options + '</select>';
                    }
                    return data;
                }
            },
            {
                data: 'plDate',
                title: 'تاريخ القائمة',
                width: '120px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        var dateValue = data ? new Date(data).toISOString().split('T')[0] : '';
                        return '<input type="date" class="form-control editable-field" data-field="plDate" data-id="' + row.plId + '" value="' + dateValue + '" required>';
                    }
                    return data;
                }
            },
            {
                data: 'year',
                title: 'السنة المالية',
                width: '100px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control editable-field" data-field="year" data-id="' + row.plId + '" value="' + (data || '') + '" required>';
                    }
                    return data;
                }
            },
            {
                title: 'الإجراءات', 
                orderable: false,   
                data: null,
                render: function (data, type, row ) {
                    return `<button class="btn btn-sm btn-danger delete-btn" data-id="${row.plId}"><i class="bi bi-trash3"></i></button>`;
                }
            }
        ],
      
        dom:
            "<'row mb-2 mt-3'" +
            "<'col-sm-6 d-flex justify-content-start'B>" +             // Top-left: buttons
            "<'col-sm-6 d-flex justify-content-end'f>" +                                       // Top-right: search box
            ">" +
            "<'row'<'col-sm-12'tr>>" +                                  // Table
            "<'row mt-2'" +
            "<'col-sm-4'l>" +                                       // Bottom-left: length menu
            "<'col-sm-4 d-flex justify-content-center'p>" +                           // Bottom-center: pagination
            "<'col-sm-4 text-end'i>" +                              // Bottom-right: info
            ">",
        buttons: [
            {
                text: '+',
                className: 'btn btn-success', // Bootstrap success (green)
                action: function (e, dt, node, config) {
                    addNewPriceListRow();
                }
            },
                {
                text: 'حفظ',
                className: 'btn btn-warning ms-1',
                action: function (e, dt, node, config) {
                    savePriceListRecord();
                }
            }
            
            
           
        ]
    });

    // Initialize Price List Detail DataTable
    var priceListDetailTable = $('#PriceListDetail').DataTable({
        data: priceListDetails,
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.13.7/i18n/ar.json'
        },
        columns: [
            {
                data: 'PLDtlId',
                title: 'كود',
                width: '70px'
            },
            {
                data: 'ClinicId',
                title: 'مستوى 1',
                width: '200px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return createSelectOptions(mainClinic, data, 'ClinicId', row.PLDtlId);
                    }
                    return data;
                }
            },
            {
                data: 'SClinicId',
                title: 'مستوى 2',
                width: '200px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return createSelectOptions(subClinic, data, 'SClinicId', row.PLDtlId);
                    }
                    return data;
                }
            },
            {
                data: 'ServId',
                title: 'مستوى 3',
                width: '200px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return createSelectOptions(services, data, 'ServId', row.PLDtlId);
                    }
                    return data;
                }
            },
            {
                data: 'ServBefDesc',
                title: 'الخدمة قبل الخصم',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control editable-field calculation-field" data-field="ServBefDesc" data-id="' + row.PLDtlId + '" value="' + (data || 0) + '">';
                    }
                    return data;
                }
            },
            {
                data: 'DiscoutComp',
                title: 'نسبة خصم الخدمة',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control editable-field calculation-field" data-field="DiscoutComp" data-id="' + row.PLDtlId + '" value="' + (data || 0) + '">';
                    }
                    return data;
                }
            },
            {
                data: 'PlValue',
                title: 'الخدمة بعد الخصم',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control" data-field="PlValue" data-id="' + row.PLDtlId + '" value="' + (data || 0) + '" readonly>';
                    }
                    return data;
                }
            },
            {
                data: 'CompCovPercentage',
                title: 'نسبة الشركة',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control editable-field" data-field="CompCovPercentage" data-id="' + row.PLDtlId + '" value="' + (data || 100) + '">';
                    }
                    return data;
                }
            },
            {
                data: 'CompValue',
                title: 'قيمة تحمل الشركة',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control editable-field" data-field="CompValue" data-id="' + row.PLDtlId + '" value="' + (data || 0) + '">';
                    }
                    return data;
                }
            },
            {
                data: 'PlValue2',
                title: 'تحمل العضو',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control editable-field" data-field="PlValue2" data-id="' + row.PLDtlId + '" value="' + (data || 0) + '">';
                    }
                    return data;
                }
            },
            {
                data: 'PlValue3',
                title: 'تحمل القريب',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control editable-field" data-field="PlValue3" data-id="' + row.PLDtlId + '" value="' + (data || 0) + '">';
                    }
                    return data;
                }
            },
            {
                data: 'ExtraVal',
                title: 'تحمل مستلزمات علي المريض',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control editable-field" data-field="ExtraVal" data-id="' + row.PLDtlId + '" value="' + (data || 0) + '">';
                    }
                    return data;
                }
            },
            {
                data: 'ExtraVal2',
                title: 'تحمل صيانة علي المريض',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control editable-field" data-field="ExtraVal2" data-id="' + row.PLDtlId + '" value="' + (data || 0) + '">';
                    }
                    return data;
                }
            },
            {
                data: 'Covered',
                title: 'تغطي الخدمة',
                width: '100px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return createSelectOptions(coveredOptions, data || 2, 'Covered', row.PLDtlId);
                    }
                    return data;
                }
            },
            {
                data: null,
                title: 'الإجراءات',
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<button class="btn btn-sm btn-success save-detail-btn" data-id="' + row.PLDtlId + '">حفظ</button> ' +
                        '<button class="btn btn-sm btn-danger delete-detail-btn" data-id="' + row.PLDtlId + '">حذف</button>';
                }
            }
        ],
        dom:"<'row mb-2 mt-3'" +
            "<'col-sm-6 d-flex justify-content-start'B>" +             // Top-left: buttons
            "<'col-sm-6 d-flex justify-content-end'f>" +                                       // Top-right: search box
            ">" +
            "<'row'<'col-sm-12'tr>>" +                                  // Table
            "<'row mt-2'" +
            "<'col-sm-4'l>" +                                       // Bottom-left: length menu
            "<'col-sm-4 d-flex justify-content-center'p>" +                           // Bottom-center: pagination
            "<'col-sm-4 text-end'i>" +                              // Bottom-right: info
            ">",
        buttons: [
            {
                text: '+',
                className: 'btn btn-success',
                action: function (e, dt, node, config) {
                    addNewPriceListDetailRow();
                }
            },
            {
                text: 'حفظ',
                className: 'btn btn-warning ms-1',
                action: function (e, dt, node, config) {
                    savePriceListRecord();
                }
            }
        ],
        responsive: true,
        pageLength: 10,
        scrollX: true
    });

    // Helper function to create select options
    function createSelectOptions(options, selectedValue, fieldName, id) {
        var selectHtml = '<select class="form-control editable-field" data-field="' + fieldName + '" data-id="' + id + '">';
        selectHtml += '<option value="">اختر...</option>';

        options.forEach(function (option) {
            var value = option.Value || option.value;
            var text = option.Text || option.text;
            var selected = value == selectedValue ? 'selected' : '';
            selectHtml += '<option value="' + value + '" ' + selected + '>' + text + '</option>';
        });

        selectHtml += '</select>';
        return selectHtml;
    }

    // Add new price list row
    function addNewPriceListRow() {
        
            const tempId = 'temp-' + Math.floor(Math.random() * 1000);

            const newRow = {
                plId: tempId,
                plDesc: '',
                flag1: '',
                plDate: moment().format("YYYY-MM-DD"),
                year: 2025
            };


        const row = priceListTable.row.add(newRow).draw(false);

        const rowNode = priceListTable.row(row).node();
        $(rowNode).addClass('new-row');
    }

    // Add new price list detail row
    function addNewPriceListDetailRow() {
        var newRow = {
            PLDtlId: 0,
            PLId: 0,
            ClinicId: null,
            SClinicId: null,
            ServId: null,
            ServBefDesc: 0,
            DiscoutComp: 0,
            PlValue: 0,
            CompCovPercentage: 100,
            CompValue: 0,
            PlValue2: 0,
            PlValue3: 0,
            ExtraVal: 0,
            ExtraVal2: 0,
            Covered: 2
        };

        priceListDetailTable.row.add(newRow).draw();
    }

    // Handle row click
    $('#PriceList tbody').on('click', 'tr', function () {
        const data = priceListTable.row(this).data();

        // Highlight selected row
        $('#PriceList tbody tr').removeClass('active-row');
        $(this).addClass('active-row');

        // Do something with the selected row
    //    console.log('Selected Row Data:', data);
    });

    $('#PriceList tbody').on('input change', 'input, select', function () {
        var row = $(this).closest('tr');
        var rowIdx = priceListTable.row(row).index();
        var rowData = priceListTable.row(rowIdx).data();
        var id = rowData ? rowData.plId : undefined;

        if (typeof id === "number" || (typeof id === "string" && !id.includes("temp"))) {
            //modifiedRows.add(masterId);
            row.addClass('modified-row');
            enableSaveBtn();
        }


    });
    $('#PriceList tbody').on('click', '.save-btn', function () {
        var id = $(this).data('id');
        savePriceListRecord(id);
    });

    $('#PriceList tbody').on('click', '.delete-btn', function () {
        
        
            deletePriceListRecord($(this),priceListTable);
        
    });

    // Price List Detail Event Handlers
    $('#PriceListDetail tbody').on('click', '.save-detail-btn', function () {
        var id = $(this).data('id');
        savePriceListDetailRecord(id);
    });

    $('#PriceListDetail tbody').on('click', '.delete-detail-btn', function () {
        var id = $(this).data('id');
        if (confirm('هل أنت متأكد من حذف هذا السجل؟')) {
            deletePriceListDetailRecord(id);
        }
    });

    // Calculation event handlers for price list detail
    $('#PriceListDetail tbody').on('input', '.calculation-field', function () {
        var id = $(this).data('id');
        updateCalculation(id);
    });

    // Update calculation function
    function updateCalculation(id) {
        var servBefDesc = parseFloat($('input[data-field="ServBefDesc"][data-id="' + id + '"]').val()) || 0;
        var discountComp = parseFloat($('input[data-field="DiscoutComp"][data-id="' + id + '"]').val()) || 0;

        var result = servBefDesc - (servBefDesc * discountComp / 100);
        $('input[data-field="PlValue"][data-id="' + id + '"]').val(result.toFixed(2));
    }

    function priceListValidateData() {
        const tableSelector = "#PriceList";
        ValidationManager.init(tableSelector);

        let isValid = true;

        // Helper function to validate rows by class
        function validateRows(rowClass) {
            $(`${tableSelector} ${rowClass}`).each(function () {
                const $requiredFields = $(this).find('[required]');
                if (!ValidationManager.validateElements($requiredFields)) {
                    isValid = false;
                }
            });
        }

        validateRows('.new-row');
        validateRows('.modified-row');

        return isValid;
    }

    // AJAX Functions
    function savePriceListRecord() {
        var insertData = [];
        var updateData = [];

            console.log("Valid", !priceListValidateData())
        if (!priceListValidateData()) {
            return;
        }
        // Collect data from input fields
        $('.new-row').not('.modified-row').each(function () {
            const row = $(this);
            const data = {};
            row.find('input, select').each(function () {
                const field = $(this).data('field');
                const value = $(this).val();
                if (field) {

                    data[field] = value;

                }
            });

            insertData.push(data)
        })


        $('.modified-row').not('.new-row').each(function () {
            const row = $(this);
            const data = {};
            row.find('input, select').each(function () {
                const field = $(this).data('field');
                const value = $(this).val();
                if (field) {

                    data[field] = value;

                }
            });
           data.plId =  priceListTable.row(row).data().plId;
            updateData.push(data)
        });

        if (insertData.length > 0) {
            $.ajax({
                url: '/Medical/PriceListNew/SaveRecordPriceList',
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                data: JSON.stringify(insertData),

                beforeSend: function () {
                    console.log('Inserting record...', insertData);
                    // Optional: disable button or show spinner
                },

                success: function (response) {
                    if (response.success) {
                        console.log('Record inserted successfully:', response);
                        priceListTable.ajax.reload(null, false); // false = keep current page
                    } else {
                        console.warn('Insert failed:', response.message || 'Unknown error');
                    }
                },

                error: function (xhr, status, error) {
                    console.error('AJAX error:', {
                        status: xhr.status,
                        statusText: xhr.statusText,
                        response: xhr.responseText,
                        errorThrown: error
                    });
                },

                complete: function () {
                    console.log('Insert request completed.');
                    // Optional: re-enable UI, hide spinner, etc.
                }
            });
        }
        if (updateData.length > 0) {
            $.ajax({
                url: '/Medical/PriceListNew/UpdateRecordPriceList',
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                data: JSON.stringify(updateData),

                beforeSend: function () {
                    console.log('Updating record...', updateData);
                },

                success: function (response) {
                    if (response.success) {
                        console.log('Record updated successfully:', response);
                        priceListTable.ajax.reload(null, false); // false = keep current page
                    } else {
                        console.warn('Update failed:', response.message || 'Unknown error');
                    }
                },

                error: function (xhr, status, error) {
                    console.error('AJAX error:', {
                        status: xhr.status,
                        statusText: xhr.statusText,
                        response: xhr.responseText,
                        errorThrown: error
                    });
                },

                complete: function () {
                    console.log('Save request completed.');
                    // Optional: re-enable UI, hide spinner, etc.
                }
            });
        }
       
    }

    function deletePriceListRecord($deleteBtn, table) {
        var id = $deleteBtn.data('id');

        if (typeof (id) === "string" && id.includes("temp")) {
            // Delete temporary row
            var row = table.row($deleteBtn.closest('tr'));
            row.remove().draw(false);
        } else {
            // Delete existing row
            if (confirm('هل تريد حذف هذا الصف')) {
                $.ajax({
                    url: '/Medical/PriceListNew/DeletePriceList',
                    type: 'POST',
                    data: { id: id },

                    beforeSend: function () {
                        console.log('Deleting record...');
                        // Optional: disable button or show spinner
                    },

                    success: function (response) {
                        if (response.success) {
                            console.log('Record deleted successfully:', response);
                            priceListTable.ajax.reload(null, false); // false = keep current page
                        } else {
                            console.warn('Delete failed:', response.message || 'Unknown error');
                        }
                    },

                    error: function (xhr, status, error) {
                        console.error('AJAX error:', {
                            status: xhr.status,
                            statusText: xhr.statusText,
                            response: xhr.responseText,
                            errorThrown: error
                        });
                    },

                    complete: function () {
                        console.log('Save request completed.');
                        // Optional: re-enable UI, hide spinner, etc.
                    }
                });
            }
        }
    }

    function savePriceListDetailRecord(id) {
        var rowData = {};

        // Collect data from input fields
        $('input[data-id="' + id + '"], select[data-id="' + id + '"]').each(function () {
            var field = $(this).data('field');
            var value = $(this).val();
            rowData[field] = value;
        });

        rowData.PLDtlId = id;

        $.ajax({
            url: saveDetailUrl,
            type: 'POST',
            data: JSON.stringify(rowData),
            contentType: 'application/json',
            headers: {
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                if (response.success) {
                    toastr.success('تم الحفظ بنجاح');
                    priceListDetailTable.ajax.reload();
                } else {
                    toastr.error('حدث خطأ أثناء الحفظ');
                }
            },
            error: function () {
                toastr.error('حدث خطأ أثناء الحفظ');
            }
        });
    }

    function deletePriceListDetailRecord(id) {
        $.ajax({
            url: deleteDetailUrl,
            type: 'POST',
            data: { id: id },
            headers: {
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                if (response.success) {
                    toastr.success('تم الحذف بنجاح');
                    priceListDetailTable.ajax.reload();
                } else {
                    toastr.error('حدث خطأ أثناء الحذف');
                }
            },
            error: function () {
                toastr.error('حدث خطأ أثناء الحذف');
            }
        });
    }

    // Load dropdown data via AJAX
    function loadDropdownData() {
        // Load Main Clinics
        $.ajax({
            url: window.mainClinicUrl,
            type: 'GET',
            success: function (data) {
                mainClinic = data;
                if (priceListDetailTable) {
                    priceListDetailTable.draw();
                }
            },
            error: function () {
                console.error('Failed to load main clinic data');
            }
        });

        // Load Sub Clinics
        $.ajax({
            url: window.subClinicUrl,
            type: 'GET',
            success: function (data) {
                subClinic = data;
                if (priceListDetailTable) {
                    priceListDetailTable.draw();
                }
            },
            error: function () {
                console.error('Failed to load sub clinic data');
            }
        });

        // Load Services
        $.ajax({
            url: window.servicesUrl,
            type: 'GET',
            success: function (data) {
                services = data;
                if (priceListDetailTable) {
                    priceListDetailTable.draw();
                }
            },
            error: function () {
                console.error('Failed to load services data');
            }
        });
    }

    // Cascade dropdown function for sub clinics based on main clinic selection
    $('#PriceListDetail tbody').on('change', 'select[data-field="ClinicId"]', function () {
        var mainClinicId = $(this).val();
        var rowId = $(this).data('id');

        if (mainClinicId) {
            loadSubClinics(mainClinicId, rowId);
        } else {
            // Clear sub clinic and services dropdowns
            $('select[data-field="SClinicId"][data-id="' + rowId + '"]').html('<option value="">اختر...</option>');
            $('select[data-field="ServId"][data-id="' + rowId + '"]').html('<option value="">اختر...</option>');
        }
    });

    // Cascade dropdown function for services based on sub clinic selection
    $('#PriceListDetail tbody').on('change', 'select[data-field="SClinicId"]', function () {
        var subClinicId = $(this).val();
        var rowId = $(this).data('id');

        if (subClinicId) {
            loadServices(subClinicId, rowId);
        } else {
            // Clear services dropdown
            $('select[data-field="ServId"][data-id="' + rowId + '"]').html('<option value="">اختر...</option>');
        }
    });

    // Load sub clinics based on main clinic
    function loadSubClinics(mainClinicId, rowId) {
        $.ajax({
            url: window.subClinicUrl,
            type: 'GET',
            data: { mainClinicId: mainClinicId },
            success: function (data) {
                var subClinicSelect = $('select[data-field="SClinicId"][data-id="' + rowId + '"]');
                subClinicSelect.html('<option value="">اختر...</option>');

                data.forEach(function (item) {
                    subClinicSelect.append('<option value="' + item.Value + '">' + item.Text + '</option>');
                });
            },
            error: function () {
                console.error('Failed to load sub clinic data');
            }
        });
    }

    // Load services based on sub clinic
    function loadServices(subClinicId, rowId) {
        $.ajax({
            url: window.servicesUrl,
            type: 'GET',
            data: { subClinicId: subClinicId },
            success: function (data) {
                var servicesSelect = $('select[data-field="ServId"][data-id="' + rowId + '"]');
                servicesSelect.html('<option value="">اختر...</option>');

                data.forEach(function (item) {
                    servicesSelect.append('<option value="' + item.Value + '">' + item.Text + '</option>');
                });
            },
            error: function () {
                console.error('Failed to load services data');
            }
        });
    }
});