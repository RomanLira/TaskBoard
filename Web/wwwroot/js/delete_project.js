//скрипт удаления проекта

//вызывает метод удаления проекта из базы данных
function deleteProject() {
    var id = document.getElementById('project_header').value;

    var confirmDelete = confirm('Delete this project?');
    if(confirmDelete) {
        $.ajax({
            type: 'DELETE',
            url: `/api/Projects/DeleteProject/${id}`,
            success: function (response) {
                getProjects();
                getProjects();
                document.getElementById('sprints_table').style.display = 'none';
                closeForm();
            },
            error: function (xhr, status, error) {
                var errorMessage = xhr.responseText || xhr.statusText;
                console.log('Error! ' + errorMessage);
            }
        });
    }
}