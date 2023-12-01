//скрипт вывода данных о пользователях

var xhr;

//получает всех пользователей из базы данных
function getUsers() {
    $.ajax({
        type: 'GET',
        url: `/api/Users`,
        success: function (response) {
            const table = document.getElementById('users_data');
            document.getElementById('users_table').style.display = 'block';
            document.getElementById('sprints_table').style.display = 'none';
            document.getElementById('tasks_table').style.display = 'none';
            table.innerHTML = '';
            response.forEach(data => {
                const row = document.createElement('tr');
                row.classList.add('data-row');
                row.dataset.id = data.id;
                const login = document.createElement('td');
                const password = document.createElement('td');
                const role = document.createElement('td');
                login.textContent = data.login;
                password.textContent = data.password;
                switch (data.role) {
                    case 0: role.textContent = 'Admin'; break;
                    case 1: role.textContent = 'Manager'; break;
                    case 2: role.textContent = 'User'; break;
                }
                row.appendChild(login);
                row.appendChild(password);
                row.appendChild(role);
                row.addEventListener('click', handleUserRowClick);
                table.appendChild(row);
            });

        },
        error: function (xhr, status, error) {
            const table = document.getElementById('users_data');
            table.innerHTML = '';
            console.log('Error! ' + error);
        }
    });
}

//обработчик события нажатия на строку таблицы пользователей
function handleUserRowClick(event) {
    const userRow = event.target.closest('.data-row');
    const popup = document.createElement('div');
    popup.className = 'popup';
    const formContainer = createFormForChangeUser(userRow.dataset.id);
    popup.appendChild(formContainer);
    popup.style.position = 'fixed';
    popup.style.top = '50%';
    popup.style.left = '50%';
    popup.style.transform = 'translate(-50%, -50%)';
    popup.classList.add('active');
    const overlay = document.querySelector('.overlay');
    overlay.classList.add('active');
    overlay.addEventListener('click', closeForm);
    document.body.appendChild(popup);
}

//добавляет обработку события нажатия на элемент при загрузке страницы
$(document).ready(function () {
    const usersMenuItem = document.getElementById('users');

    usersMenuItem.addEventListener('click', function() {
        getUsers();
    });
});
