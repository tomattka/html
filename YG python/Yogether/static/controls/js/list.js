$(document).ready(function(){

    var $iFields = $(".select-list__input");

    $iFields.on("keyup", lsShowClearButton); // clear button    
    $iFields.on("keydown", function(e) { lsCheckKey(e, $(this)); }); // check heys
    //$iFields.on("keyup", function(e) { lsShowHintList(e, $(this)); }); // show hints
    $iFields.focusout(lsRemoveHintLists);

    $(".select-list__clear").click(lsClearInput);
    
});
// -------- Item select functions --------------------

function lsSelectListItem(e){
    var $listItem = $(this);
    var $iField = $listItem.parent().prev(":first").children(".select-list__input");
    $iField.val($listItem.html());
    lsRemoveHintLists();
    e.preventDefault();
}

function lsGetHintsByField($iField){
    var $hintList = $iField.parent().next(":first");
    if (!$hintList.hasClass("select-list__hints"))
        $hintList = null;
    return $hintList;
}

function lsApplySelectedValue($iField, $hintList){
    $iField.val($hintList.children(".selected:first").html());
}

function lsSelectNextValue($iField){
    var $hintList = lsGetHintsByField($iField);
    if ($hintList){
        var $selectedItem = $hintList.children("a.selected");
        if ($selectedItem[0]){ // some item already selected
            $selectedItem.removeClass("selected");
            if ($selectedItem.next()[0]) // if there's next item
                $selectedItem.next().addClass("selected");
            else
                $hintList.children("a:first").addClass("selected");
        }
        else 
        {
            $hintList.children("a:first").addClass("selected"); // !!!!!!!!!!! think of this construction
        }
        lsApplySelectedValue($iField, $hintList);
        
    }
}

function lsSelectPrevValue(e, $iField){
    var $hintList = lsGetHintsByField($iField);
    var $selectedItem = $hintList.children(".selected");
    if ($hintList && $selectedItem[0]){
        e.preventDefault();
        $selectedItem.removeClass("selected");
        if ($selectedItem.prev("a")[0])
            $selectedItem.prev("a").addClass("selected");
        else
            $hintList.children(":last").addClass("selected");

        
        lsApplySelectedValue($iField, $hintList);
    }

}

function lsCheckKey(e, $iField){
    var code = e.key;
    // adding tag on Enter, ",", ";"
    if (code == "Enter" || code == "Escape"){        
        lsRemoveHintLists();
    }
    if (code == "ArrowDown"){
        lsSelectNextValue($iField);
    }
    if (code == "ArrowUp"){
        lsSelectPrevValue(e, $iField);
    }

}


// ------------- List display ------------------------

// function lsShowHintList(e, $iField){
//     if ($.inArray(e.key, ["ArrowUp", "ArrowDown", "Tab", "Enter", "Escape"]) == -1){
//         if ($iField.val() != ""){
//             var tagValue = $iField.val();
//             var arrTagList = ["Москва", "Моршанск", "Морозовск", "Можга"]; // !!! будет получаться из аджакса
//             if (arrTagList.length > 0){
//                 lsDisplayHintList($iField, arrTagList);
//             }
//         }
//         else
//             lsRemoveHintLists();
//     }
// }

function lsDisplayHintList($iField, arrTagList){
    // creating block
    var $selectList = $("<div></div>")
        .addClass("select-list__hints");

    $("<div class=\"select-list__split\"></div>").appendTo($selectList); // split line

    $.each(arrTagList, function(index, value){
        $("<a href='#'></a>")
            .html(value)
            .mousedown(function(e){ e.preventDefault(); }) // prevent from stealing focus
            .click(lsSelectListItem)
            .appendTo($selectList)
    });
    // adapting width
    var listWidth = $iField.parent().parent().css("width");
    $selectList.css("width", listWidth);

    // adding block
    lsRemoveHintLists();
    $selectList.insertAfter($iField.parent());
}

function lsRemoveHintLists(){
    $(".select-list__hints").remove();
}

// ----------- Clear button ------------

function lsShowClearButton(){
    var $iField = $(this),
        $iClear = $iField.next(":first");
    if ($iField.val() != "")
        $iClear.show();
    else
        $iClear.hide();
}

function lsClearInput(){
    var $iClear = $(this),
        $iField = $iClear.prev(":first");
    $iField.val('');
    $iClear.hide();  
    lsRemoveHintLists();  
}