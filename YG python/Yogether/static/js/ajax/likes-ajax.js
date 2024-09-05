$(document).ready(function(){        
    $(".user-view__like").click(likeClick);
    $(".user-info__like").click(likeClick); // Mobile like click
});

function likeClick(){
    var user_from = $("#hUserId").val();
    
    if (user_from != "None") {     
            var user_to = $("#hUserViewedId").val(),
            liked = $("#hLiked").val(),
            mode = (liked != "true") ? "like" : "unlike";   

        var url = `/likes/set-like.html?user_from=${user_from}&user_to=${user_to}&mode=${mode}`

        $.ajax({
            type: "GET",
            url: url,
        }).done(function(answer) {
             if (answer.indexOf("success") > -1){
                if (mode=="like"){
                    $(".user-view__like").addClass("user-view__like_active");
                    $(".user-info__like").addClass("user-info__like_active");
                    $("#hLiked").val("true");
                }
                else {                
                    $(".user-view__like").removeClass("user-view__like_active");
                    $(".user-info__like").removeClass("user-info__like_active");
                    $("#hLiked").val("false");
                }
             }
             else
                alert("Возникла ошибка!")
        });    
    }
    else
        location.href = "/accounts/login/";
           
}