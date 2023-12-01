//скрипт вывода данных о задачах

var xhr;
var role;

//получает все задачи из базы данных
function getTasks() {
    role = document.getElementById('username').dataset.role;
    
    var sprintId = document.getElementById('sprint_header').value;
    var userId = document.getElementById('username').dataset.id;
    
    var url;
    if(role === "2") {
        url = `/api/Users/User/${userId}/Tasks`;
    } else {
        url = `/api/Sprints/Sprint/${sprintId}/Tasks`
    }
    
    $.ajax({
        type: 'GET',
        url: url,
        success: function (response) {
            const table = document.getElementById('tasks_data');
            table.innerHTML = '';
            response.forEach(data => {
                if(data.sprintId === sprintId) {
                    const row = document.createElement('tr');
                    row.classList.add('data-row');
                    row.dataset.id = data.id;

                    const name = document.createElement('td');
                    const user = document.createElement('td');
                    const status = document.createElement('td');

                    name.textContent = data.name;

                    getUserNameById(data.userId)
                        .then(function (username) {
                            user.textContent = username;
                        })
                        .catch(function (error) {
                            console.log('Error:', error);
                        });

                    switch (data.status) {
                        case 0:
                            status.textContent = 'New';
                            break;
                        case 1:
                            status.textContent = 'In Progress';
                            break;
                        case 2:
                            status.textContent = 'Completed';
                            break;
                    }

                    row.appendChild(name);
                    row.appendChild(user);
                    row.appendChild(status);
                    row.addEventListener('click', handleTaskRowClick);
                    table.appendChild(row);
                }
            });

        },
        error: function (xhr, status, error) {
            const table = document.getElementById('tasks_data');
            table.innerHTML = '';
            console.log('Error! ' + error);
        }
    });
}

//обработчик события нажатия на строку таблицы задач
function handleTaskRowClick(event) {
    const taskRow = event.target.closest('.data-row');
    openFormForChangeTask(taskRow.dataset.id);
}

//получает и возвращает имя пользователя по его id (необходимо для вывода данных о задачах)
function getUserNameById(id) {
    return new Promise(function(resolve, reject) {
        $.ajax({
            type: 'GET',
            url: `/api/Users/User/${id}`,
            success: function (response) {
                resolve(response.login);
            },
            error: function (xhr, status, error) {
                reject(error);
            }
        });
    });
}