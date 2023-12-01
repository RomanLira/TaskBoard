//скрипт входа пользователя в систему

var xhr;

//добавляет обработку события нажатия на кнопку входа и авторизует пользователя
$(document).ready(function () {
    $('#form').on('submit', function (e) {
        e.preventDefault();

        var formData = $(this).serialize();

        $.ajax({
            type: 'POST',
            url: '/api/Users/Login?cache=' + Date.now(),
            data: formData,
            success: function (response) {
                document.getElementById("error").style.display = 'none';
                window.location.href = "main.html";
            },
            error: function (xhr, status, error) {
                var errorMessage = xhr.responseText || xhr.statusText;
                console.log('Error! ' + errorMessage);
                document.getElementById("error").style.display = 'block';
                document.getElementById("error").textContent = 'Error! ' + errorMessage;
            }
        });
    });
});