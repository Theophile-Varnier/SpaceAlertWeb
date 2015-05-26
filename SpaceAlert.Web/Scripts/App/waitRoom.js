$(function () {
    var roomHub = $.connection.waitHub;

    var colorClasses = [
        "blue",
        "red",
        "green",
        "yellow",
        "purple"
    ];

    var getFirstFreeColor = function () {
        for (var i in colorClasses) {
            if ($("." + colorClasses[i]).length == 0) {
                return colorClasses[i];
            }
        }
        return "";
    };

    var isGameOwner = function () {
        return $("#IsGameOwner").val() == "True";
    };

    var nbPlayers = function () {
        return parseInt($("#nbPlayers")[0].innerHTML);
    };

    // Connexion d'un joueur à la partie
    roomHub.client.addPlayer = function (message) {
        $("#playerList tbody").append("<tr data-color-toggle='" + message + "'><td></td><td><span class='colored-square " + getFirstFreeColor() + "'></span></td><td>" + message + "</td></tr>");
        if (isGameOwner() && $("#playerList tbody tr").length == nbPlayers()) {
            $("#startGame").removeAttr("disabled");
        }
    };

    // Permet aux joueurs de lancer la partie lorsque
    // Tout le monde a rejoint la salle d'attente
    roomHub.client.enableConnectionToGame = function () {
        $("#startGame").removeAttr("disabled");
    };

    roomHub.client.notifyColorChanged = function (oldColor, newColor) {
        $("." + oldColor)
            .addClass(newColor)
            .removeClass(oldColor)
            .prev().addClass("no-display");
    };

    // Signalement d'un joueur prêt à jouer
    roomHub.client.addPlayerReady = function (nbPlayersReady) {
        $($(".modal-body .player-ready")[nbPlayersReady - 1]).addClass("text-success");
        if ($(".player-ready").length == nbPlayers()) {

        }
    };


    roomHub.client.connectToGame = function () {
        window.location.href = $("#RedirectTo").val();
    };

    $.connection.hub.start().done(function () {

        // Indique au serveur qu'un nouveau joueur a rejoint la salle d'attente
        roomHub.server.join($("#CreatedBy").val(), $("#Game_GameId").val());


        $("#startGame").on("click", function () {
            // Le propriétaire de la partie la lance
            if (isGameOwner()) {
                roomHub.server.start($("#Game_GameId").val());
            } else {
                // Les autres joueurs indiquent qu'ils sont prêts
                roomHub.server.playerReady($("#Game_GameId").val());
            }
            // Modal indiquant le nombre de joueurs prêts
            $("#gameLoading").modal({
                backdrop: "static",
                keyboard: false
            });
        });

        $("tr[data-color-toggle='" + $("#CreatedBy").val() + "']").on("click", ".colored-square", function () {
            var nextColor = getFirstFreeColor();
            if (nextColor != "") {
                $(this).prev().removeClass("no-display");
                roomHub.server.notifyColorChanged($("#Game_GameId").val(), $(this).attr('class').split(/\s+/)[1], nextColor);
            }
        });
    });
})