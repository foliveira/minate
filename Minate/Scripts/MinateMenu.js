$(function () {
    $("#menu").lavaLamp({
        fx: "easeinout",
        speed: 700,
        click: function (event, menuItem) {
            return true;
        }
    });

    $("#loginLink > a").click(function () {
        $(this).toggle();
        $("div#menuLoginForm").toggle();
        return false;
    });
});		