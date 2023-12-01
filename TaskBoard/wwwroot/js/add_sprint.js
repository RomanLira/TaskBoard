//скрипт создания спринта

//вызывает метод создания и добавления в базу спринта
function addSprint() {
    const sprint = {
        name: document.getElementById('name').value,
        description: document.getElementById('description').value,
        startDate: document.getElementById('start').value,
        endDate: document.getElementById('end').value,
        projectId: document.getElementById('project_header').value,
        comment: document.getElementById('comment').value,
        files: document.getElementById('files').value
    };
    
    const users = [];
    const ajaxRequests = [];
    const selectedData = $('#users').select2('data');
    selectedData.forEach(data => {
        const request = $.ajax({
            type: 'GET',
            url: `/api/Users/User/${data.id}`,
            success: function (response) {
                users.push(response.id);
            },
            error: function (xhr, status, error) {
                console.log('Error! ' + error);
            }
        });
        ajaxRequests.push(request);
    });
    
    Promise.all(ajaxRequests)
        .then(() => {
            const sprintModel = {
                sprint: sprint,
                users: users
            };
            $.ajax({
                type: 'POST',
                url: '/api/Sprints',
                contentType: 'application/json',
                data: JSON.stringify(sprintModel),

                success: function (response) {
                    getSprints();
                    closeForm();
                },
                error: function (xhr, status, error) {
                    var errorMessage = xhr.responseText || xhr.statusText;
                    console.log('Error! ' + errorMessage);
                }
            });
        })
        .catch(error => {
            console.error('Error when executing requests: ', error);
        });
}

//открывает форму для создания спринта
function openFormForAddSprint() {
    const popup = document.createElement('div');
    popup.className = 'popup';
    const formContainer = createFormForAddSprint();
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

//создаёт форму для создания спринта
function createFormForAddSprint() {
    const formContainer = document.createElement('div');
    formContainer.classList.add('form-container');

    const form = document.createElement('form');
    form.id = 'form';

    const heading = document.createElement('h2');
    heading.textContent = 'Add New Sprint';

    const nameLabel = document.createElement('label');
    nameLabel.setAttribute('for', 'name');
    nameLabel.textContent = 'Name';
    const nameInput = document.createElement('input');
    nameInput.setAttribute('name', 'name');
    nameInput.setAttribute('id', 'name');
    nameInput.setAttribute('type', 'text');
    nameInput.setAttribute('placeholder', 'Type Sprint Name')
    nameInput.setAttribute('autofocus', 'true');
    nameInput.setAttribute('required', 'true');

    const descLabel = document.createElement('label');
    descLabel.setAttribute('for', 'description');
    descLabel.textContent = 'Description';
    const descInput = document.createElement('input');
    descInput.setAttribute('name', 'description');
    descInput.setAttribute('id', 'description');
    descInput.setAttribute('type', 'text');
    descInput.setAttribute('placeholder', 'Type Sprint Description')
    descInput.setAttribute('required', 'true');

    const startLabel = document.createElement('label');
    startLabel.setAttribute('for', 'start');
    startLabel.textContent = 'Start Date';
    const startInput = document.createElement('input');
    startInput.setAttribute('name', 'start');
    startInput.setAttribute('id', 'start');
    startInput.setAttribute('type', 'date');
    startInput.setAttribute('placeholder', 'Type Start Date')
    startInput.setAttribute('required', 'true');
    startInput.setAttribute('min', new Date().toISOString().split('T')[0]);
    startInput.setAttribute('value', new Date().toISOString().split('T')[0]);
    
    startInput.addEventListener('keydown', function(e) {
        e.preventDefault();
    });

    const endLabel = document.createElement('label');
    endLabel.setAttribute('for', 'end');
    endLabel.textContent = 'End Date';
    const endInput = document.createElement('input');
    endInput.setAttribute('name', 'end');
    endInput.setAttribute('id', 'end');
    endInput.setAttribute('type', 'date');
    endInput.setAttribute('placeholder', 'Type End Date')
    endInput.setAttribute('required', 'true');
    endInput.setAttribute('min', new Date().toISOString().split('T')[0]);
    endInput.setAttribute('value', new Date().toISOString().split('T')[0]);

    endInput.addEventListener('keydown', function(e) {
        e.preventDefault();
    });

    startInput.addEventListener('change', function() {
        const startDate = new Date(this.value);
        const endDate = new Date(endInput.value);
        if (endDate < startDate) {
            endInput.value = startDate.toISOString().split('T')[0];
        }
    });
    
    endInput.addEventListener('change', function() {
        const startDate = new Date(startInput.value);
        const endDate = new Date(this.value);
        if (endDate < startDate) {
            alert('Error! End date must be greater than Start date.');
            this.value = startDate.toISOString().split('T')[0];
        }
    });

    const commLabel = document.createElement('label');
    commLabel.setAttribute('for', 'comment');
    commLabel.textContent = 'Comment';
    const commInput = document.createElement('input');
    commInput.setAttribute('name', 'comment');
    commInput.setAttribute('id', 'comment');
    commInput.setAttribute('type', 'text');
    commInput.setAttribute('placeholder', 'Type Sprint Comment');

    const filesLabel = document.createElement('label');
    filesLabel.setAttribute('for', 'files');
    filesLabel.textContent = 'Files';
    const filesInput = document.createElement('input');
    filesInput.setAttribute('name', 'files');
    filesInput.setAttribute('id', 'files');
    filesInput.setAttribute('type', 'file');
    filesInput.setAttribute('placeholder', 'Attach your files');

    const usersLabel = document.createElement('label');
    usersLabel.setAttribute('for', 'users');
    usersLabel.textContent = 'Users';
    const users = document.createElement('select');
    users.setAttribute('name', 'users');
    users.setAttribute('id', 'users');
    users.setAttribute('multiple', 'multiple');


    $.ajax({
        type: 'GET',
        url: `/api/Users`,
        success: function (response) {
            response.forEach(data => {
                const userOption = document.createElement('option');
                userOption.value = data.id;
                userOption.textContent = data.login;
                users.appendChild(userOption);
            });
            $(users).select2({
                placeholder: 'Select options',
                closeOnSelect: false,
                allowClear: true,
                dropdownParent: $('.popup')
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
    addButton.textContent = 'Add Sprint';
    addButton.addEventListener('click', addSprint);

    form.appendChild(heading);
    form.appendChild(nameLabel);
    form.appendChild(nameInput);
    form.appendChild(document.createElement('br'));
    form.appendChild(document.createElement('br'));
    form.appendChild(descLabel);
    form.appendChild(descInput);
    form.appendChild(document.createElement('br'));
    form.appendChild(document.createElement('br'));
    form.appendChild(startLabel);
    form.appendChild(startInput);
    form.appendChild(document.createElement('br'));
    form.appendChild(document.createElement('br'));
    form.appendChild(endLabel);
    form.appendChild(endInput);
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