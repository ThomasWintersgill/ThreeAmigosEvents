$(document).ready(function () {

    loadEventType();

});

function loadEventType() {
    $.ajax({
        url: "https://localhost:7088/api/EventTypes",
        type: "GET",
        crossDomain: true,
        data: "{}",
        success: function (result) {
            $('#EventType').html('');
            var options = '';
            options += '<option value="Select">Select</option>';
            for (var i = 0; i < result.length; i++) {
                options += 'option value = "' + result[i].Id + '">' +
                    result[i].Title + ", " +
                '</option>';
            }
            $('#EventType').append(options);

        },
        
        error: function () {
            alert("unable to load Event Types");
        }
    });
}

