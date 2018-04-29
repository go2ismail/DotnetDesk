$(document).ready(function () {
    Ladda.bind('.btn', { timeout: 1000 });
});
function ShowMessage(msg) {
    toastr.success(msg);
}

function ShowMessageError(msg) {
    toastr.error(msg);
}