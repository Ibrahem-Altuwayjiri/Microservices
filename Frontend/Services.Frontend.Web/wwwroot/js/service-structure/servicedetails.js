/* Service Details Management JavaScript */

function loadServiceDetailsList() {
    const config = serviceConfig['ServiceDetails'];
    fetchDataViaAjax(config.tableEndpoint, function(html) {
        document.getElementById('tableContent').innerHTML = html;
        attachServiceDetailsHandlers();
    });
}

function openServiceDetailsForm(id = null) {
    const config = serviceConfig['ServiceDetails'];
    let url = config.formEndpoint;
    
    if (id) {
        url += `?id=${id}`;
    }
    
    fetchDataViaAjax(url, function(html) {
        document.getElementById('formContent').innerHTML = html;
        document.getElementById('formTitle').textContent = id ? 'Edit Service Details' : 'Add New Service Details';
        formModal.show();
    });
}

function editServiceDetails(id) {
    openServiceDetailsForm(id);
}

function deleteServiceDetails(id) {
    if (!confirm('Are you sure you want to delete this service details?')) return;
    
    const config = serviceConfig['ServiceDetails'];
    const deleteUrl = `${config.apiDelete}/${id}`;
    
    deleteViaAjax(deleteUrl, function(response) {
        if (response.isSuccess) {
            showAlert('Success', 'Service details deleted successfully', 'success');
            loadServiceDetailsList();
        } else {
            showAlert('Error', response.message || 'Failed to delete service details', 'danger');
        }
    });
}

function saveServiceDetails() {
    const id = document.getElementById('serviceDetailsId')?.value;
    const config = serviceConfig['ServiceDetails'];
    const form = document.getElementById('itemForm');
    
    if (!form) {
        showAlert('Error', 'Form not found', 'danger');
        return;
    }
    
    const url = id ? config.apiUpdate : config.apiCreate;
    
    const dto = {
        id: id || undefined,
        serviceId: document.getElementById('serviceDetailsServiceId')?.value,
        content: document.getElementById('serviceDetailsContent')?.value
    };
    
    fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(dto)
    })
    .then(r => r.json())
    .then(response => {
        if (response.isSuccess) {
            showAlert('Success', response.message || 'Service details saved successfully', 'success');
            formModal.hide();
            loadServiceDetailsList();
        } else {
            showAlert('Error', response.message || 'Failed to save service details', 'danger');
        }
    })
    .catch(error => {
        console.error('Error:', error);
        showAlert('Error', error.message, 'danger');
    });
}

function attachServiceDetailsHandlers() {
    document.querySelectorAll('[data-action="edit"]').forEach(btn => {
        btn.removeEventListener('click', serviceDetailsEditClick);
        btn.addEventListener('click', serviceDetailsEditClick);
    });

    document.querySelectorAll('[data-action="delete"]').forEach(btn => {
        btn.removeEventListener('click', serviceDetailsDeleteClick);
        btn.addEventListener('click', serviceDetailsDeleteClick);
    });
}

function serviceDetailsEditClick(e) {
    const id = this.dataset.id;
    openServiceDetailsForm(id);
}

function serviceDetailsDeleteClick(e) {
    const id = this.dataset.id;
    deleteServiceDetails(id);
}
