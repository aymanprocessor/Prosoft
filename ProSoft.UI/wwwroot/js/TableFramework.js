var InlineEditor = (function () {
    let isBatchMode = true; // Flag to track the batch mode state

    // Constants for selectors and other reusable values
    const SELECTORS = {
        saveButtonSelector: '.save-btn',
        filterInputSelector: '#productFilter',
        tableRowSelector: '.dynamic-row',
        editableInputSelector: 'input, select',
        batchModeButtonSelector: '#batchModeBtn'
    };

    const MESSAGES = {
        successMessage: 'Product updated successfully!',
        errorMessage: 'Failed to update product.'
    };

    function refreshTable(config) {
        $.ajax({
            url: config.dataUrl,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    console.table(response.data);
                    config.data = response.data;
                    InlineEditor.renderTable(config);
                    console.log('Table refreshed successfully');
                } else {
                    console.error('Failed to refresh table:', response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error('Error refreshing table:', error);
            }
        });
    }

    // Private function to send AJAX request
    function sendAjaxRequest(url, data, successCallback, errorCallback) {
        const token = $('input[name="__RequestVerificationToken"]').attr('value');
        console.log("Data :", data)
        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json', // Set content type to JSON
            dataType: 'json', // Expected response data type
            data: data,
            headers: {
                'RequestVerificationToken': token
            },
            success: function (response) {
                if (response.success) {
                    successCallback(response);
                } else {
                    errorCallback(response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error("XHR", xhr, "STATUS", status, "ERROR", error);
            }
        });
    }

    // Private function to gather form data based on input elements
    function gatherFormData(rowId, fieldMapping) {
        var formData = {};
        fieldMapping.forEach(function (field) {
            formData[field.name] = $('#' + field.name + '_' + rowId).val();
        });
        return formData;
    }

    // Private function to enable or disable batch mode
    function toggleBatchMode() {
        isBatchMode = !isBatchMode; // Toggle batch mode state

        if (isBatchMode) {
            $(SELECTORS.batchModeButtonSelector).text('Disable Batch Mode');
            enableInlineEditing(fieldMapping); // Enable inline editing for the whole table
        } else {
            $(SELECTORS.batchModeButtonSelector).text('Enable Batch Mode');
            disableInlineEditing(fieldMapping); // Disable inline editing for the whole table
        }
    }

    // Private function to enable inline editing for all rows
    // Private function to enable inline editing for all rows
    function enableInlineEditing(fieldMapping) {
        $(SELECTORS.tableRowSelector).each(function () {
            $(this).find('td').each(function (index) {
                var rowId = $(this).find('span').data('row-id'); // Assuming you have row-id set on each <tr>

                var field = fieldMapping[index]; // Get the field mapping for the current column

                // Check if the field is editable
                if (field.editable) {
                    var cellValue = $(this).text().trim(); // Current text in the cell

                    // Generate input field based on field type
                    var inputFieldHtml = generateInputField(rowId, field, cellValue);

                    // Replace cell content with the generated input field
                    $(this).html(inputFieldHtml);
                }
            });
        });
    }

    // Private function to disable inline editing for all rows
    function disableInlineEditing(fieldMapping) {
        $(SELECTORS.tableRowSelector).each(function () {
            $(this).find('td').each(function (index) {
                var rowId = $(this).find(SELECTORS.editableInputSelector).data('row-id'); // Assuming you have row-id set on each <tr>

                var input = $(this).find(SELECTORS.editableInputSelector);
                var field = fieldMapping[index]; // Get the field mapping for the current column

                if (input.length > 0 && field.editable) {
                    var inputValue = input.val(); // Get the current input value
                    $(this).html(`<span data-row-id="${rowId}">` + inputValue + '</span>'); // Replace input with plain text
                }
            });
        });
    }

    // Bind the batch mode button
    $(document).on('click', SELECTORS.batchModeButtonSelector, function () {
        toggleBatchMode(); // Toggle batch mode when the button is clicked
    });

    // Private function to filter table rows
    function filterTable(filterInputSelector, tableRowSelector) {
        $(filterInputSelector).on('keyup', function () {
            var filterValue = $(this).val().toLowerCase();
            $(tableRowSelector).filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(filterValue) > -1);
            });
        });
    }

    // Private function to handle keyboard navigation in the table
    function handleKeyboardNavigation() {
        // Add a keydown event listener to the table body
        $(document).on('keydown', function (event) {
            var focusedElement = $(document.activeElement); // The currently focused element

            if (focusedElement.is(SELECTORS.editableInputSelector)) {
                // Get the parent td and tr elements
                var td = focusedElement.closest('td');
                var tr = td.closest('tr');

                // Get the current position of the td and tr
                var currentColIndex = td.index();
                var currentRowIndex = tr.index();
                var totalRows = $(SELECTORS.tableRowSelector).length;
                var totalCols = tr.find('td').length - 1; // Exclude action buttons column

                switch (event.key) {
                    case 'Enter':
                        event.preventDefault(); // Prevent default tab behavior (moving out of the table)
                        if (currentRowIndex < totalRows - 1) {
                            // Move to the same column in the row below
                            navigateToCell(currentRowIndex + 1, currentColIndex);
                        } else {
                            // If on the last row, go to the first row's next column
                            if (currentColIndex < totalCols - 1) {
                                navigateToCell(0, currentColIndex + 1);
                            }
                        }
                        break;
                }
            }
        });
    }

    // Private function to navigate to a specific cell
    function navigateToCell(rowIndex, colIndex) {
        var targetRow = $(SELECTORS.tableRowSelector).eq(rowIndex);
        var targetCell = targetRow.find('td').eq(colIndex);

        // Focus the input/select within the target cell
        var inputElement = targetCell.find(SELECTORS.editableInputSelector).focus();

        if (inputElement.is('input[type="text"], input[type="number"], input[type="date"]')) {
            inputElement.select(); // Select the text in the input field
        }
    }

    // Private function to generate input fields dynamically based on column type
    function generateInputField(rowId, field, value, isBatchMode, row) {
        let inputField;

        if (isBatchMode === false) {
            return `<span data-row-id="${rowId}">${value}</span>`;
        }
        const disabledClass = field.editable === false ? 'disabled' : '';
        const titleCenter = field.titleCenter === true ? 'text-center' : '';
        const widthStyle = field.width ? `style="width: ${field.width};"` : `style="width: 100px;"`; // Apply width if defined

        //var value = field.valueGetter ? field.valueGetter({ data: row }) : valuet;
        switch (field.type) {
            case 'hidden':
                inputField = `<input type="hidden" value="${value}" ${widthStyle} class="form-control ${titleCenter}" id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}"/>`;
                break;
            case 'text':
                inputField = `<input type="text" value="${value}" ${widthStyle} class="form-control ${titleCenter}" id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}" onfocus="this.select()"  ${disabledClass}/>`;
                break;
            case 'number':
                inputField = `<input type="text" value="${value}" ${widthStyle} class="form-control ${titleCenter}" id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}" onfocus="this.select()" pattern="\\d*" oninput="this.value = this.value.replace(/[^0-9]/g, '');"  ${disabledClass}/>`;
                break;
            case 'float':
                inputField = `<input type="text" value="${value}" ${widthStyle} class="form-control ${titleCenter}" id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}" onfocus="this.select()" pattern="\\d+(\\.\\d+)?" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\\..*)\\./g, '$1');" ${disabledClass}/>`;
                break;
            case 'date':
                inputField = `<input type="date" value="${parseDate(value, "YYYY-MM-DD")}" class="form-control ${titleCenter}" id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}" onfocus="this.select()"  ${disabledClass}/>`;

                break;
            case 'select':
                let options = '';
                field.options.forEach(function (option) {
                    options += `<option value="${option.Value}" ${(option.Value == value ? 'selected' : '')}>${option.Text}</option>`;
                });
                inputField = `<select class="form-control form-select ${titleCenter}" ${widthStyle} id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}" onfocus="this.select()"  ${disabledClass}>${options}</select>`;
                break;
            default:
                inputField = `<input type="text" value="${value}" ${widthStyle} class="form-control ${titleCenter}" id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}" onfocus="this.select()"  ${disabledClass}/>`;
                break;
        }

        // Return the input field with an onChange event listener

        return inputField;
    }

    const changedRows = new Set()
    const deletedRows = new Set()
    // Private function to handle the value change event for a cell
    function handleCellValueChange(inputElement, updateUrl) {
        const rowId = inputElement.data('row-id');

        // Gather all field values from the row
        const row = inputElement.closest('tr');
        const isChanged = row.hasClass('changed');
        const isDeleted = row.hasClass('deleted');
        const isCreated = row.hasClass('created');

        if (!isChanged && !isDeleted && !isCreated) {
            changedRows.add(rowId);
            row.addClass('changed');
            row.css('background-color', '#FFFFE0'); // Highlight changed row
        }
    }

    function handleDeleteRow(config) {
        $(document).on('click', `#${config.tableId}-delete-btn.delete-row`, function (event) {
            var idName = config.fieldMapping
                .find(field => field.name === 'action') // Find the 'action' field
                ?.list.find(action => action.name === 'Delete Button') // Find the 'Delete Button' in the list
                ?.options.idName;

            console.log("TABLE ID", config.tableId);
            console.log("IDNAME", idName);

            var rowId = $(this).data(`row-${idName.toLowerCase()}`);
            var row = $(this).closest('tr'); // Get the closest table row

            // Show confirmation alert before proceeding
            if (confirm("Are you sure you want to delete this item?")) {
                // Retrieve the idName

                const formData = {};
                formData[idName] = rowId;

                console.log("formData", formData);

                sendAjaxRequest(config.deleteUrl,
                    JSON.stringify(formData),
                    function (response) {
                        if (response.success) {
                            console.log("Deleted successfully");
                            row.remove(); // Remove the row from the DOM
                        } else {
                            console.log("Delete failed:", response.message);
                        }
                    },
                    function (error) {
                        console.log("Error:", error);
                    });
            } else {
                // If the user clicks 'Cancel', do nothing
                console.log("Delete canceled");
            }
        });
    }

    // Save only changed rows
    function batchSaveRecords(saveUrl) {
        const dataToInsert = [];
        const dataToUpdate = [];

        // Collect data for newly created rows (rows marked with 'created' class)
        $('.dynamic-row.created').each(function () {
            //const rowId = $(this).data('row-id');

            // Initialize a formData object for each new row
            const formData = {};

            // Iterate over the input fields within the row to collect field data
            // const formData = {}
            $(this).find(SELECTORS.editableInputSelector).each(function () {
                const fieldName = $(this).data('field-name');
                let fieldValue = $(this).val();
                const fieldType = $(this).attr('type');
                console.log("FIELD NAME", fieldName);
                console.log("FIELD VALUE", fieldValue);
                // Type conversions (if necessary)
                //if (fieldType === 'number') {
                //    fieldValue = parseFloat(fieldValue);
                //} else if (fieldType === 'checkbox') {
                //    fieldValue = $(this).is(':checked');
                //} else if (fieldType === 'date') {
                //    fieldValue = new Date(fieldValue).toISOString();
                //}

                formData[fieldName] = fieldValue === "" || fieldValue === "null" ? null : fieldValue;
            });
            formData["PLDtlId"] = 0;
            dataToInsert.push(formData);
        });

        // Collect data for rows that have been modified (marked in 'changedRows')
        changedRows.forEach(rowId => {
            const row = $(`${SELECTORS.tableRowSelector}.changed input[data-row-id="${rowId}"]`).closest('tr');

            const formData = { Id: rowId };

            // Collect the changed field values
            row.find(SELECTORS.editableInputSelector).each(function () {
                console.log(this);

                const fieldName = $(this).data('field-name');
                let fieldValue = $(this).val();
                const fieldType = $(this).attr('type');

                // Type conversions (if necessary)
                if (fieldType === 'number') {
                    fieldValue = parseFloat(fieldValue);
                } else if (fieldType === 'checkbox') {
                    fieldValue = $(this).is(':checked');
                } else if (fieldType == 'date') {
                    fieldValue = new Date(fieldValue).toISOString();
                }

                formData[fieldName] = fieldValue === "" || fieldValue === "null" ? null : fieldValue;
            });

            dataToUpdate.push(formData);
        });

        // Check if we have new records or modified records to save
        if (dataToInsert.length > 0 || dataToUpdate.length > 0) {
            const records = {
                UpdateData: dataToUpdate,
                InsertData: dataToInsert
            };

            // Send AJAX request with both insert and update data
            sendAjaxRequest(saveUrl, JSON.stringify(records), function (response) {
                console.log(MESSAGES.successMessage);

                // Reset state of inserted rows
                $('.dynamic-row.created').removeClass('created').css('background-color', '');

                // Clear the list of changed rows
                changedRows.clear();
                $(SELECTORS.tableRowSelector).removeClass('changed').css('background-color', '');
            }, function (errorMessage) {
                console.error("ERROR", errorMessage);
            });
        } else {
            console.log('No changes to save.');
        }
    }

    function parseDate(isoDateString, outputFormat) {
        // Parse the ISO date string
        var parsedDate = dayjs(isoDateString);

        // Format the parsed date using the output format
        return parsedDate.format(outputFormat);
    }

    function attachChangeEventsToInputs(config) {
        $(SELECTORS.editableInputSelector).off('input change blur').on('input change blur', function () {
            handleCellValueChange($(this));
        });

        $(".datepicker").datepicker({
            format: 'dd/mm/yyyy'
        });

        console.log("DATEPICKER");

        $(document).on('change', SELECTORS.editableInputSelector, function () {
            handleCellValueChange($(this), config.updateUrl); // Trigger the AJAX call on value change
        });
    }

    // Generate a new empty row based on the fieldMapping and add it below the last row
    function addNewRow(config) {
        console.log(config.tableId);
        const tableBody = $(`#${config.tableId} tbody`);

        // Generate a new row ID (you can use a better logic for generating unique IDs)
        const newId = tableBody.find('tr').length + 1
        const newRowId = 'new_' + newId;

        let newRowHtml = `<tr  class="dynamic-row created" data-row-id="${newRowId}" style="background-color:#c8fad5">`;

        // Iterate over fieldMapping to generate editable input fields for the new row
        config.fieldMapping.forEach(field => {
            console.log("New ROW", field);
            if (field.type === "action") return;

            newRowHtml += `<td>${generateInputField(newRowId, field, field.defualtValue ?? "", true)}</td>`;
        });

        newRowHtml += `</tr>`;

        // Append the new row to the table body
        tableBody.append(newRowHtml);

        // Attach change detection for the new row inputs
        attachChangeEventsToInputs(config);
    }

    // Add "Save" button below the table
    function addSaveRowsButton(config) {
        //var ButtonHtml = '<div class="col">';
        var ButtonHtml = `<button id="${config.tableId}-save-row-btn" class="btn btn-warning mb-3" ><i class="bi bi-floppy"></i> حفظ </button>`;
        //ButtonHtml += '</div>';

        document.addEventListener('DOMContentLoaded', function () {
            document.getElementById(`${config.tableId}-save-row-btn`).addEventListener('click', function () { batchSaveRecords(config.saveUrl) });
        });
        return ButtonHtml;
    }
    // Add "Insert New Row" button below the table
    function addInsertRowButton(config) {
        //var ButtonHtml = '<div class="col">';
        var ButtonHtml = `<button id="${config.tableId}-insert-new-row-btn" class="btn btn-success mb-3" ><i class="bi bi-plus-circle"></i> اضافة </button>`;
        //ButtonHtml += '</div>';

        document.addEventListener('DOMContentLoaded', function () {
            document.getElementById(`${config.tableId}-insert-new-row-btn`).addEventListener('click', function () { addNewRow(config) });
        });

        return ButtonHtml;
    }
    function addRefreshButton(config) {
        var ButtonHtml = `<button id="${config.tableId}-refresh-btn" class="btn btn-info mb-3"><i class="bi bi-arrow-clockwise"></i> تحديث</button>`;

        document.addEventListener('DOMContentLoaded', function () {
            document.getElementById(`${config.tableId}-refresh-btn`).addEventListener('click', function () { refreshTable(config) });
        });

        return ButtonHtml;
    }
    function generateToolbar(config) {
        var tableBody
        var toolbarHtml = '';
        toolbarHtml += '<div class="d-flex gap-2">';

        toolbarHtml += addInsertRowButton(config);
        toolbarHtml += addSaveRowsButton(config);
        // toolbarHtml += addRefreshButton(config);

        toolbarHtml += '</div>';

        $("#" + config.tableId).prepend(toolbarHtml);
    }

    // Private function to generate table rows dynamically with different column types
    function generateTableBody(config, viewButtonEnable, viewUrl, data, fieldMapping, actionButtonsHtml, isBatchMode) {
        var tableBodyHtml = '';

        tableBodyHtml += '<thead><tr>';
        // Table Header
        fieldMapping.forEach(function (field) {
            var value = field.title;
            //var widthStyle = field.width ? `style="width: ${field.width};"` : ''; // Apply width if defined

            tableBodyHtml += `<th>` + value + '</th>';
        });
        tableBodyHtml += '<th></th>';

        tableBodyHtml += '</tr></thead>';

        tableBodyHtml += ' <tbody>';
        //  Table Data
        console.log("DATA", data)
        data.forEach(function (row) {
            console.log("ROW", row);
            tableBodyHtml += '<tr class="dynamic-row">';
            fieldMapping.forEach(function (field) {
                var value = row[field.name];

                if (field.type === "action") {
                    tableBodyHtml += "<td>";
                    field.list.forEach(function (action) {
                        console.log("ACTION", action);
                        switch (action.name) {
                            case "View Button":
                                tableBodyHtml += `<a class="btn btn-primary view mx-1" href="${action.options.viewUrl}?id=${row[action.options.idName]}" data-row-${action.options.idName}="${row[action.options.idName]}">عرض</a>`;
                                break;

                            case "Delete Button":
                                tableBodyHtml += `<button id='${config.tableId}-delete-btn' class="btn btn-danger delete-row mx-1" data-row-${action.options.idName}="${row[action.options.idName]}"><i class="bi bi-trash"></i></button>`;
                                break;
                        }
                    });
                    tableBodyHtml += "</td>";
                } else {
                    tableBodyHtml += `<td >${generateInputField(row[config.idName], field, value, isBatchMode, row)}</td>`;
                }
            });

            //tableBodyHtml += '<td>' + actionButtonsHtml.replace(/{{rowId}}/g, row.id) + '</td>';
            tableBodyHtml += '</tr>';
        });
        tableBodyHtml += '</tbody >';

        return `<table class="table table-hover">${tableBodyHtml}</table>`;
    }

    // Public API
    return {
        // Initialize inline edit feature
        initInlineEdit: function (config) {
            $(SELECTORS.saveButtonSelector).on('click', function () {
                var rowId = $(this).data('row-id');
                var formData = gatherFormData(rowId, config.fieldMapping);

                sendAjaxRequest(config.updateUrl, formData, function (response) {
                }, function (errorMessage) {
                    alert(MESSAGES.errorMessage + ": " + errorMessage);
                });
            });
        },

        // Initialize filter feature
        initFilter: function () {
            filterTable(SELECTORS.filterInputSelector, SELECTORS.tableRowSelector);
        },

        // Render table dynamically with inline edit and filter
        renderTable: function (config) {
            // Render the table body

            var tableBodyHtml = generateTableBody(config, config.viewButtonEnable, config.viewUrl, config.data, config.fieldMapping, config.actionButtonsHtml, isBatchMode);
            $("#" + config.tableId).html(tableBodyHtml);

            attachChangeEventsToInputs(config);

            // Initialize inline edit and filter features after rendering
            this.initInlineEdit({
                tableId: config.tableId,
                updateUrl: config.updateUrl,
                deleteUrl: config.deleteUrl,
                saveUrl: config.deleteUrl,
                fieldMapping: config.fieldMapping,
                viewButtonEnable: config.viewButtonEnable,
                viewUrl: config.viewUrl
            });

            this.initFilter();

            // Initialize keyboard navigation for table
            handleKeyboardNavigation();
            generateToolbar(config);
            handleDeleteRow(config);
        }
    }
})();