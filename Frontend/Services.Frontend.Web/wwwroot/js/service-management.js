/* Service Management Page - Main JavaScript */

let currentService = '';
let currentItemId = null;
let isEditMode = false;
let formModal, viewAllModal, deleteModal, viewDetailsModal;

// Initialize modals when document is ready
document.addEventListener('DOMContentLoaded', function() {
    formModal = new bootstrap.Modal(document.getElementById('formModal'));
    viewAllModal = new bootstrap.Modal(document.getElementById('viewAllModal'));
    deleteModal = new bootstrap.Modal(document.getElementById('deleteModal'));
    viewDetailsModal = new bootstrap.Modal(document.getElementById('viewDetailsModal'));

    // Attach event listeners
    attachEventListeners();
});

// Attach all event listeners
function attachEventListeners() {
    // Save button
    const saveBtn = document.getElementById('saveBtn');
    if (saveBtn) {
        saveBtn.addEventListener('click', handleSave);
    }

    // Delete confirmation button
    const confirmDeleteBtn = document.getElementById('confirmDeleteBtn');
    if (confirmDeleteBtn) {
        confirmDeleteBtn.addEventListener('click', handleDeleteConfirmation);
    }

    // Filter select
    const filterSelect = document.getElementById('filterSelect');
    if (filterSelect) {
        filterSelect.addEventListener('change', handleFilter);
    }

    // Sort button
    const sortBtn = document.getElementById('sortBtn');
    if (sortBtn) {
        sortBtn.addEventListener('click', handleSort);
    }
}

// ? Service configuration with form and table endpoints
const serviceConfig = {
    'MainServices': {
        formEndpoint: '/MainServices/Form',
        tableEndpoint: '/MainServices/List',
        apiCreate: '/MainServices/Create',
        apiUpdate: '/MainServices/Update',
        apiDelete: '/MainServices/Delete'
    },
    'SubServices': {
        formEndpoint: '/SubServices/Form',
        tableEndpoint: '/SubServices/List',
        apiCreate: '/SubServices/Create',
        apiUpdate: '/SubServices/Update',
        apiDelete: '/SubServices/Delete'
    },
    'SubSubServices': {
        formEndpoint: '/SubSubServices/Form',
        tableEndpoint: '/SubSubServices/List',
        apiCreate: '/SubSubServices/Create',
        apiUpdate: '/SubSubServices/Update',
        apiDelete: '/SubSubServices/Delete'
    },
    'ServiceDetails': {
        formEndpoint: '/ServiceDetails/Form',
        tableEndpoint: '/ServiceDetails/List',
        viewEndpoint: '/ServiceDetails/ViewDetails',
        apiCreate: '/ServiceDetails/Create',
        apiUpdate: '/ServiceDetails/Update',
        apiDelete: '/ServiceDetails/Delete'
    },
    'Activities': {
        formEndpoint: '/Activities/Form',
        tableEndpoint: '/Activities/List',
        apiCreate: '/Activities/Create',
        apiUpdate: '/Activities/Update',
        apiDelete: '/Activities/Delete'
    },
    'Domains': {
        formEndpoint: '/Domains/Form',
        tableEndpoint: '/Domains/List',
        apiCreate: '/Domains/Create',
        apiUpdate: '/Domains/Update',
        apiDelete: '/Domains/Delete'
    },
    'Tags': {
        formEndpoint: '/Tags/Form',
        tableEndpoint: '/Tags/List',
        apiCreate: '/Tags/Create',
        apiUpdate: '/Tags/Update',
        apiDelete: '/Tags/Delete'
    },
    'Headers': {
        formEndpoint: '/Headers/Form',
        tableEndpoint: '/Headers/List',
        apiCreate: '/Headers/Create',
        apiUpdate: '/Headers/Update',
        apiDelete: '/Headers/Delete'
    }
};

// ? Open Form Modal - Loads partial view from controller
function openForm(service, itemId = null) {
    currentService = service;
    currentItemId = itemId;
    isEditMode = itemId !== null && itemId !== undefined;

    document.getElementById('formTitle').textContent = isEditMode ? `Edit ${service}` : `Add New ${service}`;
    document.getElementById('formContent').innerHTML = `
        <div class="text-center">
            <div class="spinner-border" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
        </div>
    `;

    const config = serviceConfig[service];
    let formUrl = config.formEndpoint;
    
    // If editing, pass the ID to fetch the item
    if (isEditMode) {
        formUrl += `?id=${itemId}`;
    }

    // Use fetchDataViaAjax to load the form partial view
    fetchDataViaAjax(formUrl, function(html) {
        document.getElementById('formContent').innerHTML = html;

        // Initialize service-specific form logic after HTML is injected
        if (service === 'ServiceDetails') {
            if (typeof initServiceDetailsForm === 'function') initServiceDetailsForm();
        }
    });

    formModal.show();
}

// ? View All - Loads table partial view from controller
function viewAll(service) {
    currentService = service;
    const config = serviceConfig[service];
    document.getElementById('tableTitle').textContent = `All ${service}`;
    
    document.getElementById('tableContent').innerHTML = `
        <div class="text-center">
            <div class="spinner-border" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
        </div>
    `;

    // Use fetchDataViaAjax to load the table partial view
    fetchDataViaAjax(config.tableEndpoint, function(html) {
        document.getElementById('tableContent').innerHTML = html;
        
        // Attach event handlers to dynamically loaded buttons
        attachTableEventHandlers(service);
    });

    viewAllModal.show();
}

// ? Attach event handlers to Edit/Delete buttons in the table
function attachTableEventHandlers(service) {
    // View buttons (ServiceDetails only)
    document.querySelectorAll('[data-action="view"]').forEach(btn => {
        btn.removeEventListener('click', handleViewClick);
        btn.addEventListener('click', handleViewClick);
    });

    // Edit buttons
    document.querySelectorAll('[data-action="edit"]').forEach(btn => {
        btn.removeEventListener('click', handleEditClick);
        btn.addEventListener('click', handleEditClick);
    });

    // Delete buttons
    document.querySelectorAll('[data-action="delete"]').forEach(btn => {
        btn.removeEventListener('click', handleDeleteClick);
        btn.addEventListener('click', handleDeleteClick);
    });
}

function handleViewClick(e) {
    const itemId = this.dataset.id;
    viewServiceDetails(itemId);
}

function viewServiceDetails(id) {
    const config = serviceConfig['ServiceDetails'];
    const url = `${config.viewEndpoint}?id=${id}`;

    document.getElementById('viewDetailsContent').innerHTML = `
        <div class="text-center py-4">
            <div class="spinner-border" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
        </div>`;

    viewDetailsModal.show();

    fetchDataViaAjax(url, function (html) {
        document.getElementById('viewDetailsContent').innerHTML = html;
    });
}

function handleEditClick(e) {
    const itemId = this.dataset.id;
    viewAllModal.hide();
    openForm(currentService, itemId);
}

function handleDeleteClick(e) {
    const itemId = this.dataset.id;
    currentItemId = itemId;
    deleteModal.show();
}

// ? Delete confirmation - Uses deleteViaAjax from site.js
function handleDeleteConfirmation() {
    const config = serviceConfig[currentService];
    const deleteUrl = `${config.apiDelete}/${currentItemId}`;
    
    deleteViaAjax(deleteUrl, function(response) {
        deleteModal.hide();
        // Reload the current view
        viewAll(currentService);
    });
}

// ? Save form data
function handleSave(e) {
    e.preventDefault();

    const form = document.getElementById('itemForm');
    if (!form) {
        showAlert('Error', 'Form not found', 'danger');
        return;
    }

    const config = serviceConfig[currentService];
    const apiUrl = isEditMode ? config.apiUpdate : config.apiCreate;

    // ServiceDetails needs custom JSON build: FormData.getAll() for arrays + headers from table
    if (currentService === 'ServiceDetails') {
        const dto = buildServiceDetailsDto(form);
        $.ajax({
            url: apiUrl,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dto),
            success: function (response) {
                if (response && response.isSuccess) {
                    showAlert('Success', response.message || 'Saved successfully', 'success');
                    formModal.hide();
                    viewAll('ServiceDetails');
                } else {
                    showAlert('Error', response?.message || 'Failed to save', 'danger');
                }
            },
            error: function (xhr) {
                const msg = xhr.responseJSON?.message || 'An error occurred';
                showAlert('Error', msg, 'danger');
            }
        });
        return;
    }

    submitOrPostFormViaAjax(form, apiUrl, function(response) {
        formModal.hide();
        viewAll(currentService);
    });
}

// Build the ServiceDetails DTO correctly:
//  - scalar fields via fd.get()
//  - checkbox arrays via fd.getAll()  (Object.fromEntries loses duplicates)
//  - headers from the JS table rows
function buildServiceDetailsDto(form) {
    const fd = new FormData(form);
    return {
        Id:             fd.get('Id') ?? null,
        NameAr:         fd.get('NameAr')         || '',
        NameEn:         fd.get('NameEn')         || '',
        DescriptionAr:  fd.get('DescriptionAr')  || '',
        DescriptionEn:  fd.get('DescriptionEn')  || '',
        MainServiceId:  fd.get('MainServiceId')  || null,
        SubServiceId:   fd.get('SubServiceId')   || null,
        SubSubServiceId:fd.get('SubSubServiceId') || null,
        Activities:     fd.getAll('Activities').map(Number).filter(n => n > 0),
        Domains:        fd.getAll('Domains').map(Number).filter(n => n > 0),
        Tags:           fd.getAll('Tags').map(Number).filter(n => n > 0),
        HeadersValue:   buildHeadersFromTable()
    };
}

// Read the added-headers table and produce List<HeaderValueDto>
function buildHeadersFromTable() {
    const tableBody = document.getElementById('dataTableBody');
    if (!tableBody) return [];
    return Array.from(tableBody.querySelectorAll('tr'))
        .filter(row => row.cells.length >= 3)
        .map(row => ({
            HeaderId: parseInt(row.cells[0].getAttribute('data-value')) || 0,
            NameAr:   row.cells[1].innerText.trim(),
            NameEn:   row.cells[2].innerText.trim()
        }));
}

// ? Filter functionality
function handleFilter(e) {
    const filter = e.target.value;
    const cards = document.querySelectorAll('.service-management-card');
    
    cards.forEach(card => {
        const cardElement = card.closest('.col-md-6');
        
        if (!filter) {
            cardElement.style.display = '';
        } else if (filter === 'structure') {
            const service = card.dataset.service;
            cardElement.style.display = 
                (service === 'MainServices' || service === 'SubServices') ? '' : 'none';
        } else if (filter === 'lookups') {
            const service = card.dataset.service;
            cardElement.style.display = 
                (service === 'Activities' || service === 'Domains' || service === 'Tags' || service === 'Headers') 
                    ? '' : 'none';
        }
    });
}

// ? Sort functionality
function handleSort() {
    showAlert('Info', 'Sort functionality coming soon!', 'info');
}
