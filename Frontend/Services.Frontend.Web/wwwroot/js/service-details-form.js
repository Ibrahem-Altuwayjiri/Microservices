/* Service Details Form Management */

let headersDataMap = {};
let editingHeaderId = null;
let editHeaderModal = null;

// DOM Elements
let section = null;
let addBtn = null;
let tableBody = null;
let nameArInput = null;
let nameEnInput = null;

// Selected header state
let selectedHeaderId = null;
let selectedHeaderRow = null;

/**
 * Called when a row in the Available Headers list is clicked
 */
function selectHeaderRow(row) {
    if (selectedHeaderRow) {
        selectedHeaderRow.classList.remove('table-primary');
    }

    selectedHeaderId = row.dataset.id;
    selectedHeaderRow = row;
    row.classList.add('table-primary');

    document.getElementById('selectedHeaderName').textContent =
        row.dataset.nameAr + ' / ' + row.dataset.nameEn;

    if (section) section.classList.remove('d-none');
    if (nameArInput) { nameArInput.value = ''; nameArInput.focus(); }
    if (nameEnInput) nameEnInput.value = '';
}

/**
 * Cancel header selection and hide the value form
 */
function cancelHeaderSelection() {
    if (selectedHeaderRow) selectedHeaderRow.classList.remove('table-primary');
    selectedHeaderId = null;
    selectedHeaderRow = null;
    if (section) section.classList.add('d-none');
}

/**
 * Toggle a lookup row selection and update the badge counter
 */
function toggleLookup(row, e) {
    const chk = row.querySelector('.lookup-chk');
    if (!chk) return;
    if (e && e.target === chk) return;
    chk.checked = !chk.checked;
    updateLookupRowStyle(row, chk.checked);
    updateLookupCount(chk.name);
}

function updateLookupRowStyle(row, checked) {
    row.classList.toggle('table-primary', checked);
}

function updateLookupCount(name) {
    const countMap = { Activities: 'actCount', Domains: 'domCount', Tags: 'tagCount' };
    const badgeId = countMap[name];
    if (!badgeId) return;
    const checked = document.querySelectorAll(`input[name="${name}"]:checked`).length;
    const badge = document.getElementById(badgeId);
    if (badge) badge.textContent = checked;
}

function initLookupRows() {
    document.querySelectorAll('.lookup-chk').forEach(chk => {
        const row = chk.closest('tr');
        if (row) {
            updateLookupRowStyle(row, chk.checked);
        }
        updateLookupCount(chk.name);

        chk.addEventListener('change', function () {
            updateLookupRowStyle(row, this.checked);
            updateLookupCount(this.name);
        });
    });

    // Row click delegation for all lookup tables
    document.querySelectorAll('.lookup-row').forEach(row => {
        row.addEventListener('click', function (e) {
            toggleLookup(this, e);
        });
    });
}

/**
 * Initialize the form when DOM is loaded
 */
function initializeServiceDetailsForm() {
    section = document.getElementById("headerValueForm");
    addBtn = document.getElementById("addBtn");
    tableBody = document.getElementById("dataTableBody");
    nameArInput = document.getElementById("headerValueAr");
    nameEnInput = document.getElementById("headerValueEn");

    if (!addBtn || !tableBody) {
        console.warn("⚠ Some form elements not found. Skipping initialization.");
        return;
    }

    // Event delegation for header rows
    const headersList = document.getElementById('headersList');
    if (headersList) {
        headersList.addEventListener('click', function (e) {
            const row = e.target.closest('.header-select-row');
            if (row && !row.dataset.added) selectHeaderRow(row);
        });
    }

    // Cancel button
    const cancelHeaderBtn = document.getElementById('cancelHeaderBtn');
    if (cancelHeaderBtn) {
        cancelHeaderBtn.addEventListener('click', cancelHeaderSelection);
    }

    // Event listener for add button
    addBtn.addEventListener("click", handleAddButtonClick);

    // Event delegation for edit/delete buttons
    tableBody.addEventListener("click", handleTableActions);

    // Load existing data if available
    loadExistingHeaderData();
}

/**
 * Handle add button click
 */
function handleAddButtonClick() {
    if (!selectedHeaderId) {
        alert("Please select a header from the list");
        return;
    }

    const typeText = selectedHeaderRow.dataset.nameAr + ' / ' + selectedHeaderRow.dataset.nameEn;
    const typeValue = selectedHeaderId;
    const nameAr = nameArInput.value.trim();
    const nameEn = nameEnInput.value.trim();

    if (!nameAr || !nameEn) {
        alert("Please fill all fields");
        return;
    }

    const addedHeaderRow = selectedHeaderRow;

    const row = tableBody.insertRow();
    row.innerHTML = `
        <td data-value="${typeValue}">${typeText}</td>
        <td>${nameAr}</td>
        <td>${nameEn}</td>
        <td>
            <button class="btn btn-sm btn-danger delete-btn" type="button">
                <i class="fas fa-trash"></i> Delete
            </button>
        </td>
    `;

    // Mark header row as already added
    markHeaderRowAdded(addedHeaderRow);

    // Show result table
    const resultTable = document.getElementById('headersResultTable');
    if (resultTable) resultTable.style.display = '';

    cancelHeaderSelection();
}

function markHeaderRowAdded(row) {
    row.dataset.added = 'true';
    row.classList.add('table-success', 'text-muted');
    row.style.cursor = 'not-allowed';
    row.style.opacity = '0.6';
}

function unmarkHeaderRowAdded(row) {
    delete row.dataset.added;
    row.classList.remove('table-success', 'text-muted');
    row.style.cursor = 'pointer';
    row.style.opacity = '';
}

/**
 * Handle table action buttons (Delete only)
 */
function handleTableActions(e) {
    if (!e.target.classList.contains("delete-btn")) return;
    const row = e.target.closest("tr");
    if (!row) return;

    const headerId = row.cells[0].getAttribute("data-value");
    row.remove();

    // Re-enable the matching row in the Available Headers list
    if (headerId) {
        const listRow = document.querySelector(`#headersList tr[data-id="${headerId}"]`);
        if (listRow) unmarkHeaderRowAdded(listRow);
    }

    // Hide table when no rows remain
    const resultTable = document.getElementById('headersResultTable');
    if (resultTable && tableBody.rows.length === 0) {
        resultTable.style.display = 'none';
    }
}
            /**
             * Clear form fields
             */
function clearFormFields() {
    if (nameArInput) nameArInput.value = "";
    if (nameEnInput) nameEnInput.value = "";
    cancelHeaderSelection();
}

/**
 * Load existing header data from the JSON script block rendered by the server.
 * Populates the result table and marks corresponding Available Headers rows.
 */
function loadExistingHeaderData() {
    const jsonEl = document.getElementById('existingHeadersJson');
    if (!jsonEl || !jsonEl.textContent.trim()) return;

    try {
        const headerValues = JSON.parse(jsonEl.textContent);
        if (!Array.isArray(headerValues) || headerValues.length === 0) return;

        headerValues.forEach(hv => {
            // Look up the header name from the Available Headers list
            const listRow = document.querySelector(`#headersList tr[data-id="${hv.headerId}"]`);
            const headerNameAr = listRow ? listRow.dataset.nameAr : '';
            const headerNameEn = listRow ? listRow.dataset.nameEn : '';
            const headerLabel = (headerNameAr || headerNameEn)
                ? headerNameAr + ' / ' + headerNameEn
                : 'Header #' + hv.headerId;

            const row = tableBody.insertRow();
            row.innerHTML = `
                <td data-value="${hv.headerId}">${headerLabel}</td>
                <td>${hv.nameAr || ''}</td>
                <td>${hv.nameEn || ''}</td>
                <td>
                    <button class="btn btn-sm btn-danger delete-btn" type="button">
                        <i class="fas fa-trash"></i> Delete
                    </button>
                </td>
            `;

            if (listRow) markHeaderRowAdded(listRow);
        });

        const resultTable = document.getElementById('headersResultTable');
        if (resultTable) resultTable.style.display = '';
    } catch (e) {
        console.warn('Could not parse existing header data:', e);
    }
}

/**
 * Collect and sync all form data before submission
 /**
  * Called by service-management.js after the form HTML is injected via AJAX.
  * Initializes all form elements and event listeners.
  */
 function initServiceDetailsForm() {
     initializeServiceDetailsForm();
    initLookupRows();
    initServiceStructureDropdowns();
    initDetailsSubTabs();
}

/**
 * Cascading dropdowns for Main Service → Sub Service → Sub Sub Service.
 * All <option> elements are rendered server-side; JS shows/hides them
 * based on the parent selection using data-main-service-id / data-sub-service-id.
 */
function initServiceStructureDropdowns() {
    const mainSelect   = document.getElementById('mainServiceSelect');
    const subSelect    = document.getElementById('subServiceSelect');
    const subSubSelect = document.getElementById('subSubServiceSelect');
    const mainHidden   = document.getElementById('MainServiceId');
    const subHidden    = document.getElementById('SubServiceId');
    const subSubHidden = document.getElementById('SubSubServiceId');

    if (!mainSelect || !subSelect || !subSubSelect) return;

    // Cache all options (skip the first placeholder)
    const allSubOptions    = Array.from(subSelect.options).slice(1);
    const allSubSubOptions = Array.from(subSubSelect.options).slice(1);

    function filterSubServices(mainId) {
        // Remove all non-placeholder options
        while (subSelect.options.length > 1) subSelect.remove(1);

        if (!mainId) {
            subSelect.disabled = true;
            subSelect.value = '';
            if (subHidden) subHidden.value = '';
            filterSubSubServices('');
            return;
        }

        allSubOptions.forEach(opt => {
            if (opt.dataset.mainServiceId === mainId) {
                subSelect.appendChild(opt.cloneNode(true));
            }
        });

        subSelect.disabled = false;

        // Preserve previous selection if still available
        const prev = subHidden ? subHidden.value : '';
        if (prev && subSelect.querySelector(`option[value="${prev}"]`)) {
            subSelect.value = prev;
        } else {
            subSelect.value = '';
            if (subHidden) subHidden.value = '';
        }
        filterSubSubServices(subSelect.value);
    }

    function filterSubSubServices(subId) {
        while (subSubSelect.options.length > 1) subSubSelect.remove(1);

        if (!subId) {
            subSubSelect.disabled = true;
            subSubSelect.value = '';
            if (subSubHidden) subSubHidden.value = '';
            return;
        }

        allSubSubOptions.forEach(opt => {
            if (opt.dataset.subServiceId === subId) {
                subSubSelect.appendChild(opt.cloneNode(true));
            }
        });

        subSubSelect.disabled = false;

        const prev = subSubHidden ? subSubHidden.value : '';
        if (prev && subSubSelect.querySelector(`option[value="${prev}"]`)) {
            subSubSelect.value = prev;
        } else {
            subSubSelect.value = '';
            if (subSubHidden) subSubHidden.value = '';
        }
    }

    mainSelect.addEventListener('change', function () {
        if (mainHidden) mainHidden.value = this.value;
        if (subHidden) subHidden.value = '';
        if (subSubHidden) subSubHidden.value = '';
        filterSubServices(this.value);
    });

    subSelect.addEventListener('change', function () {
        if (subHidden) subHidden.value = this.value;
        if (subSubHidden) subSubHidden.value = '';
        filterSubSubServices(this.value);
    });

    subSubSelect.addEventListener('change', function () {
        if (subSubHidden) subSubHidden.value = this.value;
    });

    // Initialize from current hidden values (edit mode)
    const currentMain = mainHidden ? mainHidden.value : '';
    if (currentMain) {
        mainSelect.value = currentMain;
        filterSubServices(currentMain);
    }
}

/**
 * Initialize the Details sub-tabs (Prerequisites, Steps, Required Documents).
 * Each sub-tab follows the same add/delete table pattern as Headers.
 */
function initDetailsSubTabs() {
    initDetailTable({
        addBtnId: 'addPrerequisiteBtn',
        nameArId: 'prerequisiteNameAr',
        nameEnId: 'prerequisiteNameEn',
        tableBodyId: 'prerequisitesTableBody',
        resultTableId: 'prerequisitesResultTable',
        jsonId: 'existingPrerequisitesJson'
    });
    initDetailTable({
        addBtnId: 'addStepBtn',
        nameArId: 'stepNameAr',
        nameEnId: 'stepNameEn',
        tableBodyId: 'stepsTableBody',
        resultTableId: 'stepsResultTable',
        jsonId: 'existingStepsJson'
    });
    initDetailTable({
        addBtnId: 'addRequiredDocBtn',
        nameArId: 'requiredDocNameAr',
        nameEnId: 'requiredDocNameEn',
        tableBodyId: 'requiredDocsTableBody',
        resultTableId: 'requiredDocsResultTable',
        jsonId: 'existingRequiredDocumentsJson'
    });
}

function initDetailTable(cfg) {
    const addBtn = document.getElementById(cfg.addBtnId);
    const nameArInput = document.getElementById(cfg.nameArId);
    const nameEnInput = document.getElementById(cfg.nameEnId);
    const tBody = document.getElementById(cfg.tableBodyId);
    const resultTable = document.getElementById(cfg.resultTableId);

    if (!addBtn || !tBody) return;

    addBtn.addEventListener('click', function () {
        const nameAr = nameArInput.value.trim();
        const nameEn = nameEnInput.value.trim();
        if (!nameAr || !nameEn) {
            alert('Please fill both Arabic and English names');
            return;
        }

        const row = tBody.insertRow();
        row.innerHTML = `
            <td>${nameAr}</td>
            <td>${nameEn}</td>
            <td>
                <button class="btn btn-sm btn-danger detail-delete-btn" type="button">
                    <i class="fas fa-trash"></i> Delete
                </button>
            </td>
        `;

        nameArInput.value = '';
        nameEnInput.value = '';
        nameArInput.focus();
        if (resultTable) resultTable.style.display = '';
    });

    tBody.addEventListener('click', function (e) {
        if (!e.target.closest('.detail-delete-btn')) return;
        const row = e.target.closest('tr');
        if (row) row.remove();
        if (resultTable && tBody.rows.length === 0) {
            resultTable.style.display = 'none';
        }
    });

    // Load existing data from JSON script block (edit mode)
    const jsonEl = document.getElementById(cfg.jsonId);
    if (jsonEl && jsonEl.textContent.trim()) {
        try {
            const items = JSON.parse(jsonEl.textContent);
            if (Array.isArray(items) && items.length > 0) {
                items.forEach(item => {
                    const row = tBody.insertRow();
                    row.innerHTML = `
                        <td>${item.nameAr || ''}</td>
                        <td>${item.nameEn || ''}</td>
                        <td>
                            <button class="btn btn-sm btn-danger detail-delete-btn" type="button">
                                <i class="fas fa-trash"></i> Delete
                            </button>
                        </td>
                    `;
                });
                if (resultTable) resultTable.style.display = '';
            }
        } catch (e) {
            console.warn('Could not parse existing detail data:', e);
        }
    }
}
