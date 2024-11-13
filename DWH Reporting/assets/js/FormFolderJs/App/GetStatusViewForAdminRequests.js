
function UpdateRequestStatus() {
    var AppID = $("#AppID").val();
    var DomainID = $("#DomainID").val();
    var ProfileID = $("#ProfileID").val();
    var StatusID = $("#Status").val();
    $.ajax({
        type: "POST",
        url: "UpdateRequestStatus",
        data: { "AppID": AppID, "DomainID": DomainID, "ProfileID": ProfileID, "StatusID": StatusID },
        success: function (data) {
            window.location.reload();
            //swal(data.message);
            //setTimeout(function () {
            //    window.location.reload();
            //}, 4000)
        },
        error: function () { alert("Error loading data."); }
    });
}