$(function () {
    var roomHub = $.connection.waitHub;

    var isGameOwner = function () {
        return $("#IsGameOwner").val() == "True";
    };

    var nbPlayers = function () {
        return parseInt($("#nbPlayers")[0].innerHTML);
    };

    
    roomHub.client.addPlayer = function (message) {
        $("#playerList tbody").append("<tr><td>" + message + "</td></tr>");
        if (isGameOwner() && $("#playerList tbody tr").length == nbPlayers()) {
            $("#startGame").removeAttr("disabled");
        }
    };

    roomHub.client.enableConnectionToGame = function () {
        $("#startGame").removeAttr("disabled");
    };

    roomHub.client.addPlayerReady = function (nbPlayersReady) {
        $($(".modal-body .player-ready")[nbPlayersReady - 1]).addClass("text-success");
        if ($(".player-ready").length == nbPlayers()) {

        }
    };

    roomHub.client.connectToGame = function () {
        window.location.href = $("#RedirectTo").val();
    };

    $.connection.hub.start().done(function () {
        roomHub.server.join($("#CreatedBy").val(), $("#Game_GameId").val());

        $("#startGame").on("click", function () {
            if (isGameOwner()) {
                roomHub.server.start($("#Game_GameId").val());
            } else {
                roomHub.server.playerReady($("#Game_GameId").val());
            }
            $("#gameLoading").modal({
                backdrop: "static",
                keyboard: false
            });
        });
    });
})