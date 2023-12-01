//скрипт получения роли текущего пользователя

var xhr;

//при загрузке страницы получает данные о роли текущего пользователя
$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/api/Users/GetCurrentUserRole',
        success: function (response) {
            switch(response.role) {
                case '1': document.getElementById('users').style.display = 'none'; break;
                case '2': 
                    document.getElementById('users').style.display = 'none';
                    document.getElementById('username').dataset.role = response.role;
                    break;
            }
        },
        error: function (xhr, status, error) {
            console.log('Error! ' + error);
        }
    });
});