$(document).ready(function(){
    $("#userpic").mouseover(showUserMenu);
    $("#usermenu").mouseleave(hideUserMenu);
});

function showUserMenu(){
    $("#usermenu").show();
}

function hideUserMenu(){
    $("#usermenu").hide();
}