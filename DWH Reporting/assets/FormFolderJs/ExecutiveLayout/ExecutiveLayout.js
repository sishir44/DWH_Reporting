


function getSchRecord(ajaxPostUrl) {
    
    $.ajax({
        type: "POST",

        url: ajaxPostUrl,
        //  url: '@Url.Action("GetSearchFormRecord", "ExecutiveBoard" )',

        datatype: "application/json",
        success: function (data) {
            
            var dictionary = new Array();
            $.each(data, function (i, item) {
                dictionary.push({
                    data: item.FormPath,
                    value: item.FormName
                });
            });
   



            searchbox(dictionary);
        }

    });

}



$("#autocomplete").focusout(function () {
    $('#autocomplete').val("");

});


function searchbox(myJsonString) {
    debugger

    $('#autocomplete').autocomplete({
        position: { my: "right top", at: "right bottom" },
        lookup: myJsonString,


        onSelect: function (suggestion) {
            debugger;
            window.location = suggestion.data
            $('#autocomplete').val("");

            //window.open(suggestion.data);
            //$('#autocomplete').val("");
            //alert('You selected: ' + suggestion.value + ', ' + suggestion.data);
        }
    });
}
