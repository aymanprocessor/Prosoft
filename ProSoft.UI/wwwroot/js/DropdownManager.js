// Dropdown Manager Module
const DropdownManager = (function () {
    // Private variables
    let config = {
        parentDropdownId: '',
        childDropdownId: '',
        apiEndpoint: '',
        defaultChildOption: '-- اختر --'
    };

    // Private methods
    const getChildDropdown = () => document.getElementById(config.childDropdownId);
    const getParentDropdown = () => document.getElementById(config.parentDropdownId);

    const clearChildDropdown = () => {
        const childDropdown = getChildDropdown();
        childDropdown.innerHTML = `<option value="">${config.defaultChildOption}</option>`;
        childDropdown.disabled = true;
    };

    const createOption = (item) => {
        const option = document.createElement('option');

        // Set value and text using configured field names
        option.value = item[config.fields.id];
        option.textContent = item[config.fields.text];

        // Add extra fields as data attributes
        if (config.fields.extraFields) {
            config.fields.extraFields.forEach(field => {
                if (item[field] !== undefined) {
                    option.dataset[field] = item[field];
                }
            });
        }

        return option;
    };
    const checkParentDropdown = async (parentId) => {
        if (parentId) {
            populateChildDropdown(parentId);
        }
    };
    const populateChildDropdown = async (parentId) => {
        if (!parentId) {
            clearChildDropdown();
            return;
        }

        try {
            const paramsString = new URLSearchParams(config.apiEndpoint.params).toString();

            const response = await fetch(`${config.apiEndpoint.url}/${parentId}?${paramsString}`);
            if (!response.ok) throw new Error('Network response was not ok');

            const data = await response.json();
            const childDropdown = getChildDropdown();

            if (!childDropdown) return;

            childDropdown.innerHTML = `<option value="">${config.defaultChildOption}</option>`;
            childDropdown.disabled = false;

            data.forEach(item => {
                const option = createOption(item);
                childDropdown.appendChild(option);
            });

            // Dispatch custom event after child dropdown is populated
            const event = new CustomEvent('childLoaded', { detail: { parentId, data } });
            childDropdown.dispatchEvent(event);
        } catch (error) {
            console.error('Error fetching dependent dropdown data:', error);
            clearChildDropdown();
        }
    };

    //const assignValue = async (dropdownId) => {
    //};
    // Public methods
    return {
        init: function (options) {
            config = { ...config, ...options };

            const parentDropdown = getParentDropdown();
            const childDropdown = getChildDropdown();
            if (!parentDropdown) throw new Error('Parent dropdown not found');

            // Initial setup
            clearChildDropdown();
            console.log(parentDropdown);
            checkParentDropdown(parentDropdown.value)
            // Event binding
            parentDropdown.addEventListener('change', (e) =>
                populateChildDropdown(e.target.value));

            //childDropdown.addEventListener('change', (e) =>
            //    populateChildDropdown(e.target.value));
        },

        refresh: function () {
            const parentDropdown = getParentDropdown();
            populateChildDropdown(parentDropdown.value);
        }
    };
})();