$(document).ready(function(){
    $(".user-info__item .pen").click(infoPenClick);
    //$(".user-info__save").click(infoSaveClick);
    
    $(".user-data__name .pen").click(namePenClick);
    //$(".user-data__save").click(nameSaveClick);
});

function infoPenClick(){
    var $pen = $(this),
        $info = $pen.parent().children(".user-info__value"),
        $edit = $pen.parent().children(".user-info__editable");
    $info.hide();
    $pen.hide();
    $edit.css("display", "flex");
}

function infoSaveClick($bSave){
    // function changed for backend //
    var $info = $bSave.parent().parent().children(".user-info__value"),
        $edit = $bSave.parent().parent().children(".user-info__editable"),
        $pen = $bSave.parent().parent().children(".pen");
    $edit.hide();
    $info.show();
    $pen.show();
}

function namePenClick(){
    var $pen = $(this),
        $info = $pen.parent(),
        $edit = $info.next(".user-data__editable:first");
    $info.hide();
    $edit.css("display", "flex");
}

function nameSaveClick($bSave){
    var $info = $bSave.parent().prev(".user-data__name:first"),
        $edit = $bSave.parent();
    $edit.hide();
    $info.show();
}