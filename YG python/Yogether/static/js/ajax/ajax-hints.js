// Different hint fields has it's own id for individual hint popup event
// This file is responsible for popup ajax events and should go after controls js files, as it uses it's functions

$(document).ready(function(){
    // Show list hints
    $("#iLocation").on("keyup", function(e) { lsShowHintList(e, $(this), 'location'); });

    // Show tag hints
    $("#iInterests").on("keyup", function(e) { tgShowHintList(e, $(this), 'interests'); });
    $("#iDoctrine").on("keyup", function(e) { tgShowHintList(e, $(this), 'doctrines'); });
    $("#iTradition").on("keyup", function(e) { tgShowHintList(e, $(this), 'traditions'); });
    $("#iPractice").on("keyup", function(e) { tgShowHintList(e, $(this), 'practices'); });
});

function lsShowHintList(e, $iField, vocabulary){
    if ($.inArray(e.key, ["ArrowUp", "ArrowDown", "Tab", "Enter", "Escape"]) == -1){

        if ($iField.val() != ""){

            var tagValue = $iField.val();
            var arrTagList = [];

            $.ajax({
                type: "GET",
                url: "/profile/get-hints.html?vocabulary=" + vocabulary + "&value=" + tagValue
            }).done(function(answer) { 
                if (answer != ""){    
                    arrTagList =  answer.split('<br>');
                    if (arrTagList.length > 1)
                        arrTagList.pop();
                    lsDisplayHintList($iField, arrTagList);
                }
            });
        }
        else
            lsRemoveHintLists();
    }

}

function tgShowHintList(e, $iField, vocabulary){
    if ($.inArray(e.key, ["ArrowUp", "ArrowDown", "Tab", "Enter", "Escape"]) == -1){

        if ($iField.val() != ""){

            var tagValue = $iField.val();
            var arrTagList = [];

            $.ajax({
                type: "GET",
                url: "/profile/get-hints.html?vocabulary=" + vocabulary + "&value=" + tagValue
            }).done(function(answer) { 
                if (answer){
                    arrTagList =  answer.split('<br>');
                    if (arrTagList.length > 1)
                        arrTagList.pop();
                    tgDisplayTagList($iField, arrTagList);
                    tgShowHint($iField, arrTagList[0]);
                }
            });
        }
        else
            lsRemoveHintLists();
    }

}