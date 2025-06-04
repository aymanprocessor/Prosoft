// Invoice Configuration and Constants
const InvoiceConfig = {
    // API Endpoints
    endpoints: {
        // Patient Admissions
        getAdmissions: '/Medical/PatAdmission/GetAdmissions/',
        getCompanies: '/Medical/PatAdmission/GetCompanys/',
        getSubCompanies: '/Medical/PatAdmission/GetSubComp/',
        getSections: '/Medical/PatAdmission/GetSection/',

        // Clinic Transactions
        getClinicTrans: '/Medical/ClinicTrans/GetClinicTrans/',
        saveClinicTransRows: '/Medical/ClinicTrans/SaveRows',
        updateClinicTransRows: '/Medical/ClinicTrans/UpdateRows',
        deleteClinicTrans: '/Medical/ClinicTrans/Delete_ClinicTrans',
        getClinicServices: '/Medical/ClinicTrans/GetServeClinic/',

        // Common Data
        getDepartments: '/Shared/ClassificationCust/GetDepartments',
        getDoctors: '/Medical/Doctor/GetDoctors',
        getMainClinics: '/Medical/MainClinic/GetMainClinics',
        getSubClinics: '/Medical/SubClinic/GetSubClinic/',
        getStocks: '/Stocks/Stock/GetStocks'
    },

    // Table Configuration
    tables: {
        patient: {
            selector: '#myTable',
            pageLength: 10,
            searching: true,
            ordering: true
        },
        admissions: {
            selector: '.admisson-table',
            pageLength: 10,
            searching: false,
            ordering: true,
            maxHeight: '250px'
        },
        clinicTrans: {
            selector: '.clinicTrans-table',
            pageLength: 10,
            searching: false,
            ordering: true
        }
    },

    // Form Validation Rules
    validation: {
        required: ['clinicId', 'sClinicId', 'servId', 'qty'],
        numeric: ['qty', 'unitPrice', 'patientValue', 'compValue', 'discountVal', 'extraVal', 'extraVal2'],
        ranges: {
            qty: { min: 1, max: 999 },
            discountVal: { min: 0, max: 100 },
            sessionNo: { min: 0, max: 100 }
        }
    },

    // UI Settings
    ui: {
        spinners: {
            patAdmission: '#btnSavePatAdmissionSpinner',
            clinicTrans: '#btnSaveClinicTransSpinner'
        },
        buttons: {
            savePatAdmission: '#btnSavePatAdmission',
            addPatAdmission: '#btnAddPatAdmissionNew',
            saveClinicTrans: '#btnSaveClinicTrans',
            addClinicTrans: '#btnAddClinicTransNew'
        },
        totals: {
            netPatient: '#netPatient',
            totalServices: '#totalServicesForVisit'
        }
    },

    // Default Values
    defaults: {
        newAdmission: {
            masterId: "",
            patAdDate: () => moment().format('DD-MM-YYYY'),
            deal: "",
            compId: "",
            compIdDtl: "",
            brnachInitial: "",
            sendFr: "",
            drCode: "",
            patDateExit: "",
            patientValue: 0,
            prepaid: 0,
            mainInvNo: "",
            sessionNo: 1
        },
        newClinicTrans: {
            checkId: () => InvoiceUtils.generateTempId(),
            exDate: () => new Date(),
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
        }
    },

    // Dropdown Options
    dropdownOptions: {
        deal: [
            { value: "", text: "Choose" },
            { value: "1", text: "خاص" },
            { value: "2", text: "تعاقد" }
        ],
        approvalPeriod: [
            { value: "", text: "Choose" },
            { value: 1, text: "صباحي" },
            { value: 2, text: "مسائي" },
            { value: 3, text: "ليلي" },
            { value: 4, text: "ظهرا" }
        ],
        checkIdCancel: [
            { value: "", text: "Choose" },
            { value: 1, text: "حضور" },
            { value: 2, text: "حجز" },
            { value: 3, text: "ملغي" },
            { value: 4, text: "استرداد" }
        ],
        itemServiceFlag: [
            { value: 3, text: "خدمة" },
            { value: 1, text: "صنف" }
        ]
    },

    // Date Formats
    dateFormats: {
        display: 'DD-MM-YYYY',
        api: 'YYYY-MM-DD',
        datetime: 'YYYY-MM-DDTHH:mm'
    },

    // Error Messages
    messages: {
        errors: {
            validation: 'Please fill all required fields correctly',
            network: 'Network error occurred. Please try again.',
            save: 'Error saving data',
            delete: 'Error deleting item',
            load: 'Error loading data'
        },
        success: {
            save: 'Data saved successfully',
            delete: 'Item deleted successfully',
            update: 'Data updated successfully'
        },
        confirmations: {
            delete: 'Are you sure you want to delete this item?'
        }
    },

    // CSS Classes
    cssClasses: {
        activeRow: 'active-row',
        modifiedRow: 'modified-row',
        newRow: 'new-row',
        loading: 'loading',
        invalid: 'is-invalid',
        valid: 'is-valid',
        hidden: 'spinner-hidden'
    },

    // Timing Settings
    timing: {
        debounceDelay: 300,
        ajaxTimeout: 30000,
        retryDelay: 1000,
        maxRetries: 3
    },

    // Feature Flags
    features: {
        enableAutoSave: false,
        enableOfflineMode: false,
        enableBulkOperations: true,
        enableExportFunctions: true
    }
};

// Freeze the configuration to prevent modifications
Object.freeze(InvoiceConfig);