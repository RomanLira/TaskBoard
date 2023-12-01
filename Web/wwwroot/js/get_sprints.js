//скрипт вывода данных о спринтах

var xhr;
var role;

//получает все спринты из базы данных
function getSprints() {
    role = document.getElementById('username').dataset.role;

    var userId = document.getElementById('username').dataset.id;
    var projectId = document.getElementById('project_header').value;
    
    var url;
    if(role === "2") {
        url = `/api/Users/User/${userId}/Sprints`;
    } else {
        url = `/api/Projects/Project/${projectId}/Sprints`;
    }
    
    $.ajax({
        type: 'GET',
        url: url,
        success: function (response) {
            const table = document.getElementById('sprints_data');
            table.innerHTML = '';
            response.forEach(data => {
                if(data.projectId === projectId) {
                    const row = document.createElement('tr');
                    row.classList.add('data-row');
                    row.dataset.id = data.id;
                    const name = document.createElement('td');
                    const start = document.createElement('td');
                    const end = document.createElement('td');
                    name.textContent = data.name;

                    const startDate = new Date(data.startDate);
                    const endDate = new Date(data.endDate);
                    const options = {day: 'numeric', month: 'numeric', year: 'numeric'};

                    start.textContent = startDate.toLocaleDateString('ru-RU', options);
                    end.textContent = endDate.toLocaleDateString('ru-RU', options);
                    row.appendChild(name);
                    row.appendChild(start);
                    row.appendChild(end);
                    row.addEventListener('click', handleSprintRowClick);
                    table.appendChild(row);
                }
            });

        },
        error: function (xhr, status, error) {
            const table = document.getElementById('sprints_data');
            table.innerHTML = '';
            console.log('Error! ' + error);
        }
    });
}

//обработчик события нажатия на строку таблицы спринтов
function handleSprintRowClick(event) {
    const sprintRow = event.target.closest('.data-row');
    getSprintData(sprintRow.dataset.id);
}

//получает данные о конкретном спринте по его id
function getSprintData(id) {
    $.ajax({
        type: 'GET',
        url: `/api/Sprints/Sprint/${id}`,
        success: function (response) {
            const tasksTable = document.getElementById('tasks_table');
            tasksTable.style.display = 'block';
            document.getElementById('sprint_header').innerText = response.name;
            document.getElementById('sprint_header').value = response.id;
            if(role === "2") {
                document.getElementById('tasks_table_buttons').style.display = 'none';
            }
            getTasks();
        },
        error: function (xhr, status, error) {
            console.log('Error! ' + error);
        }
    });
}