$(document).ready(function(){
    $(".save-all__button").click(peSaveAllClick);
});

function getCookie(name) {
    let cookieValue = null;
    if (document.cookie && document.cookie !== '') {
        const cookies = document.cookie.split(';');
        for (let i = 0; i < cookies.length; i++) {
            const cookie = cookies[i].trim();
            // Does this cookie string begin with the name we want?
            if (cookie.substring(0, name.length + 1) === (name + '=')) {
                cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                break;
            }
        }
    }
    return cookieValue;
}

function pemShowMessage(message, type){ // types: "success", "error"
    var $msg = $(".save-all__message"),
        errorClass = "save-all__message_err";
    if (type == "success")
        $msg.removeClass(errorClass);
    else
        $msg.addClass(errorClass);
    $msg.html(message)
        .show();
}

function pemValidate(){
    var result = true;
    if ($("#iName").val() == "") {
        result = false;
        pemShowMessage('Заполните обязательное поле "Имя"', "error");
    }
    if (!dtCheckFormat("#iBirthDay")){ // function from date.js
        result = false;
        pemShowMessage('Корректно заполните поле "Дата". Например, "24.03.1991".', "error");
    }
    return result;
}

function peSaveAllClick(){  
    $(".save-all__message").hide();
    if (pemValidate()){    
        // assigning values  
        var user_info = new Object();
        user_info.first_name = $("#iName").val();
        user_info.last_name = $("#iFamily").val();
        user_info.location = $("#iLocation").val();
        user_info.birth_date = $("#iBirthDay").val();
        user_info.gender = $("#iGender").val();
        user_info.marital_status = $("#iMarital").val();
        user_info.doctrine = tgGetStrValues("#tagsDoctrines");
        user_info.tradition = tgGetStrValues("#tagsTraditions");
        user_info.practice = tgGetStrValues("#tagsPractices");
        user_info.experience = $("#iExperience").val();
        user_info.interests = tgGetStrValues("#tagsInterests");
        user_info.request = $("#tRequest").val();
        user_info.about = $("#tAbout").val();

        // sending ajax
        
        const csrftoken = getCookie('csrftoken');

        var $loading = $(".save-all__loading");
        $loading.show();

        $.ajax({
            type: "POST",
            url: "/profile/save-all.html",
            data: JSON.stringify(user_info),
            headers: {'X-CSRFToken': csrftoken},
            mode: 'same-origin',
            error: function(jqXHR, textStatus, errorThrown) {
                $loading.hide();
                pemShowMessage(`Возникла ошибка: ${errorThrown}`);
            }
        }).done(function(answer) {        
            $loading.hide();
            pemShowMessage("Данные успешно сохранены!", "success");
        });
    }

}