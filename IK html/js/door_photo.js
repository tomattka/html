var photoList = [] ; // list: linkId, photoId


$(document).ready(function(){
    getPhotoList(); // Получаем список ссылок
    $('.photo').click(function(e){
        showNextPhoto();
    });
    $('.photo > .links > a').click(function(e){
        e.stopPropagation();
        showPhotoByLink($(this));
    });
});

function getPhotoList(){
    photos = $(".links > a");
    $.each(photos, function(index, element){
        photoList.push(element);
    });
}

function showNextPhoto(){ // Показать следующее фото
    currentId = getCurrentId();
    
    nextId = currentId + 1;
    if (nextId >= photoList.length)
        nextId = 0;
    
    showPhotoByIndex(nextId);
}

function showPreviousPhoto(){ // Показать предыдущее фото
    currentId = getCurrentId();
    
    nextId = currentId - 1;
    if (nextId < 0)
        nextId = photoList.length - 1;
    
    showPhotoByIndex(nextId);
}

function getCurrentId(){    
    activePhoto = $(".links > a.active");
    currentId = 0;
    for (i = 0; i < photoList.length; i ++) // находим индекс активного фото
    {
        if (photoList[i].id == activePhoto.attr("id"))
            currentId = i;
    }
    return currentId;
}

function showPhotoByIndex(i){
    $('.photo > .links > a').removeClass();
    $('#' + photoList[i].id).addClass('active');
    $("#doorPhoto").css("background-image", "url('" + photoList[i] + "')");
}

function showPhotoByLink(elem){
    href = elem.attr('href');
    $("#doorPhoto").css("background-image", "url('" + href + "')");
    $('.photo > .links > a').removeClass();
    elem.addClass('active');
    event.preventDefault();
}