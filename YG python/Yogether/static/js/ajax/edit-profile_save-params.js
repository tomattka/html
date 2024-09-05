$(document).ready(function(){
    $("#bSaveLocation").click(paramSaveLocation);
    $("#bSaveGender").click(paramSaveGender);
    $("#bSaveMarital").click(paramSaveMarital);
    $("#bSaveExperience").click(paramSaveExperience);
    $("#bBirthDay").click(paramSaveBirthDay);
    $("#bInterests").click(paramSaveInterests);
    $("#bAbout").click(paramSaveAbout);    
    $("#bRequest").click(paramSaveRequest);    
    $("#bName").click(paramSaveName);  
    $("#bDoctrines").click(paramSaveDoctrines);     
    $("#bTraditions").click(paramSaveTraditions);     
    $("#bPractices").click(paramSavePractices);     

    // Gender change adjustment
    $("#iGender").parent().next().children().each(function(){
        $(this).click(paramChangeGender);
    });
    
});

// Location
function paramSaveLocation(){
    var value = $("#iLocation").val();
    $.ajax({
        type: "GET",
        url: "/profile/save-parameter.html?parameter=location&value=" + value
    }).done(function(answer) {
        if (value == "")
            value = "<span>не указано</span>";
        $("#dLocation").html(value);
        infoSaveClick($("#bSaveLocation")); // $(this) not working
    });
}

// Gender
function paramChangeGender(){
    let gender = $(this).html();
    if (gender == "женский"){
        // change values
        let currentVal = $("#iMarital").val(),
            newVal = currentVal.replace("свободен", "свободна").replace("женат", "замужем");
        $("#iMarital").val(newVal);
        $("#dMarital").html(newVal);
        // change list
        let $list = $("#iMarital").parent().next();
        $list.children().each(function(){
            let itemVal = $(this).html();
            $(this).html(itemVal.replace("свободен", "свободна").replace("женат", "замужем"));
        });        
    }
    else{ // non-female
        // change values
        let currentVal = $("#iMarital").val(),
        newVal = currentVal.replace("свободна", "свободен").replace("замужем", "женат");
        $("#iMarital").val(newVal);
        $("#dMarital").html(newVal);
        // change list
        let $list = $("#iMarital").parent().next();
        $list.children().each(function(){
            let itemVal = $(this).html();
            $(this).html(itemVal.replace("свободна", "свободен").replace("замужем", "женат"));
        });   
    }
}

function paramSaveGender(){
    var value = $("#iGender").val();
    $.ajax({
        type: "GET",
        url: "/profile/save-parameter.html?parameter=gender&value=" + value
    }).done(function(answer) {
        $("#dGender").html(value);
        infoSaveClick($("#bSaveGender"));
    });
}

// Marital Status
function paramSaveMarital(){
    var value = $("#iMarital").val();
    $.ajax({
        type: "GET",
        url: "/profile/save-parameter.html?parameter=marital&value=" + value
    }).done(function(answer) {
        value  = value.replace("не указано", "<span>не указано</span>");
        $("#dMarital").html(value);
        infoSaveClick($("#bSaveMarital"));
    });
}

// Experience
function paramSaveExperience(){
    var value = $("#iExperience").val();
    $.ajax({
        type: "GET",
        url: "/profile/save-parameter.html?parameter=experience&value=" + value
    }).done(function(answer) {
        value  = value.replace("не указано", "<span>не указано</span>");
        $("#dExperience").html(value);
        infoSaveClick($("#bSaveExperience"));
    });
}

// Date
function paramSaveBirthDay(){
    if (dtCheckFormat("#iBirthDay")){
        var value = $("#iBirthDay").val();
        $.ajax({
            type: "GET",
            url: "/profile/save-parameter.html?parameter=birthday&value=" + value
        }).done(function(answer) {
            if (answer == "")
                answer = "<span>не указано</span>";
            $("#dBirthDay").html(answer);
            infoSaveClick($("#bBirthDay"));
        });
    }
}

// Interests
function paramSaveInterests(){
    var value = tgGetStrValues('#tagsInterests');
    $.ajax({
        type: "GET",
        url: "/profile/save-parameter.html?parameter=interests&value=" + value
    }).done(function(answer) {
        $("#dInterests").html(tgGetLinkValues('#tagsInterests', 'interest'));
        infoSaveClick($("#bInterests"));
    });
}

// About 
function paramSaveAbout(){
    var value = $("#tAbout").val().replace(/(?:\r\n|\r|\n)/g, '<br />');
    $.ajax({
        type: "GET",
        url: "/profile/save-parameter.html?parameter=about&value=" + value
    }).done(function(answer) {
        if (value == "")
            value = "<span>не указано</span>";
        $("#dAbout").html(value);
        infoSaveClick($("#bAbout"));
    });

}

// Request 
function paramSaveRequest(){
    var value = $("#tRequest").val().replace(/(?:\r\n|\r|\n)/g, '<br />');
    $.ajax({
        type: "GET",
        url: "/profile/save-parameter.html?parameter=request&value=" + value
    }).done(function(answer) {
        if (value == "")
            value = "<span>не указано</span>";
        $("#dRequest").html(value);
        infoSaveClick($("#bRequest"));
    });

}

// Name
function paramSaveName(){
    var name = $("#iName").val(),
        family = $("#iFamily").val();
    $.ajax({
        type: "GET",
        url: "/profile/save-parameter.html?parameter=name&value=" + name + "&family=" + family
    }).done(function(answer) {
        if (name != "")
            $("#sName").html(name + " <br>" + family)
        nameSaveClick($("#bName"));
    });
}

// Doctrines
function paramSaveDoctrines(){
    var value = tgGetStrValues('#tagsDoctrines');
    $.ajax({
        type: "GET",
        url: "/profile/save-parameter.html?parameter=doctrines&value=" + value
    }).done(function(answer) {
        $("#dDoctrines").html(tgGetLinkValues('#tagsDoctrines', 'doctrine'));
        infoSaveClick($("#bDoctrines"));
    });
}

// Traditions
function paramSaveTraditions(){
    var value = tgGetStrValues('#tagsTraditions');
    $.ajax({
        type: "GET",
        url: "/profile/save-parameter.html?parameter=traditions&value=" + value
    }).done(function(answer) {
        $("#dTraditions").html(tgGetLinkValues('#tagsTraditions', 'tradition'));
        infoSaveClick($("#bTraditions"));
    });
}

// Practices
function paramSavePractices(){
    var value = tgGetStrValues('#tagsPractices');
    $.ajax({
        type: "GET",
        url: "/profile/save-parameter.html?parameter=practices&value=" + value
    }).done(function(answer) {
        $("#dPractices").html(tgGetLinkValues('#tagsPractices', 'practice'));
        infoSaveClick($("#bPractices"));
    });
}