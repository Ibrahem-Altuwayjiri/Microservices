/* Activities Management JavaScript */

// ? USE serviceConfig from ServiceManagement.cshtml
// The activities.js file should use the routes from serviceConfig
// This way, you don't hardcode routes and keep them in one place

// Load Activities List
function loadActivitiesList() {
    // Get the Activities config from serviceConfig (defined in ServiceManagement.cshtml)
    const config = serviceConfig['Activities'];
    
    fetchDataViaAjax(config.tableEndpoint, function(response) {
        if (response.isSuccess) {
            console.log('Activities loaded:', response);
        }
    });
}

// Get Activities Form
function getActivitiesForm(id = null) {
    // Get the Activities config
    const config = serviceConfig['Activities'];
    let url = config.formEndpoint;
    
    if (id) {
        url += `?id=${id}`;
    }
    
    fetchDataViaAjax(url, function(html) {
        document.getElementById('formContent').innerHTML = html;
    });
}

// Create Activity
function createActivity(formData) {
    // Get the Activities config
    const config = serviceConfig['Activities'];
    
    submitOrPostFormViaAjax(
        document.getElementById('itemForm'),
        config.apiCreate,  // Use the route from config
        function(response) {
            console.log('Activity created:', response);
        }
    );
}

// Update Activity
function updateActivity(formData) {
    // Get the Activities config
    const config = serviceConfig['Activities'];
    
    submitOrPostFormViaAjax(
        document.getElementById('itemForm'),
        config.apiUpdate,  // Use the route from config
        function(response) {
            console.log('Activity updated:', response);
        }
    );
}

// Delete Activity
function deleteActivity(id) {
    // Get the Activities config
    const config = serviceConfig['Activities'];
    const deleteUrl = `${config.apiDelete}/${id}`;  // Use the route from config
    
    deleteViaAjax(deleteUrl, function(response) {
        console.log('Activity deleted:', response);
    });
}
