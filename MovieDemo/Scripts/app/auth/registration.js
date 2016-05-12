$(function () {
    $("#registerForm").submit(function (e) {
        e.preventDefault();
        var data = $("#registerForm").serializeArray();
        var model = {};
        data.forEach(function (el) {
            model[el.name] = el.value;
        });
        $.post('api/account/register', model)
        .then(function (success) {
            location.href = location.href;
        },
        function (error) {
            var errors = error.responseJSON.ModelState;
            var fullText = "";
            for (key in errors)
            {
                fullText += errors[key][0] + '<br>';
            }
            $('#registrationFail').html(fullText).show();
        });
    });
})