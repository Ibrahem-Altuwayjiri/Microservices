/* Sub Services Management JavaScript */

function loadSubServicesList() {
    const config = serviceConfig['SubServices'];
    fetchDataViaAjax(config.tableEndpoint, function(html) {
        document.getElementById('tableContent').innerHTML = html;
        attachSubServicesHandlers();
    });
}

function openSubServiceForm(id = null) {
    const config = serviceConfig['SubServices'];
    let url = config.formEndpoint;
    
    if (id) {
        url += `?id=${id}`;
    }
    
    fetchDataViaAjax(url, function(html) {
        document.getElementById('formContent').innerHTML = html;
        document.getElementById('formTitle').textContent = id ? 'Edit Sub Service' : 'Add New Sub Service';
        formModal.show();
    });
}

function editSubService(id) {
    openSubServiceForm(id);
}

function deleteSubService(id) {
    if (!confirm('Are you sure you want to delete this sub service?')) return;
    
    const config = serviceConfig['SubServices'];
    const deleteUrl = `${config.apiDelete}/${id}`;
    
    deleteViaAjax(deleteUrl, function(response) {
        if (response.isSuccess) {
            showAlert('Success', 'Sub service deleted successfully', 'success');
            loadSubServicesList();
        } else {
            showAlert('Error', response.message || 'Failed to delete sub service', 'danger');
        }
    });
}

function saveSubService() {
    const id = document.getElementById('subServiceId')?.value;
    const config = serviceConfig['SubServices'];
    const form = document.getElementById('itemForm');
    
    if (!form) {
        showAlert('Error', 'Form not found', 'danger');
        return;
    }
    
    const url = id ? config.apiUpdate : config.apiCreate;
    
    const dto = {
        id: id || undefined,
        mainServiceId: document.getElementById('subServiceMainServiceId')?.value,
        nameAr: document.getElementById('subServiceNameAr')?.value,
        nameEn: document.getElementById('subServiceNameEn')?.value
    };
    
    fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(dto)
    })
    .then(r => r.json())
    .then(response => {
        if (response.isSuccess) {
            showAlert('Success', response.message || 'Sub service saved successfully', 'success');
            formModal.hide();
            loadSubServicesList();
        } else {
            showAlert('Error', response.message || 'Failed to save sub service', 'danger');
        }
    })
    .catch(error => {
        console.error('Error:', error);
        showAlert('Error', error.message, 'danger');
    });
}

function attachSubServicesHandlers() {
    document.querySelectorAll('[data-action="edit"]').forEach(btn => {
        btn.removeEventListener('click', subServiceEditClick);
        btn.addEventListener('click', subServiceEditClick);
    });

    document.querySelectorAll('[data-action="delete"]').forEach(btn => {
        btn.removeEventListener('click', subServiceDeleteClick);
        btn.addEventListener('click', subServiceDeleteClick);
    });
}

function subServiceEditClick(e) {
    const id = this.dataset.id;
    openSubServiceForm(id);
}

function subServiceDeleteClick(e) {
    const id = this.dataset.id;
    deleteSubService(id);
}
