$(document).ready(function () {
    $('#inputConvertationMessage');
    $('#tableTickets').DataTable({
        ajax: {
            url: 'https://localhost:44357/Panel/GetTickets',
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
                        var formatedDate = d.getDate() + '-' + d.getMonth() + '-' + d.getFullYear();
                        return formatedDate;
                    }
                    else
                        return data
                },
            },
            {
                "data": "technicalId"
            },
            {
                "data": "categoryName"
            },
            {
                "render": function (data, type, row) {
                    if (row['endDateTime'] == null) {
                        return `<button type="button" class="btn btn-outline-primary" onclick="viewConvertation('${row['id']}')" data-toggle="modal" data-target="#viewConvertationModal" data-placement="bottom" title="Chatting With Staff IT Support Helpdesk"><i class="fas fa-comment"></button>`;
                    } else {
                        if (row['review'] == 0) {
                            return `<button type="button" class="btn btn-outline-success" onclick="viewReviewTicket('${row['id']}')" data-toggle="modal" data-target="#viewReviewModal"  data-placement="bottom" title="Review"><i class="fas fa-star"></button>`;
                        } else {
                            return "-";
                        }
                    }
                }
            }
        ]
    });

    $('#tableHistoryTickets').DataTable({
        ajax: {
            url: 'https://localhost:44357/Panel/GetHistoryTickets',
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
                        var formatedDate = d.getDate() + '-' + d.getMonth() + '-' + d.getFullYear();
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
                        var formatedDate = d.getDate() + '-' + d.getMonth() + '-' + d.getFullYear();
                        return formatedDate;
                    }
                    else
                        return data
                },
            },
            {
                "data": "technicalId"
            },
            {
                "data": "categoryName"
            },
           /* {
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
            {
                "render": function (data, type, row) {
                    if (row['endDateTime'] == null) {
                        return `<button type="button" class="btn btn-outline-primary" onclick="viewConvertation('${row['id']}')" data-toggle="modal" data-target="#viewConvertationModal" data-placement="bottom" title="Chatting With Staff IT Support Helpdesk"><i class="fas fa-comment"></button>`;
                    } else {
                        if (row['review'] == 0) {
                            return `<button type="button" class="btn btn-outline-success" onclick="viewReviewTicket('${row['id']}')" data-toggle="modal" data-target="#viewReviewModal"  data-placement="bottom" title="Review"><i class="fas fa-star"></button>`;
                        } else {
                            return "-";
                        }
                    }
                }
            }
        ]
    });

});

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

function openCreateTicket() {
    $.ajax({
        url: 'https://localhost:44381/api/Categories/'
    }).done((result) => {
        text = "";
        $.each(result, function (key, val) {
            text += `<option value="${val.id}">${val.name}</option>`;
        });
        $("#inputCreateCategoryId").html(text);
    }).fail((error) => {
        console.log(error);
    });
}

function createTicket() {
    
    var obj = new Object();
    obj.EmployeeId =$("#inputCreateUserId").val();
    obj.Description = $("#inputCreateDescription").val();
    obj.CategoryId = parseInt($("#inputCreateCategoryId ").val());
    console.log(obj);
    //console.log(JSON.stringify(obj));
    if (obj.EmployeeId == "" || obj.Description == "" || obj.CategoryId == "") {
        Swal.fire({
            title: 'Error!',
            text: 'Failed create user',
            icon: 'error',
            confirmButtonText: 'OK'
        });
    } else {
        $.ajax({
            url: 'https://localhost:44381/api/tickets/CreateTicket',
            type: "POST",
            dataType: "json",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            data: JSON.stringify(obj)
        }).done((result) => {
            //alert(result);
            Swal.fire({
                title: 'Success!',
                text: 'Berhasil menambahkan data',
                icon: 'success',
                confirmButtonText: 'Cool'
            });
            //$('#tableProfiles').DataTsable().ajax.reload();
            //console.log(result);
            $('#tableTickets').DataTable().ajax.reload();
            $('#createModal').modal('hide');
        }).fail((error) => {
            //alert(error);
            Swal.fire({
                title: 'Error!',
                text: 'Gagal menambahkan data',
                icon: 'error',
                confirmButtonText: 'Cool'
            });
            //console.log(error);
        });
    }
}

function viewConvertation(caseId) {
    $("#inputConvertationCaseId").val(parseInt(caseId));
    viewChat(caseId);
}

function viewChat(caseId) {
    $.ajax({
        url: 'https://localhost:44381/api/Convertations/ViewConvertationsByCaseId/' + caseId
    }).done((result) => {
        text = "";
        $.each(result, function (key, val) {
            if (val.employeeId == viewBagUserId) {
                if (val.message != null) {
                    text += `
                    <div class="direct-chat-msg right">
                        <div class="direct-chat-infos clearfix">
                            <span class="direct-chat-name float-right">${val.userName}</span>
                            <span class="direct-chat-timestamp float-left">${val.dateTime}</span>
                        </div>
                        
                        <div class="direct-chat-text">
                            ${val.message}
                        </div>
                    </div>
                    `;
                }
            } else {
                if (val.message != null) {
                    text += `
                    <div class="direct-chat-msg">
                        <div class="direct-chat-infos clearfix">
                           <span class="direct-chat-name float-left">${val.userName} #${val.employeeId}</span>
                            <span class="direct-chat-timestamp float-right">${val.dateTime}</span>
                        </div>
                        <div class="direct-chat-text">
                            ${val.message}
                        </div>
                    </div>
                    `;
                }
            }
        });
        $("#chatMessages").html(text);
    }).fail((error) => {
        console.log(error);
    });
}

function createConvertation() {
    
    var obj = new Object();
    obj.EmployeeId = $("#inputConvertationUserId").val();
    obj.CaseId = parseInt($("#inputConvertationCaseId").val());
    obj.Message = $("#inputConvertationMessage").val();
    console.log(obj);
    //console.log(JSON.stringify(obj));
    if (obj.EmployeeId =="" || obj.CaseId < 0 || obj.Message == "") {
        Swal.fire({
            title: 'Error!',
            text: 'Failed create user',
            icon: 'error',
            confirmButtonText: 'OK'
        });
    } else {
        $.ajax({
            url: 'https://localhost:44381/api/Convertations/CreateConvertations',
            type: "POST",
            dataType: "json",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            data: JSON.stringify(obj)
        }).done((result) => {
            //alert(result);

            //$("#inputConvertationMessage").val('');
            //Swal.fire({
            //    title: 'Success!',
            //    text: 'Berhasil menambahkan data',
            //    icon: 'success',
            //    confirmButtonText: 'Cool'
            //});
            //$('#tableProfiles').DataTable().ajax.reload();
            //console.log(result);
            viewChat(obj.CaseId);
            $('#tableTickets').DataTable().ajax.reload();
            $("#inputConvertationMessage");
            $('#viewConvertationModal').modal('hide');
        }).fail((error) => {
            alert(error);
            Swal.fire({
                title: 'Error!',
                text: 'Gagal menambahkan data',
                icon: 'error',
                confirmButtonText: 'OK'
            });
            console.log(error);
        });
    }
}

function viewReviewTicket(caseId) {
    $("#inputReviewCaseId").val(parseInt(caseId));
}

function reviewTicket() {
    var obj = new Object();
    obj.CaseId = parseInt($("#inputReviewCaseId").val());
    obj.Review = parseInt($("#inputReview").val());
    obj.EmployeeId = viewBagUserId;
    console.log(obj);
    //console.log(JSON.stringify(obj));
    if (obj.CaseId < 0 || obj.Review < 0 || obj.EmployeeId=="") {
        Swal.fire({
            title: 'Error!',
            text: 'Failed review',
            icon: 'error',
            confirmButtonText: 'OK'
        });
    } else {
        $.ajax({
            url: 'https://localhost:44381/api/Tickets/ReviewTicket',
            type: "POST",
            dataType: "json",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            data: JSON.stringify(obj)
        }).done((result) => {
            $('#tableTickets').DataTable().ajax.reload();
            $('#viewReviewModal').modal('hide');
        }).fail((error) => {
            alert(error);
            Swal.fire({
                title: 'Error!',
                text: 'Gagal menambahkan data',
                icon: 'error',
                confirmButtonText: 'OK'
            });
            console.log(error);
        });
    }
}