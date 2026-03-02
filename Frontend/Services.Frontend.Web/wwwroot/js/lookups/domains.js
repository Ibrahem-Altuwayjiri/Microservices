/* Domains Management JavaScript */

// ? USE serviceConfig from ServiceManagement.cshtml
// The domains.js file should use the routes from serviceConfig

// Load Domains List
function loadDomainsList() {
    // Get the Domains config from serviceConfig
    const config = serviceConfig['Domains'];
    
    fetchDataViaAjax(config.tableEndpoint, function(response) {
        if (response.isSuccess) {
            console.log('Domains loaded:', response);
        }
    });
}

// Get Domains Form
function getDomainsForm(id = null) {
    // Get the Domains config
    const config = serviceConfig['Domains'];
    let url = config.formEndpoint;
    
    if (id) {
        url += `?id=${id}`;
    }
    
    fetchDataViaAjax(url, function(html) {
        document.getElementById('formContent').innerHTML = html;
    });
}

// Create Domain
function createDomain(formData) {
    // Get the Domains config
    const config = serviceConfig['Domains'];
    
    submitOrPostFormViaAjax(
        document.getElementById('itemForm'),
        config.apiCreate,  // Use the route from config
        function(response) {
            console.log('Domain created:', response);
        }
    );
}

// Update Domain
function updateDomain(formData) {
    // Get the Domains config
    const config = serviceConfig['Domains'];
    
    submitOrPostFormViaAjax(
        document.getElementById('itemForm'),
        config.apiUpdate,  // Use the route from config
        function(response) {
            console.log('Domain updated:', response);
        }
    );
}

// Delete Domain
function deleteDomain(id) {
    // Get the Domains config
    const config = serviceConfig['Domains'];
    const deleteUrl = `${config.apiDelete}/${id}`;  // Use the route from config
    
    deleteViaAjax(deleteUrl, function(response) {
        console.log('Domain deleted:', response);
    });
}
