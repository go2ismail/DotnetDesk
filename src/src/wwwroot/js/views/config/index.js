var popup, dataTable;

$(document).ready(function () {
    var applicationUserId = $('#Id').val();
    dataTable = $('#gridOrganization').DataTable({
        "ajax": {
            "url": "/api/organization/" + applicationUserId,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "organizationName" },
            {
                "data": "organizationId",
                "render": function (data) {
                    var view = "<a class='btn btn-primary btn-xs' href='/Customer?org=" + data + "'><i class='fa fa-gear'></i> View</a>";
                    var edit = "<a class='btn btn-default btn-xs' style='margin-left:5px' onclick=ShowPopup('/Config/AddEditOrganization/" + data + "')><i class='fa fa-pencil'></i> Edit</a>";
                    var del = "<a class='btn btn-danger btn-xs' style='margin-left:5px' onclick=Delete('" + data + "')><i class='fa fa-trash'></i> Delete</a>";
                    return view + edit + del;
                }
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "lengthChange": false,
    });
});

function ShowPopup(url) {
    var modalId = 'modalDefault';
    var modalPlaceholder = $('#' + modalId + ' .modal-dialog .modal-content');
    $.get(url)
        .done(function (response) {
            modalPlaceholder.html(response);
            popup = $('#' + modalId + '').modal({
                keyboard: false,
                backdrop: 'static'
            });
        });
}


function SubmitAddEdit(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var data = $(form).serializeJSON();
        data = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            url: '/api/organization',
            data: data,
            contentType: 'application/json',
            success: function (data) {
                if (data.success) {
                    popup.modal('hide');
                    ShowMessage(data.message);
                    dataTable.ajax.reload();
                } else {
                    ShowMessageError(data.message);
                }
            }
        });

    }
    return false;
}

function Delete(id) {
    swal({
        title: "Are you sure want to Delete?",
        text: "You will not be able to restore the data!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#dd4b39",
        confirmButtonText: "Yes, delete it!",
        closeOnConfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: '/api/organization/' + id,
            success: function (data) {
                if (data.success) {
                    ShowMessage(data.message);
                    dataTable.ajax.reload();
                } else {
                    ShowMessageError(data.message);
                }
            }
        });
    });


}




