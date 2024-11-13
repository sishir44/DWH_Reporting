
    var table, count;
    table = $('#mytable').DataTable(
        {
        "paging": false,
    "ordering": true,
    "info": true,
    "scrollY": "90px",
    "scrollCollapse": true,
            "language": {
        "emptyTable": "No data available in table",
    "searchPlaceholder": "Search records"
},
"retrieve": true,
"autoWidth": false
}
);

$('body').toggleClass('open');
$('#left-panel').removeClass('open-menu');
$("#Policies").attr("hidden", true);

    $('#menuToggle').on('click', function (event) {
        if ($("#setweidth").val() == 1) {
            var w = $(window).width();
    $("#setweidth").val('0');
    $("#Policies").attr("hidden", true);
}
        else {
            var w = $(window).width();
    $("#Policies").attr("hidden", false);
    $("#setweidth").val('1');
}
});

GetDatatable();

    function viewDetaill(profileid) {
        $('#myModal1').modal('show');
    $.ajax({
        type: "POST",
    url: "ViewDetail",
            data: {"profileId": profileid },
            success: function (data) {
        $('#tblReportDataDetailSpl tbody').empty();
    $.each(data, function (i, item) {
                    var row = "";
                    row += "<tr><td>" + item.Description
                        + "</td><td>" + item.Values
                        + "</td></tr>";
$('#tblReportDataDetailSpl tbody').append(row);
})
},
            error: function () {alert("Error loading data."); }
});
}

    function GetDatatable() {
        $($.fn.dataTable.tables(true)).DataTable().columns.adjust();
    var w = $(window).width();
    $("#crd").width(w - 120);
}

    function loadDataTable() {
        var domainids = $('#idStore').val();
    var appId = $("#appId").val();
    var actionbtn;
    $("#DataTableImageLoader").show();
    table.clear();

        $.ajax({
        type: "POST",
    url: "MyReport_ByDomainId",
            data: {"appId": appId, "domainid": domainids },
            success: function (data) {
              
                if (data.rml != "")
                {
                if (data.btnStatus == true) {

        $.each(data.rml, function (i, item) {
            actionbtn = "<button onclick='viewDetaill(" + item.ProfileId + ")' class='btn btn-info btn-sm btn-xs align-btn padding-btn' ><span class='btn-label'><i class='fa fa-list'></i></span> View</button >";
            discussbtn = "<button onclick='getcomments(" + item.ProfileId + ")' class='btn btn-info btn-sm btn-xs align-btn' ><span class='btn-label'><i class='fa fa fa-comments-o'></i></span> Discuss</button >";
            attachmentsbtn = "<button onclick='getattachments(" + item.ProfileId + ")' class='btn btn-info btn-sm btn-xs align-btn' ><span class='btn-label'><i class='fa fa-paperclip'></i></span> Attachments</button >";
            statusbtn = "<button onclick='getStatusView(" + item.ProfileId + ")' class='btn btn-info btn-sm btn-xs align-btn' ><span class='btn-label'><i class='fa fa-recycle'></i></span> Status</button >";
            table.row.add([actionbtn + discussbtn + attachmentsbtn + statusbtn, item.RequestDate, item.RequestType, item.EmployeeName, item.Designation, item.Market, item.StoreName, item.StoreUID, item.Status]);
        });

    }
                    else {
        $.each(data.rml, function (i, item) {
            actionbtn = "<button onclick='viewDetaill(" + item.ProfileId + ")' class='btn btn-info btn-sm btn-xs align-btn padding-btn' ><span class='btn-label'><i class='fa fa-list'></i></span> View</button >";
            discussbtn = "<button onclick='getcomments(" + item.ProfileId + ")' class='btn btn-info btn-sm btn-xs align-btn' ><span class='btn-label'><i class='fa fa fa-comments-o'></i></span> Discuss</button >";
            attachmentsbtn = "<button onclick='getattachments(" + item.ProfileId + ")' class='btn btn-info btn-sm btn-xs align-btn' ><span class='btn-label'><i class='fa fa-paperclip'></i></span> Attachments</button >";
            table.row.add([actionbtn + discussbtn + attachmentsbtn, item.RequestDate, item.RequestType, item.EmployeeName, item.Designation, item.Market, item.StoreName, item.StoreUID, item.Status]);
        });
    }


}
table.draw();

},
            error: function () {window.location.href = "@System.Configuration.ConfigurationManager.AppSettings["ErrorView"]"; }
});
$("#DataTableImageLoader").hide();
}

    var getcomments = function (profileId) {
        var domainId = $('#idStore').val();
    var appId = $("#appId").val();
    var url = "./Comments?appId=" + appId + "&profileId=" + profileId + "&domainId=" + domainId;
        $("#modalbody").load(url, function () {
        $("#modal").modal("show");
    });
}

    var getattachments = function (profileId) {
        var appId = $("#appId").val();
    var domainId = $('#idStore').val();
    var url = "./Attachments?appId=" + appId + "&profileid=" + profileId + "&domainId=" + domainId;
        $("#modalbody3").load(url, function () {
        $("#modal3").modal("show");
    });
}

    var getStatusView = function (profileId) {
        var appId = $("#appId").val();
    var domainId = $('#idStore').val();
    var IsMyRequest = 1;
    var url = "./GetStatusViewForMyRequests?AppID=" + appId + "&DomainID=" + domainId + "&ProfileID=" + profileId + "&IsMyRequest=" + IsMyRequest;
        $("#modalbodyStatus").load(url, function () {
        $("#modalStatus").modal("show");
    });
}

    function Drpdownbind(spname, ControlId) {
        var StoreProName = spname;
    var CName = ControlId;
    var id = "#ddl" + CName;
        $.ajax({
        type: "Get",
    contentType: "application/json; charset=utf-8",
    url: "Dropdownbind",
            data: {Spname: StoreProName },
    datatype: "application/json",
            success: function (Result) {
        $.each(Result, function (key, value) {
            $(id).append($("<option></option>").val(value.Value_Id + '/' + value.Value_Text).html(value.Value_Text));
        });
    },
            error: function (Result) {
        alert("Error");
    }
});
}

    $("#btnSubmit").click(function () {$(function () { $('#myModalStatusChange').modal('hide'); UpdateRequestData(); }); });
    $("#btnClosePopup").click(function () {$("#myModalStatusChange").modal("hide"); });


    function UpdateRequestData() {
        $('#loading').fadeIn();
    var profileId = $("#profileId_Edit").val();
    var DomID = $("#ddlDomain").val();
    var StoreID = $("#ddlStore").val();
    var CatId = $("#DdlCategory").val();
    var SubCatId = $("#DdlSubCategory").val();

    var form = $("#form2")[0];
    var formData = new FormData(form);

    formData.append("DomainId", DomID);
    formData.append("CATId", CatId);
    formData.append("SubCatId", SubCatId);
    formData.append("Storeid", StoreID);
    formData.append("profileId", profileId);

        $.ajax({
        type: "POST",
    data: formData,
    url: 'UpdateData',
    contentType: false, // Not to set any content header
    processData: false, // Not to process data
            success: function (data) {
                if (data != "") {
                    if (data == "-1") {ErrorMessage(); }
                    else if (data == "Empty") {EmptyMessage(); }
                    else {SuccessfullyUpdate(); Reset(); }
}
$('#loading').fadeOut();
}
});
}

    function ExportToExcel() { var filter = $('#idStore').val(); var appId = $("#appId").val(); window.location.href = 'ExportToExcel?filter=' + filter + "&appId=" + appId + "&RptFlag=M"; }

    $(document).ready(function () {$("#menuToggle").click(); $("#PageImageLoader").hide(); loadDataTable(); })

