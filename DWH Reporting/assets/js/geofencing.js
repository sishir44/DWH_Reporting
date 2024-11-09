function GetCoordinatesByStoreID(storeID, module, callback) {
    $.ajax({
        type: "GET",
        url: urlget,
        data: { storeID: storeID },
        dataType: "json",
        success: function (data) {
            var dataTable = JSON.parse(data);
            var results = [];

            function processRow(i) {
                if (i < dataTable.length) {
                    var row = dataTable[i];

                    if (row.RangeAlert == "1") {
                        Geofencing(row.StoreLatitude, row.StoreLongitude, storeID, module, row.UserAccuracy, function (result) {
                            // alert('Result from Geofencing: ' + result[1]);
                            callback([result[0], result[1]]);
                            results.push(result);
                            processRow(i + 1); // Process the next row
                        });
                    } else {
                        Geofencing(row.StoreLatitude, row.StoreLongitude, storeID, module, row.UserAccuracy, function (result) {
                            // alert('Result from Geofencing: ' + result[1]);
                            callback([result[0], result[1]]);
                            results.push(result);
                            processRow(i + 1); // Process the next row
                        });
                        //alert('Nothing');
                        processRow(i + 1); // Process the next row
                    }
                } else {
                    callback(["0", "0"]);
                    //return "1";
                    // callback([""], [""]);
                    // All rows processed, do something with the results if needed
                }
            }

            processRow(0); // Start processing the first row
        },
        error: function (xhr, status, error) {
            // Handle any errors here
            console.error("Error:", status, error);
        }
    });
}

// Rest of your code...

function Geofencing(Lat, Long, storeID, module, UserAccuracy, callback) {
    // Your existing Geofencing code...
    var msg = "";
    var res = "";
    var err = "";
    var userAgent = "";
    var browserName = "";
    var isJavaScriptEnabled = (typeof window !== 'undefined' && typeof window.navigator !== 'undefined' && typeof window.navigator.userAgent !== 'undefined');

    if (isJavaScriptEnabled) {
        // Get the user agent string
        userAgent = navigator.userAgent;

        // Get the browser's name and version
        browserName = getBrowserName(userAgent);
    }
    // Display the information

    if ("geolocation" in navigator) {
        const options = {
            enableHighAccuracy: true, // Enable high accuracy mode
            timeout: 5000, // Maximum time to wait for location data (in milliseconds)
            maximumAge: 0 // Maximum age of cached location data (0 means no cache)
        };

        navigator.geolocation.getCurrentPosition(function (position) {
            const userLatitude = position.coords.latitude;
            const userLongitude = position.coords.longitude;
            UserAccuracy = position.coords.accuracy; // Accuracy in meters

            // Replace these coordinates with the desired location for comparison
            const targetLatitude = Lat; // Example latitude
            const targetLongitude = Long; // Example longitude

            // Calculate the distance between the user's location and the target location
            const distance = calculateDistance(userLatitude, userLongitude, targetLatitude, targetLongitude);


            if (distance <= UserAccuracy) {
                // alert(`The user's accuracy (${userAccuracy} meters) is greater than or equal to the distance (${distance} meters).`);
                res = "1";
                msg = `The user's accuracy for (${module}) (${UserAccuracy} meters) is greater than or equal to the distance (${distance} meters).`


            } else {
                //alert(`The user's accuracy (${userAccuracy} meters) is less than the distance (${distance} meters).`);
                res = "0";
                msg = `The user's accuracy for (${module}) (${UserAccuracy} meters) is less than the distance (${distance} meters).`;

            }
            var obj = {
                StoreID: storeID,
                StoreLatitude: targetLatitude,
                StoreLongitude: targetLongitude,
                UserLatitude: userLatitude,
                UserLongitude: userLongitude,
                UserAccuracy: UserAccuracy,
                Distance: distance,
                Message: msg,
                InRadius: res,
                Module: module,
            }
            GeoFeningDetailsAdded(obj);
            callback([obj.Message, obj.InRadius]);

        }, function (error) {
            // Handle errors
            switch (error.code) {
                case error.PERMISSION_DENIED:
                    //alert("User denied the request for Geolocation.");
                    res = "0";
                    msg = "User denied the request for Geolocation.";
                    err = "User denied the request for Geolocation.";

                    break;
                case error.POSITION_UNAVAILABLE:
                    //alert("Location information is unavailable.");
                    res = "0";
                    msg = "Location information is unavailable.";
                    err = "Location information is unavailable.";

                    break;
                case error.TIMEOUT:
                    //alert("The request to get user location timed out.");
                    res = "0";
                    msg = "The request to get user location timed out.";
                    err = "The request to get user location timed out.";

                    break;
                case error.UNKNOWN_ERROR:
                    //alert("An unknown error occurred.");
                    res = "0";
                    msg = "An unknown error occurred.";
                    err = "An unknown error occurred.";

                    break;
            }
            var obj = {
                StoreID: storeID,
                StoreLatitude: Lat,
                StoreLongitude: Long,
                UserLatitude: "0",
                UserLongitude: "0",
                UserAccuracy: "0",
                Distance: "0",
                Message: msg,
                InRadius: res,
                Module: module,
            }
            GeoFeningDetailsAdded(obj);
            callback([obj.Message, obj.InRadius]);
        }, options);
    } else {
        //alert("Geolocation is not available in your browser.");
        res = "0";
        msg = "Geolocation is not available in your browser.";
        err = "Geolocation is not available in your browser.";
        var obj = {
            StoreID: storeID,
            StoreLatitude: Lat,
            StoreLongitude: Long,
            UserLatitude: "0",
            UserLongitude: "0",
            UserAccuracy: "0",
            Distance: "0",
            Message: msg,
            InRadius: res,
            Module: module,
        }
        GeoFeningDetailsAdded(obj);
        callback([obj.Message, obj.InRadius]);
    }
    // Inside your success and error callbacks, call the callback function with the result

}
function getBrowserName(userAgent) {
    var browserName = '';
    if (userAgent.indexOf('MSIE') !== -1) {
        browserName = 'Internet Explorer';
    } else if (userAgent.indexOf('Firefox') !== -1) {
        browserName = 'Firefox';
    } else if (userAgent.indexOf('Chrome') !== -1) {
        browserName = 'Chrome';
    } else if (userAgent.indexOf('Safari') !== -1) {
        browserName = 'Safari';
    } else if (userAgent.indexOf('Opera') !== -1 || userAgent.indexOf('OPR') !== -1) {
        browserName = 'Opera';
    } else if (userAgent.indexOf('Edge') !== -1) {
        browserName = 'Edge';
    }
    return browserName;
}

function GeoFeningDetailsAdded(obj) {
    // alert(obj.Message);
    //var url = '@Url.Action("GeoFeningDetailsAdded", "OptimizedCheckList")';
    $.ajax({
        type: "POST",
        url: urlpost,
        data: JSON.stringify(obj),
        contentType: "application/json",
        success: function (data) {
            // Handle success if needed
        },
        error: function (xhr, status, error) {
            // Handle any errors here
            console.error("Error:", status, error);
        }
    });
    // return [obj.Message, obj.InRadius];

}
function calculateDistance(lat1, lon1, lat2, lon2) {
    // Use the Haversine formula to calculate the distance between two coordinates
    const earthRadius = 6371000; // Earth's radius in meters
    const dLat = toRadians(lat2 - lat1);
    const dLon = toRadians(lon2 - lon1);
    const a =
        Math.sin(dLat / 2) * Math.sin(dLat / 2) +
        Math.cos(toRadians(lat1)) * Math.cos(toRadians(lat2)) *
        Math.sin(dLon / 2) * Math.sin(dLon / 2);
    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    return earthRadius * c;
}

function toRadians(degrees) {
    return degrees * (Math.PI / 180);
}