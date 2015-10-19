var progress;
$(document).ready(function() {
    window.dt = $("#table").DataTable({
        "ajax": {
            "url": "User/GetData",
            "type": "POST",
            "dataType": "json"
        },
        "columns": [
            { "data": "Account" },
            { "data": "Id" },
            { "data": "Name" },
            { "data": "Class" },
          //  { "data": "Major" },
          //  { "data": "Period" },
            { "data": "Phone" },
            { "data": "Email" },
            {
                "data": function(data) {
                    if (data.TypeUser == "1") {
                        return '<span class="label label-warning">Employee</span>';
                    } else if (data.TypeUser == "2") {
                        return '<span class="label label-success">Student</span>';
                    } else if (data.TypeUser == "3") {
                        return '<span class="label label-info">Instructor</span>';
                    }
                }
            },
            { "data": "CreateDate" },
            {
                "data": function(data, type, full, meta) {
                    if (data.IsActive) {
                        return "<div class=\"five\">" +
                            "<div class=\"button-wrap-two button-active\" onclick=\"toggleStatus(this)\" data-id=\"" + data.Account + "\" data-status=\"Deactivated\">" +
                            "<div class=\"button-bg-two\">" +
                            "<div class=\"button-out-two\">On</div>" +
                            "<div class=\"button-in-two\">Off</div>" +
                            "<div class=\"button-switch-two\"></div>" +
                            "</div>" +
                            "</div>" +
                            "</div>";
                    } else {
                        return "<div class=\"five\">" +
                            "<div class=\"button-wrap-two\" onclick=\"toggleStatus(this)\" data-id=\"" + data.Account + "\" data-status=\"Active\">" +
                            "<div class=\"button-bg-two\">" +
                            "<div class=\"button-out-two\">On</div>" +
                            "<div class=\"button-in-two\">Off</div>" +
                            "<div class=\"button-switch-two\"></div>" +
                            "</div>" +
                            "</div>" +
                            "</div>";
                    }

                }
            },
            {
                "data": function (data, type, full, meta) {
                    return " <button id=\"btnEdit\" class=\"btn btn-warning\" onclick=\"EditUser(this, event)\" data-id=\"" + data.Account + "\" class=\"glyphicon glyphicon-edit\">Edit</button>";
                }
            }
        ]
    });
    //$("#table_filter input").addClass("form-control input-sm");
    //  $("#table_length select").addClass("form-control");
    $("#li2").addClass("active");
    $("#li1").removeClass("active");
    $("#li3").removeClass("active");
    $("#li4").removeClass("active");
});

function Active(btn, event) {
    var id = $(btn).data("id");
    var status = $(btn).data("status");
    $.ajax({
        url: "User/ChangeStatus",
        type: "POST",
        data: { id: id, status: status },
        beforeSend: function() {
            StartProcessBar();
        },
        success: function(data) {
            if (data.success == false) {
                $.notify("You do not have permission.", "error", { position: "top center" });
            } else {
                $.notify("Change status of account successful", "success", { position: "top center" });
            }
            dt.ajax.reload(null, false);
        },
        complete: function() {
            EndProcessBar();
        }
    });
}

function toggleStatus(btn) {
    $(btn).toggleClass("button-active");
    Active(btn);
}

function ChangeRole(btn, event) {
    var id = $(btn).data("id");
    var status = $(btn).data("status");
    $.ajax({
        url: "User/ChangeRole",
        type: "POST",
        data: { id: id, status: status },
        beforeSend: function() {
            StartProcessBar();
        },
        success: function(data) {
            if (data.success == false) {
                $.notify("You do not have permission.", "error", { position: "top center" });
            } else {
                $.notify("Change role of account successful", "success", { position: "top center" });
            }
            dt.ajax.reload(null, false);
        },
        complete: function() {
            EndProcessBar();
        }
    });
}

function toggleChangeRole(btn) {
    $(btn).toggleClass("button-active");
    ChangeRole(btn);
}

function StartProcessBar() {
    $.blockUI();
    $("#processBar").removeClass("hide");
    progress = setInterval(function() {
        var $bar = $("#process");
        if ($bar.width() >= 1300) {
            clearInterval(progress);
            $(".progress").removeClass("active");
        } else {
            $bar.width($bar.width() + 550);
        }
    }, 800);
}

function EndProcessBar() {
    clearInterval(progress);
    $(".progress").removeClass("active");
    $("#process").removeAttr("style");
    $("#process").css("height", "2px");
    $("#processBar").addClass("hide");
    $.unblockUI();
}

$('#AddUser').on('click', function() {
    $('#AddModal').modal();
})

function EditUser(btn,event) {
    var id = $(btn).data('id');
    $.ajax({
        url: "User/GetUser",
        type: "POST",
        data: { id: id, status: status },
        success: function (data) {
            if (data.success == false) {
                $.notify("You do not have permission.", "error", { position: "top center" });
            } else {
                console.log(data);
                $('#eName').val(data.data.NameSt);
                $('#eClass').val(data.data.Class);
                $('#eAccount').val(data.data.Account);
                $('#eID').val(data.data.Id);
                $('#ePeriod').val(data.data.Period);
                $('#ePhone').val(data.data.Phone);
                $('#eEmail').val(data.data.Email);
                $('#EditModal').modal();
            }
        },
    });
}
