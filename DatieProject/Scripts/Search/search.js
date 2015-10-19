$(document).ready(function () {
    window.dt = $("#table").DataTable({
        "ajax": {
            "url": "Search/GetData",
            "type": "POST",
            "dataType": "json"
        },
        //"bFilter":false,
        "columns": [
            { "data": "ISBN" },
            { "data": "Name" },
            { "data": "Author" },
            { "data": "TypeBook" },
            { "data": "AvailableInVault" },
             {
                 "data": function (data) {

                     return ' <button class="btn btn-warning" data-id="' + data.ISBN + '" onclick="AddShop(this, event)">Order</button>'

                 }
             },
        ]
    });

    $("#li3").addClass("active");
    $("#li1").removeClass("active");
    $("#li2").removeClass("active");
    $("#li4").removeClass("active");
});
$('#searchBarbtn').on('click', function () {
    dt.search($('#searchBar').val()).draw();
})