$(document).ready(async function () {
    // Get data from server (these should be passed from the Razor view)
    var priceLists =  [];
    var priceListDetails = [];
    var mainClinic = [];
    var subClinic = [];
    var services = [];


   
          mainClinic = await  AjaxHandlers.fetchMainClinics()
    

    // Price List Type options
    var priceListTypes = [
        { value: 1, text: 'خاص' },
        { value: 2, text: 'تعاقد' }
    ];

    var coveredOptions = [
        { value: 1, text: "نعم" },
        { value: 2, text: "لا" }
    ];


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
        columnControl: [
            {
                target: 0,
                content: ['order', 'searchDropdown']
            }
            
        ],
        ordering: {
            indicators: false,
            handler: false
        },
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.13.7/i18n/ar.json'
        },
        columns: [
            {
                data: 'plId',
                title: 'كود',
                width: '60px'
            },
            {
                data: 'plDesc',
                title: 'اسم القائمة',
                width: '200px',
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
                width: '100px',
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
                className: 'btn btn-success priceListAddBtn', // Bootstrap success (green)
                action: function (e, dt, node, config) {
                    addNewPriceListRow();
                }
               

            },
                {
                text: 'حفظ',
                className: 'btn btn-warning ms-1 priceListSaveBtn',
                action: function (e, dt, node, config) {
                    savePriceListRecord();
                    },
                    enabled: false
            }
            
            
           
        ]
    });

    // Initialize Price List Detail DataTable
    var priceListDetailTable = $('#PriceListDetail').DataTable({
        data: [],
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.13.7/i18n/ar.json'
        },
        columns: [
            {
                data: 'plId',
                visible: false,
               
            },
            {
                data: 'plDtlId',
                title: 'كود',
                width: '60px'
              
            },
            {
                data: 'clinicId',
                title: 'مستوى 1',
                width: '120px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {

                        if (data) {
                            loadSubClinics(data, row);
                        }
                        return createSelectOptions(mainClinic, data, 'clinicId', row.plDtlId, "clinicId","clinicDesc");
                    }
                    return data;
                }
               
            },
            {
                data: 'sClinicId',
                title: 'مستوى 2',
                width: '120px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        if (data) {
                            loadServices(data, row);
                        }
                        return createSelectOptions(subClinic, data, 'sClinicId', row.plDtlId, "sClinicId","sClinicDesc",true);
                    }
                    return data;
                }
            },
            {
                data: 'servId',
                title: 'مستوى 3',
                width: '120px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return createSelectOptions(services, data, 'servId', row.plDtlId, "servId","servDesc",true);
                    }
                    return data;
                }
            },
            {
                data: 'servBefDesc',
                title: 'الخدمة قبل الخصم',
                width:'70px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control editable-field calculation-field" data-field="servBefDesc" data-id="' + row.plDtlId + '" value="' + (data || 0) + '">';
                    }
                    return data;
                }
            },
            {
                data: 'discoutComp',
                title: 'نسبة خصم الخدمة',
                width: '70px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control editable-field calculation-field" data-field="discoutComp" data-id="' + row.plDtlId + '" value="' + (data || 0) + '">';
                    }
                    return data;
                }
            },
            {
                data: 'plValue',
                title: 'الخدمة بعد الخصم',
                width: '70px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control editable-field" data-field="plValue" data-id="' + row.plDtlId + '" value="' + (data || 0) + '" readonly>';
                    }
                    return data;
                }
            },
            {
                data: 'compCovPercentage',
                title: 'نسبة الشركة',
                width: '70px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control editable-field" data-field="compCovPercentage" data-id="' + row.plDtlId + '" value="' + (data || 100) + '">';
                    }
                    return data;
                }
            },
            {
                data: 'compValue',
                title: 'قيمة تحمل الشركة',
                width: '70px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control editable-field" data-field="CompValue" data-id="' + row.plDtlId + '" value="' + (data || 0) + '">';
                    }
                    return data;
                }
            },
            {
                data: 'plValue2',
                title: 'تحمل العضو',
                width: '70px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control editable-field" data-field="plValue2" data-id="' + row.plDtlId + '" value="' + (data || 0) + '">';
                    }
                    return data;
                }
            },
            {
                data: 'plValue3',
                title: 'تحمل القريب',
                width: '70px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control editable-field" data-field="PlValue3" data-id="' + row.plDtlId + '" value="' + (data || 0) + '">';
                    }
                    return data;
                }
            },
            {
                data: 'extraVal',
                title: 'تحمل مستلزمات علي المريض',
                width: '70px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control editable-field" data-field="extraVal" data-id="' + row.plDtlId + '" value="' + (data || 0) + '">';
                    }
                    return data;
                }
            },
            {
                data: 'extraVal2',
                title: 'تحمل صيانة علي المريض',
                width: '70px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<input type="number" class="form-control editable-field" data-field="extraVal2" data-id="' + row.plDtlId + '" value="' + (data || 0) + '">';
                    }
                    return data;
                }
            },
            {
                data: 'covered',
                title: 'تغطي الخدمة',
                width: '100px',
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return createSelectOptions(coveredOptions, data || 2, 'covered', row.plDtlId);
                    }
                    return data;
                }
            },
            {
                data: null,
                title: 'الإجراءات',
                width: '100px',
                columnControl: [],
                render: function (data, type, row, meta) {
                    return  '<button class="btn btn-sm btn-danger delete-detail-btn" data-id="' + row.plDtlId + '"><i class="bi bi-trash3"></i></button>';
                },
                createdCell: function (td, cellData, rowData) {

                    td.style.minWidth = '100px';
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
        order: [[1, 'asc']],
        buttons: [
            {
                text: '+',
                className: 'btn btn-success priceListDetailAddBtn',
                action: function (e, dt, node, config) {
                    addNewPriceListDetailRow();
                },
                enabled: false

            },
            {
                text: 'حفظ',
                className: 'btn btn-warning ms-1 priceListDetailSaveBtn',
                action: function (e, dt, node, config) {
                    var $activeRow = $('#PriceList tbody .active-row');
                    var rowData = priceListTable.row($activeRow).data();

            

                    savePriceListDetailRecord(rowData);
                },
                enabled: false

            }
        ],
        columnControl: [
            {
                target: 0,
                content: ['order', 'searchDropdown']
            }
            
        ],
        ordering: {
            indicators: false,
            handler: false
        },
        pageLength: 10,
        scrollX: true,
    });


 
    // Helper function to create select options
    function createSelectOptions(options, selectedValue, fieldName, id, valueKey = "value", textKey = "text", disabled = false,reqiured=false) {
        const isDisabled = disabled || options.length === 0;
        const isRequired = reqiured ? `required data-parsley-required-message="هذا الحقل مطلوب"`:"";
        let selectHtml = `<select class="form-control editable-field" data-field="${fieldName}" data-id="${id}" ${isDisabled ? 'disabled' : ''} ${isRequired}>`;

        selectHtml += '<option value="">اختر...</option>';

        options.forEach(option => {
            const value = option[valueKey];
            const text = option[textKey];
            const selected = value == selectedValue ? 'selected' : '';
            selectHtml += `<option value="${value}" ${selected}>${text}</option>`;
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
        priceListTable.button(1).enable(true);
    }

    // Add new price list detail row
    function addNewPriceListDetailRow() {
        const tempId = 'temp-' + Math.floor(Math.random() * 1000);
        var newRow = {
            plDtlId: tempId ,
            plId: 0,
            clinicId: "",
            sClinicId: "",
            servId: "",
            servBefDesc: 0,
            discoutComp: 0,
            plValue: 0,
            compCovPercentage: 0,
            compValue: 0,
            plValue2: 0,
            plValue3: 0,
            extraVal: 0,
            extraVal2: 0,
            covered: 2
        };

        const row =priceListDetailTable.row.add(newRow).draw();
        const rowNode = priceListDetailTable.row(row).node();
        $(rowNode).addClass('new-row');
        priceListDetailTable.button(1).enable(true);

    }

    // Handle row click
    $('#PriceList tbody').on('click', 'tr', function () {
        const data = priceListTable.row(this).data();

        // Highlight selected row
        $('#PriceList tbody tr').removeClass('active-row');
        $(this).addClass('active-row');

        console.log('Selected Row Data:', data.plId);
        loadPriceListDetails(data.plId);

    });


    $('#PriceList tbody').on('input change', 'input, select', function () {
        var row = $(this).closest('tr');
        var rowIdx = priceListTable.row(row).index();
        var rowData = priceListTable.row(rowIdx).data();
        var id = rowData ? rowData.plId : undefined;

        if (typeof id === "number" || (typeof id === "string" && !id.includes("temp"))) {
            //modifiedRows.add(masterId);
            row.addClass('modified-row');
            priceListTable.button(1).enable(true);
        }


    });

    $('#PriceListDetail tbody').on('input change', 'input, select', function () {
        var row = $(this).closest('tr');
        var rowIdx = priceListDetailTable.row(row).index();
        var rowData = priceListDetailTable.row(rowIdx).data();
        var id = rowData ? rowData.plDtlId : undefined;

        if (typeof id === "number" || (typeof id === "string" && !id.includes("temp"))) {
            //modifiedRows.add(masterId);
            row.addClass('modified-row');
            priceListDetailTable.button(1).enable(true);
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
            deletePriceListDetailRecord($(this), priceListTable);
        
    });

    // Calculation event handlers for price list detail
    $('#PriceListDetail tbody').on('input', '.calculation-field', function () {
        var id = $(this).data('id');
        updateCalculation(id);
    });


    function loadPriceListDetails(plId) {
        $.ajax({
            url: '/Medical/PriceListNew/GetPriceListDetail/' + plId,
            type: 'GET',
            success: function (response) {
                priceListDetailTable.clear();
                priceListDetailTable.rows.add(response);
                priceListDetailTable.draw();
                priceListDetailTable.button(0).enable(true);
            },
            error: function () {
                console.error('❌ Failed to load price list details for ID:', plId);
            }
        });
    }

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


    function priceListDetailValidateData() {
        const tableSelector = "#PriceListDetail";
        ValidationManager.init(tableSelector); // Initialize Parsley on the whole table

        let isValid = true;

        // Helper function to validate rows by class
        function validateRows(rowClass) {
            // Target <tr> elements with the given class (like .new-row, .modified-row)
            $(`${tableSelector} tbody tr${rowClass}`).each(function () {
                const $requiredFields = $(this).find('[required]');

                // Attach Parsley to each required field if not already initialized
                $requiredFields.each(function () {
                    if (!$(this).data('parsley')) {
                        $(this).parsley();
                    }
                });

                // Validate the required fields in this row
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

    function savePriceListDetailRecord(rowData) {
        var insertData = [];
        var updateData = [];

        if (!priceListDetailValidateData()) {
            throw Error("Validation Error")
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
            data.plId = rowData.plId;
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
            data.plId = rowData.Id;
            data.plDtlId = priceListDetailTable.row(row).data().plDtlId;
            updateData.push(data)
        });

        if (insertData.length > 0) {
            $.ajax({
                url: '/Medical/PriceListNew/SaveRecordPriceListDetail',
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                data: JSON.stringify(insertData),

                beforeSend: function () {
                    console.log('Inserting record...', insertData);
                    priceListDetailTable.button(1).enable(false);
                },

                success: function (response) {
                    if (response.success) {
                        console.log('Record inserted successfully:', response);
                        const activeRowData = priceListTable.row('.active-row').data();
                        if (activeRowData?.plId) {
                            loadPriceListDetails(activeRowData.plId); // ✅ Reload with latest data
                        }
                        priceListDetailTable.button(1).enable(false);

                    } else {
                        console.warn('Insert failed:', response.message || 'Unknown error');
                        priceListDetailTable.button(1).enable(true);

                    }
                },

                error: function (xhr, status, error) {
                    console.error('AJAX error:', {
                        status: xhr.status,
                        statusText: xhr.statusText,
                        response: xhr.responseText,
                        errorThrown: error
                    });
                    priceListDetailTable.button(1).enable(true);

                },

                complete: function () {
                    console.log('Insert request completed.');
                }
            });
        }
        if (updateData.length > 0) {
            $.ajax({
                url: '/Medical/PriceListNew/UpdateRecordPriceListDetail',
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
                        const activeRowData = priceListTable.row('.active-row').data();
                        if (activeRowData?.plId) {
                            loadPriceListDetails(activeRowData.plId); // ✅ Reload with latest data
                        } 
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

    function deletePriceListDetailRecord($deleteBtn,table) {
        var id = $deleteBtn.data('id');

        if (typeof (id) === "string" && id.includes("temp")) {
            // Delete temporary row
            var row = table.row($deleteBtn.closest('tr'));
            row.remove().draw(false);
        } else {
            // Delete existing row
            if (confirm('هل تريد حذف هذا الصف')) {
                $.ajax({
                    url: '/Medical/PriceListNew/DeletePriceListDetail',
                    type: 'POST',
                    data: { id: id },

                    beforeSend: function () {
                        console.log('Deleting record...');
                        // Optional: disable button or show spinner
                    },

                    success: function (response) {
                        if (response.success) {
                            console.log('Record deleted successfully:', response);
                            const activeRowData = priceListTable.row('.active-row').data();
                            if (activeRowData?.plId) {
                                loadPriceListDetails(activeRowData.plId); // ✅ Reload with latest data
                            }                        } else {
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

   

    // Cascade dropdown function for sub clinics based on main clinic selection
    $('#PriceListDetail tbody').on('change', 'select[data-field="clinicId"]', function () {
        var mainClinicId = $(this).val();
        var rowId = $(this).data('id');

        const tableData = priceListDetailTable.data().toArray();
        const row = tableData.find(r => r.plDtlId == rowId); 

        if (mainClinicId) {
            loadSubClinics(mainClinicId, row);
        } else {
            // Clear sub clinic and services dropdowns
            $('select[data-field="sClinicId"][data-id="' + rowId + '"]').html('<option value="">اختر...</option>');
            $('select[data-field="servId"][data-id="' + rowId + '"]').html('<option value="">اختر...</option>');
        }
    });

    // Cascade dropdown function for services based on sub clinic selection
    $('#PriceListDetail tbody').on('change', 'select[data-field="sClinicId"]', function () {
        var subClinicId = $(this).val();
        var rowId = $(this).data('id');

        const tableData = priceListDetailTable.data().toArray();
        const row = tableData.find(r => r.plDtlId == rowId); 

        if (subClinicId) {
            loadServices(subClinicId, row);
        } else {
            // Clear services dropdown
            $('select[data-field="servId"][data-id="' + rowId + '"]').html('<option value="">اختر...</option>');
        }
    });

    // Load sub clinics based on main clinic
    function loadSubClinics(mainClinicId, row) {
        $.ajax({
            url: '/Medical/SubClinic/GetSubClinic/' + mainClinicId,
            type: 'GET',
            success: function (data) {

               
              
                var subClinicSelect = $('select[data-field="sClinicId"][data-id="' + row.plDtlId + '"]');
                subClinicSelect.removeAttr("disabled");
                subClinicSelect.html('<option value="">اختر...</option>');

                data.forEach(function (item) {
                    var isSelected = item.sClinicId == row.sClinicId ? 'selected' : '';
                    subClinicSelect.append(
                        '<option value="' + item.sClinicId + '" ' + isSelected + '>' + item.sClinicDesc + '</option>'
                    );
                });
            },
            error: function () {
                console.error('Failed to load sub clinic data');
            }
        });
    }

    // Load services based on sub clinic
    function loadServices(subClinicId, row) {
        $.ajax({
            url: '/Medical/ClinicTrans/GetServeClinic/' + subClinicId,
            type: 'GET',
           
            success: function (data) {

                var servicesSelect = $('select[data-field="servId"][data-id="' + row.plDtlId + '"]');
                servicesSelect.removeAttr("disabled");
                servicesSelect.html('<option value="">اختر...</option>');

                data.forEach(function (item) {
                    var isSelected = item.servId == row.servId ? 'selected' : '';
                    servicesSelect.append('<option value="' + item.servId + `" ${isSelected}>` + item.servDesc + '</option>');
                });
            },
            error: function () {
               
                console.error('Failed to load services data');
            }
        });
    }

    function disablePriceListDetailSaveBtn() {
        $('.priceListDetailSaveBtn').attr('disabled', 'disabled');
    }
    function enablePriceListDetailSaveBtn() {
        $('.priceListDetailSaveBtn').removeAttr('disabled');
    }

    function disablePriceListSaveBtn() {
        $('.priceListSaveBtn').attr('disabled', 'disabled');
    }
    function enablePriceListSaveBtn() {
        $('.priceListSaveBtn').removeAttr('disabled');
    }

    function disablePriceListDetailAddBtn() {
        $('.priceListDetailAddBtn').attr('disabled', 'disabled');
    }
    function enablePriceListDetailAddBtn() {
        $('.priceListDetailAddBtn').removeAttr('disabled');
    }
});