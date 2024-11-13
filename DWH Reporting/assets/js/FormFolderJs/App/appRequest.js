
// variable Declare 
var html = "";
var appId = $("#appId").val();
// Genaric DropDown Variable 
var DropdownBind = false;
var Urlajax = "";

function SetUrl(url) {Urlajax = url;}

function DomainDropFun(appId) {

    var CreateDomain = false;

    $.ajax({
        type: "GET",
        url: "GetDomainDropDown",
        data: { AppId: appId },
        datatype: "application/json",
        success: function (data) {
           
            if (data != '') {
                html += ' <div class="row">';
                $.each(data, function (i, item) {
                    //Check Genaric List of Dropdown
                    if (CreateDomain == false) {
                        if (item.ControlTypeId == "6") {
                            html += ' <div class="col-md-4 col-lg-3 col-sm-4 "><label> Domain </label><select id="ddlDomain" onchange="DomainOnchage();" name="ddlDomain" class="form-control selectclass DropDownAlignment"> <option value="-1" disabled="" selected="">Select Domain </option>  ';
                        }
                        CreateDomain = true;
                    }
                    html += '<option value="' + item.DomainId + '" >' + item.DomainName + ' </option> ';
                });
                html += '</select> </div>';
                html += '</div>'
                $("#divDomainDropdown").append(html);
                html = "";
            }
            else {CategoryCreateWithNoDomain();}
        }
    });
}

function CategoryCreateWithNoDomain() {
     var CategoryWithnoDomain = false;

    $.ajax({
        type: "Post",
        url: "GetCategoryWithNoDomain",
        data: { APPID: appId },

        datatype: "application/json",
        success: function (data) {
            if (data != '') {
                html += ' <div class="row">';
                $.each(data, function (K, item) {
                    if (CategoryWithnoDomain == false) {
                        if (item.ControlTypeId == "6") {
                            html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label>Category</label><select id="DdlCategory" onchange="CategoryOnchage();" name="ddlCategory" class="form-control selectclass DropDownAlignment"> <option value="" disabled="" selected="">Select Category </option>  ';
                        }
                        CategoryWithnoDomain = true;
                    }
                    html += '<option value="' + item.CategoryId + '" >' + item.CategoryName + ' </option> ';
                });
                html += '</select> </div>';
                html += '</div>'
                $("#divCategoryDropDown").append(html);
            }
        }
    });
}

function DomainOnchage() {
     $('#loading').fadeIn();   
    var Domid = $("#ddlDomain").val();
    var CreateCategory = false;
    var ControlArray;
    var Flag;  
    var dict = [];
    var appId = $("#appId").val();

    $.ajax({
        type: "POST",
        url:  "GetCategoryDdl",
        data: {AppId: appId, DomainID: Domid },
        datatype: "application/json",
        success: function (data) {
            $("#divSubCategoryDropDown").empty();
            $("#divCategoryDropDown").empty();
            $("#divform").empty();          
            if (data != '') {
                $.each(data, function (I, item) {Flag = item.Flag.includes("Category");});

                if (Flag == true) {
                    html += ' <div class="row">';
                    $.each(data, function (K, item) {                  
                        if (CreateCategory == false) {
                            if (item.ControlTypeId == "6")
                            {

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
                        // Drop down
                        if (item.ControlTypeId == "6") {
                            if (item.IsViewable == "1") {
                                if (item.IsRequired == "1")
                                {
                                    html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red">*</span>' + item.AttributesName + '</label><select id=ddl' + item.AttributesName.replace(/ /g, '') + '  name="' + item.AttributeId + '"  class="form-control selectclass DropDownAlignment"> <option value="" disabled="" selected="">Select ' + item.AttributesName + ' </option>   </select> </div>';
                                }
                                else {
                                    html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red"></span>' + item.AttributesName + '</label><select id=ddl' + item.AttributesName.replace(/ /g, '') + '  name="' + item.AttributeId + '"  class="form-control selectclass DropDownAlignment"> <option value="" disabled="" selected="">Select ' + item.AttributesName + ' </option>   </select> </div>';
                                }
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
                                txt += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"> <label> <span style="color:red">*</span>' + item.AttributesName + '</label><input id="' + item.AttributesName.replace(/ /g, '') + '"  name="' + item.AttributeId + '" class="form-control TxtboxAlignment txtvalidation" placeholder="' + item.AttributesName + '" onkeyup="Validation(/' + item.RExpression + '/,this);"></div>';
                                txt += "</div>"
                                $("#divform").append(txt);
                                txt = "";
                            }
                            else {
                                var txt = "";
                                txt += '<div class="row">'
                                txt += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"> <label> <span style="color:red"></span>' + item.AttributesName + '</label><input id="' + item.AttributesName.replace(/ /g, '') + '"  name="' + item.AttributeId + '" class="form-control TxtboxAlignment txtvalidation" placeholder="' + item.AttributesName + '" onkeyup="Validation(/' + item.RExpression + '/,this);"></div>';
                                txt += "</div>"
                                $("#divform").append(txt);
                                txt = "";
                            }
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
                                MultiSelectDict.push({key: item.Spname,value: item.AttributesName.replace(/ /g, '')});
                            }
                        }

                    });
                    html += '</div>'
                    $("#divform").append(html);
                    $.each(dict, function (f, item) {Drpdownbind(item.key, item.value);});
                }
                $('#loading').fadeOut();

            }
            html = "";
            $('#loading').fadeOut();
        }
    });
}


function CategoryOnchage() {

    $('#loading').fadeIn();
    var Catid = $("#DdlCategory").val();
    var CreateSubCategory = false;

    $.ajax({
        url: "GetSubCategoryDdl",
        data: { CatID: Catid },
        datatype: "application/json",
        success: function (data) {
            $("#divSubCategoryDropDown").empty();
            $("#divform").empty();
            if (data != '') {
                html += ' <div class="row">';
                $.each(data, function (K, item) {
                    if (CreateSubCategory == false) {
                        if (item.SubControlTypeId == "6") {

                            html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label>SubCategory</label><select id="DdlSubCategory" onchange="SubCategoryOnchage();" name="ddlCategory" class="form-control selectclass DropDownAlignment"> <option value="" disabled="" selected="">Select SubCategory </option>  ';
                        }
                        CreateSubCategory = true;
                    }
                    html += '<option value="' + item.SubCategoryId + '" >' + item.SubCategoryName + ' </option> ';
                });

                html += '</select> </div>';
                html += '</div>'
                $("#divSubCategoryDropDown").append(html);
            }

            else {CreateAttribute($("#ddlDomain").val(), $("#DdlCategory").val(), appId)}
       
            html = "";
            $('#loading').fadeOut();
        }
    });
}


function SubCategoryOnchage() {
    
    var Catid = $("#DdlCategory").val();
    var DomainId = $("#ddlDomain").val();
    var SubCategory = $("#DdlSubCategory").val();
    var appId = $("#appId").val();
    CreateAttribute_With_Param(DomainId, Catid, appId, SubCategory)
}

function CreateAttribute_With_Param(DomainId, CatId, appId, SubCategory) {
      $('#loading').fadeIn();

    var dict = [];
    $.ajax({
        type: "POST",
        url: "AllAttributes_With_Param",
        data: { DomainID: DomainId, CATID: CatId, APPID: appId, SubCatId: SubCategory },
        datatype: "application/json",
        success: function (data) {
            $("#divform").empty();
            html += ' <div class="row">';
            if (data != '') {
                $.each(data, function (L, item) {
                    //Check Genaric List of Dropdown
                    // Drop down
                    
                    if (item.ControlTypeId == "6") {
                        if (item.IsViewable == "1") {
                            if (item.IsRequired == "1") {
                                html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red">*</span>' + item.AttributesName + '</label><select id=ddl' + item.AttributesName.replace(/ /g, '') + '  name="' + item.AttributeId + '"  class="form-control selectclass DropDownAlignment"> <option value="" disabled="" selected="">Select ' + item.AttributesName + ' </option>   </select> </div>';
                            }
                            else {
                                html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red"></span>' + item.AttributesName + '</label><select id=ddl' + item.AttributesName.replace(/ /g, '') + '  name="' + item.AttributeId + '"  class="form-control selectclass DropDownAlignment"> <option value="" disabled="" selected="">Select ' + item.AttributesName + ' </option>   </select> </div>';
                            }
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
                            txt += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"> <label> <span style="color:red">*</span>' + item.AttributesName + '</label><input id="' + item.AttributesName.replace(/ /g, '') + '"  name="' + item.AttributeId + '" class="form-control TxtboxAlignment txtvalidation" placeholder="' + item.AttributesName + '" onkeyup="Validation(/' + item.RExpression + '/,this);"></div>';
                            txt += "</div>"
                            $("#divform").append(txt);
                            txt = "";
                        }
                        else {
                            var txt = "";
                            txt += '<div class="row">'
                            txt += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"> <label> <span style="color:red"></span>' + item.AttributesName + '</label><input id="' + item.AttributesName.replace(/ /g, '') + '"  name="' + item.AttributeId + '" class="form-control TxtboxAlignment txtvalidation" placeholder="' + item.AttributesName + '" onkeyup="Validation(/' + item.RExpression + '/,this);"></div>';
                            txt += "</div>"
                            $("#divform").append(txt);
                            txt = "";
                        }
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
                            MultiSelectDict.push({key: item.Spname,value: item.AttributesName.replace(/ /g, '')});
                        }
                    }
                });
            }

            html += '</div>'
            $("#divform").append(html);
            html = "";
            $.each(dict, function (f, item) {Drpdownbind(item.key, item.value);});
            $('#loading').fadeOut();
        }

    });

}



function CreateAttribute(DomainId, CatId, AppId) {
    $('#loading').fadeIn();
    var Domid = DomainId;
    var catid = CatId;
    var ControlArray;
    var dict = [];
    var MultiSelectDict = [];
    var appId = $("#appId").val();

    $.ajax({
        type: "POST",
        url: "AllAttributes",
        data: { DomainID: Domid, CATID: catid, APPID: appId },
        datatype: "application/json",
        success: function (data) {
          
            $("#divform").empty();
            html += ' <div class="row">';
            if (data != '') {
                $.each(data, function (L, item) {
                    //Check Genaric List of Dropdown
                    // Drop down
                    
                    if (item.ControlTypeId == "6") {
                        if (item.IsViewable == "1") {

                            if (item.IsRequired == "1") {
                                html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red">*</span>' + item.AttributesName + '</label><select id=ddl' + item.AttributesName.replace(/ /g, '') + '  name="' + item.AttributeId + '"  class="form-control selectclass DropDownAlignment"> <option value="" disabled="" selected="">Select ' + item.AttributesName + ' </option>   </select> </div>';
                            }
                            else {
                                html += ' <div class="col-md-4 col-lg-3 col-sm-4 columeclass"><label><span style="color:red"></span>' + item.AttributesName + '</label><select id=ddl' + item.AttributesName.replace(/ /g, '') + '  name="' + item.AttributeId + '"  class="form-control selectclass DropDownAlignment"> <option value="" disabled="" selected="">Select ' + item.AttributesName + ' </option>   </select> </div>';
                            }

                            dict.push({key: item.Spname,value: item.AttributesName.replace(/ /g, '')});
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
                            txt += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"> <label> <span style="color:red">*</span>' + item.AttributesName + '</label><input id="' + item.AttributesName.replace(/ /g, '') + '"  name="' + item.AttributeId + '" class="form-control TxtboxAlignment txtvalidation" placeholder="' + item.AttributesName + '" onkeyup="Validation(/' + item.RExpression + '/,this);"></div>';
                            txt += "</div>"
                            $("#divform").append(txt);
                            txt = "";
                        }
                        else {
                            var txt = "";
                            txt += '<div class="row">'
                            txt += '<div class="col-md-4 col-lg-3 col-sm-4 columeclass"> <label> <span style="color:red"></span>' + item.AttributesName + '</label><input id="' + item.AttributesName.replace(/ /g, '') + '"  name="' + item.AttributeId + '" class="form-control TxtboxAlignment txtvalidation" placeholder="' + item.AttributesName + '" onkeyup="Validation(/' + item.RExpression + '/,this);"></div>';
                            txt += "</div>"
                            $("#divform").append(txt);
                            txt = "";
                        }
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
                            MultiSelectDict.push({key: item.Spname,value: item.AttributesName.replace(/ /g, '')});
                        }
                    }
                });
            }

            html += '</div>'
            $("#divform").append(html);
            html = "";
            $.each(dict, function (f, item) {Drpdownbind(item.key, item.value);});
            //  CheckGenericDropdown(ControlArray)
            $.each(MultiSelectDict, function (f, item) {MultiSelectDropBind(item.key, item.value);});
            $('#loading').fadeOut();
        }

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
        data: { Spname: StoreProName },
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

function MultiSelectDropBind(spname, ControlId) {}

function Insert(Url) {
    $('#loading').fadeIn();
    var DomID = $("#ddlDomain").val();
    var StoreID = $("#ddlStore").val();
    var APPId = $("#appId").val();
    var CatId = $("#DdlCategory").val();
    var SubCatId = $("#DdlSubCategory").val();
    var fileUpload = $(":input[type=file]").get(0);

    if (fileUpload != undefined && fileUpload != "") {var files = fileUpload.files;}
    
    var form = $("#form1")[0];
    var formData = new FormData(form);

    formData.append("DomainId", DomID);
    formData.append("App", APPId);
    formData.append("CATId", CatId);
    formData.append("SubCatId", SubCatId);
    formData.append("Storeid", StoreID);

    $.ajax({
        type: "POST",
        data: formData,
        url: Url,
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data  
        success: function (data) {           
            if (data != "") {
                if (data == "-1") {ErrorMessage();}
                else if (data == "Empty") {EmptyMessage();}
                else {Successfully();Reset();} 
            }
            $('#loading').fadeOut();
        }
    });

}


//, expression

function Validation(Reg, val) {
    var id = val.getAttribute('id');
    var value = val.value;
    var Controlid = "#" + id;
    if (Reg.test(value)) { } else { $(Controlid).val(""); }   
}

// Popup message

function Successfully() {
    toastr.options = {
        positionClass: 'MessageSide'
    };
    toastr.success('Successfully Submitted');
    $("#loading").fadeOut()
}
function ErrorMessage() {
    toastr.options = {
        positionClass: 'MessageSide'
    };
    toastr.warning('No Connection...Posting Failed!');
    $("#loading").fadeOut()
}


function EmptyMessage() {
    toastr.options = {
        positionClass: 'MessageSide'
    };
    toastr.warning('Empty Form Can not be Save');
    $("#loading").fadeOut()
}

function Reset() {
    $("#ddlDomain").val("-1");
    $("#divCategoryDropDown").empty();
    $("#divSubCategoryDropDown").empty();
    $("#divform").empty();
}

