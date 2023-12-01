//скрипт проверки аутентификации пользователя

var url = "";
var xhr;

//проверяет аутентифицирован ли пользователь и перенаправляет его на нужную страницу (или оставляет на текущей)
$(document).ready(function () {
    var currentPage = window.location.href;
    if (currentPage.includes('registration.html') || currentPage.includes('login.html')) {
        url = "main.html";
    }
    
    $.ajax({
        type: 'GET',
        url: '/api/Users/IsAuthenticated',
        success: function (response, status, xhr) {
            if(xhr.status === 200) {
                if(url === "main.html") {
                    window.location.href = url;
                }
            }
        },
        error: function (xhr, status, error) {
            if (xhr.status === 401) {
                if(url !== "main.html") {
                    window.location.href = "login.html";
                }
            } else {
                console.log('Error! ' + error);
            }
        },
        complete: function() {
            url = ""; 
        }
    });
});