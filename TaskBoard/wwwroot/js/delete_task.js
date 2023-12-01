//скрипт удаления задачи

//вызывает метод удаления задачи из базы данных
function deleteTask() {
    var id = document.getElementById('heading').dataset.id;

    var confirmDelete = confirm('Delete this task?');
    if(confirmDelete) {
        $.ajax({
            type: 'DELETE',
            url: `/api/Tasks/DeleteTask/${id}`,
            success: function (response) {
                getTasks();
                closeForm();
            },
            error: function (xhr, status, error) {
                var errorMessage = xhr.responseText || xhr.statusText;
                console.log('Error! ' + errorMessage);
            }
        });
    }
}