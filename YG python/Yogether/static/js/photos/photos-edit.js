// should go after confirm.js
var dragCounter = 0;

$(document).ready(function(){
    var $fileDrop = $('#file-drop');
    $fileDrop.on('dragenter', fileDragEnter);
    $fileDrop.on('dragleave', fileDragLeave);    
    $fileDrop.on('dragover', function(e){ e.preventDefault(); e.stopPropagation(); });
    //$fileDrop.on('drop', fileDrop);
    
    $fileDrop.click(fileSelectClick);
    $('#iFiles').change(fileSelected);
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
            if (file.size > (1024 * 1024 * 2)) // More than 2 MB
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
        result = "Каждый файл не должен превышать 2 МБ.";
    displayFileError(result);
}

function clearAllMessages(){
    // File adding message reset
    var defaultMessage = "Вы можете добавить суммарно до 10 фото.",
        $error = $(".add-files__message");
    $error.html(defaultMessage);
    $error.removeClass("add-files__message-err");
    // Description saving message hide
    $(".photos__save-message").hide();
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

// function fileDrop(e){
//     e.preventDefault();
//     alert("File dropped");
//     $(this).css({"borderColor": "rgba(42, 16, 78, 0.2)"});
// }

// ----------------- File select -------------------

function showFileLoading(){
    $(".add-files__message").hide();
    $(".add-files__loading").show();
}

function hideFileLoading(){
    $(".add-files__message").hide();
    $(".add-files__loading").show();
}

function fileSelectClick(e){
    $('#iFiles').trigger('click');
    e.preventDefault();
}

function fileSelected(e){
    if ($(this).prop("files").length != 0){
        clearAllMessages();
        var checkResult = checkFiles($(this).prop("files"));
        if (checkResult == "ok"){
            showFileLoading();
            $("#fAddPhotos").submit();
        } else{
            showErrorMessage(checkResult);
        }
    }
}