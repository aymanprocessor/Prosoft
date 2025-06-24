let table;
let modifiedRows = new Set();

document.addEventListener('DOMContentLoaded', function () {
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
        columns: getTableColumns(),
        paging: true,
        scrollX: true,
        scrollY: "200px",
        scrollCollapse: true,
        rowId: function (data) {
            return 'row-' + data.patId;
        },
        searching: true,

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
            render: function (data, type, row) {
               
                return `<p value="${data} data-field="patId" data-id="${row.patId}">${data}</p>`;
            },
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
                    return `<input type="text" class="form-control" value="${data || ''}" data-field="patIdCard" data-id="${row.patId}" pattern="\\d{14}"
                          required      
                          data-parsley-length="[14,14]"
                          data-parsley-type="digits"
                          data-parsley-trigger="change"
                          maxlength="14"
                          data-parsley-required-message="الرقم القومي مطلوب"
                          data-parsley-type-message="يجب إدخال أرقام فقط"
                          data-parsley-length-message="يجب أن يكون الرقم القومي 14 رقمًا"
                          >`;
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
        //{
        //    data: 'entryNo',
        //    render: function (data, type, row) {
        //        if (type === 'display') {
        //            return `<input type="number" class="form-control no-spinner" value="${data || ''}" data-field="entryNo" data-id="${row.patId}" min="1" required>`;
        //        }
        //        return data;
        //    },
        //    createdCell: function (td, cellData, rowData) {

        //        td.style.minWidth = '50px';
        //    }
        //},
        {
            data: 'entryDate',
            render: function (data, type, row) {
                if (type === 'display') {
                    const maxDate = moment().format('YYYY-MM-DD');
                    const date = moment(data).format('YYYY-MM-DD');
                    const datenow = moment().format('YYYY-MM-DD');
                    return `<input type="date" class="form-control" value="${date || datenow}" data-field="entryDate" data-id="${row.patId}" max="${maxDate}" required>`;
                }
                return data;
            },
            createdCell: async function (td, cellData, rowData) {

                td.style.minWidth = '180px';
            }
        },
        {
            data: 'entryTime',
            render: function (data, type, row) {
                if (type === 'display') {
                    const date = moment(data).format("HH:mm");
                    const datenow = moment().format('HH:mm');

                    return `<input type="time" class="form-control" value="${date || null}" data-field="entryTime" data-id="${row.patId}" required>`;
                }
                return data;
            },

            createdCell: async function (td, cellData, rowData) {

                td.style.minWidth = '130px';
            }
        },
        {
            data: 'birthDate',
            render: function (data, type, row) {
                if (type === 'display') {
                    const maxDate = moment().format('YYYY-MM-DD');
                    const date = moment(data).format("YYYY-MM-DD");

                    return `<input type="date" class="form-control" value="${date || ''}" data-field="birthDate" data-id="${row.patId}" max="${maxDate}" >`;
                }
                return data;
            },

            createdCell: async function (td, cellData, rowData) {

                td.style.minWidth = '180px';
            }
        },
        {
            data: 'newOld',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="number" class="form-control no-spinner" value="${data || 0}" data-field="newOld" data-id="${row.patId}">`;
                }
                return data;
            },
            createdCell: function (td, cellData, rowData) {

                td.style.minWidth = '100px';
            }
        },

        {
            data: 'youngDay',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="number" class="form-control no-spinner" value="${data || 0}" data-field="youngDay" data-id="${row.patId}">`;
                }
                return data;
            },
            createdCell: function (td, cellData, rowData) {

                td.style.minWidth = '100px';
            }
        },

        {
            data: 'youngMonth',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="number" class="form-control no-spinner" value="${data || 0}" data-field="youngMonth" data-id="${row.patId}">`;
                }
                return data;
            },
            createdCell: function (td, cellData, rowData) {

                td.style.minWidth = '100px';
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
        //{
        //    data: 'patJob',
        //    render: function (data, type, row) {
        //        if (type === 'display') {
        //            return `<input type="text" class="form-control" value="${data || ''}" data-field="patJob" data-id="${row.patId}">`;
        //        }
        //        return data;
        //    },
        //    createdCell: function (td, cellData, rowData) {

        //        td.style.minWidth = '150px';
        //    }
        //},
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
            data: 'patTel',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="text" class="form-control" value="${data || ''}" data-field="patTel" data-id="${row.patId}">`;
                }
                return data;
            },
            createdCell: function (td, cellData, rowData) {

                td.style.minWidth = '150px';
            }
        },
        {
            data: 'patEmail',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="text" class="form-control" value="${data || ''}" data-field="patEmail" data-id="${row.patId}">`;
                }
                return data;
            },
            createdCell: function (td, cellData, rowData) {

                td.style.minWidth = '150px';
            }
        },
        //{
        //    data: 'patHospital',
        //    render: function (data, type, row) {
        //        if (type === 'display') {
        //            return `<input type="text" class="form-control" value="${data || ''}" data-field="patHospital" data-id="${row.patId}">`;
        //        }
        //        return data;
        //    },
        //    createdCell: function (td, cellData, rowData) {

        //        td.style.minWidth = '150px';
        //    }
        //},
        //{
        //    data: 'pDep',
        //    render: function (data, type, row) {
        //        if (type === 'display') {
        //            return `<input type="text" class="form-control" value="${data || ''}" data-field="pDep" data-id="${row.patId}">`;
        //        }
        //        return data;
        //    },
        //    createdCell: function (td, cellData, rowData) {

        //        td.style.minWidth = '150px';
        //    }
        //},
        //{
        //    data: 'patSector',
        //    render: function (data, type, row) {
        //        if (type === 'display') {
        //            return `<input type="text" class="form-control" value="${data || ''}" data-field="patSector" data-id="${row.patId}">`;
        //        }
        //        return data;
        //    },
        //    createdCell: function (td, cellData, rowData) {

        //        td.style.minWidth = '150px';
        //    }
        //},
        {
            data: null,
            columnControl: [],
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
            //entryNo: '',
            entryDate: moment().format('YYYY-MM-DD'),
            entryTime: moment().format('h:mm a'),
            birthDate: '',
            newOld: 0,
            youngDay: 0,
            youngMonth:0,
            maritalStatus: '',
            personKind: '',
            patAddress: '',
            //patJob: '',
            patMobile: '',
            patTel: '',
            patEmail: '',
            //patHospital: '',
            //pDep: '',
            //patSector: ''
        };

        const row = table.row.add(newRowData).draw(false);
        const rowNode = table.row(row).node();
        $(rowNode).addClass('new-row');

   
        // Scroll to top to show new rows
        $('.dt-scroll-body').scrollTop($('.dt-scroll-body')[0].scrollHeight);
  
    });

    // Track modifications
    $('.patient-table tbody').on('input change', 'input, select', function () {
        const row = $(this).closest('tr');
        const rowData = table.row(row).data();
        const id = rowData ? rowData.patId : undefined;

        if (typeof id === "number"  || (typeof id === "string" && !id.includes("temp"))) {
            modifiedRows.add(id);
            row.addClass('modified-row');
        }
        enableBtnSavePateint();
    });

    // Save button
    $('#btnSavePatient').off('click').on('click', function () {
        handleSave();
    });

    // Delete button
    $('.patient-table').off('click', '.btn-delete').on('click', '.btn-delete', function () {
        handleDelete($(this));
    });

    // Delegate keydown event for input elements inside the table
    $('.patient-table').on('keydown', 'input, select', function (e) {
        if (e.key === 'Enter') {
            e.preventDefault();

            const fields = $('.patient-table').find('input:visible, select:visible');
            const index = fields.index(this);

            if (index !== -1 && index + 1 < fields.length) {
                fields.eq(index + 1).focus();
            } else {
                console.log('End of fields reached');
            }
        }
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
            GetAdmisson(syntheticEvent, rowData);
        }
    });
}

async function handleSave() {
    showPatientSpinner();
    disableBtnSavePateint();

    try {
        if (!patientValidateData()) {
            throw new Error('Validation failed');
        }

        const insertData = prepareInsertDataPat();
        const updateData = prepareUpdateDataPat();

        console.log('Insert Data:', insertData);
        console.log('Update Data:', updateData);

        if (insertData.length > 0) {

            await AjaxHandlers.savePatientRows(insertData);
        }

        if (updateData.length > 0) {
            await AjaxHandlers.updatePatientRows(updateData);
        }


        // Clean up
        $('.modified-row').removeClass('modified-row');
        $('.new-row').removeClass('new-row');
        modifiedRows.clear();

        hidePatientSpinner();
        disableBtnSavePateint();

    } catch (error) {
        console.error('Error saving data:', error);
        hidePatientSpinner();
        enableBtnSavePateint();
    }
}

async function handleDelete($deleteBtn) {
    const id = $deleteBtn.data('id');

    if (typeof id === "string" && id.includes("temp")) {

        // Delete temporary row
        const row = table.row($deleteBtn.closest('tr'));
        row.remove().draw(false);

    } else {
        // Delete existing row
        if (confirm('هل تريد حذف هذا الصف؟')) {
            try {
                await AjaxHandlers.deletePatient(id);
                table.ajax.reload();
            } catch (error) {
                console.log('Error deleting item: ' + error.message);
            }
        }
    }
}

function patientValidateData() {
    const tableSelector = ".patient-table";
    const validation = ValidationManager.init(tableSelector);

    let isValid = true;

    // Validate new rows
    $(`${tableSelector} .new-row`).each(function () {
        const $row = $(this);
        const $requiredFields = $row.find('[required]');

        $requiredFields.each(function () {
            if (!$(this).parsley().validate()) {
                isValid = false;
            }
        });
    });

    // Validate modified rows
    $(`${tableSelector} .modified-row`).not('.new-row').each(function () {
        const $row = $(this);
        const $requiredFields = $row.find('[required]');

        $requiredFields.each(function () {
            if (!$(this).parsley().validate()) {
                isValid = false;
            }
        });
    });

    return isValid;
}

function prepareInsertDataPat() {
    const insertData = [];

    $('.new-row').each(function () {
        const row = $(this);
        const data = {};

        row.find('input, select').each(function () {
            const field = $(this).data('field');
            const value = $(this).val();
            if (field) {
                if (field === 'entryTime' && value) {

                    // Get the entry date for the same row
                    const entryDate = row.find('input[data-field="entryDate"]').val() || moment().format('YYYY-MM-DD');

                    // Combine date and time, then convert to ISO string
                    const dateTimeString = `${entryDate}T${value}:00`;

                    data[field] = new Date(dateTimeString).toISOString();
                }
             
                else {
                    data[field] = value;
                }
            }
        });

        insertData.push(data);
    });

    return insertData;
}

function prepareUpdateDataPat() {
    const updateData = [];

    $('.modified-row').not('.new-row').each(function () {
        const row = $(this);
        const data = {};

        row.find('input, select').each(function () {
            const field = $(this).data('field');
            const value = $(this).val();
            const id = $(this).data('id');
            if (field) {
                // Convert entry time to ISO string
                if (field === 'entryTime' && value) {
                    // Get the entry date for the same row
                    const entryDate = row.find('input[data-field="entryDate"]').val() || moment().format('YYYY-MM-DD');
                    // Combine date and time, then convert to ISO string
                    const dateTimeString = `${entryDate}T${value}:00`;
                    data[field] = new Date(dateTimeString).toISOString();
                } else {
                    data[field] = value;
                }
                const patId = table.row(row).data().patId;
                data.patId = patId;
            }
        });

        updateData.push(data);
    });

    return updateData;
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