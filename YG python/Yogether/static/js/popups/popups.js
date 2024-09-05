var mobMenu, loginForm, popups, darkscreen;

$(document).ready(function(){

    // Assigning elems to variables
    mobMenu = $("#mobile-menu");
    popups = $("#popups");
    darkscreen = $('#darkscreen');
    loginForm = $('#login-form');

    // --------- Mobile menu ---------- //
    $("#mobile-menu-close").click(closeMenu); // menu x button
    $("#aMobMenu").click(showMenu); // menu icon click

    // Login form show
    $('#aLogin').click(showLoginForm);
    $('#aLoginMob').click(showLoginForm);
    $('#mobMenuLogin').click(showLoginMobile);

    // Forms close functions
    darkscreen.mousedown(closeAll);
    popups.mousedown(closeAll);
    $('.x-close').click(closeAll);

    // Prevent form click reaction
    loginForm.mousedown(function(e){
        e.stopPropagation();
    });

});

$(window).resize(moveMenu); // correcting menu position on window resize


// --------------------- Popups ----------------------- //
function closeAll(){
    if (mobMenu.is(":visible"))
        closeMenu();
    else
    {
        popups.children().fadeOut(150, function(){            
            popups.hide();
            darkscreen.hide();
        });
    }
}

function showLoginForm(){
    darkscreen.show();
    popups.css('display','flex');
    loginForm.fadeIn(150);
    scrollTopMobile();
    return false;
}

function showLoginMobile(e){
    e.stopPropagation();
    mobMenu.animate({left: "+=" + mobMenu.width()}, 350, 
    function(){ // menu animate end
        mobMenu.hide();
        showLoginForm();
    });
    
}

// function scrollTop(event) {
//     event.preventDefault();
//     $('html, body').animate({ scrollTop: 0 }, 400);
// }

function scrollTopMobile(){
    windowWidth = $(window).width();
    if (windowWidth < 767)

    $('html, body').animate({ scrollTop: 0 }, 400);
}

// ----------------- Mobile menu ------------------- //

function showMenu(){
    mobMenu.show();
    darkscreen.show();
    popups.css('display','flex');
    checkMenuHeight();
    mobMenu.animate({left: "-=" + mobMenu.width()}, 350);
    return false;
}

function closeMenu(){
    mobMenu.animate({left: "+=" + mobMenu.width()}, 350, 
    function(){ // menu animate end
        popups.hide();
        darkscreen.hide();
        mobMenu.hide();
    });
    return false;
}

function moveMenu(){ // correcting menu position on window resize
    checkMenuHeight();
    if (mobMenu.is(":visible"))
    {
        leftPos = $(window).width() - mobMenu.width();
        mobMenu.css('left', leftPos + 'px');
    }
    else
        mobMenu.css('left', $(window).width() + 'px');
}

function checkMenuHeight(){
    if (mobMenu.height() < $(window).height())
        popups.css('height', '100%');
    if (popups.height() < mobMenu.height())
        popups.css('height', mobMenu.height() + 'px');
}