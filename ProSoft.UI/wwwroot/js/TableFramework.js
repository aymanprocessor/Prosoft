var InlineEditor = (function () {
    let isBatchMode = true; // Flag to track the batch mode state

    // Constants for selectors and other reusable values
    const SELECTORS = {
        tableSelector: '#EISTable',
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
        const titleCenter = field.titleCenter === true ? 'text-center' : '';

        switch (field.type) {
            case 'text':
                inputField = `<input type="text" value="${value}" class="form-control ${titleCenter}" id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}" onfocus="this.select()"  ${disabledClass}/>`;
                break;
            case 'number':
                inputField = `<input type="text" value="${value}" class="form-control ${titleCenter}" id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}" onfocus="this.select()" pattern="\\d*" oninput="this.value = this.value.replace(/[^0-9]/g, '');"  ${disabledClass}/>`;
                break;
            case 'float':
                inputField = `<input type="text" value="${value}" class="form-control ${titleCenter}" id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}" onfocus="this.select()" pattern="\\d+(\\.\\d+)?" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\\..*)\\./g, '$1');" ${disabledClass}/>`;
                break;
            case 'date':
                inputField = `<input type="date" value="${value}" class="form-control ${titleCenter}" id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}" onfocus="this.select()"  ${disabledClass}/>`;
                break;
            case 'select':
                let options = '';
                field.options.forEach(function (option) {
                    options += `<option value="${option.value}" ${(option.value === value ? 'selected' : '')}>${option.label}</option>`;
                });
                inputField = `<select class="form-control ${titleCenter}" id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}" onfocus="this.select()"  ${disabledClass}>${options}</select>`;
                break;
            default:
                inputField = `<input type="text" value="${value}" class="form-control ${titleCenter}" id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}" onfocus="this.select()"  ${disabledClass}/>`;
                break;
        }

        // Return the input field with an onChange event listener


        return inputField;
    }
    const changedRows = new Set()
    // Private function to handle the value change event for a cell
    function handleCellValueChange(inputElement, updateUrl) {
        const rowId = inputElement.data('row-id');

        // Gather all field values from the row
        const row = inputElement.closest('tr');
        const isChanged = row.hasClass('changed');

        if (!isChanged) {
            changedRows.add(rowId);
            row.addClass('changed');
            row.css('background-color', '#FFFFE0'); // Highlight changed row
        }

        //const formData = new FormData();

        //// Collect values from all input/select elements in the row
        //row.find(SELECTORS.editableInputSelector).each(function () {
        //    const fieldName = $(this).data('field-name');
        //    let fieldValue = $(this).val();

        //    // Perform type conversion based on field name or input type
        //    if ($(this).attr('type') === 'number') {
        //        fieldValue = parseFloat(fieldValue);  // Convert to a float for number fields
        //    } else if ($(this).attr('type') === 'date') {
        //        fieldValue = new Date(fieldValue).toISOString();  // Convert to ISO date format
        //    }

        //    // Append the value to the FormData object
        //    formData.append(fieldName, fieldValue);
        //});

        //// Add rowId to the FormData
        //formData.append('Id', rowId);

        //console.log([...formData]); // Log FormData content for debugging

        // Send AJAX request to update the row in the database
        //sendAjaxRequest(updateUrl, formData, function (response) {

        //    console.log("Updated Successfully");

        //}, function (errorMessage) {
           
        //    console.error(MESSAGES.errorMessage + ": " + errorMessage);
        //});
    }


    // Save only changed rows
    function batchSaveRecords(updateUrl) {
        const data = [];

        // Collect data for changed rows
        changedRows.forEach(rowId => {
            const row = $(`${SELECTORS.tableRowSelector} input[data-row-id="${rowId}"]`).closest('tr');

            const formData = { Id: rowId };
            row.find(SELECTORS.editableInputSelector).each(function () {
                const fieldName = $(this).data('field-name');
                let fieldValue = $(this).val();
                const fieldType = $(this).attr('type');
              
                // Convert field value based on its type
                //if (fieldType === 'number') {
                //    fieldValue = parseFloat(fieldValue);
                //} else if (fieldType === 'checkbox') {
                //    fieldValue = $(this).is(':checked');
                //} else if (fieldType === 'date') {
                //    fieldValue = new Date(fieldValue).toISOString();
                //}

                formData[fieldName] = fieldValue;
            });
            console.log("formdata :", formData);
            data.push(formData);
            console.log("data :", data);

        });

        if (data.length > 0) {
            // Send AJAX request with the data of changed rows
            sendAjaxRequest(updateUrl, JSON.stringify( data ), function (response) {
                console.log(MESSAGES.successMessage);
                changedRows.clear(); // Clear the changed rows
                $(SELECTORS.tableRowSelector).removeClass('changed').css('background-color', ''); // Reset row color
            }, function (errorMessage) {
                console.log(errorMessage);
            });
        } else {
            console.log('No changes to save.');
        }
    }

    function attachChangeEventsToInputs() {
        $(SELECTORS.editableInputSelector).off('input change blur').on('input change blur', function () {
            handleCellValueChange($(this));
        });
    }

    // Generate a new empty row based on the fieldMapping and add it below the last row
    function addNewRow() {
        const tableBody = $('#EISTable tbody');

        // Generate a new row ID (you can use a better logic for generating unique IDs)
        const newId = tableBody.find('tr').length + 1
        const newRowId = 'new_' + newId;

        let newRowHtml = `<tr data-row-id="${newRowId}" class="row-modified">`;

        // Iterate over fieldMapping to generate editable input fields for the new row
        fieldMapping.forEach(field => {
            newRowHtml += `<td>${generateInputField(newRowId, field, field.name === "Id" ? newId :'', true)}</td>`;
        });

        newRowHtml += `</tr>`;

        // Append the new row to the table body
        tableBody.append(newRowHtml);

        // Attach change detection for the new row inputs
       // attachChangeEventsToInputs();
    }

    function handleDeleteRow() {
        $(document).on('click', '.delete-row', function () {
            var rowId = $(this).data('row-id');
            $$('tr.dynamic-row').filter(function () {
                return $(this).find('[data-row-id="' + rowId + '"]').length > 0;
            }).addClass('deleted').css('background-color', 'E0EEFF');

        });
    }

    // Add "Save" button below the table
    function addSaveRowsButton(config) {
        //var ButtonHtml = '<div class="col">';
        var ButtonHtml = '<button id="save-row-btn" class="btn btn-warning mb-3" ><i class="bi bi-floppy"></i> حفظ </button>';
        //ButtonHtml += '</div>';

        document.addEventListener('DOMContentLoaded', function () {
            document.getElementById('save-row-btn').addEventListener('click', function () { batchSaveRecords(config.updateUrl) });
        });
        return ButtonHtml;
    }
    // Add "Insert New Row" button below the table
    function addInsertRowButton() {

        //var ButtonHtml = '<div class="col">';
       var ButtonHtml = '<button id="insert-new-row-btn" class="btn btn-success mb-3" ><i class="bi bi-plus-circle"></i></button>';
        //ButtonHtml += '</div>';

   
        document.addEventListener('DOMContentLoaded', function () {
            document.getElementById('insert-new-row-btn').addEventListener('click', addNewRow);
        });

        return ButtonHtml;
    }

    function generateToolbar(config) {
        var tableBody
        var toolbarHtml = '';
        toolbarHtml += '<div class="d-flex gap-2">';

        toolbarHtml += addInsertRowButton(config);
        toolbarHtml += addSaveRowsButton(config);

        toolbarHtml += '</div>';

        $(SELECTORS.tableSelector).prepend(toolbarHtml);
       
    }

    // Private function to generate table rows dynamically with different column types
    function generateTableBody(data, fieldMapping, actionButtonsHtml, isBatchMode) {
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
        data.forEach(function (row) {

            tableBodyHtml += '<tr class="dynamic-row">';
            fieldMapping.forEach(function (field) {


                var value = row[field.name];
                var widthStyle = field.width ? `style="width: ${field.width};"` : ''; // Apply width if defined

                tableBodyHtml += `<td ${widthStyle}>` + generateInputField(row.Id, field, value, isBatchMode) + '</td>';
            });
            tableBodyHtml += `<td ><button class="btn btn-danger class="my-auto" btn-sm delete-row" data-row-id="${row.Id}"><i class="bi bi-trash"></i></button></td>`;

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

            var tableBodyHtml = generateTableBody(config.data, config.fieldMapping, config.actionButtonsHtml, isBatchMode);
            $(SELECTORS.tableSelector).html(tableBodyHtml);

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
            generateToolbar(config);

            handleDeleteRow();
        }

    }
})();
