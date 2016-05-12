$(function()
{
    var like = $("#likeBut");
    var unlike = $("#unlikeBut");
    $(toHide).hide();
    like.click(function () {
        $.post("/api/Likes/Like", model)
        .then(function () {
            like.hide();
            unlike.show();
            $("#likesAm").text(parseInt($("#likesAm").text()) + 1);
        },
        function () {
        });

    });
    unlike.click(function () {
        $.post("/api/Likes/Unlike", model)
        .then(function () {
            unlike.hide();
            like.show();
            $("#likesAm").text(parseInt($("#likesAm").text()) -1 );
        },
        function () {
        });
      
    });
})