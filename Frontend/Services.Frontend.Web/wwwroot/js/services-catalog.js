/* Services Catalog - Filter & Pagination Helpers */

(function () {
    'use strict';

    document.addEventListener('DOMContentLoaded', function () {
        var form = document.getElementById('catalogFilterForm');
        if (!form) return;

        // Auto-submit on domain dropdown change
        var domainSelect = document.getElementById('domainId');
        if (domainSelect) {
            domainSelect.addEventListener('change', function () {
                form.submit();
            });
        }

        // Preserve multi-select values in pagination links
        // Tag helper asp-route-* only handles single values, so we append array params
        var paginationLinks = document.querySelectorAll('.pagination .page-link');
        var activityCheckboxes = document.querySelectorAll('input[name="activityIds"]:checked');
        var tagCheckboxes = document.querySelectorAll('input[name="tagIds"]:checked');

        if (activityCheckboxes.length > 0 || tagCheckboxes.length > 0) {
            paginationLinks.forEach(function (link) {
                if (!link.href || link.closest('.disabled')) return;

                var url = new URL(link.href, window.location.origin);

                activityCheckboxes.forEach(function (chk) {
                    url.searchParams.append('activityIds', chk.value);
                });

                tagCheckboxes.forEach(function (chk) {
                    url.searchParams.append('tagIds', chk.value);
                });

                link.href = url.toString();
            });
        }
    });
})();
