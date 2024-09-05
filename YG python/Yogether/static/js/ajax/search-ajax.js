$(document).ready(function(){    
    $("#iSearch").on("keydown", function(e) { searchCheckKey(e); });
    $("#iLocation").on("keydown", function(e) { searchCheckKey(e); });
    
    $("#bApplyParams").click(searchUsers);
    $("#aClearFilters").click(searchClearFilters);

    checkUrlParams();
    
});

function checkUrlParams(){
    let searchParams = new URLSearchParams(window.location.search);    
    
    // search by text
    if (searchParams.has('txt')){
        let val = searchParams.get('txt');
        $("#iSearch").val(val);
        searchUsers();
    }
    // search by tag
    if (searchParams.has('doctrine') || searchParams.has('tradition') || searchParams.has('practice') || searchParams.has('interest')){

        let val, $iTag;
        if (searchParams.has('doctrine')){
            val = searchParams.get('doctrine');
            $iTag = $("#iDoctrine"); 
        }
        if (searchParams.has('tradition')){
            val = searchParams.get('tradition');
            $iTag = $("#iTradition");
        }
        if (searchParams.has('practice')){
            val = searchParams.get('practice');
            $iTag = $("#iPractice");
        }
        if (searchParams.has('interest')){
            val = searchParams.get('interest');
            $iTag = $("#iInterests");
        }       
        $iTag.val(val);
        tgAddTag($iTag);    

        paramButtonClick(); // showing pawams if not text      
        $('html,body').animate({scrollTop: $(".params").offset().top},'slow');
        searchUsers();
    }

}

function searchCheckKey(e){
    var code = e.key;
    if (code == "Enter")
        searchUsers();
}

function getCookie(name) { // needed for csrf token
    let cookieValue = null;
    if (document.cookie && document.cookie !== '') {
        const cookies = document.cookie.split(';');
        for (let i = 0; i < cookies.length; i++) {
            const cookie = cookies[i].trim();
            // Does this cookie string begin with the name we want?
            if (cookie.substring(0, name.length + 1) === (name + '=')) {
                cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                break;
            }
        }
    }
    return cookieValue;
}

function searchUsers(){
    var request = $("#iSearch").val(),
        params = [];

    params.push({"iLocation": $("#iLocation").val()});
    params.push({"iAgeFrom": $("#iAgeFrom").val()});
    params.push({"iAgeTill": $("#iAgeTill").val()});
    params.push({"iGender": $("#iGender").val()});
    params.push({"iMarital": $("#iMarital").val()});
    params.push({"iExperience": $("#iExperience").val()});
    params.push({"tagsDoctrine": tgGetStrValues('#tagsDoctrine')});
    params.push({"tagsTradition": tgGetStrValues('#tagsTradition')});
    params.push({"tagsPractice": tgGetStrValues('#tagsPractice')});
    params.push({"tagsInterests": tgGetStrValues('#tagsInterests')});

    var search = new Object();
    search.request = request;
    search.params = params;

    const csrftoken = getCookie('csrftoken');

    // showing loader
    var $loader = $("#imgLoading"),
        $results = $("#search-results");
        $results.html('');
    $loader.css("display", "inline");

    $.ajax({
        type: "POST",
        url: "/search/get-results.html",
        data: JSON.stringify(search),
        headers: {'X-CSRFToken': csrftoken},
        mode: 'same-origin',
        error: function(jqXHR, textStatus, errorThrown) {
            alert(`Возникла ошибка: ${errorThrown}`);
            $loader.css("display", "inline");
        }
    }).done(function(answer) {
        $loader.css("display", "none");
        $results.html(answer);
    });

}

function searchClearFilters(e){
    e.preventDefault();

    $("#iLocation").val('');
    $("#iAgeFrom").val('');
    $("#iAgeTill").val('');
    $("#iGender").val('любой');
    $("#iMarital").val('любой');
    $("#iExperience").val('любой');
    tgDeleteAll('#tagsDoctrine');
    tgDeleteAll('#tagsTradition');
    tgDeleteAll('#tagsPractice');
    tgDeleteAll('#tagsInterests');
    
    searchUsers();
}