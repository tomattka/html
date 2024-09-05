var $iFile;

$(document).ready(function(){
    $iFile = $('#iFile');

    $('#bUpload').click(showFileDialogue);
    $iFile.change(fileChanged);
});

function showFileDialogue(){
    $iFile.trigger('click');
}

function checkFormatWeight(file){
    result = "ok";
    var filename = file.name.toLowerCase();
    if ((filename.indexOf(".jpg") == -1) && (filename.indexOf(".jpeg") == -1) && (filename.indexOf(".png") == -1) && (filename.indexOf(".gif") == -1))
        result = "format";
    if (file.size > (1024 * 1024 * 10)) // More than 10 MB
        result = "weight";
    return result;
}

function fileChanged(){
    $error = $("#avError");
    $error.hide();

    file = $iFile[0].files[0];
    if (file)
        checkFile = checkFormatWeight(file);
        if (checkFile == "ok"){
            $('#fUpload').submit();
        }
        else{           
            if (checkFile == "format")
                $error.html("Неверный формат файла!");
            if (checkFile == "weight")         
                $error.html("Файл превышает допустимый размер!");
            $error.show();
        }


}