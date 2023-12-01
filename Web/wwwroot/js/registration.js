//скрипт регистрации пользователя в системе

var xhr;

//добавляет обработку события нажатия на кнопку регистрации, добавляет пользователя в базу данных и авторизует его
$(document).ready(function () {
    $('#form').on('submit', function (e) {
        e.preventDefault();

        var formData = $(this).serialize();
        const ajaxRequests = [];
        
        const request = $.ajax({
            type: 'POST',
            url: '/api/Users',
            data: formData,
            success: function () {
                
            },
            error: function (xhr, status, error) {
                var errorMessage = xhr.responseText || xhr.statusText;
                console.log('Error! ' + errorMessage);
                document.getElementById("error").style.display = 'block';
                document.getElementById("error").textContent = 'Error! ' + errorMessage;
            }
        });
        ajaxRequests.push(request);
        
        Promise.all(ajaxRequests)
            .then(() => {
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
            })
    });
});