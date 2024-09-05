// !!! This file uses popups.js and should go after it

var dragCounter = 0;

$(document).ready(function(){
    var $fileDrop = $('#file-drop');
    $fileDrop.on('dragenter', fileDragEnter);
    $fileDrop.on('dragleave', fileDragLeave);    
    $fileDrop.on('dragover', function(e){ e.preventDefault(); e.stopPropagation(); });
    $fileDrop.on('drop', fileDrop);
    
    $fileDrop.click(fileSelectClick);
    $('#iFiles').change(fileSelected);

    $(".photos__delete img").click(deletePhotoClick);
});

// ---------------- File check ------------------

function countPhotosLeft(){
    var allPhotos = 10,
        occupied = $(".photos__item").length;
    return allPhotos - occupied;
}

function checkFiles(files){
    var result = "ok";
    // amount check
    var allowedAmount = countPhotosLeft();
    if (allowedAmount < files.length)
        result = "amount";
    else{
        // format and size
        for (var i=0, file; file=files[i]; i++){
            var filename = file.name.toLowerCase();
            if ((filename.indexOf(".jpg") == -1) && (filename.indexOf(".jpeg") == -1))
                result = "format";
            if (file.size > (1024 * 1024 * 10)) // More than 10 MB
                result = "weight";
        }
    }
    return result;
}

function displayFileError(msg){
    var $error = $(".add-files__message");
    $error.html(msg);
    $error.addClass("add-files__message-err");
}

function showErrorMessage(errCode){
    var result = "Выбранные файлы не соответствуют требованиям!";
    if (errCode == "amount")
        result = `Вы можете добавить не более ${countPhotosLeft()} фотографий!`;
    if (errCode == "format")
        result = "Файлы должны быть в формате jpeg!";
    if (errCode == "weight")
        result = "Каждый файл не должен превышать 10 МБ.";
    displayFileError(result);
}

function clearErrorMessage(){
    var defaultMessage = "Вы можете добавить суммарно до 10 фото.",
        $error = $(".add-files__message");
    $error.html(defaultMessage);
    $error.removeClass("add-files__message-err");
}

// ----------------- File drop -------------------

function fileDragEnter(e){
    e.preventDefault();
    dragCounter++;
    $(this).css({"borderColor": "#7551A8"});
}

function fileDragLeave(e){
    dragCounter--;
    if (dragCounter == 0)
        $(this).css({"borderColor": "rgba(42, 16, 78, 0.2)"});
}

function fileDrop(e){
    e.preventDefault();
    clearErrorMessage();
    var checkResult = checkFiles(e.originalEvent.dataTransfer.files);
    if (checkResult == "ok"){
        alert("File dropped: " + e.originalEvent.dataTransfer.files.length);
    } else{
        showErrorMessage(checkResult);
    }
    $(this).css({"borderColor": "rgba(42, 16, 78, 0.2)"});
}

// ----------------- File select -------------------

function fileSelectClick(e){
    $('#iFiles').trigger('click');
    e.preventDefault();
}

function fileSelected(e){
    if ($(this).prop("files").length != 0){
        clearErrorMessage();
        var checkResult = checkFiles($(this).prop("files"));
        if (checkResult == "ok"){
            alert("File selected: " + $(this).prop("files").length);
            //$("#fAddPhotos").submit();
        } else{
            showErrorMessage(checkResult);
        }
    }
}

// ----------------- File delete -------------------

function deletePhotoClick(){
    var $item = $(this).parents().eq(1);
    dialog('Удаление фото', 'Действительно удалить фотографию?', 'Удалить', 'Отмена', function (confirmed) {
        if (confirmed) {            
            photoId = $item.attr('id').replace('item', '');
            $item.remove();
        }     
        closeAll(); // function from popups.js
    });

}