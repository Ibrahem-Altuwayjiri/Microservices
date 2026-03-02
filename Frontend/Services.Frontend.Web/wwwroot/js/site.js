/* Site JavaScript for AJAX and Dynamic Content */

// API Base URL - Uses Gateway
const API_BASE_URL = '/api';

// Show Alert Function
//function showAlert(title, message, type = 'info') {
//    const alertHtml = `
//        <div class="alert alert-${type} alert-dismissible fade show" role="alert">
//            <strong>${title}:</strong> ${message}
//            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
//        </div>
//    `;
//    $('body').prepend(alertHtml);

//    // Auto-dismiss after 5 seconds
//    setTimeout(() => {
//        $('.alert:first').fadeOut(function () {
//            $(this).remove();
//        });
//    }, 5000);
//}

function showAlert(title, message, type = 'info') {

    const bgClass = {
        success: 'bg-success',
        danger: 'bg-danger',
        warning: 'bg-warning text-dark',
        info: 'bg-info'
    }[type] || 'bg-info';

    const toastId = `toast-${Date.now()}`;

    const toastHtml = `
        <div id="${toastId}" class="toast align-items-center text-white ${bgClass} border-0" role="alert">
            <div class="d-flex">
                <div class="toast-body">
                    <strong>${title}</strong><br/>
                    ${message}
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto"
                        data-bs-dismiss="toast"></button>
            </div>
        </div>
    `;

    $('#toastContainer').append(toastHtml);

    const toastElement = document.getElementById(toastId);
    const toast = new bootstrap.Toast(toastElement, {
        delay: 5000, // auto close after 5s
        autohide: true
    });

    toast.show();

    toastElement.addEventListener('hidden.bs.toast', () => {
        toastElement.remove();
    });
}



// Generic AJAX Handler for Loading Partial Views
function loadPartialView(url, containerId) {
    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            $(`#${containerId}`).html(data);
        },
        error: function (xhr, status, error) {
            console.error('Error loading partial view:', error);
            $(`#${containerId}`).html(`<div class="alert alert-danger">Error loading content</div>`);
        }
    });
}

// ✅ UNIFIED Form Submission Handler - Replaces submitFormAjax, submitOrPostFormViaAjax, saveFormDataViaAjax
// Usage:
//   - submitFormDataViaAjax(form, url, callback, { validate: true, resetForm: true })
function submitFormDataViaAjax(formElement, apiUrl, successCallback, options = {}) {
    // Default options
    const {
        validate = false,           // Enable jQuery Unobtrusive Validation
        resetForm = true,           // Reset form after success
        useEventDelegation = false, // Use $(document).on for dynamic forms
        formSelector = null,        // Required if useEventDelegation = true
        IsShowAlert = true            // Show success/error alerts
    } = options;

    // Handle event delegation mode
    if (useEventDelegation && formSelector) {
        $(document).on('submit', formSelector, function (e) {
            e.preventDefault();
            submitFormDataViaAjax(this, apiUrl, successCallback, { 
                validate, 
                resetForm, 
                useEventDelegation: false,
                IsShowAlert 
            });
        });
        return;
    }

    if (!formElement) {
        if (IsShowAlert) showAlert('Error', 'Form not found', 'danger');
        return;
    }

    const $form = $(formElement);
    console.log($form);

     // Run validation if enabled
    if (validate) {
        $.validator.unobtrusive.parse($form);
        if (!$form.valid()) {
            if (IsShowAlert) showAlert('Validation Error', 'Please fix the validation errors and try again', 'warning');
            return;
        }
        //if (!formElement.valid()) {
        //    if (showAlert) showAlert('Validation Error', 'Please fix the validation errors and try again', 'warning');
        //    return;
        //}
    }

    // Collect form data
    const formData = new FormData(formElement);
    const data = Object.fromEntries(formData);

    // Send data via AJAX
    $.ajax({
        url: apiUrl,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function(response) {
            // If response has isSuccess flag, check it
            if (response && typeof response.isSuccess !== 'undefined') {
                if (response.isSuccess) {
                    if (IsShowAlert) showAlert('Success', 'Item saved successfully!', 'success');
                    if (resetForm) formElement.reset();
                    if (successCallback) successCallback(response);
                } else {
                    if (IsShowAlert) showAlert('Error', response.message || 'An error occurred', 'danger');
                }
            } else {
                // Response doesn't have isSuccess, assume success
                if (IsShowAlert) showAlert('Success', 'Operation completed successfully', 'success');
                if (resetForm) formElement.reset();
                if (successCallback) successCallback(response);
            }
        },
        error: function(xhr) {
            // Handle validation errors from server
            if (xhr.status === 400) {
                try {
                    const response = JSON.parse(xhr.responseText);
                    
                    if (response.errors) {
                        // Display validation errors
                        let errorMessages = '';
                        for (const [field, messages] of Object.entries(response.errors)) {
                            if (Array.isArray(messages)) {
                                errorMessages += messages.join('<br/>') + '<br/>';
                            } else {
                                errorMessages += messages + '<br/>';
                            }
                        }
                        if (IsShowAlert) showAlert('Validation Error', errorMessages, 'danger');
                        
                        // Highlight invalid fields
                        for (const [field, messages] of Object.entries(response.errors)) {
                            const $field = $form.find(`[name="${field}"]`);
                            if ($field.length) {
                                $field.addClass('is-invalid');
                                $field.closest('.mb-3').find('span[class*="text-danger"]').text(
                                    Array.isArray(messages) ? messages[0] : messages
                                );
                            }
                        }
                    } else if (response.message) {
                        if (IsShowAlert) showAlert('Error', response.message, 'danger');
                    }
                } catch (e) {
                    if (IsShowAlert) showAlert('Error', 'An error occurred while processing your request', 'danger');
                }
            } else {
                let errorMessage = 'Operation failed';
                if (xhr.responseJSON && xhr.responseJSON.message) {
                    errorMessage = xhr.responseJSON.message;
                }
                if (IsShowAlert) showAlert('Error', errorMessage, 'danger');
            }
        }
    });
}



// Initialize Bootstrap Tooltips and Popovers
$(document).ready(function () {
    // Enable tooltips
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));

    // Enable popovers
    const popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    popoverTriggerList.map(popoverTriggerEl => new bootstrap.Popover(popoverTriggerEl));

    // Initialize tooltips on dynamically added content
    $(document).on('inserted.bs.tooltip', function () {
        const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
        tooltipTriggerList.map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));
    });
});

// Logout Handler
function logout() {
    if (confirm('Are you sure you want to logout?')) {
        $.post('/Auth/Logout', function () {
            window.location.href = '/';
        });
    }
}

// Delete Request Handler with Confirmation
function deleteViaAjax(url, successCallback) {
    if (confirm('Are you sure you want to delete this item?')) {
        $.ajax({
            url: url,
            type: 'DELETE',
            success: function (response) {
                showAlert('Success', 'Item deleted successfully', 'success');
                if (successCallback) successCallback(response);
            },
            error: function (xhr, status, error) {
                console.error('Error:', error);
                let errorMessage = 'Failed to delete item';
                if (xhr.responseJSON && xhr.responseJSON.message) {
                    errorMessage = xhr.responseJSON.message;
                }
                showAlert('Error', errorMessage, 'danger');
            }
        });
    }
}

// GET Request Handler for Fetching Data
function fetchDataViaAjax(url, successCallback) {
    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            if (successCallback) successCallback(response);
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
            let errorMessage = 'Failed to fetch data';
            if (xhr.responseJSON && xhr.responseJSON.message) {
                errorMessage = xhr.responseJSON.message;
            }
            showAlert('Error', errorMessage, 'danger');
        }
    });
}

// PUT/PATCH Request Handler for Updates
function updateViaAjax(url, data, successCallback) {
    $.ajax({
        url: url,
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function (response) {
            showAlert('Success', 'Item updated successfully', 'success');
            if (successCallback) successCallback(response);
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
            let errorMessage = 'Failed to update item';
            if (xhr.responseJSON && xhr.responseJSON.message) {
                errorMessage = xhr.responseJSON.message;
            }
            showAlert('Error', errorMessage, 'danger');
        }
    });
}

// ✅ LEGACY COMPATIBILITY - These are deprecated, use submitFormDataViaAjax instead
function submitOrPostFormViaAjax(formElement, apiUrl, successCallback) {
    submitFormDataViaAjax(formElement, apiUrl, successCallback, {
        validate: true,
        resetForm: false,
        IsShowAlert: true
    });
}
