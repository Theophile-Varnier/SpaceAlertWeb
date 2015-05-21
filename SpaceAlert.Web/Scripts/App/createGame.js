$(function () {
    var roomHub = $.connection.waitHub;

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

    $("#falseSubmit").on("click", function (e) {
        $("#modalCharName").modal({
            backdrop: 'static',
            keyboard: false
        });
    });
    $.connection.hub.start().done(function() {
        $("#falseForm").on('submit', function(e) {
            e.preventDefault();
            if ($("#createdByName").val() != "") {
                $("#CreatedBy").val($("#createdByName").val());
                $("#CreatorConnectionId").val($.connection.hub.id);
                    $("#creationForm").submit();
            }
        });
    });
})