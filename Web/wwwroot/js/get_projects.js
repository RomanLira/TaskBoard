//скрипт вывода данных о проектах

var xhr;
let projectsVisible = false;

//получает данные о конкретном проекте по его id
function getProjectData(id) {
    var role = document.getElementById('username').dataset.role;
    
    document.getElementById('users_table').style.display = 'none';
        $.ajax({
            type: 'GET',
            url: `/api/Projects/Project/${id}`,
            success: function (response) {
                const sprintsTable = document.getElementById('sprints_table');
                sprintsTable.style.display = 'block';
                document.getElementById('project_header').innerText = response.name;
                document.getElementById('project_header').value = response.id;
                document.getElementById('tasks_table').style.display = 'none';
                if(role === "2") {
                    document.getElementById('sprints_table_buttons').style.display = 'none';
                }
                getSprints();
            },
            error: function (xhr, status, error) {
                console.log('Error! ' + error);
            }
        });
}

//получает все проекты из базы данных
function getProjects() {
    var role = document.getElementById('username').dataset.role;
    
    var id = document.getElementById('username').dataset.id;
    
    const projectsMenuItem = document.getElementById('projects');
    const menu = document.querySelector('.menu');
    const usersMenuItem = document.getElementById('users');
    
    var url;
    if(role === "2") {
        url = `/api/Users/User/${id}/Projects`;
    } else {
        url = '/api/Projects/';
    }
    
    if (projectsVisible) {
        let elementToRemove = projectsMenuItem.nextElementSibling;
        while (elementToRemove !== usersMenuItem) {
            const nextElement = elementToRemove.nextElementSibling;
            menu.removeChild(elementToRemove);
            elementToRemove = nextElement;
        }
        projectsVisible = false;
    } else {
        $.ajax({
            type: 'GET',
            url: url,
            success: function (response) {
                response.forEach(data => {
                    const newSubitem = document.createElement('div');
                    newSubitem.className = 'menu-subitem';
                    newSubitem.textContent = data.name;
                    newSubitem.setAttribute('id', data.id);
                    newSubitem.addEventListener('click', function () {
                        getProjectData(data.id);
                    });
                    menu.insertBefore(newSubitem, usersMenuItem);
                });
                projectsVisible = true;
            },
            error: function (xhr, status, error) {
                console.log('Error! ' + error);
            }
        });
    }
}

//добавляет обработку события нажатия на элемент при загрузке страницы
$(document).ready(function () {
    const projectsMenuItem = document.getElementById('projects');
    
    projectsMenuItem.addEventListener('click', function() {
        getProjects();
    });
});
