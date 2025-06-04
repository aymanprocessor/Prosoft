// Clinic Transactions Management
async function GetClinicTrans(e, masterId) {
    $("#btnSaveClinicTrans").prop('disabled', true);

    let mainClinicList = [];
    let subClinicList = [];
    let servList = [];
    let doctorList = [];
    let stockList = [];

    // Pre-load common data
    try {
        [doctorList, stockList, mainClinicList] = await Promise.all([
            AjaxHandlers.fetchDoctors(),
            AjaxHandlers.fetchStocks(),
            AjaxHandlers.fetchMainClinics()
        ]);
    } catch (error) {
        console.error('Error loading initial data:', error);
    }

    // Update UI
    updateClinicTransUI(e, masterId);

    // Initialize DataTable
    initializeClinicTransTable(masterId, {
        mainClinicList,
        subClinicList,
        servList,
        doctorList,
        stockList
    });
}

function updateClinicTransUI(e, masterId) {
    // Handle active class
    let clickedTarget = e.target.closest('tr');
    let allItemRows = clickedTarget.parentElement.querySelectorAll("tr");

    allItemRows.forEach(row => {
        row.classList.remove("active-row");
    });
    clickedTarget.classList.add("active-row");

    // Update header
    let clinicTransHead = document.querySelector(".clinicTrans-table-head");
    let itemId = clickedTarget.querySelector(".item-id")?.innerText || masterId;
    let clinicTransHeader = clinicTransHead.querySelector(".header.clinic-trans");
    clinicTransHeader.innerHTML = "Clinic Transaction Invoices For Visit No : " + itemId;
}

function initializeClinicTransTable(masterId, dataLists) {
    var modifiedRows = new Set();

    // Destroy existing table
    if ($.fn.DataTable.isDataTable('.clinicTrans-table')) {
        $('.clinicTrans-table').DataTable().destroy();
    }

    var table = $('.clinicTrans-table').DataTable({
        ajax: {
            url: '/Medical/ClinicTrans/GetClinicTrans/' + masterId,
            data: function (d) {
                return { flag: 1 };
            },
            dataSrc: function (json) {
                if (json.error) {
                    alert('Error loading data: ' + json.error);
                    return [];
                }
                updateGrandTotals(json);
                return json;
            }
        },
        columns: getClinicTransColumns(dataLists),
        paging: true,
        searching: false,
        ordering: true,
        rowId: function (data) {
            return 'row-' + data.checkId;
        },
        dom: 'Bfrtip',
        buttons: ['copy', 'excel', 'pdf', 'print'],
        language: getDataTableLanguage()
    });

    // Setup event handlers
    setupClinicTransEventHandlers(table, masterId, modifiedRows, dataLists);
}

function getClinicTransColumns(dataLists) {
    return [
        {
            data: 'exDate',
            render: function (data, type, row) {
                return type === 'display' ? `<p class="exDate">${moment(data).format('DD-MM-YYYY')}</p>` : data;
            },
            createdCell: function (td) {
                td.style.minWidth = '120px';
            }
        },
        {
            data: 'itmServFlag',
            render: function (data, type, row) {
                return type === 'display' ? ((row.itmServFlag == 3) ? "خدمة" : "صنف") : data;
            },
            createdCell: function (td) {
                td.style.minWidth = '120px';
            }
        },
        {
            data: 'clinicId',
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : data;
            },
            createdCell: async function (td, cellData, rowData) {
                if (rowData) {
                    const content =  DropdownBuilders.buildMainClinicDd(rowData, dataLists.mainClinicList);
                    td.innerHTML = content;
                }
                td.style.minWidth = '150px';
            }
        },
        {
            data: 'sClinicId',
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : data;
            },
            createdCell: async function (td, cellData, rowData) {
                if (rowData) {
                    if (rowData.clinicId) {
                        dataLists.subClinicList = await AjaxHandlers.fetchSubClinics(rowData.clinicId);
                    }
                    const content =  DropdownBuilders.buildSubClinicDd(rowData, dataLists.subClinicList);
                    td.innerHTML = content;
                }
                td.style.minWidth = '150px';
            }
        },
        {
            data: 'servId',
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : data;
            },
            createdCell: async function (td, cellData, rowData) {
                if (rowData) {
                    if (rowData.sClinicId) {
                        dataLists.servList = await AjaxHandlers.fetchServices(rowData.sClinicId);
                    }
                    const content =  DropdownBuilders.buildServDd(rowData, dataLists.servList);
                    td.innerHTML = content;
                }
                td.style.minWidth = '150px';
            }
        },
        {
            data: 'drSendId',
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : data;
            },
            createdCell: async function (td, cellData, rowData) {
                if (rowData) {
                    const content =  DropdownBuilders.buildDoctorDd(rowData, dataLists.doctorList);
                    td.innerHTML = content;
                }
                td.style.minWidth = '150px';
            }
        },
        {
            data: 'qty',
            render: function (data, type, row) {
                return type === 'display' ?
                    `<input type="number" class="form-control no-spinner" value="${data}" data-field="qty" data-id="${row.checkId}" min="1" required data-parsley-type="integer">` :
                    data;
            },
            createdCell: function (td) {
                td.style.minWidth = '100px';
            }
        },
        {
            data: 'unitPrice',
            render: function (data, type, row) {
                return type === 'display' ?
                    `<input type="number" class="form-control no-spinner" value="${data}" data-field="unitPrice" data-id="${row.checkId}" min="0" data-parsley-type="number">` :
                    data;
            },
            createdCell: function (td) {
                td.style.minWidth = '100px';
            }
        },
        {
            data: 'valueService',
            render: function (data, type, row) {
                return type === 'display' ?
                    `<p class="valueService" data-field="valueService">${(parseFloat(data) || 0).toFixed(2)}</p>` :
                    data;
            }
        },
        {
            data: 'stockId',
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : data;
            },
            createdCell: async function (td, cellData, rowData) {
                if (rowData) {
                    const content =  DropdownBuilders.buildStockDd(rowData, dataLists.stockList);
                    td.innerHTML = content;
                }
                td.style.minWidth = '150px';
            }
        },
        {
            data: 'patientValue',
            render: function (data, type, row) {
                return type === 'display' ?
                    `<input type="number" class="form-control no-spinner" value="${data}" data-field="patientValue" data-id="${row.checkId}" min="0" data-parsley-type="number">` :
                    data;
            }
        },
        {
            data: 'compValue',
            render: function (data, type, row) {
                return type === 'display' ?
                    `<input type="number" class="form-control no-spinner" value="${data}" data-field="compValue" data-id="${row.checkId}" min="0" data-parsley-type="number">` :
                    data;
            }
        },
        {
            data: 'discountVal',
            render: function (data, type, row) {
                return type === 'display' ?
                    `<input type="number" class="form-control no-spinner" value="${data}" data-field="discountVal" data-id="${row.checkId}" min="0" max="100" data-parsley-type="number">` :
                    data;
            }
        },
        {
            data: 'extraVal',
            render: function (data, type, row) {
                return type === 'display' ?
                    `<input type="number" class="form-control no-spinner" value="${data}" data-field="extraVal" data-id="${row.checkId}" min="0" data-parsley-type="number">` :
                    data;
            }
        },
        {
            data: 'extraVal2',
            render: function (data, type, row) {
                return type === 'display' ?
                    `<input type="number" class="form-control no-spinner" value="${data}" data-field="extraVal2" data-id="${row.checkId}" min="0" max="100" data-parsley-type="number">` :
                    data;
            }
        },
        {
            data: 'approvalPeriod',
            render: function (data, type, row) {
                return type === 'display' ? DropdownBuilders.buildApprovalPeriodDd(row) : data;
            },
            createdCell: function (td) {
                td.style.minWidth = '150px';
            }
        },
        {
            data: 'checkIdCancel',
            render: function (data, type, row) {
                return type === 'display' ? DropdownBuilders.buildCheckIdCancelDd(row) : data;
            },
            createdCell: function (td) {
                td.style.minWidth = '150px';
            }
        },
        {
            data: null,
            render: function (data, type, row) {
                return `<button class="btn btn-sm btn-danger btn-delete" data-id="${row.checkId}">Delete</button>`;
            },
            createdCell: function (td) {
                td.style.minWidth = '100px';
            }
        }
    ];
}

function setupClinicTransEventHandlers(table, masterId, modifiedRows, dataLists) {
    // Cascade dropdown changes
    setupCascadeDropdowns(dataLists);

    // Track modifications
    $('.clinicTrans-table tbody').on('input change', 'input, select', function () {
        var row = $(this).closest('tr');
        var checkId = table.row(row).data().checkId;

        if (typeof checkId === "number" || (typeof checkId === "string" && !checkId.includes("temp"))) {
            modifiedRows.add(checkId);
            row.addClass('modified-row');
            $("#btnSaveClinicTrans").prop('disabled', false);
        }

        updateRowTotal(row);
        updateGrandTotalsFromTable();
    });

    // Add new row
    $('#btnAddClinicTransNew').off('click').on('click', function () {
        var newRowData = createNewClinicTransRow(masterId);
        var row = table.row.add(newRowData).draw(false);
        $(table.row(row).node()).addClass('new-row');
        $("#btnSaveClinicTrans").prop('disabled', false);
    });

    // Auto-calculate patient value
    $('.clinicTrans-table tbody').on('change', 'input[data-field="unitPrice"]', function () {
        var row = $(this).closest('tr');
        var unitPrice = parseFloat($(this).val()) || 0;
        row.find('input[data-field="patientValue"]').val(unitPrice);
        updateRowTotal(row);
        updateGrandTotalsFromTable();
    });

    // Save button
    $('#btnSaveClinicTrans').off('click').on('click', function () {
        handleSaveClinicTrans(table, masterId, modifiedRows);
    });

    // Delete button
    $('.clinicTrans-table').off('click', '.btn-delete').on('click', '.btn-delete', function () {
        handleDeleteClinicTrans($(this), table);
    });
}

function setupCascadeDropdowns(dataLists) {
    // Main clinic change
    $('.clinicTrans-table tbody').on('change', '.clinic-dropdown', async function () {
        let selectedClinicId = $(this).val();
        let rowId = $(this).data('id');
        let $SubClinicSelect = $(`.sub-clinic-dropdown[data-id="${rowId}"]`);
        let $ServSelect = $(`.serv-dropdown[data-id="${rowId}"]`);

        if (selectedClinicId) {
            dataLists.subClinicList = await AjaxHandlers.fetchSubClinics(selectedClinicId);

            if (dataLists.subClinicList.length > 0) {
                $SubClinicSelect.prop('disabled', false);
                $SubClinicSelect.empty().append('<option value="">Choose</option>');
                dataLists.subClinicList.forEach(subClinic => {
                    $SubClinicSelect.append(`<option value="${subClinic.sClinicId}">${subClinic.sClinicDesc}</option>`);
                });
            }
        } else {
            $SubClinicSelect.prop('disabled', true);
            $ServSelect.prop('disabled', true);
        }
    });

    // Sub clinic change
    $('.clinicTrans-table tbody').on('change', '.sub-clinic-dropdown', async function () {
        let selectedSubClinicId = $(this).val();
        let rowId = $(this).data('id');
        let $ServSelect = $(`.serv-dropdown[data-id="${rowId}"]`);

        if (selectedSubClinicId) {
            dataLists.servList = await AjaxHandlers.fetchServices(selectedSubClinicId);

            if (dataLists.servList.length > 0) {
                $ServSelect.prop('disabled', false);
                $ServSelect.empty().append('<option value="">Choose</option>');
                dataLists.servList.forEach(serv => {
                    $ServSelect.append(`<option value="${serv.servId}">${serv.servDesc}</option>`);
                });
            }
        } else {
            $ServSelect.prop('disabled', true);
        }
    });
}

function createNewClinicTransRow(masterId) {
    return {
        checkId: "temp" + (Math.random() * (1000 - 1) + 1),
        exDate: new Date(),
        itmServFlag: 3,
        clinicId: "",
        sClinicId: "",
        servId: "",
        qty: 1,
        unitPrice: 0,
        patientValue: 0,
        extraVal: 0,
        extraVal2: 0,
        compValue: 0,
        discountVal: 0,
        approvalPeriod: -1,
        checkIdCancel: -1,
        stockId: "",
        valueService: 0,
        drSendId: ""
    };
}

function updateRowTotal(row) {
    var qty = parseFloat(row.find('input[data-field="qty"]').val()) || 0;
    var unitPrice = parseFloat(row.find('input[data-field="unitPrice"]').val()) || 0;
    row.find('.valueService').text((qty * unitPrice).toFixed(2));
}

function updateGrandTotals(json) {
    let totalServicesForVisit = 0;
    let netPatient = 0;

    json.forEach(function (item) {
        totalServicesForVisit += (item.compValue + item.patientValue);
        netPatient += (item.extraVal + item.extraVal2 + item.patientValue);
    });

    document.getElementById("totalServicesForVisit").classList.add("bg-white");
    document.getElementById("totalServicesForVisit").innerHTML = totalServicesForVisit.toFixed(2);
    document.getElementById("netPatient").classList.add("bg-white");
    document.getElementById("netPatient").innerHTML = netPatient.toFixed(2);
}

function updateGrandTotalsFromTable() {
    // This would be called after manual changes to update totals
    // Implementation similar to updateGrandTotals but reading from current table state
}

async function handleSaveClinicTrans(table, masterId, modifiedRows) {
    var $btn = $('#btnSaveClinicTrans');
    var $spinner = $('#btnSaveClinicTransSpinner');

    $spinner.show();
    $btn.prop('disabled', true);

    try {
        // Validate data
        var isValid = validateClinicTransData();
        if (!isValid) {
            throw new Error('Validation failed');
        }

        // Prepare data
        var insertData = prepareInsertData(table, masterId);
        var updateData = prepareUpdateData(table, masterId, modifiedRows);

        // Save data
        if (insertData.length > 0) {
            await AjaxHandlers.saveClinicTransRows(insertData);
        }

        if (updateData.length > 0) {
            await AjaxHandlers.updateClinicTransRows(updateData);
        }

        // Clean up
        $('.modified-row').removeClass('modified-row');
        $('.new-row').removeClass('new-row');
        modifiedRows.clear();
        table.ajax.reload();

    } catch (error) {
        alert('Error saving data: ' + error.message);
    } finally {
        $btn.prop('disabled', false);
        $spinner.hide();
    }
}

function validateClinicTransData() {
    var isValid = true;
    var clinicTransValidation = $('.clinicTrans-table').parsley({
        errorClass: 'is-invalid',
        successClass: 'is-valid',
        errorsWrapper: '<div class="invalid-feedback"></div>',
        errorTemplate: '<span></span>'
    });

    // Validate new rows
    $('.new-row').each(function () {
        $(this).find('input, select').each(function () {
            if (!clinicTransValidation.validate()) {
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
                if (!clinicTransValidation.validate()) {
                    isValid = false;
                    return false;
                }
            });
            if (!isValid) return false;
        });
    }

    return isValid;
}

function prepareInsertData(table, masterId) {
    var insertData = [];
    table.rows('.new-row').every(function () {
        var row = $(this.node());
        var data = this.data();
        var checkId = data.checkId;

        if (typeof (checkId) === "string" && checkId.includes("temp")) {
            insertData.push({
                exDate: new Date(),
                masterId: masterId,
                itmServFlag: parseInt(data.itmServFlag) || 0,
                clinicId: row.find('select[data-field="clinicId"]').val(),
                sClinicId: row.find('select[data-field="sClinicId"]').val(),
                servId: row.find('select[data-field="servId"]').val(),
                drSendId: row.find('select[data-field="drSendId"]').val(),
                qty: parseInt(row.find('input[data-field="qty"]').val()) || 0,
                unitPrice: parseFloat(row.find('input[data-field="unitPrice"]').val()) || 0,
                valueService: parseFloat(row.find('.valueService').text()) || 0,
                patientValue: parseFloat(row.find('input[data-field="patientValue"]').val()) || 0,
                compValue: parseFloat(row.find('input[data-field="compValue"]').val()) || 0,
                discountVal: parseFloat(row.find('input[data-field="discountVal"]').val()) || 0,
                extraVal: parseFloat(row.find('input[data-field="extraVal"]').val()) || 0,
                extraVal2: parseFloat(row.find('input[data-field="extraVal2"]').val()) || 0,
                approvalPeriod: parseInt(row.find('select[data-field="approvalPeriod"]').val()) || 0,
                checkIdCancel: parseInt(row.find('select[data-field="checkIdCancel"]').val()) || 0,
                stockId: parseInt(row.find('select[data-field="stockId"]').val()) || 0,
            });
        }
    });
    return insertData;
}

function prepareUpdateData(table, masterId, modifiedRows) {
    var updateData = [];
    $('.modified-row').not('.new-row').each(function () {
        var row = $(this);
        var checkId = table.row(row).data().checkId;

        if (checkId) {
            updateData.push({
                checkId: checkId,
                exDate: moment(row.find('.exDate').text().trim(), 'DD/MM/YYYY').format('YYYY-MM-DD'),
                masterId: masterId,
                itmServFlag: parseInt(table.row(row).data().itmServFlag) || 0,
                clinicId: parseInt(row.find('select[data-field="clinicId"]').val()) || null,
                sClinicId: parseInt(row.find('select[data-field="sClinicId"]').val()) || null,
                servId: parseInt(row.find('select[data-field="servId"]').val()) || null,
                drSendId: parseInt(row.find('select[data-field="drSendId"]').val()) || null,
                qty: parseInt(row.find('input[data-field="qty"]').val()) || 0,
                unitPrice: parseFloat(row.find('input[data-field="unitPrice"]').val()) || 0,
                valueService: parseFloat(row.find('.valueService').text()) || 0,
                patientValue: parseFloat(row.find('input[data-field="patientValue"]').val()) || 0,
                compValue: parseFloat(row.find('input[data-field="compValue"]').val()) || 0,
                discountVal: parseFloat(row.find('input[data-field="discountVal"]').val()) || 0,
                extraVal: parseFloat(row.find('input[data-field="extraVal"]').val()) || 0,
                extraVal2: parseFloat(row.find('input[data-field="extraVal2"]').val()) || 0,
                approvalPeriod: parseInt(row.find('select[data-field="approvalPeriod"]').val()) || 0,
                checkIdCancel: parseInt(row.find('select[data-field="checkIdCancel"]').val()) || 0,
                stockId: parseInt(row.find('select[data-field="stockId"]').val()) || 0,
            });
        }
    });
    return updateData;
}

async function handleDeleteClinicTrans($deleteBtn, table) {
    var checkId = $deleteBtn.data('id');

    if (typeof (checkId) === "string" && checkId.includes("temp")) {
        // Delete temporary row
        var row = table.row($deleteBtn.closest('tr'));
        row.remove().draw(false);
    } else {
        // Delete existing row
        if (confirm('Are you sure you want to delete this row?')) {
            try {
                await AjaxHandlers.deleteClinicTransaction(checkId);
                table.ajax.reload();
            } catch (error) {
                console.log('Error deleting item: ' + error.message);
            }
        }
    }
}