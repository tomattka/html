$(document).ready(function(){        
    $("#aRecommendReset").click(recommendReset);
});

function messageTimerHide($element){
    setTimeout(function(){
        $element.fadeOut("slow");
    }, 2000);
}

function recommendReset(e){
    e.preventDefault();    
    
    var user_id = $("#hUserId").val(),
    reset_url = `/recommend/reset.html?user_id=${user_id}`;

    $.ajax({
        type: "GET",
        url: reset_url,
    }).done(function(answer) {
        $(".config__message").hide(); //hiding other messages
        $(".config__recommend").append('<div class="config__message config__message_green">Рекомендации успешно обновлены</div>');
        messageTimerHide($(".config__message"));
    });    
}
