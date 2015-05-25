$(function () {
    var roomHub = $.connection.waitHub;

    roomHub.client.addChatMessage = function (message) {
        $("#playerList tbody").append("<tr><td>" + message + "</td></tr>");
        if ($("#startGame").length > 0) {
            if ($("#playerList tbody tr").length == parseInt($("#nbPlayers")[0].innerHTML)) {
                $("#startGame").removeAttr("disabled");
            }
        }
    };

    roomHub.client.connectToGame = function () {
        window.location.href = $("#RedirectTo").val();
    };

    $.connection.hub.start().done(function () {
        roomHub.server.join($("#CreatedBy").val(), $("#Game_GameId").val());

        $("#startGame").on("click", function () {
            roomHub.server.start($("#Game_GameId").val());
        });
    });
})