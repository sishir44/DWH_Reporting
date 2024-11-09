

        //$(document).ready(function () {
        //  var year=  new Date().getFullYear();
        //    document.getElementById('effective_date').setAttribute("disabled", year + "-01-01");
        //});


        function savedate() {
            var id = $("#0011").val();
            if (id == 2 || id == 5 || id == 6) {
                var day = $('#effective_date1').val();
                var month = $('#effective_date2').val();
                var year = $('#effective_date3').val();
                $('#hidedate').val("");
                $('#hidedate').val(year + "-" + month + "-" + day);
            }
            if (id == 1 || id == 3 || id == 4) {

                $('#hidedate').removeData();
            }

        }

   





    function diss() {
        $('#submitBtn').attr('disabled', true);
    }


    function toast() {
        toastr.options.timeOut = 1500; // 1.5s
    toastr.success('Successfully Posted');
        setTimeout(function () {}, 3000);

}
    function toast1() {
        toastr.options.timeOut = 1500; // 1.5s
    toastr.warning('No Connection...Posting Failed!');
        setTimeout(function () {}, 1500);
}


        function getdetail() {

        var id = $("#0011").val();
        if (id == 2 || id == 5 || id == 6) {
            document.getElementById('effectivedateAll').style.display = "none";
        $('#hidedateall').attr('required', false);
        document.getElementById('effectivedate1n16').style.display = "block";
        $('#effectivedate1n16').attr('required', true);
    }
        else if (id == 1 || id == 3 || id == 4) {
            document.getElementById('effectivedate1n16').style.display = "none"; $('#effectivedate1n16').attr('required', false);
        document.getElementById('effectivedateAll').style.display = "block"; $('#hidedateall').attr('required', true);
    }
    $('#selected_employeee').prop('selectedIndex', 0);
    var controlid = 0;
    //alert(id);
    $.ajax(
            {

            type: "POST", //HTTP POST Method
        url: "getdetail", // Controller/View

                data: { //Passing data
            "typeid": id, //Reading text box values using Jquery

    },
                success: function (data) {

            $("#table1 .tr").remove();
        $("#table2 .tr").remove();
        //  no_indexes.empty();
                    if ($('#0011').val() == 2 || $('#0011').val() == 5) {
                        $('#table2').append('<div class=row id=chkrow><div class="form-group col-md-6 tr" id="checkTransfer"><label>Enable Store Transfer: </label><input class="form-check-input" type="checkbox" value="" onclick=checkfn() id="Check1" style="width: 70px;height: 18px;"><p style="color: red;padding-left: 16px;"><strong>Important!</strong> If you need to perform store transfer also, Please check the Enable Store Transfer Box.!</p></div></div>');
        }
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].controltypeID == "6") {
            $('#table1>.row').append('<div class="form-group col-md-6 tr" id="six"><label>' + data[i].Attribute_Name + ': <span style="color: red"></span></label><select required data-live-search="true" class="form-control"   id= t' + data[i].Attribute_UId + ' name=' + data[i].Attribute_UId + ' ><option> Select ' + data[i].Attribute_Name + ' required </option></select></div>');
        if (data[i].controltypeID == "6") {


        }
        }
                        if (data[i].controltypeID == "11") {
                            if (data[i].Attribute_UId == "22") {
            $('#table1>.row').append('<div class="form-group col-md-6 tr" id="eleven"><label>' + data[i].Attribute_Name + ': <span style="color: red">*</span></label><input required type="text" id= t' + data[i].Attribute_UId + ' name=' + data[i].Attribute_UId + '  class="form-control"></div>');
        }
                            else if (data[i].Attribute_UId == "35" || data[i].Attribute_UId == "36" || data[i].Attribute_UId == "37") {
            $('#table1>.row').append('<div class="form-group col-md-6 tr" id="nrm">' + data[i].Attribute_Name + ':<input type="text" id= t' + data[i].Attribute_UId + ' name=' + data[i].Attribute_UId + '  class="form-control" readonly="readonly"></div>');
        }
                            else {
            $('#table1>.row').append('<div class="form-group col-md-6 tr" id="eleven"><label>' + data[i].Attribute_Name + ': <span style="color: red">*</span></label><input required type="number" min="0" id= t' + data[i].Attribute_UId + ' name=' + data[i].Attribute_UId + '  class="form-control"></div>');

        }
    }
                        if (data[i].controltypeID == "1") {
                            if (data[i].Attribute_UId == "13") {
            $('#table1>.row').append('<div class="form-group col-md-6 tr" id="one"><label>' + data[i].Attribute_Name + ': <span style="color: red"></span></label><input  type="text" id= t' + data[i].Attribute_UId + ' name=' + data[i].Attribute_UId + '  class="form-control"></div>');
        }
                            else {

            $('#table1>.row').append('<div class="form-group col-md-12 tr" id="one"><label>' + data[i].Attribute_Name + ': <span style="color: red">*</span></label><Textarea required type="date" id=t' + data[i].Attribute_UId + ' name=' + data[i].Attribute_UId + '  class="form-control"></Textarea></div>');

        }
    }
                        if (data[i].controltypeID == "13") {

            $('#table1>.row').append('<div class="form-group col-md-6 tr" id="thirteen"><label>' + data[i].Attribute_Name + ': <span style="color: red">*</span></label><input required type="date" id=t' + data[i].Attribute_UId + ' name=' + data[i].Attribute_UId + ' required class="form-control"></div>');

        }
       
    }
}
});
}



    function puttypes() {

        if ($('#0011').val() == 3) {
            if ($('#t9').val() == "Voluntary") {

            $.ajax(
                {
                    type: "GET", //HTTP POST Method
                    url: "getVoluntaryType",
                    success: function (data) {
                        $('#t11').empty()
                        for (var i = 0; i < data.length; i++) {
                            $('#t11').append('<option value=' + data[i].ID + '>' + data[i].payTypeName + '</option>');
                        }
                    }
                });

        }
            if ($('#t9').val() == "InVoluntary") {

            $.ajax(
                {
                    type: "GET", //HTTP POST Method
                    url: "getInvoluntaryType",
                    success: function (data) {
                        $('#t11').empty()
                        for (var i = 0; i < data.length; i++) {
                            $('#t11').append('<option value=' + data[i].ID + '>' + data[i].payTypeName + '</option>');
                        }
                    }
                });

        }

    }


}
    function transferstore() {


         var empid = $('#selected_employeee').val();
         if ($('#0011').val() == 3) {
            //Employee Termination 3

            // $('#t9').empty()
            $('#t9').empty()
            $('#t9').append('<option selected="true" disabled="disabled" >Select Type</option>');
            $('#t9').append('<option value= Voluntary >  Voluntary </option>');
            $('#t9').append('<option value= InVoluntary >  InVoluntary  </option>');


        $('#t14').empty()
            $('#t14').append('<option value= 1 >  YES </option>');
$('#t14').append('<option  value= 0 Selected >  NO  </option>');


$('#t16').empty()
$('#t16').append('<option  value= 1 >  YES </option>');
$('#t16').append('<option  value= 0 Selected>  NO  </option>');



$('#t18').empty()
$('#t18').append('<option  value= 1 >  YES </option>');
$('#t18').append('<option  value= 0 Selected>  NO  </option>');
        }
if ($('#0011').val() == 1) {
    //EMPLOYEE STORE TRANSFER FROM 1
    $.ajax(
        {
            type: "GET", //HTTP POST Method
            url: "getstoresViaID",
            data: {
                "ID": empid
            },
            success: function (data) {
                $('#t1').empty()
                if (data.length != 0) {
                    for (var i = 0; i < data.length; i++) {

                        $('#t1').append('<option value=' + data[i].Store_UID + '>' + data[i].Store_Name + " (" + data[i].Store_UID + " " + data[i].TimeZone + ")" + '</option>');
                    }
                }
                else {
                    $('#t1').append('<option value=0> Not Available </option>');
                }
                storeTransferTo();
            }
        });
    //EMPLOYEE STORE TRANSFER TO 1
    function storeTransferTo() {
        $.ajax(
            {
                type: "GET", //HTTP POST Method
                url: "getstores",
                data: {
                    "ID": empid
                },
                success: function (data) {
                    $('#t2').empty()
                    //$('#t2').append(' <option value="" selected="">Select New Store </option>');
                    if (data.length != 0) {
                        for (var i = 0; i < data.length; i++) {

                            $('#t2').append('<option value=' + data[i].Store_UID + '>' + data[i].Store_Name + " (" + data[i].Store_UID + " " + data[i].TimeZone + ")" + '</option>');
                        }
                    }
                    else {
                        $('#t2').append('<option value=0> Not Available </option>');
                    }
                    getNextCommandingOfficer(1);
                }
            });
    }


}
if ($('#0011').val() == 2) {
    //EMPLOYEE PROMOTION FROM 2
    $.ajax(
        {
            type: "GET", //HTTP POST Method
            url: "getDesignationsViaID",
            data: {
                "ID": empid
            },
            success: function (data) {
                $('#t3').empty()
                if (data.length != 0) {

                    for (var i = 0; i < data.length; i++) {
                        $('#t3').append('<option value=' + data[i].ID + '>' + data[i].Name + '</option>');
                    }
                }
                else {
                    $('#t3').append('<option value=0> Not Available </option>');
                }
                PromotionTo();
            }
        });
    function PromotionTo() {
        //EMPLOYEE PROMOTION TO 2
        $.ajax(
            {
                type: "GET", //HTTP POST Method
                url: "getDesignations",
                data: {
                    "ID": empid,
                    "Key": "PT"
                },
                success: function (data) {
                    $('#t4').empty()
                    for (var i = 0; i < data.length; i++) {
                        $('#t4').append('<option value=' + data[i].ID + '>' + data[i].Name + '</option>');
                    }
                    if (data.length == 0) {
                        $('#t4').append('<option value=0>Not Available</option>');
                    }
                    PromotionToCurrentStore();
                }
            });
    }
    function PromotionToCurrentStore() {
        //EMPLOYEE PROMOTION 2 (CURRENT STORE)
        $.ajax(
            {
                type: "GET", //HTTP POST Method
                url: "getstoresViaID",
                data: {
                    "ID": empid
                },
                success: function (data) {
                    $('#t7').empty()
                    for (var i = 0; i < data.length; i++) {
                        $('#t7').append('<option value=' + data[i].Store_UID + '>' + data[i].Store_Name + " (" + data[i].Store_UID + " " + data[i].TimeZone + ")" + '</option>');
                    }
                    if (data.length == 0) {
                        $('#t7').append('<option value=0>Not Available</option>');
                    }
                    var checked = $('input[type=checkbox]').prop('checked');
                    if (checked) {
                        PromotionToNewStore(1);
                    }
                    else {
                        PromotionToNewStore(0);
                    }

                }
            });
    }
    function PromotionToNewStore(checked) {
        //EMPLOYEE PROMOTION 2 (NEW STORE)

        $.ajax(
            {
                type: "GET", //HTTP POST Method
                url: "getstores",
                data: {
                    "ID": empid
                },
                success: function (data) {
                    $('#t8').empty()
                    if (checked == 1) {
                        for (var i = 0; i < data.length; i++) {

                            $('#t8').append('<option value=' + data[i].Store_UID + '>' + data[i].Store_Name + " (" + data[i].Store_UID + " " + data[i].TimeZone + ")" + '</option>');
                        }
                        if (data.length == 0) {
                            var t7_selectedOptionText = $("#t7 option:selected").text();
                            var t7_selectedOptionValue = $("#t7 option:selected").val();
                            $('#t8').append('<option value=' + t7_selectedOptionValue + '>' + t7_selectedOptionText + '</option>');
                        }
                    }
                    else if (checked == 0) {
                        var t7_selectedOptionText = $("#t7 option:selected").text();
                        var t7_selectedOptionValue = $("#t7 option:selected").val();
                        $('#t8').append('<option value=' + t7_selectedOptionValue + '>' + t7_selectedOptionText + '</option>');
                    }
                    //$('#t8').append(' <option value="" selected="">Select New Store </option>');


                    getNextCommandingOfficer(2);
                }
            });
    }



}
if ($('#0011').val() == 5) {

    //$("#t29").prop("disabled", true);
    //$("#t30").prop("disabled", true);
    //EMPLOYEE DEMOTION FROM 5
    $.ajax(
        {
            type: "GET", //HTTP POST Method
            url: "getDesignationsViaID",
            data: {
                "ID": empid
            },
            success: function (data) {
                $('#t25').empty()
                for (var i = 0; i < data.length; i++) {
                    $('#t25').append('<option value=' + data[i].ID + '>' + data[i].Name + '</option>');
                }
                if (data.length == 0) {
                    $('#t25').append('<option value=0>Not Available</option>');
                }
                DemotionTo();
            }
        });
    function DemotionTo() {
        //EMPLOYEE DEMOTION TO 5
        $.ajax(
            {
                type: "GET", //HTTP POST Method
                url: "getDesignations",
                data: {
                    "ID": empid,
                    "Key": "DT"
                },
                success: function (data) {
                    $('#t26').empty()
                    for (var i = 0; i < data.length; i++) {
                        $('#t26').append('<option value=' + data[i].ID + '>' + data[i].Name + '</option>');
                    }
                    if (data.length == 0) {
                        $('#t26').append('<option value=0>Not Available</option>');
                    }
                    DemotionToCurrentStore();
                }
            });
    }
    function DemotionToCurrentStore() {
        //EMPLOYEE DEMOTION 5 (CURRENT STORE)
        $.ajax(
            {
                type: "GET", //HTTP POST Method
                url: "getstoresViaID",
                data: {
                    "ID": empid
                },
                success: function (data) {
                    $('#t29').empty()
                    for (var i = 0; i < data.length; i++) {
                        $('#t29').append('<option value=' + data[i].Store_UID + '>' + data[i].Store_Name + " (" + data[i].Store_UID + " " + data[i].TimeZone + ")" + '</option>');
                    }
                    if (data.length == 0) {
                        $('#t29').append('<option value=0>Not Available</option>');
                    }
                    var checked = $('input[type=checkbox]').prop('checked');
                    if (checked) {
                        DemotionToNewStore(1);
                    }
                    else {
                        DemotionToNewStore(0);
                    }
                }
            });
    }
    function DemotionToNewStore(checked) {
        //EMPLOYEE DEMOTION 5 (NEW STORE)
        $.ajax(
            {
                type: "GET", //HTTP POST Method
                url: "getstores",
                data: {
                    "ID": empid
                },
                success: function (data) {
                    $('#t30').empty()
                    if (checked == 1) {
                        //$('#30').append(' <option value="" selected="">Select New Store </option>');
                        for (var i = 0; i < data.length; i++) {

                            $('#t30').append('<option value=' + data[i].Store_UID + '>' + data[i].Store_Name + " (" + data[i].Store_UID + " " + data[i].TimeZone + ")" + '</option>');
                        }
                        if (data.length == 0) {
                            var t29_selectedOptionText = $("#t29 option:selected").text();
                            var t29_selectedOptionValue = $("#t29 option:selected").val();
                            $('#t30').append('<option value=' + t29_selectedOptionValue + '>' + t29_selectedOptionText + '</option>');
                        }
                    }
                    else if (checked == 0) {
                        var t29_selectedOptionText = $("#t29 option:selected").text();
                        var t29_selectedOptionValue = $("#t29 option:selected").val();
                        $('#t30').append('<option value=' + t29_selectedOptionValue + '>' + t29_selectedOptionText + '</option>');
                    }
                    getNextCommandingOfficer(3);
                }
            });
    }
}
if ($('#0011').val() == 6) {
    //EMPLOYEE PAY-CHANGE 6 (TYPE)
    $.ajax(
        {
            type: "GET", //HTTP POST Method
            url: "getPayChangeType",
            data: {
                "ID": empid
            },
            success: function (data) {
                $('#t31').empty()
                for (var i = 0; i < data.length; i++) {
                    $('#t31').append('<option value=' + data[i].ID + '>' + data[i].payTypeName + '</option>');
                }
                if (data.length == 0) {
                    $('#t31').append('<option value=0>Not Available</option>');
                }
            }
        });
}
    }
function checkfn() {
    var checked = $('input[type=checkbox]').prop('checked');
    var selectedOperation = $("#0011").val();
    if (checked) {
        if (selectedOperation == 2) {
            PromotionToNewStoreForCheck(1);
        }
        else if (selectedOperation == 5) {
            DemotionToNewStoreForCheck(1);
        }
    }
    else {
        if (selectedOperation == 2) {
            PromotionToNewStoreForCheck(0);
        }
        else if (selectedOperation == 5) {
            DemotionToNewStoreForCheck(0);
        }
    }
}
function PromotionToNewStoreForCheck(checked) {
    //EMPLOYEE PROMOTION 2 (NEW STORE)
    var empidd = $('#selected_employeee').val();
    $.ajax(
        {
            type: "GET", //HTTP POST Method
            url: "getstores",
            data: {
                "ID": empidd
            },
            success: function (data) {
                $('#t8').empty()
                if (checked == 1) {
                    for (var i = 0; i < data.length; i++) {

                        $('#t8').append('<option value=' + data[i].Store_UID + '>' + data[i].Store_Name + " (" + data[i].Store_UID + " " + data[i].TimeZone + ")" + '</option>');
                    }
                    if (data.length == 0) {
                        var t7_selectedOptionText = $("#t7 option:selected").text();
                        var t7_selectedOptionValue = $("#t7 option:selected").val();
                        $('#t8').append('<option value=' + t7_selectedOptionValue + '>' + t7_selectedOptionText + '</option>');
                    }
                }
                else if (checked == 0) {
                    var t7_selectedOptionText = $("#t7 option:selected").text();
                    var t7_selectedOptionValue = $("#t7 option:selected").val();
                    $('#t8').append('<option value=' + t7_selectedOptionValue + '>' + t7_selectedOptionText + '</option>');
                }
                //$('#t8').append(' <option value="" selected="">Select New Store </option>');


                getNextCommandingOfficer(2);
            }
        });
}
function DemotionToNewStoreForCheck(checked) {
    //EMPLOYEE DEMOTION 5 (NEW STORE)

    var empidd = $('#selected_employeee').val();
    $.ajax(
        {
            type: "GET", //HTTP POST Method
            url: "getstores",
            data: {
                "ID": empidd
            },
            success: function (data) {
                $('#t30').empty()
                if (checked == 1) {
                    //$('#30').append(' <option value="" selected="">Select New Store </option>');
                    for (var i = 0; i < data.length; i++) {

                        $('#t30').append('<option value=' + data[i].Store_UID + '>' + data[i].Store_Name + " (" + data[i].Store_UID + " " + data[i].TimeZone + ")" + '</option>');
                    }
                    if (data.length == 0) {
                        var t29_selectedOptionText = $("#t29 option:selected").text();
                        var t29_selectedOptionValue = $("#t29 option:selected").val();
                        $('#t30').append('<option value=' + t29_selectedOptionValue + '>' + t29_selectedOptionText + '</option>');
                    }
                }
                else {
                    var t29_selectedOptionText = $("#t29 option:selected").text();
                    var t29_selectedOptionValue = $("#t29 option:selected").val();
                    $('#t30').append('<option value=' + t29_selectedOptionValue + '>' + t29_selectedOptionText + '</option>');
                }
                getNextCommandingOfficer(3);
            }
        });
}
function getNextCommandingOfficer(id) {
    debugger;
    if (id == 1) {
        var nextStore = $('#t2').val();
        //$('#t35').attr('disabled', 'disabled');
    }
    else if (id == 2) {
        var nextStore = $('#t8').val();
        //$('#t36').attr('disabled', 'disabled');
    }
    else if (id == 3) {
        var nextStore = $('#t30').val();
        //$('#t36').attr('disabled', 'disabled');
    }
    var empid = $('#selected_employeee').val();


    $.ajax({
        type: "GET",
        url: "GetNextCommandingOfficer",
        data: {
            "EmpID": empid,
            "NextStoreID": nextStore
        },
        success: function (data) {
            debugger;
            if (id == 1) {
                $('#t35').val(data);
            }
            else if (id == 2) {
                $('#t36').val(data);
            }
            else if (id == 3) {
                $('#t37').val(data);
            }


        }
    });
}

        $(document).on('change', '#t9', function () {
            puttypes();

        });
    $(document).on('change', '#t2', function () {
            alert();
        getNextCommandingOfficer(1);
    });
    $(document).on('change', '#t8', function () {
            alert();
        getNextCommandingOfficer(2);
    });
    $(document).on('change', '#t30', function () {
            alert();
        getNextCommandingOfficer(3);
    });

