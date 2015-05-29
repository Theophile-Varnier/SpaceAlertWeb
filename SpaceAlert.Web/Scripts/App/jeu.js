$(function () {
    $(".draggable").draggable({
        containment: "#plateau",
        revert: "invalid",
        addClasses: false
    });

    $(".draggable").on("mouseenter", function () {
        var source = "../Content/Medias/Guss/selected" + $(this).attr("data-character") + "guss.png";
        $(this).attr("src", source);
    });

    $(".draggable").on("mouseleave", function () {
        var source = "../Content/Medias/Guss/" + $(this).attr("data-character") + "guss.png";
        $(this).attr("src", source);
    });

    $(".salle").droppable({
        drop: function (event, ui) {
            var target = $(this).children().children("." + ui.draggable.attr("data-character") + "-container");
            ui.draggable.attr("style", "position: relative;");
            ui.draggable.appendTo(target);
        }
    });

    $(".carte").on("mouseenter", "img", function (e) {
        $(this).parent().children(".menu").removeClass("hidden");
    });

    $(".carte").on("mouseleave", function (e) {
        if ($(this).children().children('.menu-open:checked').length == 0) {
            $(this).children(".menu").addClass("hidden");
        }
    });

    $("a.reverse-item").on("click", function () {
        $(this).parent().parent().children("img").toggleClass("reverse");
        $(this).parent().children('.menu-open').attr("checked", false);
    });
})