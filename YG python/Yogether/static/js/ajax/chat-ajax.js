// Should go after chat.js

var iRefreshInterval = 5000, // Chat refresh time, ms
    refreshTimer = null,
    autoScrollFlag = true,
    allLoadedFlag = false;

$(document).ready(function(){
    // Chat list
    $('.chat-list__item').click(chatGoToDialogue);

    // Chat dialogue
    if ($('#chat-messages').length > 0)
        chatAssignFunctions();
});


// -------------------- Chat common functions ---------------------------

function chatAssignFunctions(){ // Assigns functions to chat dialogue elements
    chatScrollBottom();   
    // Private chat
    $("#bSend").click(chatSendMessage);
    $("#iText").on("keydown", function(e) { chatCheckKey(e); });    
    $('#chat-messages').scroll(chatOnScroll);

    refreshTimer = setInterval(checkNewMessages, iRefreshInterval);
}

// --------------------- Chat list functions ----------------------------

function chatGoToDialogue(){ // Loading selected dialogue
    var user_id = $(this).attr("id").split("_")[1],
        chat_url = `/chat/plain/${user_id}/`,
        show_url = `/chat/${user_id}/`;
    $.ajax({
        type: "GET",
        url: chat_url,
    }).done(function(answer) {
        if (answer.indexOf("div") > -1)
            $("#with_id").val(user_id);
            $(".chat").html(answer).removeClass("chat-list");
            chatAssignFunctions();
            window.history.pushState("", "", show_url);
    });
}


// --------------------- Private chat functions ---------------------------

function chatCheckKey(e){ // Check chat input for "Enter"
    var code = e.key;
    if (code == "Enter")
        chatSendMessage();
}

function chatOnScroll(){ // Checking if the scroll reached top or bottom
    var $chat = $('#chat-messages');
    if ($chat.scrollTop() == 0 && !allLoadedFlag) {
        checkPreviousMessages();
    }
    chatCheckScrollBottom();
}

function chatScrollBottom(){ // Scroll chat bottom
    var chat = document.getElementById('chat-messages');
    chat.scrollTop = chat.scrollHeight;
}

function chatCheckScrollBottom(){ // Function checking if bottom reached
    var $chat = $('#chat-messages');
    if($chat.scrollTop() + $chat.innerHeight() >= $chat[0].scrollHeight) {
        autoScrollFlag = true;
    }
}

function chatPlaySound(){
    try {
        var $player = $("#player");
        $player.trigger("stop");
        $player.trigger("play");
        }
    catch{}
}

function checkNewMessages(){ 
    var with_id = $("#with_id").val(),
        last_msg_id;

    // получить айди последнего сообщения
    try{
        last_msg_id = $("#chat-messages .message").last().attr("id");
        last_msg_id = last_msg_id.split("_")[1];
    } catch {
        last_msg_id = 0;
    }

    // вызвать файл, передать ему параметры
    $.ajax({
        type: "GET",
        url: "/chat/check-messages.html?with_id=" + with_id + "&last_msg=" + last_msg_id,
    }).done(function(answer) {
        if (answer != "error" && answer != "") {
            if (answer.indexOf("message_from") > -1)
                chatPlaySound();
            $('#chat-messages').append(answer);
            if (autoScrollFlag == true)
                chatScrollBottom();
        }
    });
}

// Get previous messages on scroll
function checkPreviousMessages(){
    var with_id = $("#with_id").val(),
        first_msg_id = 0;
    first_msg_id = $("#chat-messages .message").first().attr("id");
    first_msg_id = first_msg_id.split("_")[1];

    $.ajax({
        type: "GET",
        url: "/chat/previous-messages.html?with_id=" + with_id + "&first_msg=" + first_msg_id,
    }).done(function(answer) {
        if (answer.indexOf("message") > -1) {
            autoScrollFlag = false; // disabling autoscroll
            var $chat = $('#chat-messages'), // Getting old scroll height
                oldHeight = $chat.prop("scrollHeight");

            $('#chat-messages').prepend(answer); // Adding html

            var newHeight = $chat.prop("scrollHeight"); // Getting new scroll height
            $chat.scrollTop(newHeight - oldHeight); // Setting the difference
        }
        else {
            allLoadedFlag = true;
        }
    });
}

function chatSendMessage(){
    clearInterval(refreshTimer);
    var msgText = $("#iText").val(),
    my_id = $("#my_id").val(),
    with_id = $("#with_id").val(),
    d = new Date(),
    msg_time = d.getFullYear()  + "-" + (d.getMonth()+1) + "-" + d.getDate() + " " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds();

    if (msgText != ''){
        $.ajax({
            type: "GET",
            url: "/chat/send-message.html?my_id=" + my_id + "&with_id=" + with_id + "&msg=" + msgText + "&time=" + msg_time,
            error: function(){ alert("Возникла ошибка при отправке. Попробуйте позже или обратитесь в поддержку."); },
        }).done(function(answer) {
            if (answer != "error") {
                $("#iText").val('');
                checkNewMessages();
            }
            else
            {
                alert("Возникла ошибка при отправке! Попробуйте позже или обратитесь к администратору сайта.");
            }
            refreshTimer = setInterval(checkNewMessages, iRefreshInterval);
        });
    }
}