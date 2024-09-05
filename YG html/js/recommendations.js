$(document).ready(function(){
    $(".item__delete").click(function(e) {reccomendationDelete(e);});
});

function reccomendationDelete(e){
    e.stopPropagation();
    e.preventDefault();
    alert('la-la-la!');
    
}