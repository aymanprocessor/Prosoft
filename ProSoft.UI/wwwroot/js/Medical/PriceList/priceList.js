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
                console.log('AJAX response:', json);

                return json;
              
            }
        },
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/ar.json'
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
                        return '<input type="text" class="form-control editable-field" data-field="PlDesc" data-id="' + row.plId + '" value="' + (data || '') + '">';
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
                        return '<select class="form-control editable-field" data-field="Flag1" data-id="' + row.plId + '">' + options + '</select>';
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
                        return '<input type="date" class="form-control editable-field" data-field="PLDate" data-id="' + row.plId + '" value="' + dateValue + '">';
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
                        return '<input type="number" class="form-control editable-field" data-field="Year" data-id="' + row.plId + '" value="' + (data || '') + '">';
                    }
                    return data;
                }
            },
            {
                title: 'الإجراءات', 
                orderable: false,   
                data: null,
                render: function (data, type, row ) {
                    return `<button class="btn btn-sm btn-danger delete-btn" data-id="${row.plId}">حذف</button>`;
                }
            }
        ],
        dom: 'Bfrtip',
        buttons: [
            {
                text: 'إضافة جديد',
                className: 'btn btn-success',
                action: function (e, dt, node, config) {
                    addNewPriceListRow();
                }
            }
        ],
        responsive: true,
        pageLength: 25
    });

    // Initialize Price List Detail DataTable
    var priceListDetailTable = $('#PriceListDetail').DataTable({
        data: priceListDetails,
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/ar.json'
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
        dom: 'Bfrtip',
        buttons: [
            {
                text: 'إضافة جديد',
                className: 'btn btn-success',
                action: function (e, dt, node, config) {
                    addNewPriceListDetailRow();
                }
            }
        ],
        responsive: true,
        pageLength: 25,
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
        var newRow = {
            PLId: 0,
            PlDesc: '',
            Flag1: 1,
            PLDate: null,
            Year: new Date().getFullYear()
        };

        priceListTable.row.add(newRow).draw();
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

    // Price List Event Handlers
    $('#PriceList tbody').on('click', '.view-btn', function () {
        var id = $(this).data('id');
        window.location.href = viewUrl + '?id=' + id;
    });

    $('#PriceList tbody').on('click', '.save-btn', function () {
        var id = $(this).data('id');
        savePriceListRecord(id);
    });

    $('#PriceList tbody').on('click', '.delete-btn', function () {
        var id = $(this).data('id');
        if (confirm('هل أنت متأكد من حذف هذا السجل؟')) {
            deletePriceListRecord(id);
        }
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

    // AJAX Functions
    function savePriceListRecord(id) {
        var rowData = {};

        // Collect data from input fields
        $('input[data-id="' + id + '"], select[data-id="' + id + '"]').each(function () {
            var field = $(this).data('field');
            var value = $(this).val();
            rowData[field] = value;
        });

        rowData.PLId = id;

        $.ajax({
            url: saveUrl,
            type: 'POST',
            data: JSON.stringify(rowData),
            contentType: 'application/json',
            headers: {
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                if (response.success) {
                    toastr.success('تم الحفظ بنجاح');
                    priceListTable.ajax.reload();
                } else {
                    toastr.error('حدث خطأ أثناء الحفظ');
                }
            },
            error: function () {
                toastr.error('حدث خطأ أثناء الحفظ');
            }
        });
    }

    function deletePriceListRecord(id) {
        $.ajax({
            url: deleteUrl,
            type: 'POST',
            data: { id: id },
            headers: {
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                if (response.success) {
                    toastr.success('تم الحذف بنجاح');
                    priceListTable.ajax.reload();
                } else {
                    toastr.error('حدث خطأ أثناء الحذف');
                }
            },
            error: function () {
                toastr.error('حدث خطأ أثناء الحذف');
            }
        });
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