// Patient Admissions Management


async function GetAdmisson(e, rowData) {
    console.log("event",e)
    let companyList = [];
    let companyDetailsList = [];
    let departmentList = [];
    let sectionList = [];
    let doctorList = [];

    if (rowData) {
        enableAddNewBtn();
    }

  

    // Pre-load common data
    try {

        [doctorList, departmentList] = await Promise.all([
            AjaxHandlers.fetchDoctors(),
            AjaxHandlers.fetchDepartments()
        ]);

    } catch (error) {
        console.error('Error loading initial data:', error);
    }

    // Update UI for active patient
    updateActivePatientUI(e, rowData);

    // Initialize/Refresh DataTable
    initializeAdmissionsTable(rowData, {
        companyList,
        companyDetailsList,
        departmentList,
        sectionList,
        doctorList
    });
}

function updateActivePatientUI(e, rowData) {
    // Handle active row highlighting
    let allItemRows = e.target.parentElement.parentElement.querySelectorAll("tr");
    allItemRows.forEach(row => {
        row.classList.remove("active-row");
    });

    let clickedTarget = e.target.parentElement;
    clickedTarget.classList.add("active-row");

    // Update header
    let patAdmissonHead = document.querySelector(".admission-table-head");
    //let itemName = clickedTarget.querySelector(".item-name").innerText;
    let patName = rowData.patName;
    let patAdmissonHeader = patAdmissonHead.querySelector(".header.pat-admission");
    //patAdmissonHeader.innerHTML = "زيارات المريض : " + patName;

    // Reset clinic trans header
    let clinicTransHeader = document.querySelector(".header.clinic-trans");
    //clinicTransHeader.innerHTML = "خدمات المريض : " + patName;
}

function initializeAdmissionsTable(rowData, dataLists) {
    // Destroy existing table
    if ($.fn.DataTable.isDataTable('.admisson-table')) {
        $('.admisson-table').DataTable().destroy();
    }

    var table = $('.admisson-table').DataTable({
        ajax: {
            url: '/Medical/PatAdmission/GetAdmissions/' + rowData.patId,
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
        columns: getAdmissionsTableColumns(dataLists),
        paging: true,
        searching: true,
   
        scrollX: true,
        scrollY: "150px",
        scrollCollapse: true,
        rowId: function (data) {
            return 'row-' + data.masterId;
        },
        dom:
            "<'row mb-2 mt-3'" +
            "<'col-sm-4 d-flex justify-content-start'B>" +             // Top-left: buttons
            "<'col-sm-4 d-flex justify-content-center'<'admission-title-header'>>" +             // Top-left: buttons
            "<'col-sm-4 d-flex justify-content-end'f>" +                                       // Top-right: search box
            ">" +
            "<'row'<'col-sm-12'tr>>" +                                  // Table
            "<'row mt-2'" +
            "<'col-sm-4'l>" +                                       // Bottom-left: length menu
            "<'col-sm-4 d-flex justify-content-center'p>" +                           // Bottom-center: pagination
            "<'col-sm-4 text-end'i>" +                              // Bottom-right: info
            ">",
        initComplete: function () {
            $('.admission-title-header').html('<h5>زيارات</h5>');
        },
        buttons: ['copy', 'excel', 'pdf', 'print'],
        language: getDataTableLanguage(),
        drawCallback: function () {
            // Auto-select first row after table is drawn
            var firstRow = this.api().row(0);
            if (firstRow.data()) {
                console.log(firstRow.data())
                var $firstRowNode = $(firstRow.node());
                $firstRowNode.addClass('active-row');

                // Auto-trigger click on first admission to load clinic transactions
                setTimeout(() => {
                    var firstRowData = firstRow.data();
                    if (firstRowData && firstRowData.masterId) {
                        GetClinicTrans({ target: $firstRowNode.find('td:first')[0] }, firstRowData.masterId);
                    }
                }, 100);
            }
        }
    });

    // Setup event handlers
    setupAdmissionsEventHandlers(table, rowData.patientId, dataLists);
}

function getAdmissionsTableColumns(dataLists) {
    return [
        {
            data: 'masterId',
            width:'50px',
            defaultContent: null,
            render: function (data, type, row) {
                return type === 'display' ? `<p style="width:50px;" class="masterId" data-field="masterId">${data || ''}</p>` : (data || null);
            },
            createdCell: function (td) {
                td.style.minWidth = '50px';
            }
        },
        {
            data: 'patAdDate',
            width:'80px',
            defaultContent: null,
            render: function (data, type, row) {
                if (type === 'display') {
                    return data ? `<p style="width:80px;" class="patAdDate" data-field="patAdDate">${moment(data).locale('en').format('DD-MM-YYYY')}</p>` : '<p class="patAdDate" data-field="patAdDate"></p>';
                }
                return data || null;
            },
            createdCell: function (td) {
                td.style.minWidth = '80px';
            }
        },
        {
            data: 'deal',
            defaultContent: null,
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : (data || null);
            },
            createdCell: function (td, cellData, rowData) {
                if (rowData) {
                    const content = DropdownBuilders.buildDealDd(rowData);
                    td.innerHTML = content;
                }
                td.style.minWidth = '100px';
            }
        },
        {
            data: 'compId',
            defaultContent: null,
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : (data || null);
            },
            createdCell: async function (td, cellData, rowData) {
                if (rowData.deal) {
                    dataLists.companyList = await AjaxHandlers.fetchCompanies(rowData.deal);
                    const content = await DropdownBuilders.buildCompaniesDd(rowData, dataLists.companyList);
                    td.innerHTML = content;
                } else {
                    const content = await DropdownBuilders.buildCompaniesDd(rowData, []);
                    td.innerHTML = content;
                }
                td.style.minWidth = '100px';

                const selectElement = document.querySelector(`select.tomselect[data-id="${rowData.masterId}"]`);
                if (selectElement) {
                }
                //$(`select.select2[data-id="${rowData.masterId}"]`).select2({
                //    width: '100%'
                //});
             
            }
        },
        {
            data: 'compIdDtl',
            defaultContent: null,
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : (data || null);
            },
            createdCell: async function (td, cellData, rowData) {
                if (rowData?.compId) {
                    dataLists.companyDetailsList = await AjaxHandlers.fetchCompanyDetails(rowData.compId);
                    const content = await DropdownBuilders.buildCompaniesDtlDd(rowData, dataLists.companyDetailsList);
                    td.innerHTML = content;
                } else {
                    const content = await DropdownBuilders.buildCompaniesDtlDd(rowData, []);
                    td.innerHTML = content;
                }
                td.style.minWidth = '100px';
            
            }

        },
        {
            data: 'brnachInitial',
            defaultContent: null,
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : (data || null);
            },
            createdCell:  function (td, cellData, rowData) {
                if (rowData) {
                    const content = DropdownBuilders.buildDepartmentDd(rowData, dataLists.departmentList);
                    td.innerHTML = content;
                } else {
                    const content = DropdownBuilders.buildDepartmentDd(rowData, []);
                    td.innerHTML = content;
                }
                td.style.minWidth = '100px';
            }
        },
        {
            data: 'sendFr',
            defaultContent: null,
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : (data || null);
            },
            createdCell: async function (td, cellData, rowData) {
                if (rowData?.brnachInitial) {
                    dataLists.sectionList = await AjaxHandlers.fetchSections(rowData.brnachInitial);
                    const content = DropdownBuilders.buildSectionDd(rowData, dataLists.sectionList);
                    td.innerHTML = content;
                } else {
                    const content = DropdownBuilders.buildSectionDd(rowData, []);
                    td.innerHTML = content;
                }
                td.style.minWidth = '100px';
            }
        },
        {
            data: 'drCode',
            defaultContent: null,
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : (data || null);
            },
            createdCell:  function (td, cellData, rowData) {
                if (rowData) {
                    const content = DropdownBuilders.buildDoctorDd(rowData, dataLists.doctorList);
                    td.innerHTML = content;
                }
                td.style.minWidth = '100px';
            }
        },
        {
            data: 'patDateExit',
            width:'60px',
            defaultContent: null,
            render: function (data, type, row) {
                if (type === 'display') {
                    if (!data || data == null) {
                        return '<input type="date" class="patDateExit form-control" value="" data-field="patDateExit"/>';
                    }
                    let date = moment(data, "DD-MM-YYYY").format('YYYY-MM-DD');
                    return `<input type="date" class="patDateExit form-control" value="${date}" data-field="patDateExit" data-id="${row.masterId}" />`;
                }
                return data || null;
            },
            createdCell:  function (td, cellData, rowData) {
            
                td.style.minWidth = '60px';
            }
        },
        {
            data: 'patientValue',
            width: '60px',

            defaultContent: null,
            render: function (data, type, row) {
                //if (type === 'display') {
                //    return `<input type="number" class="form-control no-spinner" value="${data || ''}" data-field="patientValue" data-id="${row.masterId || ''}" min="0" data-parsley-type="number" disabled>`;
                //}
                if (type === 'display') {
                    return `<p style="width:60px;" class="text-center">${data || ''}</p>`;
                }
                return data || null;
            },
            createdCell:  function (td, cellData, rowData) {
              
                td.style.minWidth = '60px';
            }
        },
        {
            data: 'compValue',
            width: '60px',

            defaultContent: null,
            render: function (data, type, row) {
                //if (type === 'display') {
                //    return `<input type="number" class="form-control no-spinner" value="${data || ''}" data-field="compValue" data-id="${row.masterId || ''}" min="0" data-parsley-type="number" disabled>`;
                //}
                if (type === 'display') {
                    return `<p style="width:60px;"  class="text-center">${data || ''}</p>`;
                }
                return data || null;
            },
            createdCell: function (td, cellData, rowData) {

                td.style.minWidth = '60px';
            }
        },
        {
            data: 'prepaid',
            width: '60px',

            defaultContent: null,
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="number" class="form-control no-spinner" value="${data || ''}" data-field="prepaid" data-id="${row.masterId || ''}" min="0" data-parsley-type="number">`;
                }
                return data || null;
            },
            createdCell:  function (td, cellData, rowData) {
               
                td.style.minWidth = '60px';
            }
        },
        {
            data: 'mainInvNo',
            width: '60px',

            defaultContent: null,
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="number" class="form-control no-spinner" value="${data || ''}" data-field="mainInvNo" data-id="${row.masterId || ''}" min="0" data-parsley-type="number">`;
                }
                return data || null;
            },
            createdCell: async function (td, cellData, rowData) {
             
                td.style.minWidth = '60px';
            }
        },
        {
            data: 'sessionNo',
            width: '60px',

            defaultContent: null,
            render: function (data, type, row) {
                if (type === 'display') {
                    return `<input type="number" class="form-control no-spinner" value="${data || ''}" data-field="sessionNo" data-id="${row.masterId || ''}" min="0" max="100" data-parsley-type="number">`;
                }
                return data || null;
            },
            createdCell: async function (td, cellData, rowData) {

                td.style.minWidth = '60px';
                $(`select.select2[data-id="${rowData.masterId}"]`).select2({
                    width: '100%'
                });
               
            }
        },
        {
            data: null,
            columnControl:[],
            render: function (data, type, row) {
                return `<button class="btn btn-sm btn-danger btn-delete" data-id="${row.masterId || ''}"><i class="bi bi-trash"></i></button>`;
            }
           
        }
    ];
}
function hideSpinnerPatAdmission() {
    // Hide the spinner
    $('#btnSavePatAdmissionSpinner').addClass('d-none');
}

function showSpinnerPatAdmission() {
    // Hide the spinner
    $('#btnSavePatAdmissionSpinner').removeClass('d-none');
}


function disableSaveBtn() {
    $('#btnSavePatAdmission').attr('disabled', 'disabled');
}
function enableSaveBtn() {
    $('#btnSavePatAdmission').removeAttr('disabled');
}

function disableAddNewBtn() {
    $('#btnAddPatAdmissionNew').attr('disabled', 'disabled');
}
function enableAddNewBtn() {
    $('#btnAddPatAdmissionNew').removeAttr('disabled');
}



function setupAdmissionsEventHandlers(table, patientId, dataLists) {


   

    setupCascadeDropdownsAdmission(dataLists, patientId);
    // Add new admission button
    $('#btnAddPatAdmissionNew').off('click').on('click', function () {

        
        var newRowData = {
            masterId: "temp_" + Math.floor(Math.random() * 1000) + 1,
            patAdDate: moment().format("YYYY-MM-DD"),
            deal: "",
            compId: "",
            compIdDtl: "",
            brnachInitial: "",
            sendFr: "",
            drCode: "",
            patDateExit: null,
            patientValue: 0,
            compValue:0,
            prepaid: 0,
            mainInvNo: 0,
            sessionNo: 0
        };

        var row = table.row.add(newRowData).draw(false);
        $(table.row(row).node()).addClass('new-row');
        enableSaveBtn();
        // Scroll to top to show new rows
        $('.dt-scroll-body').scrollTop($('.dt-scroll-body')[0].scrollHeight);
    });

    // Track modifications
    $('.admisson-table tbody').on('input change', 'input, select', function () {
        var row = $(this).closest('tr');
        var rowIdx = table.row(row).index();
        var rowData = table.row(rowIdx).data();
        var masterId = rowData ? rowData.masterId : undefined;

        if (typeof masterId === "number" || (typeof masterId === "string" && !masterId.includes("temp"))) {
            //modifiedRows.add(masterId);
            row.addClass('modified-row');
            enableSaveBtn();
        }

     
    });


    // Delegate keydown event for input elements inside the table
    $('.admisson-table').on('keydown', 'input, select', function (e) {
        if (e.key === 'Enter') {
            e.preventDefault();

            const fields = $('.admisson-table').find('input:visible, select:visible');
            const index = fields.index(this);

            if (index !== -1 && index + 1 < fields.length) {
                fields.eq(index + 1).focus();
            } else {
                console.log('End of fields reached');
            }
        }


    });

    // Row click handler for clinic transactions
    $('.admisson-table tbody').off('click', 'tr').on('click', 'tr', function () {
        if ($(this).hasClass('new-row')) return; // Don't load clinic trans for new rows

        var rowData = table.row(this).data();
        if (rowData && rowData.masterId) {
            GetClinicTrans({ target: $(this).find('td:first')[0] }, rowData);
        }
    });

    $('#btnSavePatAdmission').off('click').on('click', function () {
        handleSavePatAdmission(table, patientId);
    });

    // Delete button
    $('.admisson-table').off('click', '.btn-delete').on('click', '.btn-delete', function () {
        handleDeletePatAdmission($(this), table);
    });

 

}


function setupCascadeDropdownsAdmission(dataLists) {
    // Deal Dropdown

    $(document).off('change', '.admisson-table .deal-dropdown').on('change', '.admisson-table .deal-dropdown', async function () {
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

async function handleSavePatAdmission(table, patientId) {
    enableSaveBtn();
    showSpinnerPatAdmission();


    try {
        // Validate data
        var isValid = validatePatAdmissionData();
        if (!isValid) {
            throw new Error('Validation failed');
        }

        // Prepare data
        var insertData = patAdmissionPrepareInsertData(table, patientId);
        var updateData = patAdmissionPrepareUpdateData(table, patientId);
        console.log(insertData);
        // Save data
        if (insertData.length > 0) {

            await AjaxHandlers.savePatAdmissionRows(insertData);
        }

        if (updateData.length > 0) {
            await AjaxHandlers.updatePatAdmissionRows(updateData);
        }

        // Clean up
        $('.modified-row').removeClass('modified-row');
        $('.new-row').removeClass('new-row');
   
        disableSaveBtn();
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

        console.error('Detailed error:', error);
        enableSaveBtn();

    } finally {
        hideSpinnerPatAdmission();
    }
}


async function handleDeletePatAdmission($deleteBtn, table) {
    var masterId = $deleteBtn.data('id');

    if (typeof (masterId) === "string" && masterId.includes("temp")) {
        // Delete temporary row
        var row = table.row($deleteBtn.closest('tr'));
        row.remove().draw(false);
    } else {
        // Delete existing row
        if (confirm('هل تريد حذف هذا الصف')) {
            try {
                await AjaxHandlers.deletePatAdmission(masterId);
                table.ajax.reload();
            } catch (error) {
                console.log('Error deleting item: ' + error.message);
            }
        }
    }
}

function validatePatAdmissionData() {
    const tableSelector = ".admisson-table";
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

function patAdmissionPrepareInsertData(table, patientId) {
    var insertData = [];

    $('.new-row').not('.modified-row').every(function () {
        var row = $(this.node());

        var masterId = row.find('.masterId').text() || null;
        if (typeof masterId === "string" && masterId.includes("temp")) {
            insertData.push({
                patId: patientId,
                patAdDate: moment(row.find('[data-field="patAdDate"]').text(), "DD-MM-YYYY").toISOString() || null,
                deal: row.find('select[data-field="deal"]').val() || null,
                compId: row.find('select[data-field="compId"]').val() || null,
                compIdDtl: row.find('select[data-field="compIdDtl"]').val() || null,
                brnachInitial: row.find('select[data-field="brnachInitial"]').val() || null,
                sendFr: row.find('select[data-field="sendFr"]').val() || null,
                drCode: row.find('select[data-field="drSendId"]').val() || null,
                patDateExit: moment(row.find('.patDateExit').text()).toISOString() || null,
                patientValue: parseFloat(row.find('input[data-field="patientValue"]').val()) || 0,
                compValue: parseFloat(row.find('input[data-field="compValue"]').val()) || 0,
                prepaid: parseFloat(row.find('input[data-field="prepaid"]').val()) || 0,
                mainInvNo: parseInt(row.find('input[data-field="mainInvNo"]').val()) || 0,
                sessionNo: parseInt(row.find('input[data-field="sessionNo"]').val()) || 0
            });
        }
    });

    return insertData;
}



function patAdmissionPrepareUpdateData(table,patientId) {
    var updateData = [];

    $('.modified-row').not('.new-row').each(function () {
        var row = $(this);

        updateData.push({
            patId: patientId,
            masterId: row.find('.masterId').text() || null,
            patAdDate: moment(row.find('[data-field="patAdDate"]').text(), "DD-MM-YYYY").toISOString() || null,
            deal: row.find('select[data-field="deal"]').val() || null,
            compId: row.find('select[data-field="compId"]').val() || null,
            compIdDtl: row.find('select[data-field="compIdDtl"]').val() || null,
            brnachInitial: row.find('select[data-field="brnachInitial"]').val() || null,
            sendFr: row.find('select[data-field="sendFr"]').val() || null,
            drCode: row.find('select[data-field="drSendId"]').val() || null,
            patDateExit: moment(row.find('input[data-field="patDateExit"]').val()).toISOString() || null,
            patientValue: parseFloat(row.find('input[data-field="patientValue"]').val()) || 0,
            compValue: parseFloat(row.find('input[data-field="compValue"]').val()) || 0,
            prepaid: parseFloat(row.find('input[data-field="prepaid"]').val()) || 0,
            mainInvNo: parseInt(row.find('input[data-field="mainInvNo"]').val()) || 0,
            sessionNo: parseInt(row.find('input[data-field="sessionNo"]').val()) || 0
        });
    });

    return updateData;
}


function getDataTableLanguage() {
    return {
       
        paginate: {
            first: "الأول",
            last: "الأخير",
            next: "التالي",
            previous: "السابق"
        },
        search: "البحث:",
        info: "عرض _START_ إلى _END_ من أصل _TOTAL_ عنصر",
        infoEmpty: "عرض 0 إلى 0 من أصل 0 عنصر",
        infoFiltered: "(مرشح من _MAX_ إجمالي العناصر)",
        lengthMenu: "عرض _MENU_ عنصر",
        zeroRecords: "لا توجد سجلات مطابقة",
        emptyTable: "لا توجد بيانات متاحة في الجدول",
        loadingRecords: "جاري التحميل...",
        processing: "جاري المعالجة..."
    };
}