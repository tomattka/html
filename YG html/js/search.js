$(document).ready(function(){
    $(".search-input__params-button").click(paramButtonClick);
});

function paramButtonClick(){
    var $params = $(".params");
    if ($params.css("display") == "flex")
        $params.css("display", "none");
    else
        $params.css("display", "flex");
}