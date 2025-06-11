let table;
let modifiedRows = new Set();

$(document).ready(function () {
    initializeTable();
    setupEventHandlers();
});


function initializeTable() {
    if ($.fn.DataTable.isDataTable('.patient-table')) {
        $('.patient-table').DataTable().destroy();
    }

    table = $('.patient-table').DataTable({
        ajax: {
            url: '/Medical/Patient/GetPatients',
            type: "GET",
            dataSrc: function (json) {
                if (!json) return [];
                if (Array.isArray(json)) return json;
                if (json.data && Array.isArray(json.data)) return json.data;
                return [];
            }
        },
        columns: getTableColumns(),
        paging: true,
        searching: true,
        ordering: true,
        order:[],
        scrollX: true,
        scrollY: "200px",
        scrollCollapse: true,
        rowId: function (data) {
            console.log("id",data.patId)
            return 'row-' + data.patId;
        },
        dom: 'Bfrtip',
        buttons: ['copy', 'excel', 'pdf', 'print'],
        language: {
            search: "البحث:",
            lengthMenu: "عرض _MENU_ مدخل",
            info: "عرض _START_ إلى _END_ من _TOTAL_ مدخل",
            infoEmpty: "عرض 0 إلى 0 من 0 مدخل",
            infoFiltered: "(مرشح من _MAX_ مدخل إجمالي)",
            paginate: {
                first: "الأول",
                last: "الأخير",
                next: "التالي",
                previous: "السابق"
            }
        }
    });

    enableAddNewPatientBtn();
}

function getTableColumns() {
    return [
        {
            data: 'patId',
           visible:false,
        },
        {
            data: 'idType',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<select class="form-select" data-field="idType" data-id="${row.patId}" required>
                                <option value="">اختر</option>
                                <option value=1 ${data === 1 ? 'selected' : ''}>الرقم القومي</option>
                                <option value=2 ${data === 2 ? 'selected' : ''}>الجواز السفر</option>
                                <option value=3 ${data === 3 ? 'selected' : ''}>رقم الكارنيه</option>
                            </select>`;
                }
                return data;
            },
            createdCell: function (td, cellData, rowData) {
               
                td.style.minWidth = '150px';
            }
        },
        {
            data: 'patIdCard',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="text" class="form-control" value="${data || ''}" data-field="patIdCard" data-id="${row.patId}" required>`;
                }
                return data;
            },
            createdCell: function (td, cellData, rowData) {

                td.style.minWidth = '200px';
            }
        },
        {
            data: 'patName',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="text" class="form-control" value="${data || ''}" data-field="patName" data-id="${row.patId}" required>`;
                }
                return data;
            },
            createdCell: async function (td, cellData, rowData) {
             
                td.style.minWidth = '150px';
            }

        },
        {
            data: 'entryNo',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="number" class="form-control no-spinner" value="${data || ''}" data-field="entryNo" data-id="${row.patId}" min="1" required>`;
                }
                return data;
            },
            createdCell: function (td, cellData, rowData) {

                td.style.minWidth = '50px';
            }
        },
        {
            data: 'entryDate',
            render: function (data, type, row) {
                if (type === 'display') {
                    const maxDate = moment().format('YYYY-MM-DD');
                    return `<input type="date" class="form-control" value="${data || ''}" data-field="entryDate" data-id="${row.patId}" max="${maxDate}" required>`;
                }
                return data;
            }
        },
        {
            data: 'entryTime',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="time" class="form-control" value="${data || ''}" data-field="entryTime" data-id="${row.patId}" required>`;
                }
                return data;
            },
        },
        {
            data: 'birthDate',
            render: function (data, type, row) {
                if (type === 'display') {
                    const maxDate = moment().format('YYYY-MM-DD');
                    return `<input type="date" class="form-control" value="${data || ''}" data-field="birthDate" data-id="${row.patId}" max="${maxDate}" required>`;
                }
                return data;
            }
        },
        {
            data: 'newOld',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="number" class="form-control no-spinner" value="${data || ''}" data-field="newOld" data-id="${row.patId}" min="0" max="150" required>`;
                }
                return data;
            },
            createdCell: function (td, cellData, rowData) {

                td.style.minWidth = '50px';
            }
        },
        {
            data: 'maritalStatus',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<select class="form-select" data-field="maritalStatus" data-id="${row.patId}" required>
                                <option value="">اختر</option>
                                <option value=1 ${data === 1 ? 'selected' : ''}>اعزب</option>
                                <option value=2 ${data === 2 ? 'selected' : ''}>متزوج</option>
                            </select>`;
                }
                return data;
            },
            createdCell: function (td, cellData, rowData) {

                td.style.minWidth = '150px';
            }
        },
        {
            data: 'personKind',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<select class="form-select" data-field="personKind" data-id="${row.patId}" required>
                                <option value="">اختر</option>
                                <option value=1 ${data === 1 ? 'selected' : ''}>ذكر</option>
                                <option value=2 ${data === 2 ? 'selected' : ''}>انثى</option>
                            </select>`;
                }
                return data;
            },
            createdCell: function (td, cellData, rowData) {

                td.style.minWidth = '150px';
            }
        },
        {
            data: 'patAddress',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="text" class="form-control" value="${data || ''}" data-field="patAddress" data-id="${row.patId}">`;
                }
                return data;
            },
            createdCell: function (td, cellData, rowData) {

                td.style.minWidth = '200px';
            }
        },
        {
            data: 'patJob',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="text" class="form-control" value="${data || ''}" data-field="patJob" data-id="${row.patId}">`;
                }
                return data;
            },
            createdCell: function (td, cellData, rowData) {

                td.style.minWidth = '150px';
            }
        },
        {
            data: 'patMobile',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="text" class="form-control" value="${data || ''}" data-field="patMobile" data-id="${row.patId}">`;
                }
                return data;
            },
            createdCell: function (td, cellData, rowData) {

                td.style.minWidth = '150px';
            }
        },
        {
            data: 'patHospital',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="text" class="form-control" value="${data || ''}" data-field="patHospital" data-id="${row.patId}">`;
                }
                return data;
            },
            createdCell: function (td, cellData, rowData) {

                td.style.minWidth = '150px';
            }
        },
        {
            data: 'pDep',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="text" class="form-control" value="${data || ''}" data-field="pDep" data-id="${row.patId}">`;
                }
                return data;
            },
            createdCell: function (td, cellData, rowData) {

                td.style.minWidth = '150px';
            }
        },
        {
            data: 'patSector',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="text" class="form-control" value="${data || ''}" data-field="patSector" data-id="${row.patId}">`;
                }
                return data;
            },
            createdCell: function (td, cellData, rowData) {

                td.style.minWidth = '150px';
            }
        },
        {
            data: null,
            render: function (data, type, row) {
                return `<button class="btn btn-sm btn-danger btn-delete" data-id="${row.patId}">
                            <i class="bi bi-trash"></i>
                        </button>`;
            }
        }
    ];
}

function setupEventHandlers() {

    // Add new row
    $('#btnAddPatientNew').off('click').on('click', function () {
        enableBtnSavePateint();

        const newId = "temp_" + Math.floor(Math.random() * 1000) + 1;
        const newRowData = {
            patId: newId,
            idType: '',
            patIdCard: '',
            patName: '',
            entryNo: '',
            entryDate: moment().format('YYYY-MM-DD'),
            entryTime: moment().format('HH:mm'),
            birthDate: '',
            newOld: '',
            maritalStatus: '',
            personKind: '',
            patAddress: '',
            patJob: '',
            patMobile: '',
            patHospital: '',
            pDep: '',
            patSector: ''
        };

        const row = table.row.add(newRowData).draw(false);
        const rowNode = table.row(row).node();
        $(rowNode).addClass('new-row');

        // Move the row to the top
        const tbody = table.table().body();
        $(tbody).prepend(rowNode);

        table.draw(false); // Redraw without changing pagination
    });

    // Track modifications
    $('.patient-table tbody').on('input change', 'input, select', function () {
        const row = $(this).closest('tr');
        const rowData = table.row(row).data();
        const id = rowData ? rowData.id : undefined;

        if (typeof id === "number" || (typeof id === "string" && !id.includes("temp"))) {
            modifiedRows.add(id);
            row.addClass('modified-row');
        }
        enableSaveBtn();
    });

    // Save button
    $('#btnSave').off('click').on('click', function () {
        handleSave();
    });

    // Delete button
    $('.patient-table').off('click', '.btn-delete').on('click', '.btn-delete', function () {
        handleDelete($(this));
    });

    // Auto-calculate age from birth date
    $('.patient-table tbody').on('change', 'input[data-field="birthDate"]', function () {
        const birthDate = $(this).val();
        const rowId = $(this).data('id');

        if (birthDate) {
            const age = moment().diff(moment(birthDate), 'years');
            $(`input[data-field="newOld"][data-id="${rowId}"]`).val(age);
            $(this).closest('tr').addClass('modified-row');
            enableSaveBtn();
        }
    });

    // Row click handler for clinic transactions
    $('.patient-table tbody').off('click', 'tr').on('click', 'tr', function (e) {
        if ($(this).hasClass('new-row')) return; // Don't load clinic trans for new rows

        var rowData = table.row(this).data();
        if (rowData && rowData.patId) {
            const syntheticEvent = { target: $(this).find('td').first()[0] };
            GetAdmisson(syntheticEvent, rowData.patId);        }
    });
}

function handleSave() {
    showPatientSpinner();
    disableBtnSavePateint();

    try {
        if (!validateData()) {
            throw new Error('Validation failed');
        }

        const insertData = prepareInsertData();
        const updateData = prepareUpdateData();

        console.log('Insert Data:', insertData);
        console.log('Update Data:', updateData);

        // Simulate API calls
        setTimeout(() => {
            alert('تم حفظ البيانات بنجاح');

            // Clean up
            $('.modified-row').removeClass('modified-row');
            $('.new-row').removeClass('new-row');
            modifiedRows.clear();

            hidePatientSpinner();
            disableBtnSavePateint();
        }, 1000);

    } catch (error) {
        console.error('Error saving data:', error);
        alert('خطأ في حفظ البيانات: ' + error.message);
        hidePatientSpinner();
        enableSaveBtn();
    }
}

function handleDelete($deleteBtn) {
    const id = $deleteBtn.data('id');

    if (typeof id === "string" && id.includes("temp")) {
        // Delete temporary row
        const row = table.row($deleteBtn.closest('tr'));
        row.remove().draw(false);
    } else {
        // Delete existing row
        if (confirm('هل تريد حذف هذا الصف؟')) {
            const row = table.row($deleteBtn.closest('tr'));
            row.remove().draw(false);
            console.log('Deleted patient with ID:', id);
        }
    }
}

function validateData() {
    const validation = $('.patient-table').parsley({
        errorClass: 'is-invalid',
        successClass: 'is-valid',
        errorsWrapper: '<div class="invalid-feedback"></div>',
        errorTemplate: '<span></span>'
    });

    let isValid = true;

    // Validate new rows
    $('.new-row').each(function () {
        $(this).find('input[required], select[required]').each(function () {
            if (!$(this).val()) {
                $(this).addClass('is-invalid');
                isValid = false;
            } else {
                $(this).removeClass('is-invalid');
            }
        });
    });

    // Validate modified rows
    $('.modified-row').not('.new-row').each(function () {
        $(this).find('input[required], select[required]').each(function () {
            if (!$(this).val()) {
                $(this).addClass('is-invalid');
                isValid = false;
            } else {
                $(this).removeClass('is-invalid');
            }
        });
    });

    return isValid;
}

function prepareInsertData() {
    const insertData = [];

    $('.new-row').each(function () {
        const row = $(this);
        const data = {};

        row.find('input, select').each(function () {
            const field = $(this).data('field');
            const value = $(this).val();
            if (field) {
                data[field] = value;
            }
        });

        insertData.push(data);
    });

    return insertData;
}

function prepareUpdateData() {
    const updateData = [];

    $('.modified-row').not('.new-row').each(function () {
        const row = $(this);
        const data = {};

        row.find('input, select').each(function () {
            const field = $(this).data('field');
            const value = $(this).val();
            const id = $(this).data('id');
            if (field) {
                data[field] = value;
                data.id = id;
            }
        });

        updateData.push(data);
    });

    return updateData;
}


function disableSaveBtn() {
    $('#btnSavePatAdmission').attr('disabled', 'disabled');
}
function enableSaveBtn() {
    $('#btnSavePatAdmission').removeAttr('disabled');
}


function showPatientSpinner() {
    $('#btnSavePatientSpinner').removeClass('spinner-hidden');
}

function hidePatientSpinner() {
    $('#btnSavePatientSpinner').addClass('spinner-hidden');
}

function enableBtnSavePateint() {
    $('#btnSavePatient').removeAttr('disabled');
}

function disableBtnSavePateint() {
    $('#btnSavePatient').attr('disabled', 'disabled');
}

function enableAddNewPatientBtn() {
    $('#btnAddPatientNew').removeAttr('disabled');
}

function disableAddNewPatientBtn() {
    $('#btnAddPatientNew').attr('disabled', 'disabled');
}