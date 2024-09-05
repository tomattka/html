$(document).ready(function(){
    $("#bMobile").click(mobileMenuClick);
});

$(window).resize(function(){    
    menuFix();
});

function mobileMenuClick(){
    topMenu = $("#topMenu");
    if (topMenu.css('display') == 'none')
        topMenu.show();
    else
        topMenu.hide();
    return false;
}

function menuFix(){
    topMenu = $("#topMenu");  
    if ($(window).width() > 1651)
        topMenu.show();
    else
        topMenu.hide();
}