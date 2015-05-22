$(function() {
    var roomHub = $.connection.waitHub;

    roomHub.client.addChatMessage = function (message) {
        console.log(message);
    };
})