// Clinic Transactions Management
async function GetClinicTrans(e, rowData) {
    if (rowData) {
        enableAddNewClinicTransBtn();
    }
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
    updateClinicTransUI(e, rowData);

    // Initialize DataTable
    initializeClinicTransTable(rowData.masterId, {
        mainClinicList,
        subClinicList,
        servList,
        doctorList,
        stockList
    });
}

function updateClinicTransUI(e, rowData) {
    // Handle active class
    let clickedTarget = e.target.closest('tr');
    let allItemRows = clickedTarget.parentElement.querySelectorAll("tr");

    allItemRows.forEach(row => {
        row.classList.remove("active-row");
    });
    clickedTarget.classList.add("active-row");

    // Update header
    let clinicTransHead = document.querySelector(".clinicTrans-table-head");
    let patientName = rowData.patName;
    let clinicTransHeader = clinicTransHead.querySelector(".header.clinic-trans");
    //clinicTransHeader.innerHTML = "خدمات للمريض : " + patientName;
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
                console.log("data:", json);
                if (json.error) {
                    alert('Error loading data: ' + json.error);
                    return [];
                }
                updateGrandTotals(json);
                return json;
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
        columns: getClinicTransColumns(dataLists),
        pageLength: 10,
        paging: true,
        searching: true,
        order: [],
        scrollX: true,
        scrollY: "150px", 
        scrollCollapse: true,

        rowId: function (data) {
            return 'row-' + data.checkId;
        },
        dom:
            "<'row mb-2 mt-3'" +
            "<'col-sm-4 d-flex justify-content-start'B>" +             // Top-left: buttons
            "<'col-sm-4 d-flex justify-content-center'<'clinic-title-header'>>" +             // Top-left: buttons
            "<'col-sm-4 d-flex justify-content-end'f>" +                                       // Top-right: search box
            ">" +
            "<'row'<'col-sm-12'tr>>" +                                  // Table
            "<'row mt-2'" +
            "<'col-sm-4'l>" +                                       // Bottom-left: length menu
            "<'col-sm-4 d-flex justify-content-center'p>" +                           // Bottom-center: pagination
            "<'col-sm-4 text-end'i>" +                              // Bottom-right: info
            ">",
        initComplete: function () {
            $('.clinic-title-header').html('<h5>خدمات</h5>');
        },
        buttons: ['copy', 'excel', 'pdf', 'print'],
        language: getDataTableLanguage(),
        drawCallback: function () {
            // Auto-select first row after table is drawn
            var firstRow = this.api().row(0);
            if (firstRow.data()) {
                var $firstRowNode = $(firstRow.node());
                $firstRowNode.addClass('active-row');
            }
        }
    });

    // Setup event handlers
    setupClinicTransEventHandlers(table, masterId, modifiedRows, dataLists);
}

function getClinicTransColumns(dataLists) {
    return [
        {
            data: 'checkId',
            visible:false
        },
      
        {
            data: 'exDate',
            width: "80px",
            render: function (data, type, row) {
                return type === 'display' ? `<p style="width:80px;" class="exDate">${moment(data).locale('en').format('DD-MM-YYYY')}</p>` : data;
            },
            createdCell: function (td) {
                td.style.minWidth = '80px';
            }
        },
        {
            data: 'itmServFlag',
            width: "50px",
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : data;
            },
            createdCell:  async function (td, cellData, rowData) {
                if (rowData) {
                    td.innerHTML = DropdownBuilders.buildItmServFlagDd(rowData);

                    const $select = $(td).find('select[data-field="itmServFlag"]');
                    if ($select.length>0) {
                        const row = $select.closest('tr');
                        const itmServFlag = $select.val();
                        handleItmServFlagChange(row, itmServFlag);
                    }
                }
                td.style.minWidth = '100px';            }
        },
        {
            data: 'clinicId',
            width: "100px",
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : data;
            },
            createdCell: async function (td, cellData, rowData) {
                if (rowData) {
                    const content =  DropdownBuilders.buildMainClinicDd(rowData, dataLists.mainClinicList);
                    td.innerHTML = content;
                }
                td.style.minWidth = '100px';
            }
        },
        {
            data: 'sClinicId',
            width: "100px",
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
                td.style.minWidth = '100px';
            }
        },
        {
            data: 'servId',
            width: "100px",

            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : data;
            },
            createdCell: async function (td, cellData, rowData) {
                if (rowData) {
                    if (rowData.sClinicId) {
                        dataLists.servList = await AjaxHandlers.fetchServices(rowData.sClinicId);
                    }
                    td.innerHTML = DropdownBuilders.buildServDd(rowData, dataLists.servList);
                }
                td.style.minWidth = '100px';
            }
        },
        {
            data: 'subId',
            width: "100px",

            render: function (data, type, row) {
                // return type === 'display' ? '<div class="loading">Loading...</div>' : data;
                return DropdownBuilders.buildSubIdFlagDd(row);
            },
            createdCell: async function (td, cellData, rowData) {
                // if (rowData) {
                //
                //     td.innerHTML = DropdownBuilders.buildSubIdFlagDd(rowData);
                // }
                td.style.minWidth = '100px';
            }
        },
        {
            data: 'discountVal',
            width: "50px",
            render: function (data, type, row) {
                return type === 'display' ?
                    `<input type="number" class="form-control no-spinner" value="${data}" data-field="discountVal" data-id="${row.checkId}" min="0" max="100" data-parsley-type="number">` :
                    data;
            },
            createdCell: function (td) {
                td.style.minWidth = '50px';
            }
        },
        {
            data: 'qty',
            width:"50px",
            render: function (data, type, row) {
                return type === 'display' ?
                    `<input type="number" class="form-control no-spinner" value="${data}" data-field="qty" data-id="${row.checkId}" min="1" required data-parsley-type="integer">` :
                    data;
            },
            createdCell: function (td) {
                td.style.minWidth = '50px';
            }
        },
        {
            data: 'unitPrice',
            width: "50px",

            render: function (data, type, row) {
                return type === 'display' ?
                    `<input type="number" class="form-control no-spinner" value="${data}" data-field="unitPrice" data-id="${row.checkId}" min="0" data-parsley-type="number">` :
                    data;
            },
            createdCell: function (td) {
                td.style.minWidth = '50px';
            }
        },
        {
            data: 'valueService',
            width:'50px',
            render: function (data, type, row) {
                return type === 'display' ?
                    `<p style="width:50px;" class="valueService" data-field="valueService">${(parseFloat(data) || 0).toFixed(2)}</p>` :
                    data;
            },
            createdCell: function (td) {
                td.style.minWidth = '50px';
            }
        },
        {
            data: 'stockId',
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : data;
            },
            createdCell: async function (td, cellData, rowData) {
                if (rowData) {
                    td.innerHTML = DropdownBuilders.buildStockDd(rowData, dataLists.stockList);
                }
                td.style.minWidth = '100px';
            }
        },
        {
            data: 'patientValue',
            width: '50px',

            render: function (data, type, row) {
                return type === 'display' ?
                    `<input type="number" class="form-control no-spinner" value="${data}" data-field="patientValue" data-id="${row.checkId}" min="0" data-parsley-type="number">` :
                    data;
            },
            createdCell: function (td) {
                td.style.minWidth = '50px';
            }
        },
        {
            data: 'compValue',
            width: '50px',

            render: function (data, type, row) {
                return type === 'display' ?
                    `<input type="number" class="form-control no-spinner" value="${data}" data-field="compValue" data-id="${row.checkId}" min="0" data-parsley-type="number">` :
                    data;
            },
            createdCell: function (td) {
                td.style.minWidth = '50px';
            }
        },
        {
            data: 'doctorValue',
            width: '50px',

            render: function (data, type, row) {
                return type === 'display' ?
                    `<input type="number" class="form-control no-spinner" value="${data}" data-field="doctorValue" data-id="${row.checkId}" min="0" data-parsley-type="number">` :
                    data;
            },
            createdCell: function (td) {
                td.style.minWidth = '50px';
            }
        },
        {
            data: 'hospitalValue',
            width: '50px',

            render: function (data, type, row) {
                return type === 'display' ?
                    `<input type="number" class="form-control no-spinner" value="${data}" data-field="hospitalValue" data-id="${row.checkId}" min="0" data-parsley-type="number">` :
                    data;
            },
            createdCell: function (td) {
                td.style.minWidth = '50px';
            }
        },
        {
            data: 'drSendId',
            render: function (data, type, row) {
                return type === 'display' ? '<div class="loading">Loading...</div>' : data;
            },
            createdCell: async function (td, cellData, rowData) {
                if (rowData) {
                    const content = DropdownBuilders.buildDoctorDd(rowData, dataLists.doctorList);
                    td.innerHTML = content;
                }
                td.style.minWidth = '100px';
            }
        },
        
        {
            data: 'extraVal',
            width:'50px',
            render: function (data, type, row) {
                return type === 'display' ?
                    `<input type="number" class="form-control no-spinner" value="${data}" data-field="extraVal" data-id="${row.checkId}" min="0" data-parsley-type="number">` :
                    data;
            },
            createdCell: function (td) {
                td.style.minWidth = '50px';
            }
        },
        {
            data: 'extraVal2',
            width: '50px',

            render: function (data, type, row) {
                return type === 'display' ?
                    `<input type="number" class="form-control no-spinner" value="${data}" data-field="extraVal2" data-id="${row.checkId}" min="0" max="100" data-parsley-type="number">` :
                    data;
            },
            createdCell: function (td) {
                td.style.minWidth = '50px';
            }
        },
        {
            data: 'approvalPeriod',
            render: function (data, type, row) {
                return type === 'display' ? DropdownBuilders.buildApprovalPeriodDd(row) : data;
            },
            createdCell: function (td) {
                td.style.minWidth = '50px';
            }
        },
        {
            data: 'checkIdCancel',
            render: function (data, type, row) {
                return type === 'display' ? DropdownBuilders.buildCheckIdCancelDd(row) : data;
            },
            createdCell: function (td,_,rowData) {
                td.style.minWidth = '50px';
                $(`select.select2[data-id="${rowData.masterId}"]`).select2({
                    width: '100%'
                });
            }
        },
        {
            data: null,
            columnControl:[],
            render: function (data, type, row) {
                return `<button class="btn btn-sm btn-danger btn-delete" data-id="${row.checkId}"><i class=\"bi bi-trash\"></i></button>`;
            }
        }
    ];
}

function setupClinicTransEventHandlers(table, masterId, modifiedRows, dataLists) {

    // Cascade dropdown changes
    setupCascadeDropdowns(dataLists);

    $('.clinicTrans-table').on('draw.dt', function () {
        // Loop through every itmServFlag dropdown
        $('.clinicTrans-table').find('select[data-field="itmServFlag"]').each(function () {
            const $select = $(this);
            const row = $select.closest('tr');
            const itmServFlag = $select.val();

            handleItmServFlagChange(row, itmServFlag);
        });

    });

    // Track modifications
    $('.clinicTrans-table tbody').on('input change', 'input, select', function () {
        var row = $(this).closest('tr');
        var rowIdx = table.row(row).index();
        var rowData = table.row(rowIdx).data();
        var checkId = rowData ? rowData.checkId : undefined;

        if (typeof checkId === "number" || (typeof checkId === "string" && !checkId.includes("temp"))) {
            modifiedRows.add(checkId);
            row.addClass('modified-row');
            enableSaveClinicTransBtn();
        }

       // updateRowTotal(row);
    });

    // Add new row
    $('#btnAddClinicTransNew').off('click').on('click', function () {
        var newRowData = createNewClinicTransRow(masterId);
        var row = table.row.add(newRowData).draw(false);
        $(table.row(row).node()).addClass('new-row');
        enableSaveClinicTransBtn();
        // Scroll to top to show new rows
        $('.dt-scroll-body').scrollTop($('.dt-scroll-body')[0].scrollHeight);
    });


    // Delegate keydown event for input elements inside the table
    $('.clinicTrans-table').on('keydown', 'input, select', function (e) {
        if (e.key === 'Enter') {
            e.preventDefault();

            const fields = $('.clinicTrans-table').find('input:visible, select:visible');
            const index = fields.index(this);

            if (index !== -1 && index + 1 < fields.length) {
                fields.eq(index + 1).focus();
            } else {
                console.log('End of fields reached');
            }
        }
    });


    // Auto-calculate patient value

    //$('.clinicTrans-table tbody').on('change', 'input[data-field="unitPrice"] ,input[data-field="discountVal"],input[data-field="qty"]', function () {
    //    var row = $(this).closest('tr');

    //    updateRowTotal(row);


    //    var unitPrice = parseFloat($(this).val()) || 0;
    //    row.find('input[data-field="patientValue"]').val(unitPrice);

    //});

    $('.clinicTrans-table tbody').on('change', 'select[data-field="itmServFlag"]', function () {
        const row = $(this).closest('tr');
        const itmServFlag = $(this).val();
        handleItmServFlagChange(row, itmServFlag);
    });


    // Auto-calculate Value Service
    $('.clinicTrans-table tbody').on('input change', 'input[data-field="qty"]', function () {
        var row = $(this).closest('tr');
        var rowData = $('.clinicTrans-table').DataTable().row(row).data();

        var clinicId = row.find('select[data-field="clinicId"]').val();
        var sClinicId = row.find('select[data-field="sClinicId"]').val();
        var servId = row.find('select[data-field="servId"]').val();

        var qty = parseFloat(row.find('input[data-field="qty"]').val()) || 0;
        var unitPrice = parseFloat(row.find('input[data-field="unitPrice"]').val()) || 0;
        var valueService = row.find('[data-field="valueService"]');

        var compValue = row.find('input[data-field="compValue"]');
        var patientValue = row.find('input[data-field="patientValue"]');


        var updatedValueService = unitPrice * qty;
        row.find('.valueService').text((updatedValueService).toFixed(2));


        if (masterId && clinicId && sClinicId && servId) {

            AjaxHandlers.fetchPriceListByClinicId(
                masterId,
                clinicId,
                sClinicId,
                servId
            ).then(function (serviceDetails) {
                // Example: auto-fill unitPrice and patientValue if returned
                console.log('serviceDetails', serviceDetails)

                if (serviceDetails) {

                   

                
                    compValue.val(updatedValueService * serviceDetails.compCovPercentage /100)
                        patientValue.val(updatedValueService - compValue.val())
                    

                } else {
                    alert("لا يوجد قائمة اسعار لهذه الشركة");
                }
            }).catch(function (error) {
                console.error('Error fetching service details:', error);
            });
        }

       

        //updateRowTotal(row);



    });
    // Auto-calculate patient value
    $('.clinicTrans-table tbody').on('change', 'select[data-field="clinicId"],select[data-field="sClinicId"],select[data-field="servId"]', function () {
        var row = $(this).closest('tr');
     

        var clinicId = row.find('select[data-field="clinicId"]').val();
        var sClinicId = row.find('select[data-field="sClinicId"]').val();
        var servId = row.find('select[data-field="servId"]').val();
        var discountVal = row.find('input[data-field="discountVal"]');
        var unitPrice = row.find('input[data-field="unitPrice"]');
        var qty = row.find('input[data-field="qty"]');
        var valueService = row.find('[data-field="valueService"]');
        var compValue = row.find('input[data-field="compValue"]');
        var patientValue = row.find('input[data-field="patientValue"]');




        if (masterId && clinicId && sClinicId && servId) {
            //ajax request
            //ajax request
            AjaxHandlers.fetchPriceListByClinicId(
                masterId,
                clinicId,
                sClinicId,
                servId
            ).then(function (serviceDetails) {
                // Example: auto-fill unitPrice and patientValue if returned
                console.log('serviceDetails', serviceDetails)

                if (serviceDetails) {

                    discountVal.val(serviceDetails.discoutComp);
                    unitPrice.val(serviceDetails.plValue);

                    var valueServiceTotal = (qty.val() * unitPrice.val())

                    valueService.text(valueServiceTotal);

                    compValue.val(valueServiceTotal * serviceDetails.compCovPercentage /100)

                    patientValue.val(valueServiceTotal - compValue.val())

                    row.attr('data-compcov', serviceDetails.compCovPercentage / 100);

                } else {
                    alert("لا يوجد قائمة اسعار لهذه الشركة");
                }
            }).catch(function (error) {
                console.error('Error fetching service details:', error);
            });
        } 

            

        
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

function handleItmServFlagChange(row, itmServFlag) {
    const sClinicIdDp = row.find('select[data-field="sClinicId"]');
    const clinicIdDp = row.find('select[data-field="clinicId"]');
    const servIdDp = row.find('select[data-field="servId"]');
    const subIdDp = row.find('select[data-field="subId"]');

    if (itmServFlag == 2) {
        clinicIdDp.prop('disabled', true).removeAttr('required');
        sClinicIdDp.prop('disabled', true).removeAttr('required');
        servIdDp.prop('disabled', true).removeAttr('required');
        subIdDp.prop('disabled', false).attr('required', true);
    } else if (itmServFlag == 3) {
        clinicIdDp.prop('disabled', false).attr('required', true);
        sClinicIdDp.prop('disabled', false).attr('required', true);
        servIdDp.prop('disabled', false).attr('required', true);
        subIdDp.prop('disabled', true).removeAttr('required');
    } else {
        clinicIdDp.prop('disabled', true).removeAttr('required');
        sClinicIdDp.prop('disabled', true).removeAttr('required');
        servIdDp.prop('disabled', true).removeAttr('required');
        subIdDp.prop('disabled', true).removeAttr('required');
    }
}

function setupCascadeDropdowns(dataLists) {
    // Main clinic change
    $('.clinicTrans-table tbody').on('change', '.clinic-dropdown', async function () {
        let selectedClinicId = $(this).val();
        let rowId = $(this).data('id');
        let $SubClinicSelect = $(`.sub-clinic-dropdown[data-id="${rowId}"]`);
        let $ServSelect = $(`.serv-dropdown[data-id="${rowId}"]`);


        $SubClinicSelect.empty().append('<option value="">Choose</option>').prop('disabled', true);
        $ServSelect.empty().append('<option value="">Choose</option>').prop('disabled', true);

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

        $ServSelect.empty().append('<option value="">Choose</option>').prop('disabled', true);


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
        subId: "",
        qty: 1,
        unitPrice: 0,
        patientValue: 0,
        extraVal: 0,
        extraVal2: 0,
        compValue: 0,
        hospitalValue: 0,
        doctorValue: 0,
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
    var discountVal = parseFloat(row.find('input[data-field="discountVal"]').val()) || 0;

    var updatedUnitPrice = unitPrice - (unitPrice * discountVal / 100)
    row.find('input[data-field="unitPrice"]').val(updatedUnitPrice)

    var updatedValueService = updatedUnitPrice * qty;
    row.find('.valueService').text((updatedValueService).toFixed(2));
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
        var insertData = prepareInsertDataClinic(table, masterId);
        var updateData = prepareUpdateDataClinic(table, masterId, modifiedRows);

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
        disableSaveClinicTransBtn();

    } catch (error) {
        console.log('Error saving data: ' + error.message);
        enableSaveClinicTransBtn();

    } finally {
        hideSpinnerClinicTrans();
    }
}

function validateClinicTransData() {
    const tableSelector = ".clinicTrans-table";
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

function prepareInsertDataClinic(table, masterId) {
    var insertData = [];
    table.rows('.new-row').every(function () {
        var row = $(this.node());
        var data = this.data();
        var checkId = data.checkId;

        if (typeof (checkId) === "string" && checkId.includes("temp")) {
            insertData.push({
                exDate: moment(new Date()).locale('en').format('YYYY-MM-DD') ,
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

function prepareUpdateDataClinic(table, masterId, modifiedRows) {
    var updateData = [];
    $('.modified-row').not('.new-row').each(function () {
        var row = $(this);
        var checkId = table.row(row).data().checkId;

        if (checkId) {
            updateData.push({
                checkId: checkId,
                exDate: moment(row.find('.exDate').text().trim(), 'DD/MM/YYYY').locale('en').format('YYYY-MM-DD'),
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
        if (confirm('هل تريد حذف هذا الصف؟')) {
            try {
                await AjaxHandlers.deleteClinicTransaction(checkId);
                table.ajax.reload();
            } catch (error) {
                console.log('Error deleting item: ' + error.message);
            }
        }
    }
}

function hideSpinnerClinicTrans() {
    // Hide the spinner
    $('#btnSaveClinicTransSpinner').addClass('d-none');
}

function showSpinnerClinicTrans() {
    // Hide the spinner
    $('#btnSaveClinicTransSpinner').removeClass('d-none');
}


function disableSaveClinicTransBtn() {
    $('#btnSaveClinicTrans').attr('disabled', 'disabled');
}
function enableSaveClinicTransBtn() {
    $('#btnSaveClinicTrans').removeAttr('disabled');
}

function disableAddNewClinicTransBtn() {
    $('#btnAddClinicTransNew').attr('disabled', 'disabled');
}
function enableAddNewClinicTransBtn() {
    $('#btnAddClinicTransNew').removeAttr('disabled');
}