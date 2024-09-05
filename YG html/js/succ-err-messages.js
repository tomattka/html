$(document).ready(function(){
    //messageTimerHide($(".config__message"));
});

function messageTimerHide($element){
    setTimeout(function(){
        $element.fadeOut("slow");
    }, 2000);
}