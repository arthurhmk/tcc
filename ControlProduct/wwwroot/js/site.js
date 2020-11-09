// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    var config = {
        order: []
    }
    if (typeof(DataTableConfig) != "undefined") {
        for (var col in DataTableConfig) {
            config[col] = DataTableConfig[col];
        }
    }

    $('[class^="datatable"]').each(function () {
        $(this).DataTable(config);
    });
});