// TableManager.js
class TableManager {
    constructor(config) {
        this.config = config;
        this.changedRows = new Set();
        this.deletedRows = new Set();
        this.isBatchMode = true;
        this.onRowSelect = null;
        this.onAfterCellEdit = null;
        this.onDelete = null;
        this.onSave = null;
        this.initEventListeners();
    }

    static SELECTORS = {
        SAVE_BUTTON: '.save-btn',
        FILTER_INPUT: '#productFilter',
        TABLE_ROW: '.dynamic-row',
        EDITABLE_CELL: '.editable-cell',
        BATCH_MODE_BUTTON: '#batchModeBtn'
    };

    static MESSAGES = {
        SUCCESS: 'Product updated successfully!',
        ERROR: 'Failed to update product.'
    };

    initEventListeners() {
        $(document).on('click', `#${this.config.tableId}-save-row-btn`, this.handleSave.bind(this));
        $(TableManager.SELECTORS.FILTER_INPUT).on('keyup', this.handleFilter.bind(this));
        $(TableManager.SELECTORS.BATCH_MODE_BUTTON).on('click', this.toggleBatchMode.bind(this));
        $(document).on('keydown', this.handleKeyboardNavigation.bind(this));
        //$(document).on('click', TableManager.SELECTORS.EDITABLE_CELL, this.handleCellClick.bind(this));
        $(document).on('blur', 'input, select', this.handleAfterCellEdit.bind(this));
        $(document).on('change', 'input, select', this.handleCellValueChange.bind(this));
        $(document).on('click', '.editable-cell', this.editCell.bind(this));
        $(document).on('click', TableManager.SELECTORS.TABLE_ROW, this.handleRowSelect.bind(this));
        $(document).on('click', `#${this.config.tableId}-delete-btn`, this.handleDelete.bind(this));
    }

    //handleSave(event) {
    //    const rowId = $(event.target).data('row-id');
    //    const formData = this.gatherFormData(rowId);
    //    this.sendAjaxRequest(this.config.updateUrl, formData, this.handleUpdateSuccess, this.handleUpdateError);
    //}

    handleFilter(event) {
        const filterValue = $(event.target).val().toLowerCase();
        $(TableManager.SELECTORS.TABLE_ROW).each((_, row) => {
            $(row).toggle($(row).text().toLowerCase().includes(filterValue));
        });
    }

    toggleBatchMode() {
        this.isBatchMode = !this.isBatchMode;
        $(TableManager.SELECTORS.BATCH_MODE_BUTTON).text(this.isBatchMode ? 'Disable Batch Mode' : 'Enable Batch Mode');
        this.renderTable(); // Re-render the table to reflect the new mode
    }

    handleKeyboardNavigation(event) {
        const focusedElement = $(document.activeElement);
        if (!focusedElement.is('input, select')) return;

        const td = focusedElement.closest('td');
        const tr = td.closest('tr');
        const currentColIndex = td.index();
        const currentRowIndex = tr.index();
        const totalRows = $(TableManager.SELECTORS.TABLE_ROW).length;
        const totalCols = tr.find('td').length - 1;

        if (event.key === 'Enter') {
            event.preventDefault();
            if (currentRowIndex < totalRows - 1) {
                this.navigateToCell(currentRowIndex + 1, currentColIndex);
            } else if (currentColIndex < totalCols - 1) {
                this.navigateToCell(0, currentColIndex + 1);
            }
        }
    }

    handleCellClick(event) {
        const cell = $(event.currentTarget);
        const field = this.config.fieldMapping[cell.index()];
        if (!field.editable) return;

        const rowId = cell.closest('tr').data('row-id');
        const currentValue = cell.text().trim();
        const inputField = this.generateInputField(rowId, field, currentValue);
        cell.html(inputField);
        cell.find('input, select').focus();
    }

    handleInputBlur(event) {


        const row = $(event.currentTarget);
        var formField = {};
        row.closest("tr").find("td").each(function () {
            const td = $(this);
            const fieldName = $(this).data("field");
            let fieldValue;
            if (td.find('input').length > 0) {
                fieldValue = td.find('input').val();
            } else {
                fieldValue = td.html();
            }
            formField[fieldName] = fieldValue;
        })

        const input = $(event.target);
        const cell = input.closest('td');
        const value = input.val();
        cell.html(value);
    }

    handleCellValueChange(event) {
        const inputElement = $(event.target);
        const rowId = inputElement.data('row-id');
        const row = inputElement.closest('tr');

        if (!row.hasClass('changed') && !row.hasClass('deleted') && !row.hasClass('created')) {
            this.changedRows.add(rowId);
            row.addClass('changed').css('background-color', '#FFFFE0');
            console.log(row)
        }
    }

    editCell(event) {
        const cell = $(event.target);
        const field = this.config.fieldMapping[cell.index()];



        const rowId = cell.closest('tr').data('row-id');
        const currentValue = cell.text().trim();
        const inputField = this.generateInputField(rowId, field, currentValue);
        cell.html(inputField);
        cell.find('input, select').focus();

    }


    ///////////////// EVENTS //////////////////

    handleSave(event) {
        const rows = $("tbody tr.dynamic-row.changed");

        var formFieldList = [];
        rows.each(function () {

            var formField = {};

            $(this).find('td').each(function () {
                const td = $(this);
                const fieldName = $(this).data("field");
                let fieldValue;
                if (td.find('input').length > 0) {
                    fieldValue = td.find('input').val();
                } else {
                    fieldValue = td.html();
                }
                formField[fieldName] = fieldValue;
            });
            formFieldList.push(formField);

        });

        if (this.onSave) {
            this.onSave(formFieldList);
        }
    }
    setOnSave(callback) {
        if (typeof callback === 'function') {
            this.onSave = callback;
        } else {
            console.error('onSave must be a function');
        }
    }


    handleDelete(event) {
        const row = $(event.currentTarget);
        var formField = {};
        row.closest("tr").find("td").each(function () {
            const td = $(this);
            const fieldName = $(this).data("field");
            let fieldValue;
            if (td.find('input').length > 0) {
                fieldValue = td.find('input').val();
            } else {
                fieldValue = td.html();
            }
            formField[fieldName] = fieldValue;
        })

        if (this.onDelete) {
            this.onDelete(formField);
        }
    }
    setOnDelete(callback) {
        if (typeof callback === 'function') {
            this.onDelete = callback;
        } else {
            console.error('onDelete must be a function');
        }
    }

    handleAfterCellEdit(event) {

        const row = $(event.currentTarget);
        var formField = [];
        $(`${TableManager.SELECTORS.TABLE_ROW}.changed`).each(function () {
            var rowField = {}

            $(this).find("td").each(function () {
                const td = $(this);
                const fieldName = $(this).data("field");
                let fieldValue;
                if (td.find('input').length > 0) {
                    fieldValue = td.find('input').val();
                } else {
                    fieldValue = td.html();
                }
                rowField[fieldName] = fieldValue;
            })
            const input = $(event.target);
            const cell = input.closest('td');
            const value = input.val();
            cell.html(value);

            formField.push(rowField);

        });
       

        const input = $(event.target);
        const cell = input.closest('td');
        const value = input.val();
        cell.html(value);


        if (this.onAfterCellEdit) {
            this.onAfterCellEdit(formField);
        }
    }

    setOnAfterCellEdit(callback) {
        if (typeof callback === 'function') {
            this.onAfterCellEdit = callback;
        } else {
            console.error('onAfterCellEdit must be a function');
        }
    }

    // New method to handle row selection
    handleRowSelect(event) {
        const row = $(event.currentTarget);
        var formField = {};
        row.closest("tr").find("td").each(function () {
            const td = $(this);
            const fieldName = $(this).data("field");
            let fieldValue;
            if (td.find('input').length > 0) {
                fieldValue = td.find('input').val();
            } else {
                fieldValue = td.html();
            }
            formField[fieldName] = fieldValue;
        })

        if (this.onRowSelect) {
            this.onRowSelect(formField);
        }
    }

    // New method to set the onRowSelect callback
    setOnRowSelect(callback) {
        if (typeof callback === 'function') {
            this.onRowSelect = callback;
        } else {
            console.error('onRowSelect must be a function');
        }
    }
    ///////////////// EVENTS //////////////////

    gatherFormData(rowId) {
        const formData = {};
        this.config.fieldMapping.forEach(field => {
            formData[field.name] = $(`#${field.name}_${rowId}`).val();
        });
        return formData;
    }

    sendAjaxRequest(url, data, successCallback, errorCallback) {
        const token = $('input[name="__RequestVerificationToken"]').attr('value');
        $.ajax({
            url,
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(data),
            headers: { 'RequestVerificationToken': token },
            success: response => response.success ? successCallback(response) : errorCallback(response.message),
            error: (xhr, status, error) => console.error("XHR", xhr, "STATUS", status, "ERROR", error)
        });
    }

    generateInputField(rowId, field, value) {
        const disabledClass = '';
        const titleCenter = field.titleCenter ? 'text-center' : '';
        const widthStyle = field.width ? `style="width: ${field.width};"` : `style="width: 100px;"`;
        const fieldId = `${field.name.replace('.', '_')}_${rowId}`;
        switch (field.type) {
            case 'hidden':
                return `<input type="hidden" value="${value}" ${widthStyle} class="form-control ${titleCenter}" id="${fieldId}" data-row-id="${rowId}" data-field-name="${field.name}"/>`;
            case 'text':
                return `<input type="text" value="${value}" ${widthStyle} class="form-control ${titleCenter}" id="${fieldId}" data-row-id="${rowId}" data-field-name="${field.name}" ${disabledClass}/>`;
            case 'number':
                return `<input type="text" value="${value}" ${widthStyle} class="form-control ${titleCenter}" id="${fieldId}" data-row-id="${rowId}" data-field-name="${field.name}" pattern="\\d*" oninput="this.value = this.value.replace(/[^0-9]/g, '');" ${disabledClass}/>`;
            case 'float':
                return `<input type="text" value="${value}" ${widthStyle} class="form-control ${titleCenter}" id="${fieldId}" data-row-id="${rowId}" data-field-name="${field.name}" pattern="\\d+(\\.\\d+)?" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\\..*)\\./g, '$1');" ${disabledClass}/>`;
            case 'date':
                return `<input type="date" value="${this.parseDate(value, "YYYY-MM-DD")}" class="form-control ${titleCenter}" id="${fieldId}" data-row-id="${rowId}" data-field-name="${field.name}" ${disabledClass}/>`;
            case 'select':
                const options = field.options.map(option => `<option value="${option.Value}" ${option.Value == value ? 'selected' : ''}>${option.Text}</option>`).join('');
                return `<select class="form-control form-select ${titleCenter}" ${widthStyle} id="${field.name}_${rowId}" data-row-id="${rowId}" data-field-name="${field.name}" ${disabledClass}>${options}</select>`;
            default:
                return `<input type="text" value="${value}" ${widthStyle} class="form-control ${titleCenter}" id="${fieldId}" data-row-id="${rowId}" data-field-name="${field.name}" ${disabledClass}/>`;
        }
    }

    getNestedValue(obj, path) {
        return path.split('.').reduce((current, key) => (current && current[key] !== undefined) ? current[key] : undefined, obj);
    }


    parseDate(isoDateString, outputFormat) {
        return dayjs(isoDateString).format(outputFormat);
    }

    renderTable() {
        const tableBodyHtml = this.generateTableBody();
        $(`#${this.config.tableId}`).html(tableBodyHtml);
        this.generateToolbar();
    }

    generateTableBody() {
        const headerHtml = this.generateTableHeader();
        const bodyHtml = this.generateTableRows();
        return `<table class="table table-hover">${headerHtml}${bodyHtml}</table>`;
    }

    generateTableHeader() {
        const headerCells = this.config.fieldMapping
            .filter(field => field.type !== 'hidden')
            .map(field => `<th>${field.title}</th>`)
            .join('');
        return `<thead><tr>${headerCells}<th></th></tr></thead>`;
    }

    generateTableRows() {
        if (!this.config.data || !this.config.data.length) {
            return '<tbody></tbody>';
        }
        return `<tbody>${this.config.data.map(row => this.generateTableRow(row)).join('')}</tbody>`;
    }

    generateTableRow(row) {
        const cells = this.config.fieldMapping
            .map(field => this.generateTableCell(field, row))
            .join('');
        return `<tr class="dynamic-row" data-row-id="${row[this.config.idName]}">${cells}</tr>`;
    }

    generateTableCell(field, row) {
        const value = this.getNestedValue(row, field.name) ?? (field.defaultValue ?? '');
        const widthStyle = field.width ?? "100px";

        if (field.type === "action") {
            return this.generateActionCell(field, row);
        }

        if (field.type === 'hidden') {
            return this.generateInputField(row[this.config.idName], field, value);
        }

        const editableClass = field.editable ? 'editable-cell' : 'cell-disabled';
        return `<td class="${editableClass}" data-field="${field.name}" style="width:${widthStyle};">${value}</td>`;
    }

    generateActionCell(field, row) {
        const actions = field.list.map(action => {
            switch (action.name) {
                case "View Button":
                    return `<a class="btn btn-primary view mx-1" href="${action.options.viewUrl}?id=${row[action.options.idName]}" data-row-${action.options.idName}="${row[action.options.idName]}">عرض</a>`;
                case "Delete Button":
                    return `<button id='${this.config.tableId}-delete-btn' class="btn btn-danger delete-row mx-1" data-row-${action.options.idName}="${row[action.options.idName]}"><i class="bi bi-trash"></i></button>`;
                default:
                    return '';
            }
        }).join('');
        return `<td>${actions}</td>`;
    }

    generateToolbar() {
        const toolbarHtml = `
            <div class="d-flex gap-2">
                ${this.addInsertRowButton()}
                ${this.addSaveRowsButton()}
            </div>
        `;
        $(`#${this.config.tableId}`).prepend(toolbarHtml);
    }

    addInsertRowButton() {
        return `<button id="${this.config.tableId}-insert-new-row-btn" class="btn btn-success mb-3"><i class="bi bi-plus-circle"></i> اضافة </button>`;
    }

    addSaveRowsButton() {
        return `<button id="${this.config.tableId}-save-row-btn" class="btn btn-warning mb-3"><i class="bi bi-floppy"></i> حفظ </button>`;
    }

    navigateToCell(rowIndex, colIndex) {
        const targetRow = $(TableManager.SELECTORS.TABLE_ROW).eq(rowIndex);
        const targetCell = targetRow.find('td').eq(colIndex);
        targetCell.click(); // Trigger the click event to transform the cell into an input
    }



}
