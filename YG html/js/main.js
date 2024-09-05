$(document).ready(function(){
    // --------- More button ---------- //
    $("#aMore").click(scrollToFeatures);

    // ------- Search Field -------- //
    $("input.search").on("keydown", searchKeyDown);
});


// ------------- More button ----------------- //
function scrollToFeatures(){
    $('html,body').animate({scrollTop: $("#features").offset().top},'slow');
    return false;
}

// ------------- Search field ----------------- //
function searchKeyDown(e){
    var val = $(this).val();
    if (val != "" && e.key == "Enter")
        window.location.href = "#" + val;
}