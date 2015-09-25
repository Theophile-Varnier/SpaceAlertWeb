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

    $("#addCharacter").on("click", function (e) {
        $("#wait").removeClass("hidden");
        if ($("#newChar").val()) {
            $.ajax({
                method: "POST",
                url: createCharUrl,
                data: {
                    charName: $("#newChar").val()
                }
            }).success(function (newChar) {
                $("#CreatedBy").append($('<option>', {
                    value: newChar,
                    text: newChar
                }));
                $("#Game_Player_Name").val(newChar);
                $('#wait').addClass("hidden");
            });
        }
    });
})