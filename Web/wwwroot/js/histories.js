$(document).ready(function () {
    var i = 1;
    console.log("Coba");
    $('#tableHistories').DataTable({
        ajax: {
            url: 'https://localhost:44381/api/Histories/GetHistory',
            dataSrc: ''
        },
        columns: [

            {
                "data": null, "sortable": false,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "description"
            },
            {
                "data": "dateTime",
                render: function (data, type, row) {
                    if (data) {
                        var m = data.split(/[T-]/);
                        var d = new Date(parseInt(m[0]), parseInt(m[1]), parseInt(m[2]));
                        var curr_date = d.getDate();
                        var curr_month = d.getMonth()
                        var curr_year = d.getFullYear();
                        var formatedDate = d.getDate() + '-' + d.getMonth() + '-' + d.getFullYear();
                        return formatedDate;
                    }
                    else
                        return data
                },
            },
            {
                "data": "level"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return row['userName'] + ' ' + '#' + row['employeeId'];
                }
            },
            {
                "data": "caseName"
            },
            {
                "data": "statusCodeName"
            }
        ]
    });

});

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

function insertUser() {
    var obj = new Object();
    obj.Name = $("#inputCreateName").val();
    obj.Email = $("#inputCreateEmail").val();
    obj.Password = $("#inputCreatePassword").val();
    obj.BirthDate = $("#inputCreateBirthDate").val();
    obj.Phone = $("#inputCreatePhone").val();
    obj.Address = $("#inputCreateAddress").val();
    obj.Department = $("#inputCreateDepartment").val();
    obj.Company = $("#inputCreateCompany").val();
    obj.RoleId = parseInt($("#inputCreateRole").val());
    obj.Detail = "";
    console.log(obj);
    console.log(JSON.stringify(obj));
    if (obj.Name == "" || obj.Email == "" || obj.Password == "" || obj.BirthDate == "" || obj.Phone == "" || obj.Address == "" || obj.Department == "" ) {
        Swal.fire({
            title: 'Error!',
            text: 'Failed create user',
            icon: 'error',
            confirmButtonText: 'OK'
        });
    } else {
        $.ajax({
            url: 'https://localhost:44357/api/Users/Register',
            type: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            data: JSON.stringify(obj)
        }).done((result) => {
            alert(result);
            Swal.fire({
                title: 'Success!',
                text: 'Berhasil menambahkan data',
                icon: 'success',
                confirmButtonText: 'Cool'
            });
            //$('#tableProfiles').DataTable().ajax.reload();
            console.log(result);
            $('#tableUsers').DataTable().ajax.reload();
        }).fail((error) => {
            alert(error);
            Swal.fire({
                title: 'Error!',
                text: 'Gagal menambahkan data',
                icon: 'error',
                confirmButtonText: 'Cool'
            });
            console.log(error);
        });
    }
}