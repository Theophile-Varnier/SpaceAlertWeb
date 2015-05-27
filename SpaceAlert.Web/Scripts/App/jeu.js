$(function () {
    $(".draggable").draggable({
        containment: "#plateau",
        revert: "invalid"
    });

    $(".draggable").on("mouseenter", function() {
        $(this).attr("src", "../Content/Medias/Guss/selectedblueguss.png");
    });

    $(".draggable").on("mouseleave", function () {
        $(this).attr("src", "../Content/Medias/Guss/blueguss.png");
    });

    $(".salle").droppable({
        drop: function (event, ui) {
            var target = $(this).children().children(".blue-container");
            ui.draggable.attr("style", "position: relative; left: 0px; top: 0px;");
            ui.draggable.appendTo(target);
        }
    });
})