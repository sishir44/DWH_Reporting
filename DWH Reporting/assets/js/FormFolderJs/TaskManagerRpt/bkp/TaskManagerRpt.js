
// variable Declare 

var html = "";

var Appid = "27";


// Genaric DropDown Variable 

var DropdownBind = false;

var Urlajax = "";

//function CheckGenericDropdown(ControlBind) {
//     //debugger;
//   // var Store = ControlBind.includes("Store"); 

//    if (ControlBind.Store == true) {
//        GetStorebind();
//    }

//    if (ControlBind.Assigned == true) {
//        GetAssignebind();
//    }
//}


function SetUrl(url) {
    debugger;

    Urlajax = url;

}


function DomainDropFun(url) {

    var CreateDomain = false;

    $.ajax({
        type: "GET",
        url: url,
        datatype: "application/json",
        success: function (data) {
            html += ' <div class="row">';
            //debugger;
            if (data != '') {

                $.each(data, function (i, item) {
                    //Check Genaric List of Dropdown


                    if (CreateDomain == false) {
                        if (item.ControlTypeId == "6") {

                            html += ' <div class="col-md-4 col-lg-3 col-sm-4 "><label> Domain </label><select id="ddlDomain" onchange="DomainOnchage();" name="ddlDomain" class="form-control selectclass DropDownAlignment"> <option value="-1" disabled="" selected="">Select Domain </option>  ';
                            //html += '<input type="hidden" id=ArrId' + item.AttributesName + '  name=Arr' + item.AttributesName + ' value= "' + item.AttributeId + '"/>'



                        }
                        CreateDomain = true;
                    }

                    html += '<option value="' + item.DomainId + '" >' + item.DomainName + ' </option> ';




                    //if (item.ControlTypeId == "11") {
                    //    if (item.IsViewable == "1") {
                    //        html += '<div class="col-md-4 col-lg-3 col-sm-4"><input id=txt' + item.AttributesName + '  name=' + item.AttributeId + ' class="form-control TxtboxAlignment" placeholder=' + item.AttributesName + '> </div>';
                    //        //html += '<input type="hidden" id=ArrId' + item.AttributesName + '  name=Arr' + item.AttributesName + ' value= "' + item.AttributeId + '"/>'
                    //    }

                    //}
                    //if (item.ControlTypeId == "15") {

                    //    if (item.IsViewable == "1") {
                    //        html += '<div class="col-md-4 col-lg-3 col-sm-4"><input id="DatePicker" name=' + item.AttributeId + ' type="date" class="form-control DatePickerAlignment" > </div>';
                    //        //html += '<input type="hidden" id=ArrId' + item.AttributesName + '  name=' + item.AttributeId + ' value= "' + item.AttributesName + '"/>'
                    //    }

                    //}




                });
            }


            html += '</select> </div>';
            html += '</div>'
            $("#divDomainDropdown").append(html);
            html = "";



        }

    });

}




function DomainOnchage() {
    $('#loading').fadeIn();
    debugger;

    var Domid = $("#ddlDomain").val();
    var AppId = "27";
    var CreateCategory = false;

    var ControlArray;

    var Flag;



    var dict = [];

    $.ajax({
        type: "POST",

        //url: "/TaskManagerRpt/GetCategoryDdl",
        url: Urlajax + "/TaskManagerRpt/GetCategoryDdl",
        data: { DomainID: Domid },
        datatype: "application/json",
        success: function (data) {
            $("#divSubCategoryDropDown").empty();
            $("#divCategoryDropDown").empty();
            $("#divform").empty();

            if (data != '') {
                $.each(data, function (I, item) {

                    Flag = item.Flag.includes("Category");

                });




                if (Flag == true) {

                    html += ' <div class="row">';

                    $.each(data, function (K, item) {





                        if (CreateCategory == false) {
                            if (item.ControlTypeId == "6") {

                                html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label>Category</label><select id="DdlCategory" onchange="CategoryOnchage();" name="ddlCategory" class="form-control selectclass DropDownAlignment"> <option value="" disabled="" selected="">Select Category </option>  ';
                            }
                            CreateCategory = true;
                        }

                        html += '<option value="' + item.CategoryId + '" >' + item.CategoryName + ' </option> ';


                    });

                    html += '</select> </div>';
                    html += '</div>'
                    $("#divCategoryDropDown").append(html);

                }

                else {
                    html += ' <div class="row">';
                    $.each(data, function (L, item) {
                        //Check Genaric List of Dropdown



                        //var Store = item.AttributesName.includes("Store");
                        //var Assign = item.AttributesName.includes("Assign");
                        //if (Store == true) {
                        //    ControlArray["Store"] = Store;
                        //}
                        //if (Assign == true) {
                        //    ControlArray["Assigned"] = Assign;
                        //}

                        //if (item.IsViewable == "0") {

                        //    html += '<input type="hidden" id=hf' + item.AttributesName + '  name=' + item.AttributeId + ' value="' + item.AttributeId + '"/>'
                        //}
                        // item.AttributesName = item.AttributesName.replace(/ /g, '');
                        //alert(item.AttributesName);



                        // Drop down
                        if (item.ControlTypeId == "6") {
                            if (item.IsViewable == "1") {

                                if (item.IsRequired == "1") {
                                    html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red">*</span>' + item.AttributesName + '</label><select id=ddl' + item.AttributesName.replace(/ /g, '') + '  name="' + item.AttributeId + '"  class="form-control selectclass DropDownAlignment"> <option value="" disabled="" selected="">Select ' + item.AttributesName + ' </option>   </select> </div>';

                                }

                                else {
                                    html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red"></span>' + item.AttributesName + '</label><select id=ddl' + item.AttributesName.replace(/ /g, '') + '  name="' + item.AttributeId + '"  class="form-control selectclass DropDownAlignment"> <option value="" disabled="" selected="">Select ' + item.AttributesName + ' </option>   </select> </div>';


                                }

                                //html += '<input type="hidden" id=ArrId' + item.AttributesName + '  name=Arr' + item.AttributesName + ' value= "' + item.AttributeId + '"/>'



                                dict.push({
                                    key: item.Spname,
                                    value: item.AttributesName.replace(/ /g, '')
                                });


                            }
                        }

                        // Textbox
                        if (item.ControlTypeId == "11") {
                            if (item.IsViewable == "1") {

                                if (item.IsRequired == "1") {
                                    html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red">*</span>' + item.AttributesName + '</label><input id=txt' + item.AttributesName.replace(/ /g, '') + '  name=' + item.AttributeId + ' class="form-control TxtboxAlignment" placeholder=' + item.AttributesName + '> </div>';

                                }
                                else {
                                    html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red"></span>' + item.AttributesName + '</label><input id=txt' + item.AttributesName.replace(/ /g, '') + '  name=' + item.AttributeId + ' class="form-control TxtboxAlignment" placeholder=' + item.AttributesName + '> </div>';


                                }

                                //html += '<input type="hidden" id=ArrId' + item.AttributesName + '  name=Arr' + item.AttributesName + ' value= "' + item.AttributeId + '"/>'


                            }

                        }

                        // Date Picker
                        if (item.ControlTypeId == "15") {

                            if (item.IsViewable == "1") {

                                if (item.IsRequired == "1") {
                                    html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red">*</span>' + item.AttributesName + '</label><input id="DatePicker" name=' + item.AttributeId + ' type="date" class="form-control DatePickerAlignment" > </div>';

                                }
                                else {

                                    html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red"></span>' + item.AttributesName + '</label><input id="DatePicker" name=' + item.AttributeId + ' type="date" class="form-control DatePickerAlignment" > </div>';

                                }

                                //html += '<input type="hidden" id=ArrId' + item.AttributesName + '  name=' + item.AttributeId + ' value= "' + item.AttributesName + '"/>'


                            }



                        }


                        // Image Uploader 
                        if (item.ControlTypeId == "7") {

                            if (item.IsRequired == "1") {

                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass" ><label><span style="color:red">*</span>' + item.AttributesName + '</label><input type="hidden" name=SubFile-' + item.AttributeId + '> <input type="file" id="gallery-photo-add" class="form-control Fileupload" name="SubFile-' + item.AttributeId + '"  required></br></div>';

                            }
                            else {
                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass" ><label><span style="color:red"></span>' + item.AttributesName + '</label><input type="hidden" name=SubFile-' + item.AttributeId + '> <input type="file" id="gallery-photo-add" class="form-control Fileupload" name="SubFile-' + item.AttributeId + '"  required></br></div>';


                            }




                        }


                        // File Uploader
                        if (item.ControlTypeId == "9") {

                            if (item.IsRequired == "1") {
                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red">*</span>' + item.AttributesName + '</label><input type="hidden" name=SubFile-' + item.AttributeId + '> <input type="file" id="Fileadd" class="form-control Fileupload" name=SubFile-' + item.AttributeId + '  required></br></div>';

                            }
                            else {

                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red"></span>' + item.AttributesName + '</label><input type="hidden" name=SubFile-' + item.AttributeId + '> <input type="file" id="Fileadd" class="form-control Fileupload" name=SubFile-' + item.AttributeId + '  required></br></div>';

                            }




                        }


                        // Textbox Validation
                        if (item.ControlTypeId == "16") {

                            if (item.IsRequired == "1") {
                                var txt = "";
                                txt += '<div class="row">'
                                //  txt += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label>' + item.AttributesName + '</label><input id="' + item.AttributesName.replace(/ /g, '') + '"  name="' + item.AttributeId + '" class="form-control TxtboxAlignment txtvalidation" placeholder="' + item.AttributesName + '"></div>';
                                txt += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"> <label> <span style="color:red">*</span>' + item.AttributesName + '</label><input id="' + item.AttributesName.replace(/ /g, '') + '"  name="' + item.AttributeId + '" class="form-control TxtboxAlignment txtvalidation" placeholder="' + item.AttributesName + '" onkeyup="Validation(/' + item.RExpression + '/,this);"></div>';



                                txt += "</div>"
                                $("#divform").append(txt);
                                txt = "";
                            }
                            else {

                                var txt = "";
                                txt += '<div class="row">'
                                //  txt += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label>' + item.AttributesName + '</label><input id="' + item.AttributesName.replace(/ /g, '') + '"  name="' + item.AttributeId + '" class="form-control TxtboxAlignment txtvalidation" placeholder="' + item.AttributesName + '"></div>';
                                txt += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"> <label> <span style="color:red"></span>' + item.AttributesName + '</label><input id="' + item.AttributesName.replace(/ /g, '') + '"  name="' + item.AttributeId + '" class="form-control TxtboxAlignment txtvalidation" placeholder="' + item.AttributesName + '" onkeyup="Validation(/' + item.RExpression + '/,this);"></div>';



                                txt += "</div>"
                                $("#divform").append(txt);
                                txt = "";
                            }







                            //  Validation(item.AttributesName.replace(/ /g, ''), item.RExpression);
                        }

                        // Text Area
                        if (item.ControlTypeId == "1") {

                            if (item.IsViewable == "1") {
                                if (item.IsRequired == "1") {
                                    html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                                    +'<label><span style="color:red">*</span>' + item.AttributesName + '</label>'
                                        + ' <div class="input-group">'
                                        + '<div class="input-group-prepend">'
                                        + '  <span class="input-group-text">' + item.AttributesName + '</span> </div>'
                                        + '<textarea class="form-control" name=' + item.AttributeId + ' aria-label="With textarea"></textarea></div></div>';

                                }
                                else {


                                    html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                                    +'<label>' + item.AttributesName + '</label>'
                                        + ' <div class="input-group">'
                                        + '<div class="input-group-prepend">'
                                        + '  <span class="input-group-text">' + item.AttributesName + '</span> </div>'
                                        + '<textarea class="form-control" name=' + item.AttributeId + ' aria-label="With textarea"></textarea></div></div>';



                                }
                            }



                        }





                        // Radion button
                        if (item.ControlTypeId == "2") {
                            if (item.IsViewable == "1") {
                                if (item.IsRequired == "1") {
                                    html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                                    +'<label><span style="color:red">*</span>' + item.AttributesName + '</label>'
                                        + '  <div class="form-check">'
                                        + '<label class="form-check-label">'
                                        + '<input type="radio" class="form-check-input" name="' + item.AttributeId + '">' + item.AttributesName + ''
                                        + ' </label></div></div>';
                                }
                                else {
                                    html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                                    +'<label><span style="color:red"></span>' + item.AttributesName + '</label>'
                                        + '  <div class="form-check">'
                                        + '<label class="form-check-label">'
                                        + '<input type="radio" class="form-check-input" name="' + item.AttributeId + '">' + item.AttributesName + ''
                                        + ' </label></div></div>';
                                }
                            }



                        }


                        // Check box 
                        if (item.ControlTypeId == "4") {
                            if (item.IsViewable == "1") {
                                if (item.IsRequired == "1") {
                                    html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                                    +'<label><span style="color:red">*</span>' + item.AttributesName + '</label>'


                                        + '<div class="form-group form-check">'
                                        + '   <input type="checkbox" class="form-check-input"  name="' + item.AttributeId + '">'
                                        + '      <label class="form-check-label" for="exampleCheck1">' + item.AttributesName + '</label> </div >'

                                        + ' </div ></div > ';

                                }
                                else {
                                    html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                                    +'<label><span style="color:red"></span>' + item.AttributesName + '</label>'
                                        + '<div class="form-group form-check">'
                                        + '   <input type="checkbox" class="form-check-input"  name="' + item.AttributeId + '">'
                                        + '      <label class="form-check-label" for="exampleCheck1">' + item.AttributesName + '</label> </div >'

                                        + ' </div ></div > ';
                                }
                            }



                        }



                        //if (item.ControlTypeId == "4") {
                        //    if (item.IsViewable == "1") {
                        //        if (item.IsRequired == "1") {
                        //            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                        //            +'<label><span style="color:red">*</span>' + item.AttributesName + '</label>'


                        //                + '<div class="form-group form-check">'
                        //                + '   <input type="checkbox" class="form-check-input"  name="' + item.AttributeId + '">'
                        //                + '      <label class="form-check-label" for="exampleCheck1">' + item.AttributesName + '</label> </div >'

                        //                + ' </div ></div > ';

                        //        }
                        //        else {
                        //            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                        //            +'<label><span style="color:red"></span>' + item.AttributesName + '</label>'
                        //                + '<div class="form-group form-check">'
                        //                + '   <input type="checkbox" class="form-check-input"  name="' + item.AttributeId + '">'
                        //                + '      <label class="form-check-label" for="exampleCheck1">' + item.AttributesName + '</label> </div >'

                        //                + ' </div ></div > ';
                        //        }
                        //    }



                        //}

                        // Checkbox
                        //if (item.ControlTypeId == "17") {
                        //    if (item.IsViewable == "1") {
                        //        if (item.IsRequired == "1") {
                        //            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                        //            +'<label><span style="color:red">*</span>' + item.AttributesName + '</label>'


                        //                + '<div class="form-group form-check">'
                        //                + '   <input type="checkbox" class="form-check-input"  name="' + item.AttributeId + '">'
                        //                + '      <label class="form-check-label" for="exampleCheck1">' + item.AttributesName + '</label> </div >'

                        //                + ' </div ></div > ';

                        //        }
                        //        else {
                        //            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                        //            +'<label><span style="color:red"></span>' + item.AttributesName + '</label>'
                        //                + '<div class="form-group form-check">'
                        //                + '   <input type="checkbox" class="form-check-input"  name="' + item.AttributeId + '">'
                        //                + '      <label class="form-check-label" for="exampleCheck1">' + item.AttributesName + '</label> </div >'

                        //                + ' </div ></div > ';
                        //        }
                        //    }



                        //}






                        // Multi select Dropdown
                        if (item.ControlTypeId == "17") {
                            if (item.IsViewable == "1") {
                                if (item.IsRequired == "1") {

                                    html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red">*</span>' + item.AttributesName + '</label>'
                                        + '  <select id= ddl' + item.AttributesName.replace(/ /g, '') + '  name = "' + item.AttributeId + '"  class="selectpicker DropDownAlignment" multiple data-live-search="true"> </select>'

                                        + '</div>';


                                }
                                else {


                                    html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red"></span>' + item.AttributesName + '</label>'
                                        + '  <select id= ddl' + item.AttributesName.replace(/ /g, '') + '  name = "' + item.AttributeId + '"  class="selectpicker DropDownAlignment" multiple data-live-search="true"> </select>'

                                        + '</div>';

                                }



                                MultiSelectDict.push({
                                    key: item.Spname,
                                    value: item.AttributesName.replace(/ /g, '')
                                });


                            }
                        }



                    });
                    html += '</div>'


                    $("#divform").append(html);


                    $.each(dict, function (f, item) {

                        Drpdownbind(item.key, item.value);

                    });
                }
                $('#loading').fadeOut();

            }

            //  html += '<div class="col-md-4 col-lg-4 col-sm-4"><input id="btnSubmit" type="submit"></div>'



            html = "";



        }

    });

}




function CategoryOnchage() {

    if (Appid == "undefined") {
        Appid = "27"
    }

    $('#loading').fadeIn();

    var Catid = $("#DdlCategory").val();

    var CreateSubCategory = false;



    $.ajax({
        //type: "GET",
        //url: "/TaskManagerRpt/GetSubCategoryDdl",
        url: Urlajax + "/TaskManagerRpt/GetSubCategoryDdl",
        data: { CatID: Catid },
        datatype: "application/json",
        success: function (data) {
            $("#divSubCategoryDropDown").empty();
            $("#divform").empty();
            //debugger;
            if (data != '') {


                html += ' <div class="row">';

                $.each(data, function (K, item) {
                    //Check Genaric List of Dropdown




                    if (CreateSubCategory == false) {
                        if (item.SubControlTypeId == "6") {

                            html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label>SubCategory</label><select id="DdlSubCategory" onchange="SubCategoryOnchage();" name="ddlCategory" class="form-control selectclass DropDownAlignment"> <option value="" disabled="" selected="">Select SubCategory </option>  ';
                            //html += '<input type="hidden" id=ArrId' + item.AttributesName + '  name=Arr' + item.AttributesName + ' value= "' + item.AttributeId + '"/>'

                        }
                        CreateSubCategory = true;
                    }

                    html += '<option value="' + item.SubCategoryId + '" >' + item.SubCategoryName + ' </option> ';


                });

                html += '</select> </div>';
                html += '</div>'
                $("#divSubCategoryDropDown").append(html);






            }

            else {


                if (Appid == "undefined") {
                    Appid = "27"
                }

                CreateAttribute($("#ddlDomain").val(), $("#DdlCategory").val(), Appid)

            }


            html = "";

            $('#loading').fadeOut();

        }

    });



}



function SubCategoryOnchage() {
    if (Appid == "undefined") {
        Appid = "27"
    }

    //debugger;
    var Catid = $("#DdlCategory").val();
    var DomainId = $("#ddlDomain").val();
    var SubCategory = $("#DdlSubCategory").val();

    CreateAttribute_With_Param(DomainId, Catid, Appid, SubCategory)




}


function CreateAttribute_With_Param(DomainId, CatId, AppId, SubCategory) {
    $('#loading').fadeIn();
    //debugger;
    var Domid = DomainId;
    var catid = CatId;
    var Appid = AppId;
    var ControlArray;
    var Sub = SubCategory;

    //ControlArray = {
    //    "Store": false,
    //    "Market": false,
    //    "Assigned" : false
    //}
    var dict = [];
    $.ajax({
        type: "POST",
        // url: "/TaskManagerRpt/AllAttributes_With_Param",
        url: Urlajax + "/TaskManagerRpt/AllAttributes_With_Param",
        //  url: '@Url.Action("GetSearchFormRecord", "ExecutiveBoard" )',
        data: { DomainID: Domid, CATID: catid, APPID: Appid, SubCatId: Sub },
        datatype: "application/json",
        success: function (data) {
            //debugger;
            $("#divform").empty();
            html += ' <div class="row">';


            if (data != '') {


                $.each(data, function (L, item) {
                    //Check Genaric List of Dropdown



                    //var Store = item.AttributesName.includes("Store");
                    //var Assign = item.AttributesName.includes("Assign");
                    //if (Store == true) {
                    //    ControlArray["Store"] = Store;
                    //}
                    //if (Assign == true) {
                    //    ControlArray["Assigned"] = Assign;
                    //}

                    //if (item.IsViewable == "0") {

                    //    html += '<input type="hidden" id=hf' + item.AttributesName + '  name=' + item.AttributeId + ' value="' + item.AttributeId + '"/>'
                    //}



                    // Drop down
                    if (item.ControlTypeId == "6") {
                        if (item.IsViewable == "1") {

                            if (item.IsRequired == "1") {
                                html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red">*</span>' + item.AttributesName + '</label><select id=ddl' + item.AttributesName.replace(/ /g, '') + '  name="' + item.AttributeId + '"  class="form-control selectclass DropDownAlignment"> <option value="" disabled="" selected="">Select ' + item.AttributesName + ' </option>   </select> </div>';

                            }

                            else {
                                html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red"></span>' + item.AttributesName + '</label><select id=ddl' + item.AttributesName.replace(/ /g, '') + '  name="' + item.AttributeId + '"  class="form-control selectclass DropDownAlignment"> <option value="" disabled="" selected="">Select ' + item.AttributesName + ' </option>   </select> </div>';


                            }

                            //html += '<input type="hidden" id=ArrId' + item.AttributesName + '  name=Arr' + item.AttributesName + ' value= "' + item.AttributeId + '"/>'



                            dict.push({
                                key: item.Spname,
                                value: item.AttributesName.replace(/ /g, '')
                            });


                        }
                    }

                    // Textbox
                    if (item.ControlTypeId == "11") {
                        if (item.IsViewable == "1") {

                            if (item.IsRequired == "1") {
                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red">*</span>' + item.AttributesName + '</label><input id=txt' + item.AttributesName.replace(/ /g, '') + '  name=' + item.AttributeId + ' class="form-control TxtboxAlignment" placeholder=' + item.AttributesName + '> </div>';

                            }
                            else {
                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red"></span>' + item.AttributesName + '</label><input id=txt' + item.AttributesName.replace(/ /g, '') + '  name=' + item.AttributeId + ' class="form-control TxtboxAlignment" placeholder=' + item.AttributesName + '> </div>';


                            }

                            //html += '<input type="hidden" id=ArrId' + item.AttributesName + '  name=Arr' + item.AttributesName + ' value= "' + item.AttributeId + '"/>'


                        }

                    }

                    // Date Picker
                    if (item.ControlTypeId == "15") {

                        if (item.IsViewable == "1") {

                            if (item.IsRequired == "1") {
                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red">*</span>' + item.AttributesName + '</label><input id="DatePicker" name=' + item.AttributeId + ' type="date" class="form-control DatePickerAlignment" > </div>';

                            }
                            else {

                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red"></span>' + item.AttributesName + '</label><input id="DatePicker" name=' + item.AttributeId + ' type="date" class="form-control DatePickerAlignment" > </div>';

                            }

                            //html += '<input type="hidden" id=ArrId' + item.AttributesName + '  name=' + item.AttributeId + ' value= "' + item.AttributesName + '"/>'


                        }



                    }


                    // Image Uploader 
                    if (item.ControlTypeId == "7") {

                        if (item.IsRequired == "1") {

                            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass" ><label><span style="color:red">*</span>' + item.AttributesName + '</label><input type="hidden" name=SubFile-' + item.AttributeId + '> <input type="file" id="gallery-photo-add" class="form-control Fileupload" name="SubFile-' + item.AttributeId + '"  required></br></div>';

                        }
                        else {
                            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass" ><label><span style="color:red"></span>' + item.AttributesName + '</label><input type="hidden" name=SubFile-' + item.AttributeId + '> <input type="file" id="gallery-photo-add" class="form-control Fileupload" name="SubFile-' + item.AttributeId + '"  required></br></div>';


                        }




                    }


                    // File Uploader
                    if (item.ControlTypeId == "9") {

                        if (item.IsRequired == "1") {
                            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red">*</span>' + item.AttributesName + '</label><input type="hidden" name=SubFile-' + item.AttributeId + '> <input type="file" id="Fileadd" class="form-control Fileupload" name=SubFile-' + item.AttributeId + '  required></br></div>';

                        }
                        else {

                            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red"></span>' + item.AttributesName + '</label><input type="hidden" name=SubFile-' + item.AttributeId + '> <input type="file" id="Fileadd" class="form-control Fileupload" name=SubFile-' + item.AttributeId + '  required></br></div>';

                        }




                    }


                    // Textbox Validation
                    if (item.ControlTypeId == "16") {

                        if (item.IsRequired == "1") {
                            var txt = "";
                            txt += '<div class="row">'
                            //  txt += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label>' + item.AttributesName + '</label><input id="' + item.AttributesName.replace(/ /g, '') + '"  name="' + item.AttributeId + '" class="form-control TxtboxAlignment txtvalidation" placeholder="' + item.AttributesName + '"></div>';
                            txt += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"> <label> <span style="color:red">*</span>' + item.AttributesName + '</label><input id="' + item.AttributesName.replace(/ /g, '') + '"  name="' + item.AttributeId + '" class="form-control TxtboxAlignment txtvalidation" placeholder="' + item.AttributesName + '" onkeyup="Validation(/' + item.RExpression + '/,this);"></div>';



                            txt += "</div>"
                            $("#divform").append(txt);
                            txt = "";
                        }
                        else {

                            var txt = "";
                            txt += '<div class="row">'
                            //  txt += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label>' + item.AttributesName + '</label><input id="' + item.AttributesName.replace(/ /g, '') + '"  name="' + item.AttributeId + '" class="form-control TxtboxAlignment txtvalidation" placeholder="' + item.AttributesName + '"></div>';
                            txt += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"> <label> <span style="color:red"></span>' + item.AttributesName + '</label><input id="' + item.AttributesName.replace(/ /g, '') + '"  name="' + item.AttributeId + '" class="form-control TxtboxAlignment txtvalidation" placeholder="' + item.AttributesName + '" onkeyup="Validation(/' + item.RExpression + '/,this);"></div>';



                            txt += "</div>"
                            $("#divform").append(txt);
                            txt = "";
                        }







                        //  Validation(item.AttributesName.replace(/ /g, ''), item.RExpression);
                    }
                    // Text Area
                    if (item.ControlTypeId == "1") {

                        if (item.IsViewable == "1") {
                            if (item.IsRequired == "1") {
                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                                +'<label><span style="color:red">*</span>' + item.AttributesName + '</label>'
                                    + ' <div class="input-group">'
                                    + '<div class="input-group-prepend">'
                                    + '  <span class="input-group-text">' + item.AttributesName + '</span> </div>'
                                    + '<textarea class="form-control" name=' + item.AttributeId + ' aria-label="With textarea"></textarea></div></div>';

                            }
                            else {


                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                                +'<label>' + item.AttributesName + '</label>'
                                    + ' <div class="input-group">'
                                    + '<div class="input-group-prepend">'
                                    + '  <span class="input-group-text">' + item.AttributesName + '</span> </div>'
                                    + '<textarea class="form-control" name=' + item.AttributeId + ' aria-label="With textarea"></textarea></div></div>';



                            }
                        }



                    }





                    // Radion button
                    if (item.ControlTypeId == "2") {
                        if (item.IsViewable == "1") {
                            if (item.IsRequired == "1") {
                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                                +'<label><span style="color:red">*</span>' + item.AttributesName + '</label>'
                                    + '  <div class="form-check">'
                                    + '<label class="form-check-label">'
                                    + '<input type="radio" class="form-check-input" name="' + item.AttributeId + '">' + item.AttributesName + ''
                                    + ' </label></div></div>';
                            }
                            else {
                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                                +'<label><span style="color:red"></span>' + item.AttributesName + '</label>'
                                    + '  <div class="form-check">'
                                    + '<label class="form-check-label">'
                                    + '<input type="radio" class="form-check-input" name="' + item.AttributeId + '">' + item.AttributesName + ''
                                    + ' </label></div></div>';
                            }
                        }



                    }


                    // Check box 
                    if (item.ControlTypeId == "4") {
                        if (item.IsViewable == "1") {
                            if (item.IsRequired == "1") {
                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                                +'<label><span style="color:red">*</span>' + item.AttributesName + '</label>'


                                    + '<div class="form-group form-check">'
                                    + '   <input type="checkbox" class="form-check-input"  name="' + item.AttributeId + '">'
                                    + '      <label class="form-check-label" for="exampleCheck1">' + item.AttributesName + '</label> </div >'

                                    + ' </div ></div > ';

                            }
                            else {
                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                                +'<label><span style="color:red"></span>' + item.AttributesName + '</label>'
                                    + '<div class="form-group form-check">'
                                    + '   <input type="checkbox" class="form-check-input"  name="' + item.AttributeId + '">'
                                    + '      <label class="form-check-label" for="exampleCheck1">' + item.AttributesName + '</label> </div >'

                                    + ' </div ></div > ';
                            }
                        }



                    }



                    //if (item.ControlTypeId == "4") {
                    //    if (item.IsViewable == "1") {
                    //        if (item.IsRequired == "1") {
                    //            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                    //            +'<label><span style="color:red">*</span>' + item.AttributesName + '</label>'


                    //                + '<div class="form-group form-check">'
                    //                + '   <input type="checkbox" class="form-check-input"  name="' + item.AttributeId + '">'
                    //                + '      <label class="form-check-label" for="exampleCheck1">' + item.AttributesName + '</label> </div >'

                    //                + ' </div ></div > ';

                    //        }
                    //        else {
                    //            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                    //            +'<label><span style="color:red"></span>' + item.AttributesName + '</label>'
                    //                + '<div class="form-group form-check">'
                    //                + '   <input type="checkbox" class="form-check-input"  name="' + item.AttributeId + '">'
                    //                + '      <label class="form-check-label" for="exampleCheck1">' + item.AttributesName + '</label> </div >'

                    //                + ' </div ></div > ';
                    //        }
                    //    }



                    //}

                    // Checkbox
                    //if (item.ControlTypeId == "17") {
                    //    if (item.IsViewable == "1") {
                    //        if (item.IsRequired == "1") {
                    //            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                    //            +'<label><span style="color:red">*</span>' + item.AttributesName + '</label>'


                    //                + '<div class="form-group form-check">'
                    //                + '   <input type="checkbox" class="form-check-input"  name="' + item.AttributeId + '">'
                    //                + '      <label class="form-check-label" for="exampleCheck1">' + item.AttributesName + '</label> </div >'

                    //                + ' </div ></div > ';

                    //        }
                    //        else {
                    //            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                    //            +'<label><span style="color:red"></span>' + item.AttributesName + '</label>'
                    //                + '<div class="form-group form-check">'
                    //                + '   <input type="checkbox" class="form-check-input"  name="' + item.AttributeId + '">'
                    //                + '      <label class="form-check-label" for="exampleCheck1">' + item.AttributesName + '</label> </div >'

                    //                + ' </div ></div > ';
                    //        }
                    //    }



                    //}






                    // Multi select Dropdown
                    if (item.ControlTypeId == "17") {
                        if (item.IsViewable == "1") {
                            if (item.IsRequired == "1") {

                                html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red">*</span>' + item.AttributesName + '</label>'
                                    + '  <select id= ddl' + item.AttributesName.replace(/ /g, '') + '  name = "' + item.AttributeId + '"  class="selectpicker DropDownAlignment" multiple data-live-search="true"> </select>'

                                    + '</div>';


                            }
                            else {


                                html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red"></span>' + item.AttributesName + '</label>'
                                    + '  <select id= ddl' + item.AttributesName.replace(/ /g, '') + '  name = "' + item.AttributeId + '"  class="selectpicker DropDownAlignment" multiple data-live-search="true"> </select>'

                                    + '</div>';

                            }



                            MultiSelectDict.push({
                                key: item.Spname,
                                value: item.AttributesName.replace(/ /g, '')
                            });


                        }
                    }


                });
            }

            //  html += '<div class="col-md-4 col-lg-4 col-sm-4"><input id="btnSubmit" type="submit"></div>'

            html += '</div>'
            $("#divform").append(html);
            html = "";
            $.each(dict, function (f, item) {

                Drpdownbind(item.key, item.value);

            });

            //  CheckGenericDropdown(ControlArray)




            $('#loading').fadeOut();
        }

    });

}



function CreateAttribute(DomainId, CatId, AppId) {
    $('#loading').fadeIn();
    // debugger;
    var Domid = DomainId;
    var catid = CatId;
    var Appid = "27";
    var ControlArray;

    //ControlArray = {
    //    "Store": false,
    //    "Market": false,
    //    "Assigned": false
    //}

    var dict = [];

    var MultiSelectDict = [];

    $.ajax({
        type: "POST",

        //url: "/TaskManagerRpt/AllAttributes",
        url: Urlajax + "/TaskManagerRpt/AllAttributes",
        //  url: '@Url.Action("GetSearchFormRecord", "ExecutiveBoard" )',
        data: { DomainID: Domid, CATID: catid, APPID: Appid },
        datatype: "application/json",
        success: function (data) {
            //  debugger;
            $("#divform").empty();
            html += ' <div class="row">';


            if (data != '') {


                $.each(data, function (L, item) {
                    //Check Genaric List of Dropdown



                    //var Store = item.AttributesName.includes("Store");
                    //var Assign = item.AttributesName.includes("Assign");
                    //if (Store == true) {
                    //    ControlArray["Store"] = Store;
                    //}
                    //if (Assign == true) {
                    //    ControlArray["Assigned"] = Assign;
                    //}

                    //if (item.IsViewable == "0") {

                    //    html += '<input type="hidden" id=hf' + item.AttributesName + '  name=' + item.AttributeId + ' value="' + item.AttributeId + '"/>'
                    //}


                    // Drop down
                    if (item.ControlTypeId == "6") {
                        if (item.IsViewable == "1") {

                            if (item.IsRequired == "1") {
                                html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red">*</span>' + item.AttributesName + '</label><select id=ddl' + item.AttributesName.replace(/ /g, '') + '  name="' + item.AttributeId + '"  class="form-control selectclass DropDownAlignment"> <option value="" disabled="" selected="">Select ' + item.AttributesName + ' </option>   </select> </div>';

                            }

                            else {
                                html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red"></span>' + item.AttributesName + '</label><select id=ddl' + item.AttributesName.replace(/ /g, '') + '  name="' + item.AttributeId + '"  class="form-control selectclass DropDownAlignment"> <option value="" disabled="" selected="">Select ' + item.AttributesName + ' </option>   </select> </div>';


                            }

                            //html += '<input type="hidden" id=ArrId' + item.AttributesName + '  name=Arr' + item.AttributesName + ' value= "' + item.AttributeId + '"/>'



                            dict.push({
                                key: item.Spname,
                                value: item.AttributesName.replace(/ /g, '')
                            });


                        }
                    }

                    // Textbox
                    if (item.ControlTypeId == "11") {
                        if (item.IsViewable == "1") {

                            if (item.IsRequired == "1") {
                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red">*</span>' + item.AttributesName + '</label><input id=txt' + item.AttributesName.replace(/ /g, '') + '  name=' + item.AttributeId + ' class="form-control TxtboxAlignment" placeholder=' + item.AttributesName + '> </div>';

                            }
                            else {
                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red"></span>' + item.AttributesName + '</label><input id=txt' + item.AttributesName.replace(/ /g, '') + '  name=' + item.AttributeId + ' class="form-control TxtboxAlignment" placeholder=' + item.AttributesName + '> </div>';


                            }

                            //html += '<input type="hidden" id=ArrId' + item.AttributesName + '  name=Arr' + item.AttributesName + ' value= "' + item.AttributeId + '"/>'


                        }

                    }

                    // Date Picker
                    if (item.ControlTypeId == "15") {

                        if (item.IsViewable == "1") {

                            if (item.IsRequired == "1") {
                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red">*</span>' + item.AttributesName + '</label><input id="DatePicker" name=' + item.AttributeId + ' type="date" class="form-control DatePickerAlignment" > </div>';

                            }
                            else {

                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red"></span>' + item.AttributesName + '</label><input id="DatePicker" name=' + item.AttributeId + ' type="date" class="form-control DatePickerAlignment" > </div>';

                            }

                            //html += '<input type="hidden" id=ArrId' + item.AttributesName + '  name=' + item.AttributeId + ' value= "' + item.AttributesName + '"/>'


                        }



                    }


                    // Image Uploader 
                    if (item.ControlTypeId == "7") {

                        if (item.IsRequired == "1") {

                            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass" ><label><span style="color:red">*</span>' + item.AttributesName + '</label><input type="hidden" name=SubFile-' + item.AttributeId + '> <input type="file" id="gallery-photo-add" class="form-control Fileupload" name="SubFile-' + item.AttributeId + '"  required></br></div>';

                        }
                        else {
                            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass" ><label><span style="color:red"></span>' + item.AttributesName + '</label><input type="hidden" name=SubFile-' + item.AttributeId + '> <input type="file" id="gallery-photo-add" class="form-control Fileupload" name="SubFile-' + item.AttributeId + '"  required></br></div>';


                        }




                    }


                    // File Uploader
                    if (item.ControlTypeId == "9") {

                        if (item.IsRequired == "1") {
                            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red">*</span>' + item.AttributesName + '</label><input type="hidden" name=SubFile-' + item.AttributeId + '> <input type="file" id="Fileadd" class="form-control Fileupload" name=SubFile-' + item.AttributeId + '  required></br></div>';

                        }
                        else {

                            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red"></span>' + item.AttributesName + '</label><input type="hidden" name=SubFile-' + item.AttributeId + '> <input type="file" id="Fileadd" class="form-control Fileupload" name=SubFile-' + item.AttributeId + '  required></br></div>';

                        }




                    }



                    // Textbox Validation
                    if (item.ControlTypeId == "16") {

                        if (item.IsRequired == "1") {
                            var txt = "";
                            txt += '<div class="row">'
                            //  txt += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label>' + item.AttributesName + '</label><input id="' + item.AttributesName.replace(/ /g, '') + '"  name="' + item.AttributeId + '" class="form-control TxtboxAlignment txtvalidation" placeholder="' + item.AttributesName + '"></div>';
                            txt += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"> <label> <span style="color:red">*</span>' + item.AttributesName + '</label><input id="' + item.AttributesName.replace(/ /g, '') + '"  name="' + item.AttributeId + '" class="form-control TxtboxAlignment txtvalidation" placeholder="' + item.AttributesName + '" onkeyup="Validation(/' + item.RExpression + '/,this);"></div>';



                            txt += "</div>"
                            $("#divform").append(txt);
                            txt = "";
                        }
                        else {

                            var txt = "";
                            txt += '<div class="row">'
                            //  txt += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label>' + item.AttributesName + '</label><input id="' + item.AttributesName.replace(/ /g, '') + '"  name="' + item.AttributeId + '" class="form-control TxtboxAlignment txtvalidation" placeholder="' + item.AttributesName + '"></div>';
                            txt += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"> <label> <span style="color:red"></span>' + item.AttributesName + '</label><input id="' + item.AttributesName.replace(/ /g, '') + '"  name="' + item.AttributeId + '" class="form-control TxtboxAlignment txtvalidation" placeholder="' + item.AttributesName + '" onkeyup="Validation(/' + item.RExpression + '/,this);"></div>';



                            txt += "</div>"
                            $("#divform").append(txt);
                            txt = "";
                        }







                        //  Validation(item.AttributesName.replace(/ /g, ''), item.RExpression);
                    }







                    //  Validation(item.AttributesName.replace(/ /g, ''), item.RExpression);


                    // Text Area
                    if (item.ControlTypeId == "1") {

                        if (item.IsViewable == "1") {
                            if (item.IsRequired == "1") {
                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                                +'<label><span style="color:red">*</span>' + item.AttributesName + '</label>'
                                    + ' <div class="input-group">'
                                    + '<div class="input-group-prepend">'
                                    + '  <span class="input-group-text">' + item.AttributesName + '</span> </div>'
                                    + '<textarea class="form-control" name=' + item.AttributeId + ' aria-label="With textarea"></textarea></div></div>';

                            }
                            else {


                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                                +'<label>' + item.AttributesName + '</label>'
                                    + ' <div class="input-group">'
                                    + '<div class="input-group-prepend">'
                                    + '  <span class="input-group-text">' + item.AttributesName + '</span> </div>'
                                    + '<textarea class="form-control" name=' + item.AttributeId + ' aria-label="With textarea"></textarea></div></div>';



                            }
                        }



                    }





                    // Radion button
                    if (item.ControlTypeId == "2") {
                        if (item.IsViewable == "1") {
                            if (item.IsRequired == "1") {
                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                                +'<label><span style="color:red">*</span>' + item.AttributesName + '</label>'
                                    + '  <div class="form-check">'
                                    + '<label class="form-check-label">'
                                    + '<input type="radio" class="form-check-input" name="' + item.AttributeId + '">' + item.AttributesName + ''
                                    + ' </label></div></div>';
                            }
                            else {
                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                                +'<label><span style="color:red"></span>' + item.AttributesName + '</label>'
                                    + '  <div class="form-check">'
                                    + '<label class="form-check-label">'
                                    + '<input type="radio" class="form-check-input" name="' + item.AttributeId + '">' + item.AttributesName + ''
                                    + ' </label></div></div>';
                            }
                        }



                    }


                    // Check box 
                    if (item.ControlTypeId == "4") {
                        if (item.IsViewable == "1") {
                            if (item.IsRequired == "1") {
                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                                +'<label><span style="color:red">*</span>' + item.AttributesName + '</label>'


                                    + '<div class="form-group form-check">'
                                    + '   <input type="checkbox" class="form-check-input"  name="' + item.AttributeId + '">'
                                    + '      <label class="form-check-label" for="exampleCheck1">' + item.AttributesName + '</label> </div >'

                                    + ' </div ></div > ';

                            }
                            else {
                                html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                                +'<label><span style="color:red"></span>' + item.AttributesName + '</label>'
                                    + '<div class="form-group form-check">'
                                    + '   <input type="checkbox" class="form-check-input"  name="' + item.AttributeId + '">'
                                    + '      <label class="form-check-label" for="exampleCheck1">' + item.AttributesName + '</label> </div >'

                                    + ' </div ></div > ';
                            }
                        }



                    }



                    //if (item.ControlTypeId == "4") {
                    //    if (item.IsViewable == "1") {
                    //        if (item.IsRequired == "1") {
                    //            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                    //            +'<label><span style="color:red">*</span>' + item.AttributesName + '</label>'


                    //                + '<div class="form-group form-check">'
                    //                + '   <input type="checkbox" class="form-check-input"  name="' + item.AttributeId + '">'
                    //                + '      <label class="form-check-label" for="exampleCheck1">' + item.AttributesName + '</label> </div >'

                    //                + ' </div ></div > ';

                    //        }
                    //        else {
                    //            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                    //            +'<label><span style="color:red"></span>' + item.AttributesName + '</label>'
                    //                + '<div class="form-group form-check">'
                    //                + '   <input type="checkbox" class="form-check-input"  name="' + item.AttributeId + '">'
                    //                + '      <label class="form-check-label" for="exampleCheck1">' + item.AttributesName + '</label> </div >'

                    //                + ' </div ></div > ';
                    //        }
                    //    }



                    //}

                    // Checkbox
                    //if (item.ControlTypeId == "17") {
                    //    if (item.IsViewable == "1") {
                    //        if (item.IsRequired == "1") {
                    //            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                    //            +'<label><span style="color:red">*</span>' + item.AttributesName + '</label>'


                    //                + '<div class="form-group form-check">'
                    //                + '   <input type="checkbox" class="form-check-input"  name="' + item.AttributeId + '">'
                    //                + '      <label class="form-check-label" for="exampleCheck1">' + item.AttributesName + '</label> </div >'

                    //                + ' </div ></div > ';

                    //        }
                    //        else {
                    //            html += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass">';
                    //            +'<label><span style="color:red"></span>' + item.AttributesName + '</label>'
                    //                + '<div class="form-group form-check">'
                    //                + '   <input type="checkbox" class="form-check-input"  name="' + item.AttributeId + '">'
                    //                + '      <label class="form-check-label" for="exampleCheck1">' + item.AttributesName + '</label> </div >'

                    //                + ' </div ></div > ';
                    //        }
                    //    }



                    //}






                    // Multi select Dropdown
                    if (item.ControlTypeId == "17") {
                        if (item.IsViewable == "1") {
                            if (item.IsRequired == "1") {

                                html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red">*</span>' + item.AttributesName + '</label>'
                                    + '  <select id= ddl' + item.AttributesName.replace(/ /g, '') + '  name = "' + item.AttributeId + '"  class="selectpicker DropDownAlignment" multiple data-live-search="true"> </select>'

                                    + '</div>';


                            }
                            else {


                                html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red"></span>' + item.AttributesName + '</label>'
                                    + '  <select id= ddl' + item.AttributesName.replace(/ /g, '') + '  name = "' + item.AttributeId + '"  class="selectpicker DropDownAlignment" multiple data-live-search="true"> </select>'

                                    + '</div>';

                            }



                            MultiSelectDict.push({
                                key: item.Spname,
                                value: item.AttributesName.replace(/ /g, '')
                            });


                        }
                    }


                });



            }

            //  html += '<div class="col-md-4 col-lg-4 col-sm-4"><input id="btnSubmit" type="submit"></div>'

            html += '</div>'
            $("#divform").append(html);
            html = "";
            $.each(dict, function (f, item) {

                Drpdownbind(item.key, item.value);

            });
            //  CheckGenericDropdown(ControlArray)
            $.each(MultiSelectDict, function (f, item) {

                MultiSelectDropBind(item.key, item.value);

            });



            $('#loading').fadeOut();
        }

    });

}



function Drpdownbind(spname, ControlId) {
    //debugger;


    var StoreProName = spname;
    var CName = ControlId;
    var id = "#ddl" + CName;
    $.ajax({
        type: "Get",
        contentType: "application/json; charset=utf-8",
        //url: "/TaskManagerRpt/Dropdownbind",
        url: Urlajax + "/TaskManagerRpt/Dropdownbind",
        data: { Spname: StoreProName },
        datatype: "application/json",
        success: function (Result) {
            //debugger;
            $.each(Result, function (key, value) {


                $(id).append($("<option></option>").val(value.Value_Id + '/' + value.Value_Text).html(value.Value_Text));
            });

            //changeStore();

        },
        error: function (Result) {
            alert("Error");
        }
    });


}



function MultiSelectDropBind(spname, ControlId) {
    //debugger;


    //var StoreProName = spname;
    //var CName = ControlId;
    //var id = "#ddl" + CName;
    //$.ajax({
    //    type: "Get",
    //    contentType: "application/json; charset=utf-8",
    //    url: "/TaskManagerRpt/Dropdownbind",
    //    data: { Spname: StoreProName },
    //    datatype: "application/json",
    //    success: function (Result) {
    //        //debugger;
    //        $.each(Result, function (key, value) {


    //            $(id).append($("<option></option>").val(value.Value_Id + '/' + value.Value_Text).html(value.Value_Text));
    //        });

    //        //changeStore();

    //    },
    //    error: function (Result) {
    //        alert("Error");
    //    }
    //});


}


function Insert(Url) {
    $('#loading').fadeIn();
    //debugger;
    var DomID = $("#ddlDomain").val();
    var StoreID = $("#ddlStore").val();

    var APPId = Appid

    var CatId = $("#DdlCategory").val();

    var SubCatId = $("#DdlSubCategory").val();

    // var StoreSelectedtext = $("#ddlStore option:selected").text();





    var fileUpload = $(":input[type=file]").get(0);

    if (fileUpload != undefined && fileUpload != "") {
        var files = fileUpload.files;
    }



    var form = $("#form1")[0];
    var formData = new FormData(form);
    //for (var i = 0; i < files.length; i++) {
    //    formData.append(files[i].name, files[i]);
    //}

    formData.append("DomainId", DomID);
    formData.append("App", APPId);
    formData.append("CATId", CatId);
    formData.append("SubCatId", SubCatId);
    formData.append("Storeid", StoreID);



    // var array = $("form").serialize() + '&' + $.param({ 'DomainId': DomID }) + '&' + $.param({ 'Storeid': StoreID }) + '&' + $.param({ 'App': APPId }) + '&' + $.param({ 'CATId': CatId }) + '&' + $.param({ 'SubCatId': SubCatId }) + '&' + $.param({ 'DdlStoreTxt': StoreSelectedtext }  );

    $.ajax({
        type: "POST",
        data: formData,
        url: Url,
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data  
        // traditional: true,
        //  url: '@Url.Action("GetSearchFormRecord", "ExecutiveBoard" )',
        //datatype: "application/json",
        success: function (data) {
            debugger;
            if (data != "") {

                if (data == "-1") {
                    ErrorMessage();
                }
                else if (data == "Empty") {
                    EmptyMessage();
                }
                else {
                    Successfully();
                    Reset();
                }

            }



            $('#loading').fadeOut();

        }

    });



}


//, expression

function Validation(Reg, val) {



    // var val = $(this).val();
    var id = val.getAttribute('id');
    var value = val.value;
    var Controlid = "#" + id;


    if (Reg.test(value)) {

    } else {

        $(Controlid).val("");
    }
    //$("#ddlStore").change(function () {

    //     //debugger;
    //    var ddlStore = $("#ddlStore").val();
    //    var ddlStoretxt = $("#ddlStore option:selected").text();
    // //   $("#hfStoreId").val(ddlStore + '-' + ddlStoretxt);


    //    $("#hfStoreId").val(ddlStore);

    //});

    //var ex = "/" + expression + "/";


    //$(".txtvalidation").keyup(function () {
    //     //debugger;
    //    var val = $(this).val();


    //    var pattern = /^[1-9][0-9]?$|^100$/;       
    //    if (pattern.test(val)) {

    //    } else {

    //        $(Controlid).val("");
    //    }
    //});
}




// Popup message

function Successfully() {
    //toastr.options.timeOut = 1500; // 1.5s
    toastr.options = {
        positionClass: 'MessageSide'
    };
    toastr.success('Successfully Submitted');
    //setTimeout(function () { window.location = "../Login/usermenu"; }, 3000);

    $("#loading").fadeOut()

}
function ErrorMessage() {
    //toastr.options.timeOut = 1500; // 1.5s
    //toastr.options = {
    //    positionClass: 'toast-center'
    //};
    toastr.options = {
        positionClass: 'MessageSide'
    };
    toastr.warning('No Connection...Posting Failed!');
    //setTimeout(function () { }, 1500);
    $("#loading").fadeOut()
}


function EmptyMessage() {
    //toastr.options.timeOut = 1500; // 1.5s
    //toastr.options = {
    //    positionClass: 'toast-center'
    //};
    toastr.options = {
        positionClass: 'MessageSide'
    };
    toastr.warning('Empty Form Can not be Save');
    //setTimeout(function () { }, 1500);
    $("#loading").fadeOut()
}



function Reset() {


    $("#ddlDomain").val("-1");
    $("#divCategoryDropDown").empty();
    $("#divSubCategoryDropDown").empty();
    $("#divform").empty();


}





//function DomainOnchage(url) {

//     //debugger;
//    var Domid = $("#ddlDomain").val();

//    var ControlArray;

//    ControlArray = {
//        "Store": false,
//        "Market": false



//    }


//    $.ajax({
//        type: "POST",

//        url: url,
//        //  url: '@Url.Action("GetSearchFormRecord", "ExecutiveBoard" )',
//        data: { DomainID: Domid },
//        datatype: "application/json",
//        success: function (data) {
//            html += ' <div class="row">';
//             //debugger;
//            if (data != '') {

//                $.each(data, function (i, item) {
//                    //Check Genaric List of Dropdown



//                    var Store = item.AttributesName.includes("Store"); 
//                    if (Store ==  true) {
//                        ControlArray["Store"] = Store;
//                    }

//                    if (item.IsViewable == "0") {

//                        html += '<input type="hidden" id=ArrId' + item.AttributesName + '  name=Arr' + item.AttributesName + ' value="'+ item.AttributeId + '"/>'
//                    }


//                    if (item.ControlTypeId == "6") {
//                        if (item.IsViewable == "1") {
//                            html += ' <div class="col-md-4 col-lg-3 col-sm-4"><select id=ddl' + item.AttributesName + '  name=' + item.AttributesName + ' class="form-control selectclass DropDownAlignment"> <option value="" disabled="" selected="">Select ' + item.AttributesName + ' </option>  </select> </div>';
//                            html += '<input type="hidden" id=ArrId' + item.AttributesName + '  name=Arr' + item.AttributesName + ' value= "' + item.AttributeId + '"/>'
//                        }
//                    }

//                    if (item.ControlTypeId == "11") {
//                        if (item.IsViewable == "1") {
//                            html += '<div class="col-md-4 col-lg-3 col-sm-4"><input id=txt' + item.AttributesName + '  name=' + item.AttributesName + ' class="form-control TxtboxAlignment" placeholder=' + item.AttributesName + '> </div>';
//                            html += '<input type="hidden" id=ArrId' + item.AttributesName + '  name=Arr' + item.AttributesName + ' value= "' + item.AttributeId + '"/>'
//                        }

//                    }
//                    if (item.ControlTypeId == "15") {

//                        if (item.IsViewable == "1") {
//                            html += '<div class="col-md-4 col-lg-3 col-sm-4"><input id="DatePicker" name=' + item.AttributesName + ' type="date" class="form-control DatePickerAlignment" > </div>';
//                            html += '<input type="hidden" id=ArrId' + item.AttributesName + '  name=Arr' + item.AttributesName + ' value= "' + item.AttributeId + '"/>'
//                        }

//                    }




//                });
//            }

//            //  html += '<div class="col-md-4 col-lg-4 col-sm-4"><input id="btnSubmit" type="submit"></div>'

//            html += '</div>'
//            $("#divform").append(html);
//            html = "";
//            CheckGenericDropdown(ControlArray)


//        }

//    });

//}





//function GetAssignebind() {
//     //debugger;
//    $.ajax({
//        type: "Get",
//        contentType: "application/json; charset=utf-8",
//        url: "/TaskManagerRpt/GetAccessData",
//        //data: "{}",
//        datatype: "application/json",
//        success: function (Result) {

//            $.each(Result, function (key, value) {


//                $("#ddlAssignTo").append($("<option></option>").val(value.Accessid + '/' + value.AccessName).html(value.AccessName));
//            });

//            changeStore();
//        },
//        error: function (Result) {
//            alert("Error");
//        }
//    });


//}





//function GetStorebind() {
//     //debugger;
//    $.ajax({
//        type: "Get",
//        contentType: "application/json; charset=utf-8",
//        url: "/TaskManagerRpt/Getstore",
//        //data: "{}",
//        datatype: "application/json",
//        success: function (Result) {

//            $.each(Result, function (key, value) {


//                $("#ddlStore").append($("<option></option>").val(value.storID + '/' + value.storeName).html(value.storeName));
//            });

//            changeStore();
//        },
//        error: function (Result) {
//            alert("Error");
//        }
//    });


//}

