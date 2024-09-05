function calcGreetHeight(){
    var wdth = $("#greet").width();
    var hght = Math.round(wdth*0.5);
    $("#greet").height(hght);
}

$(window).resize(calcGreetHeight);

function calcGetitHeight(){
    var wdth = $("#getit").width();
    var hght = Math.round(wdth*0.4);
    $("#getit").height(hght);
}

$(window).resize(calcGetitHeight);


function advOn(advId){
    $("#adv" + advId).css("border-color", "#6202d1");
    $("#adv" + advId + " h3").css("color", "#6202d1");
    $("#adv" + advId + " img").attr("src", "img/advantages/a" + advId + "_hover.png");
}
function advOut(advId){
    $("#adv" + advId).css("border-color", "#60605f");
    $("#adv" + advId + " h3").css("color", "#60605f");
    $("#adv" + advId + " img").attr("src", "img/advantages/a" + advId + ".png");
}

function tbSearch_keyDown() {
    var keypressed = event.keyCode || event.which;
    if (keypressed == 13) {
        newUrl = "/search/?text=" + document.getElementById('tbSearch').value;
        window.location.href = newUrl;
    }
}


/*function preloadImages() {
    for (var i = 0; i < arguments.length; i++) {
      new Image().src = arguments[i];
      wait(300);
    }
  }

  preloadImages(
      "img/list-icons/5t_hl.jpg",
      "img/list-icons/server_hl.jpg",
      "img/list-icons/terminal_hl.jpg",
      "img/list-icons/money_hl.jpg",
      "img/list-icons/time_hl.jpg",
      "img/list-icons/round_up_hl.jpg",
      "img/list-icons/question_hl.jpg",
      "img/list-icons/config_hl.jpg",
      "img/social/soc1_hover.png",
      "img/social/soc2_hover.png",
      "img/social/soc3_hover.png",
      "img/social/soc4_hover.png",
      "img/social/soc5_hover.png",
      "img/social/soc6_hover.png",
      "img/menu/5t_hover.png",
      "img/menu/news_hover.png",
      "img/menu/articles_hover.png",
      "img/menu/instructions_hover.png",
      "img/menu/support_hover.png",
      "img/menu/contacts_hover.png"
  );*/