$(document).ready(function(){    
    $(".item__delete").click(recommendDelete);
});

var recommendContent = {}

function recommendHide($item){
    $item.html('<div class="item__removed">Пользователь удалён из рекомендаций <a href="#">Отмена</a></div>');
    $item.find(".item__removed a").click(recommendCancel);
}

function recommendCancel(e){
    e.preventDefault();
    
    // getting values
    var $item = $(this).closest(".search__item");
    self_id = $("#hUserId").val();
    remove_id = $item.attr("id").split('_')[1];
    recommend_url = `/recommend/cancel-remove.html?self_id=${self_id}&remove_id=${remove_id}`;

    // calling ajax
    $.ajax({
        type: "GET",
        url: recommend_url,
    }).done(function(answer) {
            // restoring
        $item.html(recommendContent[remove_id]);
        $item.find(".item__delete").click(recommendDelete);
    });    
}

function recommendDelete(e){
    e.preventDefault();
    e.stopPropagation();

    var $item = $(this).parent(".search__item");

    self_id = $("#hUserId").val();
    remove_id = $item.attr("id").split('_')[1];
    recommend_url = `/recommend/remove.html?self_id=${self_id}&remove_id=${remove_id}`;

    recommendContent[remove_id] = $item.html();

    $.ajax({
        type: "GET",
        url: recommend_url,
    }).done(function(answer) {
        if (answer.indexOf("success") > -1)
            recommendHide($item);
        else
            alert("Возникла ошибка!");
    });
}