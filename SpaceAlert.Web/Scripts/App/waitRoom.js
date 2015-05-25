$(function () {
    var roomHub = $.connection.waitHub;

    roomHub.client.addChatMessage = function (message) {
        $("#playerList tbody").append("<tr><td>" + message + "</td></tr>");
    };

    $.connection.hub.start().done(function () {
        roomHub.server.join($("#CreatedBy").val(), $("#Game_GameId").val());
    });
})