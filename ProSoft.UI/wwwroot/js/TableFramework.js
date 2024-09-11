var InlineEditor = (function () {
    let isBatchMode = true; // Flag to track the batch mode state

    // Constants for selectors and other reusable values
    const SELECTORS = {
        tableBodySelector: '#TableBody',
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

    // Private function to send AJAX request
    function sendAjaxRequest(url, data, successCallback, errorCallback) {
        const token = $('input[name="__RequestVerificationToken"]').attr('value');

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            contentType: false,    // Required for FormData
            processData: false,    // Required for FormData
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
                errorCallback(error);
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
                    //case 'ArrowUp':
                    //    if (currentRowIndex > 0) {
                    //        navigateToCell(currentRowIndex - 1, currentColIndex);
                    //    }
                    //    break;
                    //case 'ArrowDown':
                    //    if (currentRowIndex < totalRows - 1) {
                    //        navigateToCell(currentRowIndex + 1, currentColIndex);
                    //    }
                    //    break;
                    //case 'ArrowRight':
                    //    if (currentColIndex > 0) {
                    //        navigateToCell(currentRowIndex, currentColIndex - 1);
                    //    }
                    //    break;
                    //case 'ArrowLeft':
                    //    if (currentColIndex < totalCols - 1) {
                    //        navigateToCell(currentRowIndex, currentColIndex + 1);
                    //    }
                    //    break;
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
    //function generateInputField(rowId, field, value, isBatchMode) {

    //    if (isBatchMode === false) {
    //        return value;
    //    }

    //    switch (field.type) {
    //        case 'text':
    //            return '<input type="text" value="' + value + '" class="form-control" id="' + field.name + '_' + rowId + '">';
    //        case 'number':
    //            return '<input type="number" value="' + value + '" class="form-control" id="' + field.name + '_' + rowId + '">';
    //        case 'date':
    //            return '<input type="date" value="' + value + '" class="form-control" id="' + field.name + '_' + rowId + '">';
    //        case 'select':
    //            var options = '';
    //            field.options.forEach(function (option) {
    //                options += '<option value="' + option.value + '" ' + (option.value === value ? 'selected' : '') + '>' + option.label + '</option>';
    //            });
    //            return '<select class="form-control" id="' + field.name + '_' + rowId + '">' + options + '</select>';
    //        default:
    //            return '<input type="text" value="' + value + '" class="form-control" id="' + field.name + '_' + rowId + '">';
    //    }
    //}

    // Private function to generate input fields dynamically based on column type
    function generateInputField(rowId, field, value, isBatchMode) {
        let inputField;
        if (isBatchMode === false) {
            return `<span data-row-id="${rowId}">${value}</span>`;
        }
        const disabledClass = field.editable === false ? 'disabled' : '';
        switch (field.type) {
            case 'text':
                inputField = `<input type="text" value="${value}" class="form-control" id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}" onfocus="this.select()" ${disabledClass}/>`;
                break;
            case 'number':
                inputField = `<input type="number" value="${value}" class="form-control " id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}" onfocus="this.select()" ${disabledClass}/>`;
                break;
            case 'date':
                inputField = `<input type="date" value="${value}" class="form-control " id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}" onfocus="this.select()" ${disabledClass}/>`;
                break;
            case 'select':
                let options = '';
                field.options.forEach(function (option) {
                    options += `<option value="${option.value}" ${(option.value === value ? 'selected' : '')}>${option.label}</option>`;
                });
                inputField = `<select class="form-control" id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}" onfocus="this.select()" ${disabledClass}>${options}</select>`;
                break;
            default:
                inputField = `<input type="text" value="${value}" class="form-control }" id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}" onfocus="this.select()" ${disabledClass}/>`;
                break;
        }

        // Return the input field with an onChange event listener


        return inputField;
    }

    // Private function to handle the value change event for a cell
    function handleCellValueChange(inputElement, updateUrl) {
        const rowId = inputElement.data('row-id');

        // Gather all field values from the row
        const row = inputElement.closest('tr');
        const formData = new FormData();

        // Collect values from all input/select elements in the row
        row.find(SELECTORS.editableInputSelector).each(function () {
            const fieldName = $(this).data('field-name');
            let fieldValue = $(this).val();

            // Perform type conversion based on field name or input type
            if ($(this).attr('type') === 'number') {
                fieldValue = parseFloat(fieldValue);  // Convert to a float for number fields
            } else if ($(this).attr('type') === 'date') {
                fieldValue = new Date(fieldValue).toISOString();  // Convert to ISO date format
            }

            // Append the value to the FormData object
            formData.append(fieldName, fieldValue);
        });

        // Add rowId to the FormData
        formData.append('Id', rowId);

        console.log([...formData]); // Log FormData content for debugging

        // Send AJAX request to update the row in the database
        sendAjaxRequest(updateUrl, formData, function (response) {

           
        }, function (errorMessage) {
            alert(MESSAGES.errorMessage + ": " + errorMessage); // Error callback
        });
    }



    // Private function to generate table rows dynamically with different column types
    function generateTableBody(data, fieldMapping, actionButtonsHtml, isBatchMode) {
        var tableBodyHtml = '';

        data.forEach(function (row) {

            tableBodyHtml += '<tr class="dynamic-row">';
            fieldMapping.forEach(function (field) {


                var value = row[field.name];
                tableBodyHtml += '<td>' + generateInputField(row.Id, field, value, isBatchMode) + '</td>';
            });
            //tableBodyHtml += '<td>' + actionButtonsHtml.replace(/{{rowId}}/g, row.id) + '</td>';
            tableBodyHtml += '</tr>';
        });

        return tableBodyHtml;
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

            var tableBodyHtml = generateTableBody(config.data, config.fieldMapping, config.actionButtonsHtml, isBatchMode);
            $(SELECTORS.tableBodySelector).html(tableBodyHtml);

            $(document).on('change', SELECTORS.editableInputSelector, function () {
                handleCellValueChange($(this), config.updateUrl); // Trigger the AJAX call on value change
            });

            // Initialize inline edit and filter features after rendering
            this.initInlineEdit({
                updateUrl: config.updateUrl,
                fieldMapping: config.fieldMapping
            });

            this.initFilter();

            // Initialize keyboard navigation for table
            handleKeyboardNavigation();

        }

    }
})();
