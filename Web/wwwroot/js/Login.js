// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function myLogin() {
    debugger;
    var validate = new Object();
    validate.Email = $('#Email').val();
    validate.Password = $('#Password').val();
    $.ajax({
        type: 'POST',
        url: "/validate/",
        cache: false,
        dataType: "JSON",
        data: validate
    }).then((result) => {

        if (result.status == true) {
            window.location.href = "/dashboard";
        } else {
            toastr.warning(result.msg)
        }
    })
};