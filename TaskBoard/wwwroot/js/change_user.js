//скрипт для изменения пользователя

var userId;

//вызывает метод изменения пользователя
function changeUser() {
    const user = {
        'id': userId,
        'login': document.getElementById('name').value,
        'password': document.getElementById('password').value,
        'role': document.getElementById('role').value
    }
    $.ajax({
        type: 'PUT',
        url: '/api/Users',
        data: user,
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

//создаёт форму для изменения пользователя
function createFormForChangeUser(id) {
    userId = id;
    const formContainer = document.createElement('div');
    formContainer.classList.add('form-container');
    $.ajax({
        type: 'GET',
        url: `/api/Users/User/${id}`,
        success: function (response) {
            const form = document.createElement('form');
            form.id = 'form';

            const heading = document.createElement('h2');
            heading.setAttribute('id', 'heading');
            heading.dataset.id = id;
            heading.textContent = 'Change User';

            const nameLabel = document.createElement('label');
            nameLabel.setAttribute('for', 'name');
            nameLabel.textContent = 'Login';
            const nameInput = document.createElement('input');
            nameInput.setAttribute('name', 'name');
            nameInput.setAttribute('id', 'name');
            nameInput.setAttribute('type', 'text');
            nameInput.setAttribute('autofocus', 'true');
            nameInput.setAttribute('required', 'true');
            nameInput.value =  response.login;

            const passLabel = document.createElement('label');
            passLabel.setAttribute('for', 'password');
            passLabel.textContent = 'Password';
            const passInput = document.createElement('input');
            passInput.setAttribute('name', 'password');
            passInput.setAttribute('id', 'password');
            passInput.setAttribute('type', 'text');
            passInput.setAttribute('required', 'true');
            passInput.value = response.password;

            const roleLabel = document.createElement('label');
            roleLabel.setAttribute('for', 'role');
            roleLabel.textContent = 'Role';
            const role = document.createElement('select');
            role.setAttribute('name', 'role');
            role.setAttribute('id', 'role');
            role.setAttribute('placeholder', 'Select role');

            const adminOption = document.createElement('option');
            adminOption.value = '0';
            adminOption.textContent = 'Admin';
            const managerOption = document.createElement('option');
            managerOption.value = '1';
            managerOption.textContent = 'Manager';
            const userOption = document.createElement('option');
            userOption.value = '2';
            userOption.textContent = 'User';

            switch (response.role) {
                case 0: adminOption.selected = true; break;
                case 1: managerOption.selected = true; break;
                case 2: userOption.selected = true; break;
            }

            role.appendChild(adminOption);
            role.appendChild(managerOption);
            role.appendChild(userOption);

            const saveButton = document.createElement('button');
            saveButton.setAttribute('type', 'button');
            saveButton.setAttribute('name', 'saveButton');
            saveButton.setAttribute('id', 'saveButton');
            saveButton.setAttribute('value', 'save');
            saveButton.style.width = '300px';
            saveButton.textContent = 'Save changes';
            saveButton.addEventListener('click', changeUser);

            const deleteButton = document.createElement('button');
            deleteButton.setAttribute('type', 'button');
            deleteButton.setAttribute('name', 'deleteButton');
            deleteButton.setAttribute('id', 'deleteButton');
            deleteButton.setAttribute('value', 'delete');
            deleteButton.style.width = '300px';
            deleteButton.textContent = 'Delete User';
            deleteButton.addEventListener('click', deleteUser);

            form.appendChild(heading);
            form.appendChild(nameLabel);
            form.appendChild(nameInput);
            form.appendChild(document.createElement('br'));
            form.appendChild(document.createElement('br'));
            form.appendChild(passLabel);
            form.appendChild(passInput);
            form.appendChild(document.createElement('br'));
            form.appendChild(document.createElement('br'));
            form.appendChild(roleLabel);
            form.appendChild(role);
            form.appendChild(document.createElement('br'));
            form.appendChild(document.createElement('br'))
            form.appendChild(saveButton);
            form.appendChild(document.createElement('br'));
            form.appendChild(document.createElement('br'));
            form.appendChild(deleteButton);

            formContainer.appendChild(form);
        },
        error: function (xhr, status, error) {
            console.log('Error! ' + error);
        }
    });
    return formContainer;
}