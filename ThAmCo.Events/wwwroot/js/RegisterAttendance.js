

$(document).ready(function () {

    $('#RegisterAttendance').click(function () {
        alert("changed");

        validateCheckboxes();
    
        
        });

});


function validateCheckboxes() {
    
    var checkboxes = document.getElementsByName('RegisterAttendance').checked;
    var checkboxValues = [];
    for (var i = 0, n = checkboxes.length; i < n; i++) {
        if (checkboxes[i].checked) {
            checkboxValues.push(checkboxes[i].value);

        }
        else {
            alert("didnt work")
        }
    }
    var data = { checkboxValues: checkboxValues };
    alert("data is");

    $.ajax({
        url: "https://localhost:7004/Guests/RegisterAttendance",
        type: "POST",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d == true) {
                alert("it worked");
            }
        },
        error: function () {
            alert("Didnt work");
        }

    })
    
}






