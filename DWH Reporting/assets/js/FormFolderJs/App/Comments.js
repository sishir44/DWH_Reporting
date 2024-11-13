
var count = 0;
var profileId = $("#profileId").val();
var domainId = $("#domainId").val();

$.ajax(
    {
        type: "GET", //HTTP POST Method
        url: "getnotes",
        data: { "profileId": profileId, "domainId": domainId },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                if (i % 2 == 0) {
                    $("#chat").append("<div class='container sender'> <i class='fas fa-smile' style='font-size:36px'></i> <p>" + data[i] + "</p> <span class='time - right'></span> </div>");
                }
                else {
                    $("#chat").append("<div class='container darker'> <i class='fas fa-smile' style='font-size:36px'></i> <p>" + data[i] + "</p> <span class='time - left'></span> </div>");
                }
            }
        }
    });

$('#file').click(function () {
    // Checking whether FormData is available in browser
    var requestid = $('#comment').val();
    if (window.FormData !== undefined) {
        var fileUpload = $("#fileupload").get(0);
        var files = fileUpload.files;
        // Create FormData object
        var fileData = new FormData();
        // Looping over all files and add it to FormData object
        for (var i = 0; i < files.length; i++) { fileData.append(files[i].name, files[i]); }
        // Adding one more key to FormData object
        fileData.append('typeid', '9');
        fileData.append('requestid', requestid);
        fileData.append('domainId', domainId);
        fileData.append('profileId', profileId);

        $.ajax({
            url: 'UploadFiles',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,
            success: function (result) { alert(result); },
            error: function (err) { alert(err.statusText); }
        });
    } else { alert("FormData is not supported."); }
});

$('#image_btn').click(function () {
    // Checking whether FormData is available in browser

    var requestid = $('#comment').val();
    if (window.FormData !== undefined) {
        var fileUpload = $("#imageupload").get(0);
        var files = fileUpload.files;
        // Create FormData object
        var fileData = new FormData();
        // Looping over all files and add it to FormData object
        for (var i = 0; i < files.length; i++) { fileData.append(files[i].name, files[i]); }
        // Adding one more key to FormData object
        fileData.append('typeid', '7');
        fileData.append('requestid', requestid);
        fileData.append('domainId', domainId);
        fileData.append('profileId', profileId);

        $.ajax({
            url: 'UploadFiles',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,
            success: function (result) { alert(result); },
            error: function (err) { alert(err.statusText); }
        });
    } else { alert("FormData is not supported."); }
});

$('#video_btn').click(function () {
    // Checking whether FormData is available in browser
    var requestid = $('#comment').val();
    if (window.FormData !== undefined) {
        var fileUpload = $("#videoupload").get(0);
        var files = fileUpload.files;
        // Create FormData object
        var fileData = new FormData();
        // Looping over all files and add it to FormData object
        for (var i = 0; i < files.length; i++) { fileData.append(files[i].name, files[i]); }
        // Adding one more key to FormData object
        fileData.append('typeid', '10');
        fileData.append('requestid', requestid);
        fileData.append('domainId', domainId);
        fileData.append('profileId', profileId);

        $.ajax({
            url: 'UploadFiles',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,
            success: function (result) { alert(result); },
            error: function (err) { alert(err.statusText); }
        });
    } else { alert("FormData is not supported."); }
});

function addnote() {
    var notes = $("#inputsm").val();
    var dt = new Date();
    insert_note(notes);
    var time = "(" + dt.getDate() + "/" + dt.getMonth() + "/" + dt.getFullYear() + " " + dt.getHours() + ":" + dt.getMinutes() + ")" + " " + "By " + $('#session').val() + "---- " + " " + notes;
    $("#chat").append("<div class='container sender'> <p>" + time + "</p> </div>");
}

function insert_note(note) {
    var domainId = $("#domainId").val();
    var appId = $("#appId").val();
    var profileId = $("#profileId").val();
    var token_number = $("#tokennumber").val();
    var requestid = $("#comment").val();
    $.ajax(
        {
            type: "POST", //HTTP POST Method
            url: "insert_note",
            data: {
                "note": note,
                "profileId": profileId,
                "domainId": domainId,
                "appId": appId
            }
        });
}