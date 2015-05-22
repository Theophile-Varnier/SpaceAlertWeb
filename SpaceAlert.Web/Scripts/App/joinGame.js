$(function () {
    $.connection.hub.start().done(function() {
        $(".game-detail").on("dblclick", function(e) {
            $("#GameToJoin").val($(this).find(".game-id")[0].innerHTML);
            $("#ConnectionId").val($.connection.hub.id);
            $("#modalCharName").modal({
                backdrop: 'static',
                keyboard: false
            });
        });
    });
})