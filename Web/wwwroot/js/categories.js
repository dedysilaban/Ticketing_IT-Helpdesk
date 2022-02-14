$(document).ready(function () {
    $('#tableCategories').DataTable({
        ajax: {
            url: 'https://localhost:44381/api/Categories',
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
                "data": "name"
            },
            {
                "data": "description"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    var data = data;
                    var type = type;
                    var row = row;
                    return `<button type="button" class="btn btn-outline-info" onclick="getCategory('${row.id}')" data-toggle="modal" data-target="#editModal"  data-placement="bottom" title="Edit Category"><i class="far fa-edit"></i></button> | <button type="button" class="btn btn-outline-danger" onclick="deleteCategory('${row['id']}')"><i class="fas fa-times"  data-placement="bottom" title="Delete Category"></i></button>`;
                }
            }
        ]
    });

});

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
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

function insertCategory() {
    debugger;
    var obj = new Object();
    obj.Name = $("#inputCreateName").val();
    obj.Description = $("#inputCreateDescription").val();
    console.log(obj);
/*    console.log(JSON.stringify(obj));*/
    if (obj.Name == "" || obj.Description == "") {
        Swal.fire({
            title: 'Error!',
            text: 'Failed create categories',
            icon: 'error',
            confirmButtonText: 'OK'
        });
    } else {
        $.ajax({
            url: 'https://localhost:44381/api/Categories',
            type: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            data: JSON.stringify(obj)
        }).done((result) => {
            Swal.fire({
                title: 'Success!',
                text: 'Berhasil menambahkan data',
                icon: 'success',
                confirmButtonText: 'Cool'
            });
            //$('#tableProfiles').DataTable().ajax.reload();
            console.log(result);
            $('#tableCategories').DataTable().ajax.reload();
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

function editCategory() {
    debugger
    var obj = new Object();
    obj.id = $("#Id").val();
    obj.Name = $("#Name").val();
    obj.Description = $("#Description").val();
    console.log(obj);
    console.log(JSON.stringify(obj));
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
                url: 'https://localhost:44381/api/Categories',
                type: "PUT",
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
        console.log(result);
        $('#tableCategories').DataTable().ajax.reload();
    }).fail((error) => {
        alert(error);
        Swal.fire({
            title: 'Error!',
            text: 'Gagal update data',
            icon: 'error',
            confirmButtonText: 'Ok'
        });
        console.log(error);
    });
}

function getCategory(id) {
    console.log(id);
    $.ajax({
        url: 'https://localhost:44381/api/Categories/' + id,
        dataSrc: ''
        //type: "GET",
        //headers: {
        //    'Accept': 'application/json',
        //    'Content-Type': 'application/json'
        //},
    }).done((result) => {
        console.log(result);
        $("#Id").val(result.id);
        $("#Name").val(result.name);
        $("#Description").val(result.description);
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

function deleteCategory(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: 'https://localhost:44381/api/Categories/' + id,
                type: "DELETE",
            }).done((result) => {
                alert(result);
                Swal.fire(
                    'Deleted!',
                    'Your file has been deleted.',
                    'success'
                )
            });
            $('#tableCategories').DataTable().ajax.reload();
        }
    });
}