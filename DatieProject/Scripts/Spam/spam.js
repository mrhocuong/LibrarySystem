var progress;
$(document).ready(function() {
    window.dt = $("#table").DataTable({
        "ajax": {
            "url": "Spam/GetData",
            "type": "POST",
            "dataType": "json"
        },
        "columns": [
            { "data": "IdCmt" },
            { "data": "Username" },
            { "data": "IdShop" },
            { "data": "NameShop" },
            { "data": "Comment" },
            { "data": "DateCmt" },
            {
                 "data": function (data, type, full, meta) {
                     return " <button id=\"btnDelete\" class=\"btn btn-danger\" onclick=\"DeleteComment(this, event)\" data-id=\"" + data.IdCmt + "\" class=\"glyphicon glyphicon-edit\">Delete</button>";
                 }
            }
        ],
        "columnDefs": [
           {
               "targets": [0],
               "visible": false
           }
        ],
        "order": [[0, "desc"]]
    });
    $("#table_filter input").addClass("form-control input-sm");
    //  $("#table_length select").addClass("form-control");
    $("#li3").addClass("active");
    $("#li1").removeClass("active");
    $("#li2").removeClass("active");
});

function DeleteComment(btn, event) {
    var id = $(btn).data("id");
    $.ajax({
        url: "Spam/DeleteComment",
        type: "POST",
        data: { id: id },
        beforeSend: function() {
            StartProcessBar();
        },
        success: function (data) {
            if (data.success) {
                $.notify("Delete comment successful!", 'success', { position: "top center" });
            } else {
                $.notify("Delete comment fail. Try Again!", 'error', { position: "top center" });
            }
            dt.ajax.reload(null, false);
        },
        complete: function() {
            EndProcessBar();
        }
    });
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
    $("#process").css('height', '2px');
    $("#processBar").addClass("hide");
    $.unblockUI();
}