// Patient Admissions Management
async function GetAdmisson(e, id) {
    let companyList = [];
    let companyDetailsList = [];
    let departmentList = [];
    let sectionList = [];
    let doctorList = [];

    // Pre-load common data
    try {
        doctorList = await AjaxHandlers.fetchDoctors();
        departmentList = await AjaxHandlers.fetchDepartments();
    } catch (error) {
        console.error('Error loading initial data:', error);
    }

    // Update UI for active patient
    updateActivePatientUI(e, id);

    // Initialize/Refresh DataTable
    initializeAdmissionsTable(id, {
        companyList,
        companyDetailsList,
        departmentList,
        sectionList,
        doctorList
    });
}

function updateActivePatientUI(e, id) {
    // Handle active row highlighting
    let allItemRows = e.target.parentElement.parentElement.querySelectorAll("tr");
    allItemRows.forEach(row => {
        row.classList.remove("active-row");
    });

    let clickedTarget = e.target.parentElement;
    clickedTarget.classList.add("active-row");

    // Update header
    let patAdmissonHead = document.querySelector(".admission-table-head");
    let itemName = clickedTarget.querySelector(".item-name").innerText;
    let patAdmissonHeader = patAdmissonHead.querySelector(".header.pat-admission");
    patAdmissonHeader.innerHTML = "خدمات لرقم الزيارة  : " + itemName;

    // Reset clinic trans header
    let clinicTransHeader = document.querySelector(".header.clinic-trans");
    clinicTransHeader.innerHTML = "خدمات  ";
}

function initializeAdmissionsTable(patientId, dataLists) {
    // Destroy existing table
    if ($.fn.DataTable.isDataTable('.admisson-table')) {
        $('.admisson-table').DataTable().destroy();
    }

    var table = $('.admisson-table').DataTable({
        ajax: {
            url: '/Medical/PatAdmission/GetAdmissions/' + patientId,
            type: "GET",
            dataSrc: function (json) {
                if (!json) return [];
                if (Array.isArray(json)) return json;
                if (json.data && Array.isArray(json.data)) return json.data;
                return [];
            }
        },
        columns: getAdmissionsTableColumns(dataLists),
        paging: true,
        searching: false,
        ordering: true,
        rowId: function (data) {
            return 'row-' + data.masterId;
        },
        dom: 'Bfrtip',
        buttons: ['copy', 'excel', 'pdf', 'print'],
        language: getDataTableLanguage()
    });

    // Setup event handlers
    setupAdmissionsEventHandlers(table, patientId, dataLists);
}

function getAdmissionsTableColumns(dataLists) {
    return [
        {
            data: 'masterId',
            render: function (data, type, row) {
                return type === 'display' ? `<p class="masterId">${data}</p>` : data;
            },
            createdCell: function (td) {
                td.style.minWidth = '50px';
            }
        },
        {
            data: 'patAdDate',
            render: function (data, type, row) {
                return type === 'display' ? `<p class="patAdDate">${moment(data).format('DD-MM-YYYY')}</p>` : data;
            },
            createdCell: function (td) {
                td.style.minWidth = '120px';
            }
        },
        {
            data: 'deal',
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : data;
            },
            createdCell: function (td, cellData, rowData) {
                if (rowData) {
                    const content = DropdownBuilders.buildDealDd(rowData);
                    td.innerHTML = content;
                }
                td.style.minWidth = '150px';
            }
        },
        {
            data: 'compId',
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : data;
            },
            createdCell: async function (td, cellData, rowData) {
                if (rowData) {
                    if (rowData.deal) {
                        dataLists.companyList = await AjaxHandlers.fetchCompanies(rowData.deal);
                    }
                    const content = await DropdownBuilders.buildCompaniesDd(rowData, dataLists.companyList);
                    td.innerHTML = content;
                }
                td.style.minWidth = '150px';
            }
        },
        {
            data: 'compIdDtl',
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : data;
            },
            createdCell: async function (td, cellData, rowData) {
                if (rowData) {
                    if (rowData.compId) {
                        dataLists.companyDetailsList = await AjaxHandlers.fetchCompanyDetails(rowData.compId);
                    }
                    const content = await DropdownBuilders.buildCompaniesDtlDd(rowData, dataLists.companyDetailsList);
                    td.innerHTML = content;
                }
                td.style.minWidth = '150px';
            }
        },
        {
            data: 'brnachInitial',
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : data;
            },
            createdCell: async function (td, cellData, rowData) {
                if (rowData) {
                    const content = await DropdownBuilders.buildDepartmentDd(rowData, dataLists.departmentList);
                    td.innerHTML = content;
                }
                td.style.minWidth = '150px';
            }
        },
        {
            data: 'sendFr',
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : data;
            },
            createdCell: async function (td, cellData, rowData) {
                if (rowData) {
                    if (rowData.brnachInitial) {
                        dataLists.sectionList = await AjaxHandlers.fetchSections(rowData.brnachInitial);
                    }
                    const content = await DropdownBuilders.buildSectionDd(rowData, dataLists.sectionList);
                    td.innerHTML = content;
                }
                td.style.minWidth = '150px';
            }
        },
        {
            data: 'drCode',
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : data;
            },
            createdCell: async function (td, cellData, rowData) {
                if (rowData) {
                    const content = await DropdownBuilders.buildDoctorDd(rowData, dataLists.doctorList);
                    td.innerHTML = content;
                }
                td.style.minWidth = '150px';
            }
        },
        {
            data: 'patDateExit',
            render: function (data, type, row) {
                if (type === 'display') {
                    if (!data) return '<input type="date" value="" data-field="patDateExit"/>';
                    let date = moment(data, "DD-MM-YYYY").format('YYYY-MM-DD'); // format to ISO date
                    return `<input type="date" value="${date}" data-field="patDateExit" data-id="${row.masterId}" />`;
                }
                return data;
            }
        },
        {
            data: 'patientValue',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="number" class="form-control no-spinner" value="${data}" data-field="patientValue" data-id="${row.masterId}" min="0" data-parsley-type="number">`;
                }
                return data;
            }
           
        },
        {
            data: 'prepaid',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="number" class="form-control no-spinner" value="${data}" data-field="prepaid" data-id="${row.masterId}" min="0" data-parsley-type="number">`;
                }
                return data;
            }
        },
        {
            data: 'mainInvNo',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="number" class="form-control no-spinner" value="${data}" data-field="mainInvNo" data-id="${row.masterId}" min="0" data-parsley-type="number">`;
                }
                return data;
            }
        },
        {
            data: 'sessionNo',
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="number" class="form-control no-spinner" value="${data}" data-field="sessionNo" data-id="${row.masterId}" min="0" max="100" data-parsley-type="number">`;
                }
                return data;
            }
        },
        {
            data: null,
            render: function (data, type, row) {
                return `<button class="btn btn-sm btn-danger btn-delete" data-id="${row.masterId}">Delete</button>`;
            },
            createdCell: function (td) {
                td.style.minWidth = '100px';
            }
        }
    ];
}

function setupAdmissionsEventHandlers(table, patientId,dataLists) {

    setupCascadeDropdownsAdmission(dataLists);
    // Add new admission button
    $('#btnAddPatAdmissionNew').off('click').on('click', function () {
        var newRowData = {
            masterId: "temp_" + Math.floor(Math.random() * 1000) + 1,
            patAdDate: moment().format("DD-MM-YYYY"),
            deal: "",
            compId: "",
            compIdDtl: "",
            brnachInitial: "",
            sendFr: "",
            drCode: "",
            patDateExit: "",
            patientValue: 0,
            prepaid: 0,
            mainInvNo:0,
            sessionNo: 0
        };

        var row = table.row.add(newRowData).draw(false);
        $(table.row(row).node()).addClass('new-row');
        $("#btnSavePatAdmission").prop('disabled', false);
    });

    // Row click handler for clinic transactions
    $('.admisson-table tbody').off('click', 'tr').on('click', 'tr', function () {
        if ($(this).hasClass('new-row')) return; // Don't load clinic trans for new rows

        var rowData = table.row(this).data();
        if (rowData && rowData.masterId) {
            GetClinicTrans({ target: $(this).find('td:first')[0] }, rowData.masterId);
        }
    });

    $('#btnSavePatAdmission').off('click').on('click', function () {
        handleSavePatAdmission(table);
    });

}


function setupCascadeDropdownsAdmission(dataLists) {
    // Deal Dropdown

    $('.admisson-table tbody').on('change', '.deal-dropdown', async function () {
        let selectedDeal = $(this).val();
        let rowId = $(this).data('id');
        let $compIdSelect = $(`.compId-dropdown[data-id="${rowId}"]`);
        let $compIdDtlSelect = $(`.compIdDtl-dropdown[data-id="${rowId}"]`);
        console.log($(this).val());
        if (selectedDeal) {
            dataLists.companyList = await AjaxHandlers.fetchCompanies(selectedDeal);
            console.log(dataLists.companyList);
            if (dataLists.companyList.length > 0) {
                $compIdSelect.prop('disabled', false);
                $compIdSelect.empty().append('<option value="">اختر</option>');
                dataLists.companyList.forEach(comp => {
                    $compIdSelect.append(`<option value="${comp.compId}">${comp.compName}</option>`);
                });
            } else {
                $compIdSelect.prop('disabled', true);
                $compIdDtlSelect.prop('disabled', true);
            }
        } else {
            $compIdSelect.prop('disabled', true);
            $compIdDtlSelect.prop('disabled', true);
        }
    });

    // CompId Dropdown
    $('.admisson-table tbody').on('change', '.compId-dropdown', async function () {
        let selectedCompId = $(this).val();
        let rowId = $(this).data('id');
        let $CompIdDtlSelect = $(`.compIdDtl-dropdown[data-id="${rowId}"]`);

        if (selectedCompId) {
            dataLists.companyDetailsList = await AjaxHandlers.fetchCompanyDetails(selectedCompId);

            if (dataLists.companyDetailsList.length > 0) {
                $CompIdDtlSelect.prop('disabled', false);
                $CompIdDtlSelect.empty().append('<option value="">اختر</option>');
                dataLists.companyDetailsList.forEach(compDtl => {
                    $CompIdDtlSelect.append(`<option value="${compDtl.compIdDtl}">${compDtl.compNameDtl}</option>`);
                });
            } else {
                $CompIdDtlSelect.prop('disabled', true);

            }
        } else {
            $CompIdDtlSelect.prop('disabled', true);
        }
    });

    $('.admisson-table tbody').on('change', '.brnachInitial-dropdown', async function () {
        let selectedBrnachInitial = $(this).val();
        let rowId = $(this).data('id');
        let $SectionSelect = $(`.sendFr-dropdown[data-id="${rowId}"]`);

        if (selectedBrnachInitial) {
            dataLists.sectionList = await AjaxHandlers.fetchSections(selectedBrnachInitial);

            if (dataLists.sectionList.length > 0) {
                $SectionSelect.prop('disabled', false);
                $SectionSelect.empty().append('<option value="">اختر</option>');
                dataLists.sectionList.forEach(section => {
                    $SectionSelect.append(`<option value="${section.regionCode}">${section.regionDesc}</option>`);
                });
            } else {
                $SectionSelect.prop('disabled', true);

            }
        } else {
            $SectionSelect.prop('disabled', true);
        }
    });

}

async function handleSavePatAdmission(table, modifiedRows) {
    var $btn = $('#btnSavePatAdmission');
    var $spinner = $('#btnSavePatAdmissionSpinner');

    $spinner.show();
    $btn.prop('disabled', true);

    try {
        // Validate data
        var isValid = validatePatAdmissionData();
        if (!isValid) {
            throw new Error('Validation failed');
        }

        // Prepare data
        var insertData = patAdmissionPrepareInsertData(table);
        //var updateData = patAdmissionPrepareUpdateData(table);
        console.log(insertData);
        // Save data
        if (insertData.length > 0) {

            await AjaxHandlers.savePatAdmissionRows(insertData);
            console.log("savePatAdmissionRows Called");
        }

        //if (updateData.length > 0) {
        //    await AjaxHandlers.updatePatAdmissionRows(updateData);
        //}

        // Clean up
        $('.modified-row').removeClass('modified-row');
        $('.new-row').removeClass('new-row');
        //modifiedRows.clear();
        table.ajax.reload();

    } catch (error) {

        let message = 'Error saving data: ';

        if (error instanceof Error) {
            message += error.message;

            // Check for inner error (e.g., Axios or custom errors)
            if (error.stack) {
                console.log('Stack trace:', error.stack);
            }

            // If your error has a cause (supported in newer JavaScript environments)
            if (error.cause) {
                console.log('Caused by:', error.cause);
            }

            // If it's an AJAX error and contains response details
            if (error.response) {
                console.log('AJAX response error:', error.response);
                if (error.response.data) {
                    message += '\nDetails: ' + JSON.stringify(error.response.data);
                }
            }
        } else {
            message += JSON.stringify(error);
        }

        alert(message);
        console.error('Detailed error:', error);
    } finally {
        $btn.prop('disabled', false);
        $spinner.hide();
    }
}

function validatePatAdmissionData() {
    var isValid = true;
    var patAdmissionValidation = $('.admisson-table').parsley({
        errorClass: 'is-invalid',
        successClass: 'is-valid',
        errorsWrapper: '<div class="invalid-feedback"></div>',
        errorTemplate: '<span></span>'
    });

    // Validate new rows
    $('.new-row').each(function () {
        $(this).find('input, select').each(function () {
            if (!patAdmissionValidation.validate()) {
                isValid = false;
                return false;
            }
        });
        if (!isValid) return false;
    });

    // Validate modified rows
    if (isValid) {
        $('.modified-row').not('.new-row').each(function () {
            $(this).find('input, select').each(function () {
                if (!patAdmissionValidation.validate()) {
                    isValid = false;
                    return false;
                }
            });
            if (!isValid) return false;
        });
    }

    return isValid;
}

function patAdmissionPrepareInsertData(table) {
    var insertData = [];

    table.rows('.new-row').every(function () {
        var row = $(this.node());

        var masterId = row.find('.masterId').text() || null;
        if (typeof masterId === "string" && masterId.includes("temp")) {
            insertData.push({
                patAdDate: row.find('.patAdDate').text() || null,
                deal: row.find('select[data-field="deal"]').val() || null,
                compId: row.find('select[data-field="compId"]').val() || null,
                compIdDtl: row.find('select[data-field="compIdDtl"]').val() || null,
                brnachInitial: row.find('select[data-field="brnachInitial"]').val() || null,
                sendFr: row.find('select[data-field="sendFr"]').val() || null,
                drCode: row.find('select[data-field="drCode"]').val() || null,
                patDateExit: row.find('input[data-field="patDateExit"]').val() || null,
                patientValue: parseFloat(row.find('input[data-field="patientValue"]').val()) || 0,
                prepaid: parseFloat(row.find('input[data-field="prepaid"]').val()) || 0,
                mainInvNo: parseInt(row.find('input[data-field="mainInvNo"]').val()) || 0,
                sessionNo: parseInt(row.find('input[data-field="sessionNo"]').val()) || 0
            });
        }
    });

    return insertData;
}



function patAdmissionPrepareUpdateData(table) {
    var updateData = [];

    $('.modified-row').not('.new-row').each(function () {
        var row = $(this);

        updateData.push({
            masterId: row.find('input[data-field="masterId"]').val() || null,
            patAdDate: row.find('input[data-field="patAdDate"]').val() || null,
            deal: row.find('select[data-field="deal"]').val() || null,
            compId: row.find('select[data-field="compId"]').val() || null,
            compIdDtl: row.find('select[data-field="compIdDtl"]').val() || null,
            brnachInitial: row.find('select[data-field="brnachInitial"]').val() || null,
            sendFr: row.find('select[data-field="sendFr"]').val() || null,
            drCode: row.find('select[data-field="drCode"]').val() || null,
            patDateExit: row.find('input[data-field="patDateExit"]').val() || null,
            patientValue: parseFloat(row.find('input[data-field="patientValue"]').val()) || 0,
            prepaid: parseFloat(row.find('input[data-field="prepaid"]').val()) || 0,
            mainInvNo: parseInt(row.find('input[data-field="mainInvNo"]').val()) || 0,
            sessionNo: parseInt(row.find('input[data-field="sessionNo"]').val()) || 0
        });
    });

    return updateData;
}


function getDataTableLanguage() {
    return {
        search: "Search:",
        lengthMenu: "Show _MENU_ entries",
        info: "Showing _START_ to _END_ of _TOTAL_ entries",
        infoEmpty: "Showing 0 to 0 of 0 entries",
        infoFiltered: "(filtered from _MAX_ total entries)",
        paginate: {
            first: "First",
            last: "Last",
            next: "Next",
            previous: "Previous"
        }
    };
}