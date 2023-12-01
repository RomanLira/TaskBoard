//скрипт удаления пользователя

//вызывает метод удаления пользователя из базы данных
function deleteUser() {
    var id = document.getElementById('heading').dataset.id;
    var confirmDelete = confirm('Delete this user?');
    if(confirmDelete) {
        $.ajax({
            type: 'DELETE',
            url: `/api/Users/DeleteUser/${id}`,
            success: function (response) {
                getUsers();
                closeForm();
            },
            error: function (xhr, status, error) {
                var errorMessage = xhr.responseText || xhr.statusText;
                console.log('Error! ' + errorMessage);
            }
        });
    }
}