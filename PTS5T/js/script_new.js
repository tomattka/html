function advOn(advId){
    $("#ia" + advId).attr("src", "img/advantages/a" + advId + "_hover.png");
}
function advOut(advId){
    $("#ia" + advId).attr("src", "img/advantages/a" + advId + ".png");
}
function tbSearch_keyDown() {
    var keypressed = event.keyCode || event.which;
    if (keypressed == 13) {
        newUrl = "/search/?text=" + document.getElementById('tbSearch').value;
        window.location.href = newUrl;
    }
}

function setImageLinks() {
    imgs = $('.page img').each(function () {
        //alert($(this).attr('src'));
        $(this).wrap($('<a>', {
            href: $(this).attr('src'),
            'data-fancybox': 'page'
        }));
    });

}

$(document).ready(setImageLinks);