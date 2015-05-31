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
        },
        accept: ".draggable"
    });

    $(".salle").on("mousedown", function (e) {
        if (e.which == 3) {
            $('.menu').attr('style', 'left:' + (e.pageX - 50) + 'px; top:' + (e.pageY - 50) + 'px;');
            $('.menu').removeClass('hidden');
            $('.menu').addClass("open");
        }
    });

    $(".carte").on("mouseup", function (e) {
        if (e.which == 3 && this.className.indexOf("posay") === -1) {
            $(this).find("img").toggleClass("reverse");
        }
    });

    $(".carte").draggable({
        container: "#playerInfo",
        revert: "invalid",
        addClasses: false
    });

    $('.carte-container').droppable({
        hoverClass: "hovered",
        accept: ".carte",
        drop: function (e, ui) {
            ui.draggable.addClass("posay");
            var targetwidth = $(this).width();
            var targetheight = $(this).height();
            ui.draggable.css("transform", "scale(" + targetwidth / ui.draggable.width() + "," + targetheight / ui.draggable.height() + ")");
        }
    });

    $('#playerDeck').droppable({
        accept: ".carte",
        drop: function (e, ui) {
            ui.draggable.removeClass("posay");
            ui.draggable.attr("style", "position: relative; top:0; left:0;");
            ui.draggable.css("transform", "scale(1)");
        }
    });

    $(".carte-container").on("mouseenter", function (e) {
        if ($(this).children(".carte").length > 0) {
            $(this).children(".carte").removeClass("posay");
        }
    });

    $(".carte-container").on("mouseleave", function (e) {
        if ($(this).children(".carte").length > 0) {
            $(this).children(".carte").addClass("posay");
        }
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
        $('.carte, .flipper').attr("style", "width:" + imgWidth + "px; height:" + imgHeight + "px;");
    }

    //resizeCard();
    //$(window).resize(function () {
    //    resizeCard();
    //})
})