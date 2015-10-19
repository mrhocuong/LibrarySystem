$(document).ready(function () {
    window.dt = $("#example").DataTable({
        "ajax": {
            "url": "Borrow/GetData",
            "type": "POST",
            "dataType": "json"
        },
        "aoColumnDefs": [
                { "bVisible": true, "aTargets": [0, 1, 2, 3, 4, 5, 6] }
        ],
    });

    $("#example_filter input").addClass("form-control input-sm");
    $("#li4").addClass("active");
    $("#li1").removeClass("active");
    $("#li2").removeClass("active");
    $("#li3").removeClass("active");

});

$('#studentID').on('keyup', function (e) {
    var id = $('#studentID').val();
    $.ajax({
        url: "Borrow/GetDatabyStudentId",
        type: "POST",
        data: { id: id },
        success: function (data) {
            console.log(data);
            var user = data.data[0];
            $('#name').val(user.NameSt);
            $('#class').val(user.Class);
            $('#major').val(user.MajorSt);
            $('#phone').val(user.Phone);
            $('#email').val(user.Email);
            $('#role').val(user.TypeUser);
            for (var i = 1; i < data.data.length; i++) {
                dt.row.add([
                            data.data[i].ID,
                            data.data[i].Name,
                            data.data[i].Author,
                            data.data[i].BorrowDate,
                            data.data[i].ReturnDate,
                            data.data[i].Status,
                            '<button class="btn btn-danger btn-sm" onclick="removeRecord(this)" data-id="' + data.data[i].ID + '">Return</button'
                ]).draw();
            }
            $('#table').focus();
        }
    });
})

$('#BookId').on('keyup', function (e) {
    var id = $('#BookId').val();
    $.ajax({
        url: "Borrow/GetDatabyBookId",
        type: "POST",
        data: { id: id },
        success: function (data) {
            console.log(data);
            var user = data.data[0];
            $('#name').val(user.NameSt);
            $('#class').val(user.Class);
            $('#major').val(user.MajorSt);
            $('#phone').val(user.Phone);
            $('#email').val(user.Email);
            $('#role').val(user.TypeUser);
            for (var i = 1; i < data.data.length; i++) {
                dt.row.add([
                    data.data[i].ID,
                    data.data[i].Name,
                    data.data[i].Author,
                    data.data[i].BorrowDate,
                    data.data[i].ReturnDate,
                    data.data[i].Status,
                    '<button class="btn btn-danger btn-sm" onclick="removeRecord(this)" data-id="' + data.data[i].ID + '">Return</button'
                ]).draw();
            }
            $('#table').focus();
        }
    });
});

$('#studentID').on('keyup', function (e) {
    checkInput();
});
$('#BookId').on('keyup', function (e) {
    checkInput();
});

function checkInput() {
    var st = $('#studentID').val();
    var bk = $('#BookId').val();
    if (st != "") {
        $('#BookId').attr('disabled', 'disabled');

    } else {
        $('#BookId').removeAttr('disabled', 'disabled');
        dt.clear().draw();
    }
    if (bk != "") {
        $('#studentID').attr('disabled', 'disabled');
    } else {
        $('#studentID').removeAttr('disabled', 'disabled');
        dt.clear().draw();
    }
}
$('#borrowBtn').on('click', function (e) {
    $(this).addClass('active');
    $('#returnBtn').removeClass('active');
    $('#status').html('<center><h2>Borrow Book</h2></center>');
});
$('#returnBtn').on('click', function (e) {
    $(this).addClass('active');
    $('#borrowBtn').removeClass('active');
    $('#status').html('<center><h2>Return Book</h2></center>');
});
$('#clearbtn').on('click', function (e) {
    dt.clear().draw();
    $('input').val('');
    $('input').removeAttr('disabled', 'disabled');
});