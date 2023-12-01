//скрипт удаления спринта

//вызывает метод удаления спринта из базы данных
function deleteSprint() {
    var id = document.getElementById('sprint_header').value;

    var confirmDelete = confirm('Delete this sprint?');
    if(confirmDelete) {
        $.ajax({
            type: 'DELETE',
            url: `/api/Sprints/DeleteSprint/${id}`,
            success: function (response) {
                getSprints();
                document.getElementById('tasks_table').style.display = 'none';
                closeForm();
            },
            error: function (xhr, status, error) {
                var errorMessage = xhr.responseText || xhr.statusText;
                console.log('Error! ' + errorMessage);
            }
        });
    }
}