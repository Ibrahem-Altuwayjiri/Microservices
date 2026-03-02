/* Sub-Sub Services Management JavaScript */

function loadSubSubServicesList() {
    const config = serviceConfig['SubSubServices'];
    fetchDataViaAjax(config.tableEndpoint, function(html) {
        document.getElementById('tableContent').innerHTML = html;
        attachSubSubServicesHandlers();
    });
}

function openSubSubServiceForm(id = null) {
    const config = serviceConfig['SubSubServices'];
    let url = config.formEndpoint;
    
    if (id) {
        url += `?id=${id}`;
    }
    
    fetchDataViaAjax(url, function(html) {
        document.getElementById('formContent').innerHTML = html;
        document.getElementById('formTitle').textContent = id ? 'Edit Sub-Sub Service' : 'Add New Sub-Sub Service';
        formModal.show();
    });
}

function editSubSubService(id) {
    openSubSubServiceForm(id);
}

function deleteSubSubService(id) {
    if (!confirm('Are you sure you want to delete this sub-sub service?')) return;
    
    const config = serviceConfig['SubSubServices'];
    const deleteUrl = `${config.apiDelete}/${id}`;
    
    deleteViaAjax(deleteUrl, function(response) {
        if (response.isSuccess) {
            showAlert('Success', 'Sub-sub service deleted successfully', 'success');
            loadSubSubServicesList();
        } else {
            showAlert('Error', response.message || 'Failed to delete sub-sub service', 'danger');
        }
    });
}

function saveSubSubService() {
    const id = document.getElementById('subSubServiceId')?.value;
    const config = serviceConfig['SubSubServices'];
    const form = document.getElementById('itemForm');
    
    if (!form) {
        showAlert('Error', 'Form not found', 'danger');
        return;
    }
    
    const url = id ? config.apiUpdate : config.apiCreate;
    
    const dto = {
        id: id || undefined,
        subServiceId: document.getElementById('subSubServiceParentServiceId')?.value,
        nameAr: document.getElementById('subSubServiceNameAr')?.value,
        nameEn: document.getElementById('subSubServiceNameEn')?.value
    };
    
    fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(dto)
    })
    .then(r => r.json())
    .then(response => {
        if (response.isSuccess) {
            showAlert('Success', response.message || 'Sub-sub service saved successfully', 'success');
            formModal.hide();
            loadSubSubServicesList();
        } else {
            showAlert('Error', response.message || 'Failed to save sub-sub service', 'danger');
        }
    })
    .catch(error => {
        console.error('Error:', error);
        showAlert('Error', error.message, 'danger');
    });
}

function attachSubSubServicesHandlers() {
    document.querySelectorAll('[data-action="edit"]').forEach(btn => {
        btn.removeEventListener('click', subSubServiceEditClick);
        btn.addEventListener('click', subSubServiceEditClick);
    });

    document.querySelectorAll('[data-action="delete"]').forEach(btn => {
        btn.removeEventListener('click', subSubServiceDeleteClick);
        btn.addEventListener('click', subSubServiceDeleteClick);
    });
}

function subSubServiceEditClick(e) {
    const id = this.dataset.id;
    openSubSubServiceForm(id);
}

function subSubServiceDeleteClick(e) {
    const id = this.dataset.id;
    deleteSubSubService(id);
}
