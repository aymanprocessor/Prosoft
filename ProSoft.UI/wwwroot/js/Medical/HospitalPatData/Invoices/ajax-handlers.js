// AJAX Helper Functions
const AjaxHandlers = {
    // Fetch Companies
    async fetchCompanies(deal) {
        try {
            const data = await $.ajax({
                url: `/Medical/PatAdmission/GetCompanys/${deal}`,
                type: 'GET'
            });
            return data || [];
        } catch (error) {
            console.error('Error fetching companies:', error);
            return [];
        }
    },

    // Fetch Company Details
    async fetchCompanyDetails(compId) {
        try {
            const data = await $.ajax({
                url: `/Medical/PatAdmission/GetSubComp/${compId}`,
                type: 'GET'
            });
            return data || [];
        } catch (error) {
            console.error('Error fetching company details:', error);
            return [];
        }
    },

    // Fetch Departments
    async fetchDepartments() {
        try {
            const data = await $.ajax({
                url: `/Shared/ClassificationCust/GetDepartments`,
                type: 'GET'
            });
            return data || [];
        } catch (error) {
            console.error('Error fetching departments:', error);
            return [];
        }
    },

    // Fetch Sections
    async fetchSections(deptId) {
        try {
            const data = await $.ajax({
                url: `/Medical/PatAdmission/GetSection/${deptId}`,
                type: 'GET'
            });
            return data || [];
        } catch (error) {
            console.error('Error fetching sections:', error);
            return [];
        }
    },

    // Fetch Doctors
    async fetchDoctors() {
        try {
            const data = await $.ajax({
                url: `/Medical/Doctor/GetDoctors`,
                type: 'GET'
            });
            return data || [];
        } catch (error) {
            console.error('Error fetching doctors:', error);
            return [];
        }
    },

    // Fetch Main Clinics
    async fetchMainClinics() {
        try {
            const data = await $.ajax({
                url: '/Medical/MainClinic/GetMainClinics',
                type: 'GET'
            });
            return data || [];
        } catch (error) {
            console.error('Error fetching main clinics:', error);
            return [];
        }
    },

    // Fetch Sub Clinics
    async fetchSubClinics(clinicId) {
        try {
            const data = await $.ajax({
                url: '/Medical/SubClinic/GetSubClinic/' + clinicId,
                type: 'GET'
            });
            return data || [];
        } catch (error) {
            console.error('Error fetching sub clinics:', error);
            return [];
        }
    },

    // Fetch Services
    async fetchServices(subClinicId) {
        try {
            const data = await $.ajax({
                url: '/Medical/ClinicTrans/GetServeClinic/' + subClinicId,
                type: 'GET'
            });
            return data || [];
        } catch (error) {
            console.error('Error fetching services:', error);
            return [];
        }
    },

    // Fetch Stocks
    async fetchStocks() {
        try {
            const data = await $.ajax({
                url: '/Stocks/Stock/GetStocks',
                type: 'GET'
            });
            return data || [];
        } catch (error) {
            console.error('Error fetching stocks:', error);
            return [];
        }
    },

    // Save Clinic Transaction Rows
    async saveClinicTransRows(insertData) {
        try {
            const response = await $.ajax({
                url: '/Medical/ClinicTrans/SaveRows',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(insertData)
            });
            return response;
        } catch (error) {
            console.error('Error saving clinic trans rows:', error);
            throw error;
        }
    },

    // Update Clinic Transaction Rows
    async updateClinicTransRows(updateData) {
        try {
            const response = await $.ajax({
                url: '/Medical/ClinicTrans/UpdateRows',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(updateData)
            });
            return response;
        } catch (error) {
            console.error('Error updating clinic trans rows:', error);
            throw error;
        }
    },

    // Update Pat Admission Rows
    async updatePatAdmissionRows(updateData) {
        try {
            const response = await $.ajax({
                url: '/Medical/PatAdmission/UpdateRows',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(updateData)
            });
            return response;
        } catch (error) {
            console.error('Error updating clinic trans rows:', error);
            throw error;
        }
    },
    // Delete Clinic Transaction
    async deleteClinicTransaction(checkId) {
        try {
            const response = await $.ajax({
                url: '/Medical/ClinicTrans/Delete_ClinicTrans',
                type: 'POST',
                data: {
                    id: checkId,
                    redirect: 'Invoices'
                }
            });
            return response;
        } catch (error) {
            console.error('Error deleting clinic transaction:', error);
            throw error;
        }
    },

    // Delete Pat Admission
    async deletePatAdmission(Id) {
        try {
            const response = await $.ajax({
                url: '/Medical/PatAdmission/Delete_PatientAdmission',
                type: 'POST',
                data: {
                    id: Id,
                    redirect: 'Invoices'

                   }
            });
            return response;
        } catch (error) {
            console.error('Error deleting clinic transaction:', error);
            throw error;
        }
    },

    // Save Clinic Transaction Rows
    async savePatAdmissionRows(insertData) {
        try {
            const response = await $.ajax({
                url: '/Medical/PatAdmission/SaveRows',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(insertData)
            });
            return response;
        } catch (error) {
            console.error('Error saving Pat Admission rows:', error);
            throw error;
        }
    },
};