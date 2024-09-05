// !!! This file uses popups.js and should go after it
var $confirmPopup;

$(document).ready(function(){
    $confirmPopup = $("#confirm");
    $confirmPopup.mousedown(function(e){ e.stopPropagation(); });
});

function dialog(title, message, okText, cancelText, callback) {  
    // define buttons
    var $confirmButton = $("#bOk"), 
        $cancelButton = $("#bCancel");

    // set text
    $('.confirm__title').html(title);
    $('.confirm__text').html(message);
    $confirmButton.html(okText);
    $cancelButton.html(cancelText);

    $confirmButton.click(function() { callback(true); });
    $cancelButton.click(function() { callback(false); });
    
    // show popup
    $('#darkscreen').show();
    $("#popups").css('display','flex');
    $('#confirm').fadeIn(150);
    scrollTopMobile(); // function from popups.js
    return false;

}

