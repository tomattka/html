// should go after popups.js, confirm.js, photos-edit.js

$(document).ready(function(){
    $(".photos__delete img").click(deletePhotoClick);
    $(".photos__save").click(savePhotosClick);
    $(".arrow-up").click(movePhotoUp);
    $(".arrow-down").click(movePhotoDown);

    $('#file-drop').on('drop', fileDrop);
    
});

// --------------- File drop ---------------------

function checkPhotosAmount(){
    if ($(".photos__item").length == 0)
        $(".photos__save").remove();
}

function fileInArray(file, allFiles){
    var res = false;
    for(var i=0, f; f=allFiles[i]; i++) {
        if (f.name  == file.name && f.size == file.size)
        res = true;
    }
    return res;
}

function fileDrop(e){
    e.preventDefault();
    clearAllMessages();
    $(this).css({"borderColor": "rgba(42, 16, 78, 0.2)"});

    showFileLoading();

    var checkResult = checkFiles(e.originalEvent.dataTransfer.files);
    if (checkResult == "ok"){
        formData = new FormData($('#fPhotos')[0]);

        var count = 0;    
        files = e.originalEvent.dataTransfer.files;

        $.each(files, function(i, file){
            formData.append("iFiles", file);
            count++;
        });

        $.ajax({
            type: "POST",
            url: "/photos/addPhotos.html",
            data: formData,        
            contentType: false,
            processData: false,
            success: function(answer) {
                document.location.reload();
            },
            error: function(){
                hideFileLoading();
                displayFileError("При загрузке файлов возникла ошибка. Попробуйте позже или обратитесь в поддержку сайта.")
            },
        });
    } else{
        hideFileLoading();
        showErrorMessage(checkResult);
    }
    $(this).css({"borderColor": "rgba(42, 16, 78, 0.2)"});    
}

// ---------------- Order functions --------------------- //

function showPhotoLoading($item){
    $item.children(".photos__arrows, .photos__delete").hide();
    $item.children(".photos__loading").show();
}
function hidePhotoLoading($item){
    $item.children(".photos__loading").hide();
    $item.children(".photos__arrows, .photos__delete").show();

}

function changePhotoOrder($item1, $item2){
    clearAllMessages();
    var item1_id = $item1.attr("id").replace("item", ""),
        item2_id = $item2.attr("id").replace("item", "");

    $.ajax({
        type: "GET",
        url: "/photos/orderPhotos.html?first=" + item1_id + "&second=" + item2_id
    }).done(function(answer) {
        if (answer == "success"){
            // change html order
            $item2.insertBefore($item1);

            hidePhotoLoading($item1);
            hidePhotoLoading($item2);
        }
    });    

}

function movePhotoUp(){
    var $item = $(this).parent().parent(),
        $prevItem = $item.prev(".photos__item");
    if ($prevItem[0]){
        showPhotoLoading($item);
        changePhotoOrder($prevItem, $item);
    }
}

function movePhotoDown(){
    var $item = $(this).parent().parent(),
        $nextItem = $item.next(".photos__item");
    if ($nextItem[0]){
        showPhotoLoading($item);
        changePhotoOrder($item, $nextItem);
    }
}

// ---------------- Delete photo --------------------- //

function deletePhotoClick(){
    clearAllMessages();
    var $item = $(this).parents().eq(1);

    dialog('Удаление фото', 'Действительно удалить фотографию?', 'Удалить', 'Отмена', function (confirmed) {
        if (confirmed) {                
            showPhotoLoading($item);        
            var photoId = $item.attr('id').replace('item', ''); 
            $.ajax({
                type: "GET",
                url: "/photos/deletePhoto.html?photoId=" + photoId
            }).done(function(answer) {
                $item.remove();        
                checkPhotosAmount();
                hidePhotoLoading($item);
            });
        }     
        closeAll(); // function from popups.js
    });

   
}

// ---------------- Save descriptions --------------------- //

function savePhotosClick(){
    clearAllMessages();
    var $btn = $(".photos__save"),
        $loading = $(".photos__save-loading");
    $btn.hide();
    $loading.show();
    $.ajax({
        type: "POST",
        url: "/photos/savePhotos.html",
        data: $("#fPhotos").serialize(),
        error: function(){ alert("Возникла ошибка при сохранении. Попробуйте позже или обратитесь в поддержку."); },
    }).done(function(answer) {
        $(".photos__save-message").show();   
        $loading.hide();
        $btn.show();
    });
}

