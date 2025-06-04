// Main Invoice Application Initialization
$(document).ready(function () {
    InvoiceApp.init();
});

// Main Invoice Application Object
const InvoiceApp = {
    // Application state
    state: {
        currentPatientId: null,
        currentMasterId: null,
        isInitialized: false
    },

    // Initialize the application
    init() {
        try {
            this.bindGlobalEvents();
            this.initializeValidation();
            this.setupErrorHandling();
            this.state.isInitialized = true;
            console.log('Invoice application initialized successfully');
        } catch (error) {
            console.error('Error initializing invoice application:', error);
            InvoiceUtils.showError('Failed to initialize application');
        }
    },

    // Bind global event handlers
    bindGlobalEvents() {
        // Global error handler for AJAX requests
        $(document).ajaxError(function (event, xhr, settings, error) {
            console.error('AJAX Error:', error, 'URL:', settings.url);
            if (xhr.status === 0) {
                InvoiceUtils.showError('Network connection error');
            } else if (xhr.status === 500) {
                InvoiceUtils.showError('Server error occurred');
            } else {
                InvoiceUtils.showError('An error occurred: ' + error);
            }
        });

        // Global AJAX setup
        $.ajaxSetup({
            timeout: InvoiceConfig.timing.ajaxTimeout,
            cache: false
        });

        // Prevent form submission on Enter key in number inputs
        $(document).on('keypress', 'input[type="number"]', function (e) {
            if (e.which === 13) {
                e.preventDefault();
                $(this).blur();
            }
        });

        // Auto-format number inputs
        $(document).on('blur', 'input[type="number"]', function () {
            const value = parseFloat($(this).val());
            if (!isNaN(value)) {
                $(this).val(value.toFixed(2));
            }
        });

        // Confirmation dialogs for delete operations
        $(document).on('click', '.btn-delete', function (e) {
            if (!confirm(InvoiceConfig.messages.confirmations.delete)) {
                e.preventDefault();
                e.stopPropagation();
                return false;
            }
        });
    },

    // Initialize form validation
    initializeValidation() {
        // Set up Parsley.js if available
        if (typeof window.Parsley !== 'undefined') {
            window.Parsley.addValidator('positiveNumber', {
                requirementType: 'number',
                validateNumber: function (value, requirement) {
                    return value >= requirement;
                },
                messages: {
                    en: 'This value should be greater than or equal to %s'
                }
            });

            // Custom validator for percentage
            window.Parsley.addValidator('percentage', {
                validateString: function (value) {
                    const num = parseFloat(value);
                    return !isNaN(num) && num >= 0 && num <= 100;
                },
                messages: {
                    en: 'This value should be between 0 and 100'
                }
            });
        }
    },

    // Setup global error handling
    setupErrorHandling() {
        window.addEventListener('error', function (event) {
            console.error('Global error:', event.error);
            // You might want to send this to a logging service
        });

        window.addEventListener('unhandledrejection', function (event) {
            console.error('Unhandled promise rejection:', event.reason);
            // You might want to send this to a logging service
        });
    },

    // Utility methods for the main app
    utils: {
        // Reset all forms in the page
        resetAllForms() {
            $('form').each(function () {
                this.reset();
                $(this).find('.is-invalid').removeClass('is-invalid');
                $(this).find('.is-valid').removeClass('is-valid');
            });
        },

        // Clear all tables
        clearAllTables() {
            $('.admisson-table tbody').empty();
            $('.clinicTrans-table tbody').empty();
            $(InvoiceConfig.ui.totals.netPatient).text('0.00');
            $(InvoiceConfig.ui.totals.totalServices).text('0.00');
        },

        // Reset application state
        resetState() {
            this.state.currentPatientId = null;
            this.state.currentMasterId = null;
            this.clearAllTables();
            $('.active-row').removeClass('active-row');
        },

        // Get current application state
        getState() {
            return { ...this.state };
        },

        // Update application state
        updateState(newState) {
            Object.assign(this.state, newState);
        }
    },

    // Debug helpers
    debug: {
        // Log current state
        logState() {
            console.log('Current Application State:', InvoiceApp.state);
        },

        // Log configuration
        logConfig() {
            console.log('Invoice Configuration:', InvoiceConfig);
        },

        // Test all AJAX endpoints
        async testEndpoints() {
            const endpoints = InvoiceConfig.endpoints;
            const results = {};

            for (const [name, url] of Object.entries(endpoints)) {
                try {
                    // Skip endpoints that require parameters
                    if (url.includes('/')) {
                        const testUrl = url.replace(/\/\d+$/, '/1'); // Replace with test ID
                        await $.ajax({ url: testUrl, type: 'GET', timeout: 5000 });
                        results[name] = 'OK';
                    }
                } catch (error) {
                    results[name] = 'ERROR: ' + error.message;
                }
            }

            console.table(results);
            return results;
        }
    }
};

// Export for global access
window.InvoiceApp = InvoiceApp;