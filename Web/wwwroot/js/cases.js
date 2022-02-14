$(document).ready(function () {
    var i = 1;
    console.log("Coba");
    $('#tableCases').DataTable({
        ajax: {
            url: 'https://localhost:44381/api/Tickets/Getcase',
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
                "data": "startDateTime",
                render: function (data, type, row) {
                    if (data) {
                        var m = data.split(/[T-]/);
                        var d = new Date(parseInt(m[0]), parseInt(m[1]), parseInt(m[2]));
                        var curr_date = d.getDate();
                        var curr_month = d.getMonth();
                        var curr_year = d.getFullYear();
                        var formatedDate = d.getDate() + '.' + d.getMonth() + '.' + d.getFullYear();
                        return formatedDate;
                    }
                    else
                        return data
                },
            },
            {
                "data": "endDateTime",
                render: function (data, type, row) {
                    if (data) {
                        var m = data.split(/[T-]/);
                        var d = new Date(parseInt(m[0]), parseInt(m[1]), parseInt(m[2]));
                        var curr_date = d.getDate();
                        var curr_month = d.getMonth();
                        var curr_year = d.getFullYear();
                        var formatedDate = d.getDate() + '.' + d.getMonth() + '.' + d.getFullYear();
                        return formatedDate;
                    }
                    else
                        return data
                },
            },
            /*{
                "data": "review",
                render: function (data, type, row) {
                    if (row['review'] == 0) {
                        return '-';
                    }
                    else if (row['review'] == 1) {
                        return '&#11088';
                    }
                    else if (row['review'] == 2) {
                        return '&#11088' + '&#11088';
                    }
                    else if (row['review'] == 3) {
                        return '&#11088' + '&#11088' + '&#11088';
                    }
                    else if (row['review'] == 4) {
                        return '&#11088' + '&#11088' + '&#11088' + '&#11088';
                    }
                    else if (row['review'] == 5) {
                        return '&#11088' + '&#11088' + '&#11088' + '&#11088' + '&#11088';
                    }
                }
            },*/
           /* {
                "data": "level"
            },*/
            {
                "data": null,
                "render": function (data, type, row) {
                    return row['userName'] + ' ' + '#' + row['employeeId'];
                }
            },
            {
                "data": "categoryName"
            },
            {
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-danger" onclick="deleteCase('${row['id']}')"><i class="fas fa-times"  data-placement="bottom" title="Delete Case"></i></button>`;
                }
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

//function insertCase() {
//    var obj = new Object();
//    obj.Description = $("#inputDescription").val();
//    obj.StartDateTime = $("#inputStartDate").val();
//    obj.EndDateTime = $("#inputEndDate").val();
//    obj.Review = parseInt($("#inputReview").val());
//    obj.Level = parseInt($("#inputLevel").val());
//    obj.UserId = parseInt($("#inputUserId").val());
//    obj.PriorityId = parseInt($("#inputPriorityId").val());
//    obj.CategoryId = parseInt($("#inputCategoryId").val());
//    console.log(obj);
//    console.log(JSON.stringify(obj));
//    if (obj.Description == "" || obj.StartDateTime == "" || obj.EndDateTime == "" || obj.Review == "" || obj.Level == "" || obj.UserId == "" || obj.PriorityId == "" || obj.CategoryId == "") {
//        Swal.fire({
//            title: 'Error!',
//            text: 'Failed create case',
//            icon: 'error',
//            confirmButtonText: 'OK'
//        });
//    } else {
//        $.ajax({
//            url: 'https://localhost:44357/api/cases',
//            type: "POST",
//            headers: {
//                'Accept': 'application/json',
//                'Content-Type': 'application/json'
//            },
//            data: JSON.stringify(obj)
//        }).done((result) => {
//            alert(result);
//            Swal.fire({
//                title: 'Success!',
//                text: 'Berhasil menambahkan data',
//                icon: 'success',
//                confirmButtonText: 'Cool'
//            });
//            //$('#tableProfiles').DataTable().ajax.reload();
//            console.log(result);
//            $('#tableCases').DataTable().ajax.reload();
//        }).fail((error) => {
//            alert(error);
//            Swal.fire({
//                title: 'Error!',
//                text: 'Gagal menambahkan data',
//                icon: 'error',
//                confirmButtonText: 'Cool'
//            });
//            console.log(error);
//        });
//    }
//}

//function editCase() {
//    var obj = new Object();
//    obj.Description = $("#inputDescription").val();
//    obj.StartDateTime = $("#inputStartDate").val();
//    obj.EndDateTime = $("#inputEndDate").val();
//    obj.Review = parseInt($("#inputReview").val());
//    obj.Level = parseInt($("#inputLevel").val());
//    obj.UserId = parseInt($("#inputUserId").val());
//    obj.PriorityId = parseInt($("#inputPriorityId").val());
//    obj.CategoryId = parseInt($("#inputCategoryId").val());
//    console.log(obj);
//    console.log(JSON.stringify(obj));
//    if (obj.Description == "" || obj.StartDateTime == "" || obj.EndDateTime == "" || obj.Review == "" || obj.Level == "" || obj.UserId == "" || obj.PriorityId == "" || obj.CategoryId == "") {
//        Swal.fire({
//            title: 'Error!',
//            text: 'Failed create user',
//            icon: 'error',
//            confirmButtonText: 'OK'
//        });
//    } else {
//        $.ajax({
//            url: 'https://localhost:44357/api/cases',
//            type: "POST",
//            headers: {
//                'Accept': 'application/json',
//                'Content-Type': 'application/json'
//            },
//            data: JSON.stringify(obj)
//        }).done((result) => {
//            alert(result);
//            Swal.fire({
//                title: 'Success!',
//                text: 'Berhasil mengubah data',
//                icon: 'success',
//                confirmButtonText: 'Cool'
//            });
//            //$('#tableProfiles').DataTable().ajax.reload();
//            console.log(result);
//            $('#tableCases').DataTable().ajax.reload();
//        }).fail((error) => {
//            alert(error);
//            Swal.fire({
//                title: 'Error!',
//                text: 'Gagal mengubah data',
//                icon: 'error',
//                confirmButtonText: 'Cool'
//            });
//            console.log(error);
//        });
//    }
//}

//function getCase(id) {
//    console.log(id);
//    $.ajax({
//        url: 'https://localhost:44381/api/Case/' + id,
//        type: "GET",
//        headers: {
//            'Accept': 'application/json',
//            'Content-Type': 'application/json'
//        },
//    }).done((result) => {
//        console.log(result);
//        $("#inputDescription").val();
//        $("#inputStartDate").val();
//        $("#inputEndDate").val();
//        $("#inputReview").val();
//        $("#inputLevel").val();
//        $("#inputUserId").val();
//        $("#inputPriorityId").val();
//        $("#inputCategoryId").val();
//    }).fail((error) => {
//        alert(error);
//        Swal.fire({
//            title: 'Error!',
//            text: 'Gagal menampilkan data',
//            icon: 'error',
//            confirmButtonText: 'Ok'
//        });
//        console.log(error);
//    });
//}

function deleteCase(id) {
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
                url: 'https://localhost:44381/api/Tickets/' + id,
                type: "DELETE",
            }).done((result) => {
                alert(result);
                Swal.fire(
                    'Deleted!',
                    'Your file has been deleted.',
                    'success'
                )
            });
            $('#tableCases').DataTable().ajax.reload();
        }
    });
}
