var imgPath = "/static/controls/img/",
    searchLink = "/search/";

$(document).ready(function(){

    // Tags functions
    $(".tags").click(tgFocusInput);

    var $tagInputs = $(".input-box__input");
    $tagInputs.on("keyup change", function(e) { tgAdjustInputSize($(this));  });
    $tagInputs.on("keydown", function(e) { tgCheckKey(e, $(this)); });
    //$tagInputs.on("keyup", function(e) { tgCheckInput(e, $(this)); });
    $tagInputs.focusout(tgRemoveHintLists); 

    $(".tags__item button").each(function(){
        $(this).click(tgDeleteTag);
    });

});

// --------------------------------- List function -------------------------------- //

function tgListItemClick(e){
    var $iTag = $(this).parent().prevAll().eq(1).children('.input-box:first').children(".input-box__input:first"); // getting input of linked tag box
    $iTag.val($(this).html());
    tgAddTag($iTag);
    e.preventDefault();
}

function tgIsItemInArrayCI(value, arr){ // Case insensetive check
    var res = false;
    $.each(arr, function(index, item){
        if (value.toLowerCase() == item.toLowerCase())
            res = true;
    });
    return res;
}

// function tgShowTagList($iTag){
//     var tagValue = $iTag.val();
//     // var arrTagList = getTagList(tagValue); !!! будет получаться из аджакса
//     var arrTagList = ["Москаленки", "Москва", "Московская область", "Подмосковье"];
//     if (arrTagList.length > 0){
//         tgDisplayTagList($iTag, arrTagList);
//         tgShowHint($iTag, arrTagList[0]);
//     }
// }

function tgSetHintListPosition($iTag, $tagsSelect){
    var position  = $iTag.offset();
    var lTop = position.top + 35;
    var lLeft = position.left - 18;
    $tagsSelect.css({top: lTop + 'px', left: lLeft + 'px'});
}

function tgDisplayTagList($iTag, arrTagList){
    // creating block
    var $tagsSelect = $("<div></div>")
        .addClass("tags-select");
    $.each(arrTagList, function(index, value){
        $("<a href='#'></a>")
            .mousedown(function(e){ e.preventDefault(); }) // prevent from stealing focus
            .click(tgListItemClick)
            .addClass("tags-select__item")
            .html(value)
            .appendTo($tagsSelect)
    });
    // adding block
    tgSetHintListPosition($iTag, $tagsSelect);
    tgRemoveHintLists();
    $tagsSelect.insertAfter(tgGetErrorByTag($iTag));
}

function tgRemoveHintLists(){
    $(".tags-select").remove();
}

// ------------------- Tag list arrows ------------------

function tgGetHintListByTag($iTag){
    var $hintList = $iTag.parent().parent().next().next();
    if (!$hintList.hasClass("tags-select"))
        $hintList = null;
    return $hintList;
}

function tgApplySelectedText($iTag, $hintList){
    tgGetHintByTag($iTag).val('');
    $iTag.val($hintList.children(".selected:first").html());
    tgAdjustInputSize($iTag); // aditionaly calling for not to wait for event finish
    tgSetHintListPosition($iTag, $hintList);
}

function tgSelectNextValue($iTag){
    var $hintList = tgGetHintListByTag($iTag);
    if ($hintList){
        var $selectedItem = $hintList.children(".selected");
        if ($selectedItem[0]){ // some item already selected
            $selectedItem.removeClass("selected");
            if ($selectedItem.next()[0]) // if there's next item
                $selectedItem.next().addClass("selected");
            else
                $hintList.children(":first").addClass("selected");
        }
        else 
        {
            $hintList.children(":first").addClass("selected"); // !!!!!!!!!!! think of this construction
        }
        tgApplySelectedText($iTag, $hintList);
        
    }
}

function tgSelectPrevValue(e, $iTag){
    var $hintList = tgGetHintListByTag($iTag);
    var $selectedItem = $hintList.children(".selected");
    if ($hintList && $selectedItem[0]){
        e.preventDefault();
        $selectedItem.removeClass("selected");
        if ($selectedItem.prev()[0])
            $selectedItem.prev().addClass("selected");
        else
            $hintList.children(":last").addClass("selected");

        tgApplySelectedText($iTag, $hintList);
    }

}

// --------------------------------- Tags function -------------------------------- //

// Get hint input element by tag input
function tgGetHintByTag($iTag){
    return $iTag.parent().children(".input-box__hint:first");
}

// Get error block by tag input
function tgGetErrorByTag($iTag){
    return $iTag.parent().parent().next();
}

// Get array of values in tagbox
function tgGetValues($tagbox, mode){ // mode=normal|strict for not-including unentered value
    var arrValues = [];
    $tagbox.children(".tags__item").each(function(){
        var itemHtml = $(this).html();
        var itemValue = itemHtml.split("<button")[0];
        arrValues.push(itemValue);
    });
    // adding value from input
    if (mode != "strict"){
        let input_val = $tagbox.find(".input-box__input").val();
        if (input_val) arrValues.push(input_val);
    }

    return arrValues;
}

// Adjust width of input depending on content
function tgAdjustInputSize($iTag){
    var $iHint = tgGetHintByTag($iTag);
    size = $iHint.val().length;
    if ($iTag.val().length > size)
        size = $iTag.val().length;
    size += 3;

    $iHint.attr('size', size);
    $iTag.attr('size', size);
}

// Show hint (!!! needs development)
function tgShowHint($iTag, hint){
    var $iHint = tgGetHintByTag($iTag);

    var $val = $iTag.val();
    if (hint.indexOf($val) == 0 && $val != '')
        $iHint.val(hint);
    else
        $iHint.val('');
    tgAdjustInputSize($iTag);
}

// Focus on input when the tag box clicked
function tgFocusInput(){
    var $iTag = $(this).children(".input-box:first").children(".input-box__input:first");
    $iTag.focus();
    var iLen = $iTag.val().length;
    $iTag[0].setSelectionRange(iLen, iLen);
}

// Check key codes when typing new tag
function tgCheckKey(e, $iTag){
    // Hide an arror if there's any
    tgGetErrorByTag($iTag).hide();
    var $iHint = tgGetHintByTag($iTag);
    var code = e.key;
    // adding tag on Enter, ",", ";"
    if (code == "Enter" || code == "," || code == ";"){
        e.preventDefault();
        tgAddTag($iTag);
    }
    // adding hint on tab
    if (code == "Tab" && $iHint.val() != ""){
        e.preventDefault();
        $iTag.val( $iHint.val());        
        tgAddTag($iTag);
    }
    // deleting tag on Backspace
    if (code == "Backspace" && $iTag.val() == ""){
        tgDeleteLastTag($iTag.parent().parent());
    }
    if (code == "ArrowDown"){
        tgSelectNextValue($iTag);
    }
    if (code == "ArrowUp"){
        tgSelectPrevValue(e, $iTag);
    }
    if (code == " " && $iTag.val() == ""){
        e.preventDefault();
    }

}

// function tgCheckInput(e, $iTag){
//     if ($iTag.val() != "" && $.inArray(e.key, ["ArrowUp", "ArrowDown", "Tab", "Enter"]) == -1){
//         tgShowTagList($iTag);
//     }
// }

// Add new tag from field
function tgAddTag($iTag){    
    tgRemoveHintLists();
    var tagVal = $iTag.val(),
        arrValues = tgGetValues($iTag.parent().parent(), "strict");
    if (!tgIsItemInArrayCI(tagVal, arrValues)){
        // creating DOM elements
        var $newTag = $("<div></div>")
            .addClass("tags__item")
            .html(tagVal)
        $("<button tabindex='-1'></button>")
            .html("<img src='" + imgPath + "tag_close.png' alt='Закрыть'>")
            .click(tgDeleteTag)
            .appendTo($newTag)
        $newTag.insertBefore($iTag.parent());

        var $iHint = tgGetHintByTag($iTag);
        $iHint.val('');
        $iTag.val('');
    }
    else{
        tgGetErrorByTag($iTag).show();
    }
}

// Delete tag by the button it's containing
function tgDeleteTag(){
    $(this).parent().remove();
}

// Delete last tag in the tagbox
function tgDeleteLastTag($tagBox){
    if ($tagBox.children().length > 1)
        $tagBox.children().eq(-2).remove();
}

// Clears tags and input by id
function tgDeleteAll(tagsId){
    $(tagsId).children(".tags__item").remove();
    $(tagsId + " input").val('');
}

// Return tag values by it's parent id
function tgGetStrValues(tagsId){
    res = '';
    $(tagsId).children(".tags__item").each(function(){
        res += $(this).html().split("<button")[0] + ',';
    });

    // adding value from input
    let input_val = $(tagsId).find(".input-box__input").val();
    if (input_val) res += input_val + ',';

    // removing last symbol ','
    if (res != '')
        res = res.slice(0, -1);
    return res;
}

// Return tag values with links by it's parent id
function tgGetLinkValues(tagsId, linkType){
    var arrValues = tgGetValues($(tagsId));
    res = '';
    $.each(arrValues, function(index, item){
        res += `<a href="${searchLink}?${linkType}=${item}">${item}</a>, `;
    });
    if (res != '')
        res = res.slice(0, -2);
    else
        res = "<span>не указано</span>";
    return res;
}