// Dropdown Builder Functions
const DropdownBuilders = {
    // Build Deal Dropdown
     buildDealDd(row) {
        let selectedValue = row.deal;
        let html = `<select class="form-control text-center deal-dropdown select2" data-field="deal" data-id="${row.masterId}" required >`;
        html += `
        <option value="" ${!selectedValue ? 'selected' : ''}>Choose</option>
        <option value=1 ${selectedValue === 1 ? 'selected' : ''}>خاص</option>
        <option value=2 ${selectedValue === 2 ? 'selected' : ''}>تعاقد</option>
    `;
        html += `</select>`;
        return html;
    },
    // Build ItemService Dropdown

    buildItmServFlagDd(row) {
        let selectedValue = row.itmServFlag;
        let html = `<select class="form-control text-center itmServFlag-dropdown select2" data-field="itmServFlag" data-id="${row.masterId}" required >`;
        html += `
        <option value="" ${!selectedValue ? 'selected' : ''}>Choose</option>
        <option value=3 ${selectedValue === 3 ? 'selected' : ''}>خدمة</option>
        <option value=2 ${selectedValue === 2 ? 'selected' : ''}>صنف</option>
    `;
        html += `</select>`;
        return html;
    },

    // Build SubItem Dropdown

    buildSubIdFlagDd(row) {
        let selectedValue = row.subId;
        let html = `<select class="form-control text-center subId-dropdown select2" data-field="subId" data-id="${row.masterId}"  >`;
        html += `
        <option value="" ${!selectedValue ? 'selected' : ''}>Choose</option>
       
    `;
        html += `</select>`;
        return html;
    },
    
    // Build Companies Dropdown
    async buildCompaniesDd(row, companyList) {
        let disabled = row.deal ? '' : ' disabled';
        let html = `<select class="form-control text-center compId-dropdown select2" data-field="compId" data-id="${row.masterId}" ${disabled} required="">`;

        if (row.deal && companyList) {
            for (let comp of companyList) {
                let selected = (comp.compId === row.compId) ? ' selected' : '';
                html += `<option value="${comp.compId}" ${selected}>${comp.compName}</option>`;
            }
        }
        html += `</select>`;
        return html;
    },

    // Build Company Details Dropdown
    async buildCompaniesDtlDd(row, companyDetailsList) {
        let disabled = row.compId ? '' : ' disabled';
        let html = `<select class="form-control text-center compIdDtl-dropdown select2" data-field="compIdDtl" data-id="${row.masterId}" ${disabled} required="">`;

        if (row.compId && companyDetailsList) {
            for (let compDtl of companyDetailsList) {
                let selected = (compDtl.compIdDtl === row.compIdDtl) ? ' selected' : '';
                html += `<option value="${compDtl.compIdDtl}" ${selected}>${compDtl.compNameDtl}</option>`;
            }
        }
        html += `</select>`;
        return html;
    },

    // Build Doctor Dropdown
    buildDoctorDd(row, doctorList) {
        let html = `<select class="form-control text-center doctor-dropdown select2" data-field="drSendId" data-id="${row.masterId || row.checkId}" required>`;
        html += `<option value="">Choose</option>`;

        if (doctorList) {
            for (let item of doctorList) {
                let selected = (item.drId === (row.drCode || row.drSendId)) ? ' selected' : '';
                html += `<option value="${item.drId}" ${selected}>${item.drDesc}</option>`;
            }
        }
        html += `</select>`;
        return html;
    },

    // Build Department Dropdown
    buildDepartmentDd(row, departmentList) {
        let html = `<select class="form-control text-center brnachInitial-dropdown select2" data-field="brnachInitial" data-id="${row.masterId}" required>`;
        html += `<option value="">Choose</option>`;

        if (departmentList) {
            for (let item of departmentList) {
                let selected = (item.classificationCust1 === row.brnachInitial) ? ' selected' : '';
                html += `<option value="${item.classificationCust1}" ${selected}>${item.classificationDesc}</option>`;
            }
        }
        html += `</select>`;
        return html;
    },

    // Build Section Dropdown
    buildSectionDd(row, sectionList) {
        let disabled = row.brnachInitial ? '' : ' disabled';
        let html = `<select class="form-control text-center sendFr-dropdown select2" data-field="sendFr" data-id="${row.masterId}" ${disabled} required="">`;

        if (row.brnachInitial && sectionList) {
            for (let section of sectionList) {
                let selected = (section.regionCode === row.sendFr) ? ' selected' : '';
                html += `<option value="${section.regionCode}" ${selected}>${section.regionDesc}</option>`;
            }
        }
        html += `</select>`;
        return html;
    },

    // Build Main Clinic Dropdown
    buildMainClinicDd(row, mainClinicList) {
        let html = `<select class="form-control text-center clinic-dropdown select2" data-field="clinicId" data-id="${row.checkId}" required >`;
        html += `<option value="">Choose</option>`;

        if (mainClinicList) {
            for (let item of mainClinicList) {
                let selected = (item.clinicId === row.clinicId) ? ' selected' : '';
                html += `<option value="${item.clinicId}"${selected}>${item.clinicDesc}</option>`;
            }
        }
        html += `</select>`;
        return html;
    },

    // Build Sub Clinic Dropdown
    buildSubClinicDd(row, subClinicList) {
        let disabled = row.clinicId ? '' : ' disabled';
        let html = `<select class="form-control text-center sub-clinic-dropdown select2" data-field="sClinicId" data-id="${row.checkId}" ${disabled} required="">`;

        if (row.clinicId && subClinicList) {
            for (let subClinic of subClinicList) {
                let selected = (subClinic.sClinicId === row.sClinicId) ? ' selected' : '';
                html += `<option value="${subClinic.sClinicId}"${selected}>${subClinic.sClinicDesc}</option>`;
            }
        }
        html += `</select>`;
        return html;
    },

    // Build Service Dropdown
    buildServDd(row, servList) {
        let disabled = row.sClinicId ? '' : ' disabled';
        let html = `<select class="form-control text-center serv-dropdown select2" data-field="servId" data-id="${row.checkId}" ${disabled} required>`;

        if (row.sClinicId && servList) {
            for (let serv of servList) {
                let selected = (serv.servId === row.servId) ? ' selected' : '';
                html += `<option value="${serv.servId}"${selected}>${serv.servDesc}</option>`;
            }
        }
        html += `</select>`;
        return html;
    },

    // Build Stock Dropdown
    buildStockDd(row, stockList) {
        let html = `<select class="form-control text-center stock-dropdown select2" data-field="stockId" data-id="${row.checkId}" required>`;
        html += `<option value="">Choose</option>`;

        if (stockList) {
            for (let item of stockList) {
                let selected = (item.stkcod === row.stockId) ? ' selected' : '';
                html += `<option value="${item.stkcod}" ${selected}>${item.stknam}</option>`;
            }
        }
        html += `</select>`;
        return html;
    },

    // Build Approval Period Dropdown
    buildApprovalPeriodDd(row) {
        let html = `<select class="form-control text-center approval-period-dropdown select2" data-field="approvalPeriod" data-id="${row.checkId}" required>`;
        html += `
            <option value="">Choose</option>
            <option value=1 ${row.approvalPeriod == 1 ? ' selected' : ''}>صباحي</option>
            <option value=2 ${row.approvalPeriod == 2 ? ' selected' : ''}>مسائي</option>
            <option value=3 ${row.approvalPeriod == 3 ? ' selected' : ''}>ليلي</option>
            <option value=4 ${row.approvalPeriod == 4 ? ' selected' : ''}>ظهرا</option>
        `;
        html += `</select>`;
        return html;
    },

    // Build Check ID Cancel Dropdown
    buildCheckIdCancelDd(row) {
        let html = `<select class="form-control text-center check-cancel-dropdown select2" data-field="checkIdCancel" data-id="${row.checkId}" required>`;
        html += `
            <option value="">Choose</option>
            <option value=1 ${row.checkIdCancel == 1 ? ' selected' : ''}>حضور</option>
            <option value=2 ${row.checkIdCancel == 2 ? ' selected' : ''}>حجز</option>
            <option value=3 ${row.checkIdCancel == 3 ? ' selected' : ''}>ملغي</option>
            <option value=4 ${row.checkIdCancel == 4 ? ' selected' : ''}>استرداد</option>
        `;
        html += `</select>`;
        return html;
    }
};