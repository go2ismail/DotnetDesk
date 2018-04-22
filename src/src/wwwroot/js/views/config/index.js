var popup, dataTable;

$(document).ready(function () {
    dataTable = $('#gridOrganization').DataTable({
        "ajax": {
            "url": "/api/organization",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "organizationName" },
            {
                "data": "organizationId",
                "render": function (data) {
                    var view = "<a class='btn btn-primary btn-sm' href='/Module/Index/?org=" + data + "'><i class='fa fa-gear'></i> View</a>";
                    var edit = "<a class='btn btn-default btn-sm' style='margin-left:5px' onclick=ShowPopup('/Config/AddEditOrganization/" + data + "')><i class='fa fa-pencil'></i> Edit</a>";
                    var del = "<a class='btn btn-danger btn-sm' style='margin-left:5px' onclick=Delete('" + data + "')><i class='fa fa-trash'></i> Delete</a>";
                    return view + edit + del;
                    //return "<a class='btn btn-default btn-sm' onclick=ShowPopup('/Config/AddEditOrganization/" + data + "')><i class='fa fa-pencil'></i> Edit</a><a class='btn btn-danger btn-sm' style='margin-left:5px' onclick=Delete('" + data + "')><i class='fa fa-trash'></i> Delete</a>";
                }
            }
        ],
        "language": {
            "emptyTable": "no data found."
        }
    });
});

function ShowPopup(url) {
    var formDiv = $('<div/>');
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            popup = formDiv.dialog({
                autoOpen: true,
                resizeable: false,
                width: 600,
                height: 400,
                title: 'Add or Edit Data',
                close: function () {
                    popup.dialog('destroy').remove();
                }
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
                    popup.dialog('close');
                    ShowMessage(data.message);
                    dataTable.ajax.reload();
                }
            }
        });

    }
    return false;
}

function Delete(id) {
    swal({
        title: "Are you sure want to Delete?",
        text: "You will not be able to restore the file!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
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
                }
            }
        });
    });


}


function ShowMessage(msg) {
    toastr.success(msg);
}

