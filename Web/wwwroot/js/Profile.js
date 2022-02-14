(function () {
    'use strict';
    window.addEventListener('load', function () {
        // Fetch all the forms we want to apply custom Bootstrap validation styles to
        var forms = document.getElementsByClassName('needs-validation');
        // Loop over them and prevent submission
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();



function editProfile() {
    var obj = new Object();
    obj.Id = $("#Id").val();
    obj.Name = $("#Name").val();
    obj.Email = $("#Email").val();
    obj.Password = $("#Password").val();
    obj.BirthDate = $("#BirthDate").val();
    obj.Phone = $("#Phone").val();
    obj.Address = $("#Address").val();
    obj.Department = $("#Department").val();
    obj.RoleId = parseInt($("#Role").val());
    console.log(obj);
    if (obj.Name == "" || obj.Email == "" || obj.Password == "" || obj.BirthDate == "" || obj.Phone == "" || obj.Address == "" || obj.Department == "" || obj.Company == "") {
        Swal.fire({
            title: 'Error!',
            text: 'Failed Update Profile',
            icon: 'error',
            confirmButtonText: 'OK'
        });
    } else {
        Swal.fire({
            title: 'Do you want to save the changes?',
            showDenyButton: true,
            showCancelButton: true,
            confirmButtonText: `Save`,
            denyButtonText: `Don't save`,
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $.ajax({
                    url: 'https://localhost:44381/api/Employees/UpdateProfile',
                    type: "POST",
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    data: JSON.stringify(obj),
                }).done((result) => {
                    Swal.fire('Saved!', '', 'success')
                })
            } else if (result.isDenied) {
                Swal.fire('Changes are not saved', '', 'info')
            }
        }).fail((error) => {
            Swal.fire({
                title: 'Error!',
                text: 'Gagal update data',
                icon: 'error',
                confirmButtonText: 'Ok'
            });
            console.log(error);
        });
    }
}

function getProfile(id) {
    console.log(id);
    $.ajax({
        url: 'https://localhost:44381/api/Employees/' + id,
        type: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    }).done((result) => {
        console.log(result);
        $("#Id").val(result.id);
        $("#Name").val(result.name);
        $("#Email").val(result.email);
        var birthDtae = new Date(result.birthDate);
        var dd = String(birthDtae.getDate()).padStart(2, '0');
        var mm = String(birthDtae.getMonth()).padStart(2, '0');
        var yyyy = birthDtae.getFullYear();

        birthDtae = yyyy + '-' + mm + '-' + dd;
        $("#BirthDate").val(birthDtae);
        $("#Phone").val(result.phone);
        $("#Address").val(result.address);
        $("#Department").val(result.department);
        $("#Role").val(result.roleId);
    }).fail((error) => {
        Swal.fire({
            title: 'Error!',
            text: 'Gagal menampilkan data',
            icon: 'error',
            confirmButtonText: 'Ok'
        });
        console.log(error);
    });
}

function uploadAvatar() {
    var formData = new FormData();
    formData.append("UserId", parseInt($("#UserId").val()));
    formData.append("File", $("#File")[0].files[0]);
    console.log(formData);
    if (formData.UserId < 0 || formData.File == "") {
        Swal.fire({
            title: 'Error!',
            text: 'Failed Update Profile',
            icon: 'error',
            confirmButtonText: 'OK'
        });
    } else {
        Swal.fire({
            title: 'Do you want to save the changes?',
            showDenyButton: true,
            showCancelButton: true,
            confirmButtonText: `Save`,
            denyButtonText: `Don't save`,
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            console.log(result);
            if (result.isConfirmed) {
                $.ajax({
                    url: 'https://localhost:44381/api/Users/UploadAvatar',
                    type: "POST",
                    enctype: 'multipart/form-data',
                    data: formData,
                    cache: false,
                    contentType: false,
                    processData: false
                }).done((result) => {
                    Swal.fire('Saved!', '', 'success')
                })
            } else if (result.isDenied) {
                Swal.fire('Changes are not saved', '', 'info')
            }
        }).fail((error) => {
            console.log(error);
            Swal.fire({
                title: 'Error!',
                text: 'Gagal update data',
                icon: 'error',
                confirmButtonText: 'Ok'
            });
            console.log(error);
        });
    }
}