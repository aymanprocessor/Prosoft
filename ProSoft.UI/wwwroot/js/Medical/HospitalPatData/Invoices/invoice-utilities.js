// Invoice Utilities and Helper Functions
const InvoiceUtils = {
    // Format currency
    formatCurrency(amount) {
        return parseFloat(amount || 0).toFixed(2);
    },

    // Format date for display
    formatDate(date, format = 'DD-MM-YYYY') {
        return moment(date).format(format);
    },

    // Generate temporary ID for new rows
    generateTempId() {
        return "temp" + Math.floor(Math.random() * (1000 - 1) + 1);
    },

    // Validate required fields
    validateRequiredFields(formSelector) {
        let isValid = true;
        $(formSelector).find('[required]').each(function () {
            if (!$(this).val()) {
                $(this).addClass('is-invalid');
                isValid = false;
            } else {
                $(this).removeClass('is-invalid');
            }
        });
        return isValid;
    },

    // Show loading spinner
    showSpinner(spinnerId) {
        $(spinnerId).show();
    },

    // Hide loading spinner
    hideSpinner(spinnerId) {
        $(spinnerId).hide();
    },

    // Show success message
    showSuccess(message) {
        // You can integrate with your notification system here
        alert('Success: ' + message);
    },

    // Show error message
    showError(message) {
        // You can integrate with your notification system here
        alert('Error: ' + message);
    },

    // Disable button with spinner
    disableButtonWithSpinner(buttonId, spinnerId) {
        $(buttonId).prop('disabled', true);
        this.showSpinner(spinnerId);
    },

    // Enable button and hide spinner
    enableButtonAndHideSpinner(buttonId, spinnerId) {
        $(buttonId).prop('disabled', false);
        this.hideSpinner(spinnerId);
    },

    // Calculate row total (qty * unit price)
    calculateRowTotal(qty, unitPrice) {
        return (parseFloat(qty || 0) * parseFloat(unitPrice || 0));
    },

    // Parse numeric value safely
    parseNumeric(value, defaultValue = 0) {
        const parsed = parseFloat(value);
        return isNaN(parsed) ? defaultValue : parsed;
    },

    // Parse integer value safely
    parseInteger(value, defaultValue = 0) {
        const parsed = parseInt(value);
        return isNaN(parsed) ? defaultValue : parsed;
    },

    // Debounce function for search inputs
    debounce(func, wait) {
        let timeout;
        return function executedFunction(...args) {
            const later = () => {
                clearTimeout(timeout);
                func(...args);
            };
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
        }
            ;
    },

    // Highlight row as modified
    markRowAsModified(row, className = 'modified-row') {
        $(row).addClass(className);
    },

    // Remove modification highlighting
    clearRowModification(row, className = 'modified-row') {
        $(row).removeClass(className);
    },

    // Check if row is new (temporary)
    isNewRow(checkId) {
        return typeof checkId === "string" && checkId.includes("temp");
    },

    // Check if row is existing
    isExistingRow(checkId) {
        return typeof checkId === "number" || (typeof checkId === "string" && !checkId.includes("temp"));
    },

    // Get current timestamp
    getCurrentTimestamp() {
        return new Date().toISOString();
    },

    // Sanitize input for safety
    sanitizeInput(input) {
        if (typeof input !== 'string') return input;
        return input.replace(/ [<>] / g, '');
    },

    // Convert form data to object
    formToObject(formSelector) {
        const formData = {};
        $(formSelector).find('input, select, textarea').each(function () {
            const $field = $(this);
            const fieldName = $field.attr('name') || $field.data('field');
            if (fieldName) {
                formData[fieldName] = $field.val();
            }
        });
        return formData;
    },

    // Populate form from object
    objectToForm(data, formSelector) {
        Object.keys(data).forEach(key => {
            const $field = $(formSelector).find(`[name = "${key}"], [data - field = "${key}"]`);
            if ($field.length) {
                $field.val(data[key]);
            }
        });
    },

    // Deep clone object
    deepClone(obj) {
        return JSON.parse(JSON.stringify(obj));
    },

    // Check if object is empty
    isEmpty(obj) {
        return Object.keys(obj).length === 0;
    },

    // Retry function with exponential backoff
    async retry(fn, maxRetries = 3, delay = 1000) {
        for (let i = 0; i < maxRetries; i++) {
            try {
                return await fn();
            }
            catch (error) {
                if (i === maxRetries - 1) throw error;
                await new Promise(resolve => setTimeout(resolve, delay * Math.pow(2, i)));
            }
        }
    },

    // Local storage helpers (with error handling)
    localStorage:
    {
        set(key, value) {
            try {
                localStorage.setItem(key, JSON.stringify(value));
                return true;
            }
            catch (error) {
                console.warn('LocalStorage set failed:', error);
                return false;
            }
        },

        get(key, defaultValue = null) {
            try {
                const item = localStorage.getItem(key);
                return item ? JSON.parse(item) : defaultValue;
            }
            catch (error) {
                console.warn('LocalStorage get failed:', error);
                return defaultValue;
            }
        },

        remove(key) {
            try {
                localStorage.removeItem(key);
                return true;
            }
            catch (error) {
                console.warn('LocalStorage remove failed:', error);
                return false;
            }
        }
    }
};