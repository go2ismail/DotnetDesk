var popup, dataTable;
var entity = 'Product';
var apiurl = '/api/' + entity;

$(document).ready(function () {
    var organizationId = $('#organizationId').val();
    dataTable = $('#grid').DataTable({
        "ajax": {
            "url": apiurl + '/' + organizationId,
            "type": 'GET',
            "datatype": 'json'
        },
        "columns": [
            { "data": "productName" },
            {
                "data": "productId",
                "render": function (data) {
                    var btnEdit = "<a class='btn btn-default btn-xs' onclick=ShowPopup('/" + entity + "/AddEdit/" + data + "')><i class='fa fa-pencil'></i></a>";
                    var btnDelete = "<a class='btn btn-danger btn-xs' style='margin-left:2px' onclick=Delete(" + data + ")><i class='fa fa-trash'></i></a>";
                    return btnEdit + btnDelete;
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
    var modalPlaceholder = $('#modal .modal-dialog .modal-content');
    $.get(url)
        .done(function (response) {
            modalPlaceholder.html(response);
            popup = $('#modal').modal({
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
            url: apiurl,
            data: data,
            contentType: 'application/json',
            success: function (data) {
                if (data.success) {
                    
                    popup.modal('hide');
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
            url: apiurl + '/' + id,
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

