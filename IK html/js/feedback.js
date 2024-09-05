
/* === Amount of feedback items and sizes are calculated automatically === */
/* ===    Default values are just for example, no need to be set up    === */

var fbItemsCount = 5; // feedback items count
var fbItemWidth = 750; // feedback item width
var fbMarginRight = 75; // feedback item margin-right
var fbItemsDisplayed = 2; // feedback items displayed
var fbMoveWidth = (fbItemWidth + fbMarginRight) * fbItemsDisplayed; // feedback width to move
var fbCurrentPos = 0;  // feedback ribbon current position (number of item)

var fbSliderWidth = 114; // feedback slider width

/* DOCUMENT READY */
$(document).ready(function(){

    /* Binding actions on elements */
    $("#bFbLeft").click(feedbackMoveLeft); // Slider
    $("#bFbRight").click(feedbackMoveRight);

    $("#darkened").click(hideMessage); // Message
    $("#bCloseMessage").click(hideMessage);
    $("#bSend").click(showFbMessageSent);
    
    $('#bAttach').click(fileAttach); // File attach

    /* Calculating variables */
    calcFbValues();
    setFbSliderSize();
});

/* WINDOW RESIZE */
$(window).resize(function(){    
    calcFbValues();
    setFbSliderSize(); 
    resetFbPosition();
});

/* Feedback move left button click */
function feedbackMoveLeft(){
    moveFbItemsLeft();
    return false;
}

/* Feedback move right button click */
function feedbackMoveRight(){
    moveFbItemsRight();
    return false;
}

function calcFbValues(){
    fbItemsCount = $(".item").length;
    fbItemWidth = $(".item").first().outerWidth();
    fbMarginRight = parseInt($(".item").first().css("margin-right"));
    frameWidth = $("#fbFrame").width();
    fbItemsDisplayed = parseInt(frameWidth / fbItemWidth);
    if (fbItemsDisplayed == 0)
        fbItemsDisplayed = 1;
    fbMoveWidth = (fbItemWidth + fbMarginRight) * fbItemsDisplayed;
    if (fbItemsCount <= fbItemsDisplayed)
        setFbBtnState('Right', 0);
}

function resetFbPosition(){
    fbCurrentPos = 0;
    $("#fbItems").css('margin-left', '0');
    $("#fbSlider").css('margin-left', '0');
    setFbBtnState('Left', 0); 
    if (fbItemsCount > fbItemsDisplayed)
        setFbBtnState('Right', 1);
}

function moveFbItemsLeft(){
    if (isBtnActive("bFbLeft"))
        {
            fbCurrentPos -= fbItemsDisplayed;
            $("#fbItems").animate({
                marginLeft: '+=' + fbMoveWidth + 'px'
            }, 500);

            /* If moving left is impossible, disabling left arrow */
            if (fbCurrentPos < fbItemsDisplayed)
                setFbBtnState('Left', 0);   

            /* Enabling right arrow */
            if (!isBtnActive("bFbRight"))
                setFbBtnState('Right', 1);

            moveFbSlider(-1);
            
        }    
}

function moveFbItemsRight(){
    if (isBtnActive("bFbRight"))
    {
        fbCurrentPos += fbItemsDisplayed;
        $("#fbItems").animate({
            marginLeft: '-=' + fbMoveWidth + 'px'
        }, 500);

        /* If moving right is impossible, disabling right arrow */
        if (fbCurrentPos + fbItemsDisplayed >= fbItemsCount)
            setFbBtnState('Right', 0);   

        /* Enabling left arrow */
        if (!isBtnActive("bFbLeft"))
            setFbBtnState('Left', 1); 

        moveFbSlider(1);

        }
}

function setFbBtnState(btnSide, isActive){ // Example: setFbBtnState('Left', 1)
    bgPath = 'img/arrow_' + btnSide.toLowerCase();
    if (!isActive)
        bgPath += '_inactive';    
    bgPath += '.png';
    btnId = 'bFb' + btnSide;
    $("#" + btnId).css("background-image", "url('" + bgPath + "')");  
}

/* Button state check */
function isBtnActive(btnId){
    res = 1;
    btnBg = $("#" + btnId).css('background-image');
    if (btnBg.indexOf('inactive') > -1)
        res = 0;
    return res;
}

/* Setting slider size */
function setFbSliderSize(){
   fullWidth = $("#fbSliderBase").width() - $("#bFbLeft").outerWidth() - $("#bFbRight").outerWidth();
    if (fbItemsDisplayed > 1)
        pagesCount = parseInt((fbItemsCount + 1) / fbItemsDisplayed);
    else
        pagesCount = fbItemsCount;
   fbSliderWidth = parseInt(fullWidth / pagesCount);
   $("#fbSlider").width(fbSliderWidth);
}

function moveFbSlider(sliderPos){ // 1 = right and -1 = left
    $("#fbSlider").animate({
        marginLeft: '+=' + sliderPos * fbSliderWidth + 'px'
    }, 500);
}

/* File attach */
function fileAttach(){
    $('#fileInput').trigger('click');
    return false;
}

/* Message sent */
function showFbMessageSent(){
    $("#darkened").show();
    return false;
}

function hideMessage(){
    $("#darkened").hide();
    return false;
}