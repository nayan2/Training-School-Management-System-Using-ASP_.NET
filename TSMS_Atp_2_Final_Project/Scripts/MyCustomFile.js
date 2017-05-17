function CustomMessage(title,message,buttonlabel)
{
    var types = [BootstrapDialog.TYPE_DANGER];

    $.each(types, function (index, type) {
        BootstrapDialog.show({
            type: type,
            title: title,
            message: message,
            buttons: [{
                label: buttonlabel,
                action: function (dialogRef) {
                    dialogRef.close();
                }
            }]
        });
    });
}

function readURL(input, id) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(id)
                .attr('src', e.target.result)
                .width(250)
                .height(200);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function d() {
    $("[data-valmsg-for]").text("");
    if (document.getElementById('ifile_img').files.length == 0)
    {
        $("[data-valmsg-for='pic_path']").text("Picture Is required!");
    }
    if ($('#VendorName').val().trim() == "")
    {
        $("[data-valmsg-for='heading']").text("Vendor Name Is required!");
    }
    if($('#VendorDetails').val().trim() == "")
    {
        $("[data-valmsg-for='body']").text("Vendor Details Is required!");
    }
    if($("[data-valmsg-for]").text() == "")
    {
        if (confirm('Are to sure to add a new vendor?') == true)
        {
            $('#AddVendorForm').submit();
        }
    }
}

function uppercase(val) {
    val.value = val.value.toUpperCase();
}

function redirectToAnotherUrl(obj) {
    window.location.href = obj.value;
}

function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
}

function ShowBatchCode() {
    var batchcode = $('#BatchList').val();
    $('#StudentList tbody tr').remove();
    $.ajax({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        url: 'Batche/Getdata',
        data: JSON.stringify({ batchcode: batchcode }),
        success: function (data) {
            data.forEach(function (obj) {
                addStudentToTable(obj.UserId);
            });
        },
        error: function (result) {
            CustomMessage('Error', 'Something went Wrong! Try Again After Some Time!', 'Close');
        }
    });
}

function addStudentToTable(UserId) {
    $.ajax({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        url: 'Batche/UserDetailsAsUserId',
        data: JSON.stringify({ UserId: UserId }),
        success: function (obj) {
            var imglink = obj.pic_path.substring(2);
            $('#StudentList tbody').append("<tr><td>" + obj.fullname + "</td><td>" + obj.first_name + "</td><td>" + obj.last_name + "</td><td><img src=" + imglink + " height='80' width='80' style='border-radius:2px;'/></td><td>" + obj.company_name + "</td><td>" + obj.city + "</td><td>" + obj.phone_number + "</td><td><a href=mailto:" + obj.email + " >" + obj.email + "</a></td><td>" + obj.zip_code + "</td><td>" + obj.nationality + "</td><td>" + obj.sex + "</td><td>" + obj.religion + "</td><td>" + obj.blood_group + "</td><td>" + ToJavaScriptDate(obj.dob) + "</td><td>" + ToJavaScriptDate(obj.user_activation_date) + "</td><td>" + obj.user_active + "</td></tr>");
        },
        error: function (result) {
            CustomMessage('Error', 'Something went Wrong! addto Try Again After Some Time!', 'Close');
        }
    });
}


function FacultyName() {
    $("#faculty_name").val($("#first_name").val() + " " + $("#last_name").val());
}

function CheckDayInAWeek() {

    $('#1Day,#2Day,#DayInAWeekError').hide();

    if (!$('#DayInAWeek').val())
    {
        $('#DayInAWeekError').hide();
        $('#DayInAWeekError').html("");
    }
    if ($('#DayInAWeek').val() > 2 || $('#DayInAWeek').val() <= 0)
    {
        $('#DayInAWeekError').show();
        $('#DayInAWeekError').html('Invalid Input! Day In A Week Must BE Less Than 2 And Greater Than 0!');
    }
    else
    {
        if ($('#DayInAWeek').val() == 1)
        {
            $('#1Day').show();
        }
        else
        {
            $('#1Day').hide();
            $('#2Day').show();
        }
    }
}

function GenerateRoutine() {

    $('#totalRoutinefield').val("");
    $('#routine').val("");

    if ($('#DayInAWeek').val() == 1) {
        var Day1 = $('#Day1').val();

        var Time1 = $('#Time1').val();
        var Time2 = $('#Time2').val();

        var routine = Day1 + "-" + Time1 + " " + "to" + " " + Time2;

        $('#totalRoutinefield').val(routine);
        $('#routine').val(routine);
    }
    else if ($('#DayInAWeek').val() == 2) {
        var Day2 = $('#Day2').val();
        var Day3 = $('#Day3').val();

        var Time3 = $('#Time3').val();
        var Time4 = $('#Time4').val();
        var Time5 = $('#Time5').val();
        var Time6 = $('#Time6').val();

        var routine = Day2 + "-" + Time3 + " " + "to" + " " + Time4 + " " + "and" + " " + Day3 + "-" + Time5 + " " + "to" + " " + Time6;

        $('#totalRoutinefield').val(routine);
        $('#routine').val(routine);
    }
}
