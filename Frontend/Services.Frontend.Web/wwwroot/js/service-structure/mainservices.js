/* Main Services Management JavaScript */

function loadMainServicesList() {
    const config = serviceConfig['MainServices'];
    fetchDataViaAjax(config.tableEndpoint, function(html) {
        document.getElementById('tableContent').innerHTML = html;
        attachMainServicesHandlers();
    });
}

function openMainServiceForm(id = null) {
    const config = serviceConfig['MainServices'];
    let url = config.formEndpoint;
    
    if (id) {
        url += `?id=${id}`;
    }
    
    fetchDataViaAjax(url, function(html) {
        document.getElementById('formContent').innerHTML = html;
        document.getElementById('formTitle').textContent = id ? 'Edit Main Service' : 'Add New Main Service';
        formModal.show();
    });
}

function editMainService(id) {
    openMainServiceForm(id);
}

function deleteMainService(id) {
    if (!confirm('Are you sure you want to delete this main service?')) return;
    
    const config = serviceConfig['MainServices'];
    const deleteUrl = `${config.apiDelete}/${id}`;
    
    deleteViaAjax(deleteUrl, function(response) {
        if (response.isSuccess) {
            showAlert('Success', 'Main service deleted successfully', 'success');
            loadMainServicesList();
        } else {
            showAlert('Error', response.message || 'Failed to delete main service', 'danger');
        }
    });
}

function saveMainService() {
    const id = document.getElementById('mainServiceId')?.value;
    const config = serviceConfig['MainServices'];
    const form = document.getElementById('itemForm');
    
    if (!form) {
        showAlert('Error', 'Form not found', 'danger');
        return;
    }
    
    const url = id ? config.apiUpdate : config.apiCreate;
    
    const dto = {
        id: id || undefined,
        nameAr: document.getElementById('mainServiceNameAr')?.value,
        nameEn: document.getElementById('mainServiceNameEn')?.value
    };
    
    fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(dto)
    })
    .then(r => r.json())
    .then(response => {
        if (response.isSuccess) {
            showAlert('Success', response.message || 'Main service saved successfully', 'success');
            formModal.hide();
            loadMainServicesList();
        } else {
            showAlert('Error', response.message || 'Failed to save main service', 'danger');
        }
    })
    .catch(error => {
        console.error('Error:', error);
        showAlert('Error', error.message, 'danger');
    });
}

function attachMainServicesHandlers() {
    document.querySelectorAll('[data-action="edit"]').forEach(btn => {
        btn.removeEventListener('click', mainServiceEditClick);
        btn.addEventListener('click', mainServiceEditClick);
    });

    document.querySelectorAll('[data-action="delete"]').forEach(btn => {
        btn.removeEventListener('click', mainServiceDeleteClick);
        btn.addEventListener('click', mainServiceDeleteClick);
    });
}

function mainServiceEditClick(e) {
    const id = this.dataset.id;
    openMainServiceForm(id);
}

function mainServiceDeleteClick(e) {
    const id = this.dataset.id;
    deleteMainService(id);
}
