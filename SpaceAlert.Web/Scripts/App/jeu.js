$(function () {
    $(".draggable").draggable({
        containment: "#plateau"
    });

    $(".draggable").on("mouseenter", function() {
        $(this).attr("src", "../Content/Medias/Guss/selectedblueguss.png");
    });

    $(".draggable").on("mouseleave", function () {
        $(this).attr("src", "../Content/Medias/Guss/blueguss.png");
    });
})