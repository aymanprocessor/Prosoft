const ValidationManager = {
    // Initialize validation for a specific form/table
    init(selector, options = {}) {
        const defaultOptions = {
            errorClass: 'is-invalid',
            successClass: 'is-valid',
            errorsWrapper: '<div class="invalid-feedback"></div>',
            errorTemplate: '<span></span>',
            trigger: 'focusout change'
        };

const config = { ...defaultOptions, ...options };

// Destroy existing Parsley instance if exists
if ($(selector).data('parsley')) {
            $(selector).parsley().destroy();
}

// Initialize new instance
return $(selector).parsley(config);
    },
    
    // Validate specific elements
    validateElements(elements) {
    let isValid = true;
    elements.each(function() {
        const $field = $(this);
        if ($field.data('parsley')) {
            if (!$field.parsley().validate()) {
                isValid = false;
            }
        }
    });
    return isValid;
},
    
    // Add validation to dynamically created elements
    addValidation(element) {
        $(element).parsley();
}
};