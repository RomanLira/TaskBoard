//скрипт получения имени текущего пользователя

var xhr;

//при загрузке страницы получает имя текущего пользователя и его id, записывая эти данные для дальнейшего функционирования программы
$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/api/Users/GetCurrentUserName',
        success: function (response) {
            document.getElementById("username").textContent = response.login;
        },
        error: function (xhr, status, error) {
            console.log('Error! ' + error);
        }
    });

    $.ajax({
        type: 'GET',
        url: '/api/Users/GetCurrentUserId',
        success: function (response) {
            document.getElementById("username").dataset.id = response.id;
        },
        error: function (xhr, status, error) {
            console.log('Error! ' + error);
        }
    });
});