﻿$(function () {
    var playHub = $.connection.playHub;
    var lastPosition;

    var gameId = $("#GameId").val();

    $("#playersModal").modal({
        backdrop: 'static',
        show: false
    });

    $("#menaceModal").modal({
        backdrop: 'static',
        show: false
    });

    var cardDragOptions = {
        container: "#playerInfo",
        zIndex: 10000,
        revert: "invalid",
        addClasses: false,
        scroll: false,
        start: function () {
            $(".carte-container .carte").addClass("locked");
            if ($(this).parent()[0].className.indexOf("carte-container") == -1) {
                $("#cartes .carte:not(.ui-draggable-dragging)").each(function () {
                    var original = $(this);
                    generateCardClone(original);
                });
            }
        },
        stop: function () {
            $(".carte").removeClass("locked");
            var nbClones = $("#cartes .carte").length;
            var i = 0;
            // On anime les cartes restantes de la main du joueur
            $("#cartes .carte").each(function () {
                var item = $(this);
                var clone = item.data("clone");
                if (!clone) {
                    nbClones--;
                } else {
                    clone.stop(true, false);

                    var position = item.position();
                    clone.animate({ left: position.left, top: position.top }, 400, "easeInQuart", function () {
                        i++;
                        if (i === nbClones) {
                            $("#cartes .carte").css("visibility", "visible");
                            $("#carteClones").empty();
                        }
                    });
                }
            });
        }
    };

    var removeModal = function () {
        $("#menaceModal").modal('hide');
        $("#menaceModal .carte").addClass("posay");
    }

    //var soundtrack = document.createElement('audio');
    //soundtrack.setAttribute('src', '../Content/Medias/Scenarii/' + $("#MissionId").val() + '.wav');
    //soundtrack.play();

    playHub.client.popMenace = function (frontImg, backImg) {
        $("#menaceModal .front img").attr("src", serverPath + frontImg);
        $("#menaceModal .back img").attr("src", serverPath + backImg);
        //$("#menaceModal").modal('show');
    };

    playHub.client.enableDataTransfert = function () {
        $("#playersModal").modal('show');
        setTimeout(function () {
            $("#playersModal").modal('hide');
        }, 10000);
    };

    var receiveCard = function (direction, action) {
        $.ajax({
            type: "POST",
            url: getCardUrl,
            data: {
                action: action,
                direction: direction
            }
        }).success(function (card) {
            card.draggable(cardDragOptions);
            $("#cartes").append(card);
        });
    };

    playHub.client.receiveNewCard = function (charId, direction, action) {
        if (currentPlayer.characterId == charId) {
            receiveCard(direction, action);
        }
    }

    $("#menaceModal").on("shown.bs.modal", function () {
        //setTimeout(function(){
        //    $("#menaceModal .carte").removeClass("posay");
        //}, 500);
        $("#menaceModal .carte").removeClass("posay");
        setTimeout(removeModal, 2000);
    });

    $(".carte-container").on('webkitAnimationEnd oanimationend msAnimationEnd animationend', ".locker",
        function () {
            $(".carte-container").removeAttr("css");
        });

    var endPhase = function (phase) {
        var actionsToSend = [];
        $(".ligne .carte-container").each(function () {
            var tour = parseInt($(this).attr("data-tour"));
            if (tour < debutPhase[phase] && tour >= debutPhase[phase - 1]) {
                console.log("tour verrouillé : " + tour);
                $(this).droppable("destroy");
                $(this).css("overflow-y", "hidden");
                $(this).prepend("<img class='locker bottom animated bounceInUp' src='../Content/Medias/cartelockedbottom.png'></img>");
                $(this).prepend("<img class='locker top animated bounceInDown' src='../Content/Medias/cartelockedtop.png'></img>");
                if ($(this).find(".carte").length == 1) {
                    var carte = $($(this).find(".carte")[0]);
                    var reversed = carte.find("img")[0].className.indexOf("reverse") == -1;
                    actionsToSend.push({
                        Genre: reversed ? 0 : 1,
                        Value: reversed ? parseInt(carte.attr("data-action")) : parseInt(carte.attr("data-mouvement")),
                        Tour: tour
                    });
                }
            }
        });
        if (actionsToSend.length) {
            var datas = JSON.stringify(actionsToSend);
            $.ajax({
                type: "POST",
                url: addActionsUrl,
                dataType: "application/json",
                data: {
                    gameId: gameId,
                    actions: datas
                }
            });
        }
    };

    playHub.client.endPhase = function (phase) {
        endPhase(phase);
    };

    $.connection.hub.start().done(function () {
        playHub.server.joinAsync("", gameId);

        $(".avatar").droppable({
            accept: '.carte',
            drop: function (e, ui) {
                playHub.server.transfertCard(gameId, $(this).attr("data-char"), ui.draggable.attr("data-mouvement"), ui.draggable.attr("data-action"));
                ui.draggable.remove();
            }
        });

    });

    // Le nombre de tours dans chaque phase
    var toursParPhase = [3, 4, 5];

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
        hoverClass: 'hovered',
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

    $("#playerInfo .carte").draggable(cardDragOptions);

    $('.carte-container').droppable({
        hoverClass: "hovered",
        accept: ".carte",
        drop: function (e, ui) {
            // S'il y a déjà une carte à l'emplacement choisi
            // On la renvoie dans la main du joueur
            if ($(this).find('.carte').length > 0) {
                var carte = $(this).find(".carte");
                carte.removeClass("posay");
                carte.attr("style", "position: relative;");
                carte.appendTo("#cartes");

                generateCardClone(carte);
            }

            // On enregistre la dernière position pour une animation
            lastPosition = ui.draggable.offset();
            lastPosition.top = lastPosition.top - $(this).offset().top;
            lastPosition.left = lastPosition.left - $(this).offset().left;
            // Pose la carte à l'emplacement choisi

            ui.draggable.appendTo($(this));
            var tempClone = ui.draggable.clone();
            ui.draggable.attr("style", "position: relative; width: 100%; height: 100%;");
            ui.draggable.css("visibility", "hidden");
            tempClone.css("position", "absolute")
                .css("width", ui.draggable.width())
                .css("height", ui.draggable.height())
                .css("top", lastPosition.top)
                .css("left", lastPosition.left);

            tempClone.appendTo($(this));

            var newPos = ui.draggable.position();
            tempClone.animate({ left: newPos.left, top: newPos.top }, 300, "swing", function () {
                ui.draggable.css("visibility", "visible");
                tempClone.remove();
                ui.draggable.addClass("posay");
            });
        }
    });

    $('#playerDeck').droppable({
        accept: ".carte",
        drop: function (e, ui) {
            ui.draggable.removeClass("posay");
            ui.draggable.attr("style", "position: relative;");
            ui.draggable.css("transform", "scale(1)");
            ui.draggable.appendTo("#cartes");
        }
    });

    $(".carte-container").on("mouseenter", function () {
        if ($(this).children(".carte").length > 0 && $(this).children(".carte")[0].className.indexOf("locked") == -1) {
            $(this).children(".carte").removeClass("posay");
        }
    });

    $(".carte-container").on("mouseleave", function () {
        if ($(this).children(".carte").length > 0 && $(this).children(".carte")[0].className.indexOf("locked") == -1) {
            $(this).children(".carte").addClass("posay");
        }
    });

    $(".menu-item").on("mouseenter", function () {
        $(this).addClass("chosen");
    });

    $(".menu-item").on("mouseleave", function () {
        $(this).removeClass("chosen");
    });

    var generateCardClone = function (carte) {
        var tempClone = carte.clone();
        carte.data("clone", tempClone);
        carte.css("visibility", "hidden");

        var position = carte.position();
        tempClone.css("top", position.top)
            .css("left", position.left);

        $("#carteClones").append(tempClone);
    };
})