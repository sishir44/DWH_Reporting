
$(document).ready(function () {




    $("#imageloader").hide();
    $(document).ajaxStart(function () {
        $('body').addClass("loading");
    });
    $(document).ajaxStop(function () {
        $('body').removeClass("loading");
    });
});



$('body').toggleClass('open');
$('#left-panel').removeClass('open-menu');
$("#Policies").attr("hidden", true);
    $('#menuToggle').on('click', function (event) {
        if ($("#setweidth").val() == 1) {
            var w = $(window).width();
    // $("#crd").width(w - 280)
    $("#setweidth").val('0');
    $("#Policies").attr("hidden", true);
}
        else {
            var w = $(window).width();
    // $("#crd").width(w - 120)
    $("#Policies").attr("hidden", false);
    $("#setweidth").val('1');
}
});

$("#menuToggle").click();



var table;
    table = $('#example').DataTable({
        "paging": false,
    "ordering": true,
    "info": true,
    "scrollY": "300",
    "scrollCollapse": true,
        "language": {
        "emptyTable": "No data available in table",
    "searchPlaceholder": "Search records"
},
"retrieve": true,
"autoWidth": false,
"fixedColumns": true,
"fixedHeader": true,
"scrollX": true,
fixedColumns: true,
        'fixedColumns': {
        leftColumns: 1,
},


});
