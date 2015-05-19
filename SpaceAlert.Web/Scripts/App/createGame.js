$(function () {
    $("#NbJoueurs").on("change", function (e) {
        var selectedValue = parseInt($(e.target).val());
        if (selectedValue == 5) {
            $("#NbAndroids").attr("disabled", "");
        } else {
            $("#NbAndroids").removeAttr("disabled");
        }
        $("#NbAndroids").empty();
        for (var i = 0; i < 6 - selectedValue; i++) {
            $("#NbAndroids").append("<option>" + i + "</option>");
        }
    });
})