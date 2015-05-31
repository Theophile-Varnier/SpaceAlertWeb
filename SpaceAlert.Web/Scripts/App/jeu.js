$(function () {
    $(".draggable").draggable({
        containment: "#plateau",
        revert: "invalid",
        addClasses: false
    });

    $("html").on("mouseup", function () {
        $('.menu').addClass("hidden");
        $('.menu').removeClass("open");
    });

    $("html").on("contextmenu", function (e) {
        e.preventDefault();
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

    $(".salle").on("mousedown", function (e) {
        if (e.which == 3) {
            $('.menu').attr('style', 'left:' + (e.pageX - 50) + 'px; top:' + (e.pageY - 50) + 'px;');
            $('.menu').removeClass('hidden');
            $('.menu').addClass("open");
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

    $(".menu-item").on("mouseenter", function () {
        $(this).addClass("chosen");
    });

    $(".menu-item").on("mouseleave", function () {
        $(this).removeClass("chosen");
    })

    var resizeCard = function () {
        var imgWidth = $(".carte img")[0].scrollWidth;
        var imgHeight = $(".carte img")[0].scrollHeight;
        $('.carte').attr("style", "width:" + imgWidth + "px; height:" + imgHeight + "px;");
    }
    resizeCard();
    $(window).resize(function () {
        resizeCard();
    })
})