// variable Declare 
var html = "";
var appId = $("#appId").val();
var DropdownBind = false;
var Urlajax = "";

function SetUrl(url) { Urlajax = url; }



function Drpdownbind(spname, IsRequired, attributeId, User) {
    // debugger;

    var StoreProName = spname;
    var CreateStore = "";
    $.ajax({
        type: "Get",
        url: "./GetUserDropDown",
        data: { user: User },

        success: function (Result) {

            //debugger;
            if (Result != '') {
                $.each(Result, function (key, value) {
                    if (IsRequired == "1") {
                        if (CreateStore == false) {
                            html += ' <select style="font-size:14px;" onchange="setDropDownId(this.id)" data-live-search="true" id="' + attributeId + '" name="' + attributeId + '" required> <option value="" disabled="" selected="">Select Name </option>  ';
                            CreateStore = true;
                        }
                        html += '<option id="' + value.ID + '" value="' + value.Name + '" >' + value.EmpName + ' </option> ';
                    }
                    else {
                        if (CreateStore == false) {
                            html += ' <select style="font-size:14px;" onchange="setDropDownId(this.id)" data-live-search="true" id="' + attributeId + '" name="' + attributeId + '"> <option value="" disabled="" selected="">Select Name </option>  ';
                            CreateStore = true;
                        }
                        html += '<option id="' + value.ID + '" value="' + value.Name + '" >' + value.EmpName + ' </option> ';
                    }
                });
                html += '</select>';
                $('.' + attributeId).append(html);
                html = "";
            }
        },
        error: function (Result) {
            alert("Error");
        }
    });


}

function setDropDownId(attid) {
    //debugger
    var ddl_name = $("#" + attid + "").children(":selected").attr("id");
    $("#ddl_drop_" + attid).val(ddl_name);
}
