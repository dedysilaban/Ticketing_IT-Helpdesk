$(document).ready(function () {
    $('#inputConvertationMessage');
    $('#tableViewHandleTickets').DataTable({
        ajax: {
            url: 'https://localhost:44357/Panel/GetHandleTickets',
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
/*            {
                "data": "level"
            },*/
            {
                "data": null,
                "render": function (data, type, row) {
                    return row['userName'] + ' ' + '#' + row['employeeId'];
                }
            },
/*            {
                "render": function (data, type, row) {
                    if (row['priorityName'] == "Low") {
                        return `<span class="right badge badge-info">${row['priorityName']}</span>`;
                    } else if (row['priorityName'] == "Low") {
                        return `<span class="right badge badge-warning">${row['priorityName']}</span>`;
                    } else if (row['priorityName'] == "High") {
                        return `<span class="right badge badge-danger">${row['priorityName']}</span>`;
                    } else {
                        return `<span class="right badge badge-info">${row['priorityName']}</span>`;
                    }
                }
            },*/
            {
                "data": "categoryName"
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
            {
                "render": function (data, type, row) {
                    if (row['endDateTime'] == null) {
                        if (row['level'] == viewBagLevel) {
                            return `<button type="button" class="btn btn-outline-info" onclick="askNextLevel('${row['id']}')"  data-placement="bottom" title="Ask Next Level to Help"><i class="fas fa-question"></i></button> | <button type="button" class="btn btn-outline-primary" onclick="viewConvertation('${row['id']}')" data-toggle="modal" data-target="#viewConvertationModal"  data-placement="bottom" title="Chatting With Client"><i class="fas fa-comment"></i></button> | <button type="button" class="btn btn-outline-danger" onclick="closeTicket('${row['id']}','${viewBagUserId}')"  data-placement="bottom" title="Close Ticket"><i class="fas fa-times"></i></button>`;
                        } else {
                            return null;
                        }
                        //return `<button type="button" class="btn btn-info" onclick="asknextlevel('${row['id']}')">ask next level</button> | <button type="button" class="btn btn-primary" onclick="viewconvertation('${row['id']}')" data-toggle="modal" data-target="#viewconvertationmodal">chat</button> | <button type="button" class="btn btn-danger" onclick="closeticket('${row['id']}','${userid}')">close</button>`;
                    } else {
                        return null;
                    }
                }
            }
        ]
    });

});


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
            console.log(val.employeeId);
            console.log(viewBagUserId);
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
    if (obj.EmployeeId=="" || obj.CaseId < 0 || obj.Message == "") {
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
            viewChat(obj.CaseId);
            $('#tableViewHandleTickets').DataTable().ajax.reload();
            $("#inputConvertationMessage");
            $('#viewconvertationmodal').modal('hide');
            //Swal.fire({
            //    title: 'Success!',
            //    text: 'Berhasil menambahkan data',
            //    icon: 'success',
            //    confirmButtonText: 'Cool'
            //});
            //$('#tableProfiles').DataTable().ajax.reload();
            //console.log(result);

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


function closeTicket(caseId, employeeId) {
    var obj = new Object();
    obj.CaseId = parseInt(caseId);
    obj.EmployeeId = employeeId;
    Swal.fire({
        title: 'Konfirmasi Penutupan Ticket',
        text: 'Apakan Anda yakin untuk menutup CaseId #' + caseId + ' oleh StaffId #' + employeeId + ' ?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ya',
        cancelButtonText: 'Tidak'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: 'https://localhost:44381/api/tickets/CloseTicket',
                type: "POST",
                dataType: "json",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                data: JSON.stringify(obj)
            }).done((result) => {
                console.log(result);
                console.log(obj);
                $('#tableViewHandleTickets').DataTable().ajax.reload();
                Swal.fire({
                    title: 'Success!',
                    text: 'Berhasil menambahkan Case untuk ditangani Anda',
                    icon: 'success',
                    confirmButtonText: 'Oke'
                });
            }).fail((error) => {
                console.log(error);
                console.log(obj);
                Swal.fire({
                    title: 'Error!',
                    text: 'Gagal menambahkan Case untuk ditangani Anda',
                    icon: 'error',
                    confirmButtonText: 'Oke'
                });
            });
        }
    });

}


function askNextLevel(caseId) {
    var obj = new Object();
    obj.CaseId = parseInt(caseId);
    Swal.fire({
        title: 'Konfirmasi Meminta Bantuan Ticket',
        text: 'Apakan Anda yakin meminta bantuan ke Level selanjutnya untuk CaseId #' + caseId + ' oleh StaffId #' + viewBagUserId + ' ?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ya',
        cancelButtonText: 'Tidak'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: 'https://localhost:44381/api/tickets/AskNextLevel',
                type: "POST",
                dataType: "json",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                data: JSON.stringify(obj)
            }).done((result) => {
                console.log(result);
                console.log(obj);
                $('#tableViewHandleTickets').DataTable().ajax.reload();
                Swal.fire({
                    title: 'Success!',
                    text: 'Berhasil menambahkan Case untuk ditangani Anda',
                    icon: 'success',
                    confirmButtonText: 'Oke'
                });
            }).fail((error) => {
                console.log(error);
                console.log(obj);
                Swal.fire({
                    title: 'Error!',
                    text: 'Gagal menambahkan Case untuk ditangani Anda',
                    icon: 'error',
                    confirmButtonText: 'Oke'
                });
            });
        }
    });

}