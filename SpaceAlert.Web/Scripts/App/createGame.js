$(function () {

    $("#Game_NbJoueurs").on("change", function (e) {
        var selectedValue = parseInt($(e.target).val());
        if (selectedValue == 5) {
            $("#Game_NbAndroids").attr("disabled", "");
        } else {
            $("#Game_NbAndroids").removeAttr("disabled");
        }
        $("#Game_NbAndroids").empty();
        for (var i = 0; i < 6 - selectedValue; i++) {
            $("#Game_NbAndroids").append("<option>" + i + "</option>");
        }
    });

    $("#falseSubmit").on("click", function (e) {
        $("#modalCharName").modal({
            backdrop: 'static',
            keyboard: false
        });
    });
    $("#falseForm").on('submit', function (e) {
        e.preventDefault();
        if ($("#createdByName").val() != "") {
            $("#CreatedBy").val($("#createdByName").val());
            $("#creationForm").submit();
        }
    });
})