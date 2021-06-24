// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function confirmDelete(uniqueid, isDeleteClicked) {

    var DeleteSpan = 'DeleteSpan_' + uniqueid;
    var ConfirmDeleteSpan = 'ConfirmDeleteSpan_' + uniqueid;

    if (isDeleteClicked) {

        $("#" + DeleteSpan).hide();
        $("#" + ConfirmDeleteSpan).show();
    }
    else {
        $("#" + DeleteSpan).show();
        $("#" + ConfirmDeleteSpan).hide();
    }

}