$(document).ready(function(){
    $(".hint").click(hShowHint);
    $(".hint-box__hidden").focusout(hHideHints);
});

$(window).resize(function(){
    $(".hint").each(function(){ 
        hHideHints();
        hCalcPosition($(this));
    });
});

function hCalcPosition($button){
    var $hint = $button.next();
    var iPos = $button.offset();
    var hLeft = iPos.left - $hint.outerWidth()/2;
    if (hLeft + $hint.outerWidth() > $(document).width() || hLeft < 5)
        hLeft = 5;

    var hTop = iPos.top - $hint.outerHeight() - 5;
    if (hTop < 5)
        hTop = 5;
    

    $hint.css({top: hTop + 'px',left: hLeft + 'px'});
}

function hShowHint(){
    hCalcPosition($(this));

    var $hint = $(this).next();
    $hint.show();
    $hint.find(".hint-box__hidden").focus();
}

function hHideHints(){
    $(".hint-box").hide();
}