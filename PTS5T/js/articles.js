function catRoll()
{
    if ($("#catul").css("display") == "none")
    {
        $("#catul").fadeIn('slow');
        $("#catroll").text("(-)");
    }
    else
    {
        $("#catul").hide();
        $("#catroll").text("(+)");
    }
}