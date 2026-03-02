/* Headers Management JavaScript */

// ? USE serviceConfig from ServiceManagement.cshtml
// The headers.js file should use the routes from serviceConfig

// Load Headers List
function loadHeadersList() {
    // Get the Headers config from serviceConfig
    const config = serviceConfig['Headers'];
    
    fetchDataViaAjax(config.tableEndpoint, function(response) {
        if (response.isSuccess) {
            console.log('Headers loaded:', response);
        }
    });
}

// Get Headers Form
function getHeadersForm(id = null) {
    // Get the Headers config
    const config = serviceConfig['Headers'];
    let url = config.formEndpoint;
    
    if (id) {
        url += `?id=${id}`;
    }
    
    fetchDataViaAjax(url, function(html) {
        document.getElementById('formContent').innerHTML = html;
    });
}

// Create Header
function createHeader(formData) {
    // Get the Headers config
    const config = serviceConfig['Headers'];
    
    submitOrPostFormViaAjax(
        document.getElementById('itemForm'),
        config.apiCreate,  // Use the route from config
        function(response) {
            console.log('Header created:', response);
        }
    );
}

// Update Header
function updateHeader(formData) {
    // Get the Headers config
    const config = serviceConfig['Headers'];
    
    submitOrPostFormViaAjax(
        document.getElementById('itemForm'),
        config.apiUpdate,  // Use the route from config
        function(response) {
            console.log('Header updated:', response);
        }
    );
}

// Delete Header
function deleteHeader(id) {
    // Get the Headers config
    const config = serviceConfig['Headers'];
    const deleteUrl = `${config.apiDelete}/${id}`;  // Use the route from config
    
    deleteViaAjax(deleteUrl, function(response) {
        console.log('Header deleted:', response);
    });
}
