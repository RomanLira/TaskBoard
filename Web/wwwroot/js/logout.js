//скрипт выхода пользователя из системы

var xhr;

//завершает сеанс текущего пользователя
function logout() {
    $.ajax({
        type: 'POST',
        url: '/api/Users/Logout',
        success: function (response) {
            window.location.href = "login.html";
        },
        error: function (xhr, status, error) {
            console.log('Error! ' + error);
        }
    });
}