/* Tags Management JavaScript */

// ? USE serviceConfig from ServiceManagement.cshtml
// The tags.js file should use the routes from serviceConfig

// Load Tags List
function loadTagsList() {
    // Get the Tags config from serviceConfig
    const config = serviceConfig['Tags'];
    
    fetchDataViaAjax(config.tableEndpoint, function(response) {
        if (response.isSuccess) {
            console.log('Tags loaded:', response);
        }
    });
}

// Get Tags Form
function getTagsForm(id = null) {
    // Get the Tags config
    const config = serviceConfig['Tags'];
    let url = config.formEndpoint;
    
    if (id) {
        url += `?id=${id}`;
    }
    
    fetchDataViaAjax(url, function(html) {
        document.getElementById('formContent').innerHTML = html;
    });
}

// Create Tag
function createTag(formData) {
    // Get the Tags config
    const config = serviceConfig['Tags'];
    
    submitOrPostFormViaAjax(
        document.getElementById('itemForm'),
        config.apiCreate,  // Use the route from config
        function(response) {
            console.log('Tag created:', response);
        }
    );
}

// Update Tag
function updateTag(formData) {
    // Get the Tags config
    const config = serviceConfig['Tags'];
    
    submitOrPostFormViaAjax(
        document.getElementById('itemForm'),
        config.apiUpdate,  // Use the route from config
        function(response) {
            console.log('Tag updated:', response);
        }
    );
}

// Delete Tag
function deleteTag(id) {
    // Get the Tags config
    const config = serviceConfig['Tags'];
    const deleteUrl = `${config.apiDelete}/${id}`;  // Use the route from config
    
    deleteViaAjax(deleteUrl, function(response) {
        console.log('Tag deleted:', response);
    });
}
