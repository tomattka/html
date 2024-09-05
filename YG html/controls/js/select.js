$(document).ready(function(){

    //var $iDDFields = $(".dropdown__input");

    var $ddInputBox = $(".dropdown__input-box"),
        $ddInvisible = $(".dropdown__invisible"),
        $ddHintLink = $(".dropdown__hints a");
    $ddInputBox.mousedown(function(e){e.preventDefault();}) // prevent stealing focus from focus control imput
    $ddInputBox.click(ddClick);

    $ddInvisible.focusout(ddFocusOut); // invisible button to control focus
    $ddInvisible.keyup(ddKeyUp);

    $ddHintLink.mousedown(function(e){e.preventDefault();}) // prevent stealing focus
    $ddHintLink.click(ddHintClick);
    
});

function ddHintsDisplay($ddHints, show){ // show = true/false
    var $ddArrow = $ddHints.prev().children(".dropdown__expand"),
        imgUrl = $ddArrow.attr("src");

    if (show) {
        // adapting width
        var ddWidth = $ddHints.parent().css("width");      
        //alert(ddWidth)  ;
        $ddHints.css("width", ddWidth);
        //show
        $ddHints.show();
        imgUrl = imgUrl.replace("down", "up");
    }
    else
    {
        $ddHints.hide();
        imgUrl = imgUrl.replace("up", "down");
    }
    
    $ddArrow.attr("src", imgUrl);

}

function ddClick(e){
    var $ddHints = $(this).next(":first");
    $(this).children(".dropdown__invisible").focus();

    if ($ddHints.hasClass("dropdown__hints")){
        var show = false;
        if ($ddHints.css("display") != "block")
            show = true;
        ddHintsDisplay($ddHints, show);

    }

}

function ddHintsByInput($ddInput){
    var $ddHints = $ddInput.parent().next(":first");
    if ($ddHints.hasClass("dropdown__hints"))
        return $ddHints;
    else
        return null;
}

function ddFocusOut(){
    var $ddHints = ddHintsByInput($(this));
    if ($ddHints){
        ddHintsDisplay($ddHints, false);
    }
}

function ddKeyUp(e){
    if (e.code == "Escape"){
        var $ddHints = ddHintsByInput($(this));
        if ($ddHints){
            ddHintsDisplay($ddHints, false);
        }
    }
}

function ddHintClick(e){
    e.preventDefault();
    var $ddItem = $(this);
    var $ddInput = $ddItem.parent().prev(":first").children(".dropdown__input:first");
    $ddInput.val($ddItem.html());
    ddHintsDisplay($ddItem.parent(), false);
}