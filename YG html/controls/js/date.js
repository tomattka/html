function dtCheckFormat(dateId){
    var res = true,
        date_regex = /^(0[1-9]|1[0-2]).(0[1-9]|1\d|2\d|3[01]).(19|20)\d{2}$/,
        testDate = $(dateId).val();
    if (testDate != "" && !(date_regex.test(testDate)) && !(Date.parse(testDate)) )
    {
        $(".date__error").show();
        res = false;
    }
    else
    {
        $(".date__error").hide();
    }
    return res;

}