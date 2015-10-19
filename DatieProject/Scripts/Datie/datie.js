var progress;
$(document).ready(function() {
    window.dt = $("#example").DataTable({
        "ajax": {
            "url": "Datie/GetData",
            "type": "POST",
            "dataType": "json"
        },
        "columns": [
            //{ "data": "ISBN" },
            { "data": "ID" },
            { "data": "Name" },
            { "data": "Author" },
            { "data": "Description" },
            { "data": "TypeBook" },
            { "data": "Major" },
            { "data": "AvailableInVault" },
            {
                "data": function(data) {
                    if (data.IsDelete) {
                        return '<span class="label label-warning">Deleted</span>';
                    } else {
                        return "";
                    }
                }
            },
             {
                 "data": function (data) {
                     if (data.IsBorrow) {
                         return '<span class="label label-info">Borrow</span>';
                     } else {
                         return "";
                     }
                 }
             },
            {
                "data": function (data, type, full, meta) {
                    return " <button id=\"btnEdit\" class=\"btn btn-warning\" onclick=\"EditShop(this, event)\" data-id=\"" + data.ID + "\" class=\"glyphicon glyphicon-edit\">Edit</button>";
                }
            }
        ]
    });

    $("#example_filter input").addClass("form-control input-sm");
    $("#li1").addClass("active");
    $("#li2").removeClass("active");
    $("#li3").removeClass("active");
    $("#li4").removeClass("active");
});

function EditShop(btn, event) {
    var id = $(btn).data("id");
    $("#dialog").empty();
    $.ajax({
        url: "Datie/EditShop",
        type: "GET",
        data: { id: id },
   
        success: function(data) {
            $("#dialog").html(data);
            $("#EditModal").find(".modal-body").css({
                width: "auto", //probably not needed
                height: "auto", //probably not needed 
                'max-height': "100%"
            });
            $("#EditModal").modal();
            $(".numeric").numeric();
        },
      
    });
}

function Edit(btn, event) {
    var check = $("#editForm").valid();
    if (check) {
        var model = $("#editForm").serialize();
        //get status checked of checkbox
        var str1 = $("#ShopIsDeleted").map(function() { return this.id + "=" + this.checked; }).get().join("&");
        if (str1 != "" && model != "") model += "&" + str1;
        else model += str1;
        $.ajax({
            url: "Datie/EditShop",
            type: "POST",
            data: model,
            beforeSend: function() {
                $("#EditModal").modal("hide");
                StartProcessBar();
            },
            success: function(data) {
                EndProcessBar();
                if (data.success) {
                    $.notify("Edit data success.", "success", { position: "top center" });
                } else {
                    $.notify("Edit data fail. Try Again!", "error", { position: "top center" });
                }
                dt.ajax.reload(null, false);
            }
        });

    }
}

function AddShop(btn, event) {
    $("#dialog").empty();
    $.ajax({
        url: "Datie/AddShop",
        type: "GET",
        success: function(data) {
            $("#dialog").html(data);
            $("#AddModal").find(".modal-body").css({
                width: "auto", //probably not needed
                height: "auto", //probably not needed 
                'max-height': "100%"
            });
            $("#AddModal").modal();
            $(".numeric").numeric();
        },
        
    });
}

function Add(btn, event) {
    var check = $("#addForm").valid();
    if (check) {
        var model = $("#addForm").serializeArray();
        var data = [];
        //get the input and UL list
        var input = document.getElementById("Image");
        //for every file...
        for (var x = 0; x < input.files.length; x++) {
            //add to list
            var FR = new FileReader();
            FR.onload = function(e) {
                console.debug(x);
                var encoded = e.target.result;
                data.push({ ImgId: x, ImgLink: encoded });
            };
            FR.readAsDataURL(input.files[x]);
        };
        console.debug("data image" + JSON.stringify(data));
        var jsonObj = { 'name': "Image", 'value': data };
        console.debug("jsonObject" + JSON.stringify(jsonObj));
        model.push(jsonObj);
        console.debug(model);
        $.ajax({
            url: "Datie/Add",
            type: "POST",
            datatype: "json",
            data: model,
            beforeSend: function() {
                $("#AddModal").modal("hide");
                StartProcessBar();
            },
            success: function(data) {
                if (data.success) {
                    $.notify("Add data success.", "success", { position: "top center" });
                } else {
                    $.notify("Add data fail. Try Again!", "error", { position: "top center" });
                }
                dt.ajax.reload(null, false);
            },
            complete: function() {
                EndProcessBar();
            }
        });
    }
}

function AddImage() {
    var formData = new FormData();
    var image = document.getElementById("Image").files;
    for (var i = 0; i < image.length; i++) {
        formData.append("file" + i, image[0]);
    }
    console.debug(formData);
    $.ajax({
        url: "Datie/AddImage",
        type: "POST",
        data: formData,
        dataType: "json",
        contentType: false,
        processData: false,
        success: function(data) {
            return data.success;
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
    $("#process").css("height", "2px");
    $("#processBar").addClass("hide");
    $.unblockUI();
}

$('#searchBar').on('keyup', function () {
    dt.search($('#searchBar').val()).draw();
})