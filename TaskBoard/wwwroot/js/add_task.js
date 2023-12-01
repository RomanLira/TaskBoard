//скрипт создания задачи

//вызывает метод создания и добавления в базу задачи
function addTask() {
    const task = {
        name: document.getElementById('name').value,
        description: document.getElementById('description').value,
        status: 0,
        comment: document.getElementById('comment').value,
        files: document.getElementById('files').value,
        userId: document.getElementById('usersDdl').value,
        sprintId: document.getElementById('sprint_header').value
    };

    $.ajax({
        type: 'POST',
        url: '/api/Tasks',
        contentType: 'application/json',
        data: JSON.stringify(task),
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

//открывает форму для создания задачи
function openFormForAddTask() {
    const popup = document.createElement('div');
    popup.className = 'popup';
    const formContainer = createFormForAddTask();
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

//создаёт форму для создания задачи
function createFormForAddTask() {
    var id = document.getElementById('sprint_header').value;
    
    const formContainer = document.createElement('div');
    formContainer.classList.add('form-container');

    const form = document.createElement('form');
    form.id = 'form';

    const heading = document.createElement('h2');
    heading.textContent = 'Add New Task';

    const nameLabel = document.createElement('label');
    nameLabel.setAttribute('for', 'name');
    nameLabel.textContent = 'Name';
    const nameInput = document.createElement('input');
    nameInput.setAttribute('name', 'name');
    nameInput.setAttribute('id', 'name');
    nameInput.setAttribute('type', 'text');
    nameInput.setAttribute('placeholder', 'Type Task Name')
    nameInput.setAttribute('autofocus', 'true');
    nameInput.setAttribute('required', 'true');

    const descLabel = document.createElement('label');
    descLabel.setAttribute('for', 'description');
    descLabel.textContent = 'Description';
    const descInput = document.createElement('input');
    descInput.setAttribute('name', 'description');
    descInput.setAttribute('id', 'description');
    descInput.setAttribute('type', 'text');
    descInput.setAttribute('placeholder', 'Type Task Description')
    descInput.setAttribute('required', 'true');

    const commLabel = document.createElement('label');
    commLabel.setAttribute('for', 'comment');
    commLabel.textContent = 'Comment';
    const commInput = document.createElement('input');
    commInput.setAttribute('name', 'comment');
    commInput.setAttribute('id', 'comment');
    commInput.setAttribute('type', 'text');
    commInput.setAttribute('placeholder', 'Type Task Comment');

    const filesLabel = document.createElement('label');
    filesLabel.setAttribute('for', 'files');
    filesLabel.textContent = 'Files';
    const filesInput = document.createElement('input');
    filesInput.setAttribute('name', 'files');
    filesInput.setAttribute('id', 'files');
    filesInput.setAttribute('type', 'file');
    filesInput.setAttribute('placeholder', 'Attach your files');

    const usersLabel = document.createElement('label');
    usersLabel.setAttribute('for', 'usersDdl');
    usersLabel.textContent = 'Users';
    const users = document.createElement('select');
    users.setAttribute('name', 'usersDdl');
    users.setAttribute('id', 'usersDdl');
    users.setAttribute('placeholder', 'Select user');

    $.ajax({
        type: 'GET',
        url: `/api/Sprints/Sprint/${id}/Users`,
        success: function (response) {
            response.forEach(user => {
                const userOption = document.createElement('option');
                userOption.value = user.id;
                userOption.textContent = user.login;
                users.appendChild(userOption);
            });
        },
        error: function (xhr, status, error) {
            console.log('Error! ' + error);
        }
    });

    const addButton = document.createElement('button');
    addButton.setAttribute('type', 'button');
    addButton.setAttribute('name', 'addButton');
    addButton.setAttribute('id', 'addButton');
    addButton.setAttribute('value', 'add');
    addButton.style.width = '300px';
    addButton.textContent = 'Add Task';
    addButton.addEventListener('click', addTask);

    form.appendChild(heading);
    form.appendChild(nameLabel);
    form.appendChild(nameInput);
    form.appendChild(document.createElement('br'));
    form.appendChild(document.createElement('br'));
    form.appendChild(descLabel);
    form.appendChild(descInput);
    form.appendChild(document.createElement('br'));
    form.appendChild(document.createElement('br'));
    form.appendChild(commLabel);
    form.appendChild(commInput);
    form.appendChild(document.createElement('br'));
    form.appendChild(document.createElement('br'));
    form.appendChild(filesLabel);
    form.appendChild(filesInput);
    form.appendChild(document.createElement('br'));
    form.appendChild(document.createElement('br'));
    form.appendChild(usersLabel);
    form.appendChild(users);
    form.appendChild(document.createElement('br'));
    form.appendChild(document.createElement('br'));
    form.appendChild(addButton);

    formContainer.appendChild(form);

    return formContainer;
}