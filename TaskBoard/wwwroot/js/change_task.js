//скрипт изменения задачи

var taskId;
var sprintId;
var role;

//вызывает метод изменения задачи
function changeTask() {
    const task = {
        'id': taskId,
        'name': document.getElementById('name').value,
        'description': document.getElementById('description').value,
        'status': document.getElementById('status').value,
        'comment': document.getElementById('comment').value,
        'userId': document.getElementById('usersDdl').value,
        'sprintId': sprintId
    };
    $.ajax({
        type: 'PUT',
        url: '/api/Tasks',
        data: task,
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

//открывает форму для изменения задачи
function openFormForChangeTask(id) {
    role = document.getElementById('username').dataset.role;
    taskId = id;
    const popup = document.createElement('div');
    popup.className = 'popup';
    const formContainer = createFormForChangeTask(id);
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

//создаёт форму для изменения задачи
function createFormForChangeTask(id) {
    const formContainer = document.createElement('div');
    formContainer.classList.add('form-container');
    $.ajax({
        type: 'GET',
        url: `/api/Tasks/Task/${id}`,
        success: function (response) {
            sprintId = response.sprintId;
            
            const form = document.createElement('form');
            form.id = 'form';

            const heading = document.createElement('h2');
            heading.setAttribute('id', 'heading');
            heading.dataset.id = id;
            heading.textContent = 'Change Task';

            const nameLabel = document.createElement('label');
            nameLabel.setAttribute('for', 'name');
            nameLabel.textContent = 'Name';
            const nameInput = document.createElement('input');
            nameInput.setAttribute('name', 'name');
            nameInput.setAttribute('id', 'name');
            nameInput.setAttribute('type', 'text');
            nameInput.setAttribute('autofocus', 'true');
            nameInput.setAttribute('required', 'true');
            nameInput.value =  response.name;

            const descLabel = document.createElement('label');
            descLabel.setAttribute('for', 'description');
            descLabel.textContent = 'Description';
            const descInput = document.createElement('input');
            descInput.setAttribute('name', 'description');
            descInput.setAttribute('id', 'description');
            descInput.setAttribute('type', 'text');
            descInput.setAttribute('required', 'true');
            descInput.value = response.description;

            const statusLabel = document.createElement('label');
            statusLabel.setAttribute('for', 'status');
            statusLabel.textContent = 'Status';
            const status = document.createElement('select');
            status.setAttribute('name', 'status');
            status.setAttribute('id', 'status');
            status.setAttribute('placeholder', 'Select status');
            
            const newOption = document.createElement('option');
            newOption.value = '0';
            newOption.textContent = 'New';
            const inProgressOption = document.createElement('option');
            inProgressOption.value = '1';
            inProgressOption.textContent = 'In Progress';
            const completedOption = document.createElement('option');
            completedOption.value = '2';
            completedOption.textContent = 'Completed';
            
            switch (response.status) {
                case 0: newOption.selected = true; break;
                case 1: inProgressOption.selected = true; break;
                case 2: completedOption.selected = true; break;
            }
            
            status.appendChild(newOption);
            status.appendChild(inProgressOption);
            status.appendChild(completedOption);
            
            const commLabel = document.createElement('label');
            commLabel.setAttribute('for', 'comment');
            commLabel.textContent = 'Comment';
            const commInput = document.createElement('input');
            commInput.setAttribute('name', 'comment');
            commInput.setAttribute('id', 'comment');
            commInput.setAttribute('type', 'text');
            commInput.setAttribute('required', 'true');
            commInput.value =  response.comment;

            const usersLabel = document.createElement('label');
            usersLabel.setAttribute('for', 'usersDdl');
            usersLabel.textContent = 'Users';
            const users = document.createElement('select');
            users.setAttribute('name', 'usersDdl');
            users.setAttribute('id', 'usersDdl');
            users.setAttribute('placeholder', 'Select user');
            
            var taskUser = response.userId;
            $.ajax({
                type: 'GET',
                url: `/api/Sprints/Sprint/${response.sprintId}/Users`,
                success: function (response) {
                    response.forEach(user => {
                        const userOption = document.createElement('option');
                        userOption.value = user.id;
                        userOption.textContent = user.login;
                        if(user.id === taskUser) {
                            userOption.selected = true;
                        }
                        users.appendChild(userOption);
                    });
                },
                error: function (xhr, status, error) {
                    console.log('Error! ' + error);
                }
            });

            const saveButton = document.createElement('button');
            saveButton.setAttribute('type', 'button');
            saveButton.setAttribute('name', 'saveButton');
            saveButton.setAttribute('id', 'saveButton');
            saveButton.setAttribute('value', 'save');
            saveButton.style.width = '300px';
            saveButton.textContent = 'Save changes';
            saveButton.addEventListener('click', changeTask);

            const deleteButton = document.createElement('button');
            deleteButton.setAttribute('type', 'button');
            deleteButton.setAttribute('name', 'deleteButton');
            deleteButton.setAttribute('id', 'deleteButton');
            deleteButton.setAttribute('value', 'delete');
            deleteButton.style.width = '300px';
            deleteButton.textContent = 'Delete Task';
            deleteButton.addEventListener('click', deleteTask);

            if(role === "2") {
                nameLabel.style.display = 'none';
                nameInput.style.display = 'none';
                descLabel.style.display = 'none';
                descInput.style.display = 'none';
                commLabel.style.display = 'none';
                commInput.style.display = 'none';
                usersLabel.style.display = 'none';
                users.style.display = 'none';
                deleteButton.style.display = 'none';
                form.appendChild(heading);
                form.appendChild(nameLabel);
                form.appendChild(nameInput);
                form.appendChild(descLabel);
                form.appendChild(descInput);
                form.appendChild(statusLabel);
                form.appendChild(status);
                form.appendChild(document.createElement('br'));
                form.appendChild(document.createElement('br'));
                form.appendChild(commLabel);
                form.appendChild(commInput);
                form.appendChild(usersLabel);
                form.appendChild(users);
                form.appendChild(saveButton);
                form.appendChild(deleteButton);
            } else {
                form.appendChild(heading);
                form.appendChild(nameLabel);
                form.appendChild(nameInput);
                form.appendChild(document.createElement('br'));
                form.appendChild(document.createElement('br'));
                form.appendChild(descLabel);
                form.appendChild(descInput);
                form.appendChild(document.createElement('br'));
                form.appendChild(document.createElement('br'));
                form.appendChild(statusLabel);
                form.appendChild(status);
                form.appendChild(document.createElement('br'));
                form.appendChild(document.createElement('br'));
                form.appendChild(commLabel);
                form.appendChild(commInput);
                form.appendChild(document.createElement('br'));
                form.appendChild(document.createElement('br'));
                form.appendChild(usersLabel);
                form.appendChild(users);
                form.appendChild(document.createElement('br'));
                form.appendChild(document.createElement('br'));
                form.appendChild(saveButton);
                form.appendChild(document.createElement('br'));
                form.appendChild(document.createElement('br'));
                form.appendChild(deleteButton);
            }

            formContainer.appendChild(form);
        },
        error: function (xhr, status, error) {
            console.log('Error! ' + error);
        }
    });
    return formContainer;
}