//скрипт изменения проекта

//вызывает метод изменения проекта
function changeProject() {
    const project = {
        'id': document.getElementById('project_header').value,
        'name': document.getElementById('name').value,
        'description': document.getElementById('description').value
    };
    $.ajax({
        type: 'PUT',
        url: '/api/Projects',
        data: project,
        success: function (response) {
            getProjects();
            getProjects();
            getProjectData(project.id);
            closeForm();
        },
        error: function (xhr, status, error) {
            var errorMessage = xhr.responseText || xhr.statusText;
            console.log('Error! ' + errorMessage);
        }
    });
}

//открывает форму для изменения проекта
function openFormForChangeProject() {
    const popup = document.createElement('div');
    popup.className = 'popup';
    const formContainer = createFormForChangeProject();
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

//создаёт форму для изменения проекта
function createFormForChangeProject() {
    const formContainer = document.createElement('div');
    formContainer.classList.add('form-container');
    var id = document.getElementById('project_header').value;
    $.ajax({
        type: 'GET',
        url: `/api/Projects/Project/${id}`,
        success: function (response) {
            const form = document.createElement('form');
            form.id = 'form';

            const heading = document.createElement('h2');
            heading.textContent = 'Change Project';

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

            const saveButton = document.createElement('button');
            saveButton.setAttribute('type', 'button');
            saveButton.setAttribute('name', 'saveButton');
            saveButton.setAttribute('id', 'saveButton');
            saveButton.setAttribute('value', 'save');
            saveButton.style.width = '300px';
            saveButton.textContent = 'Save changes';
            saveButton.addEventListener('click', changeProject);

            const deleteButton = document.createElement('button');
            deleteButton.setAttribute('type', 'button');
            deleteButton.setAttribute('name', 'deleteButton');
            deleteButton.setAttribute('id', 'deleteButton');
            deleteButton.setAttribute('value', 'delete');
            deleteButton.style.width = '300px';
            deleteButton.textContent = 'Delete Project';
            deleteButton.addEventListener('click', deleteProject);

            form.appendChild(heading);
            form.appendChild(nameLabel);
            form.appendChild(nameInput);
            form.appendChild(document.createElement('br'));
            form.appendChild(document.createElement('br'));
            form.appendChild(descLabel);
            form.appendChild(descInput);
            form.appendChild(document.createElement('br'));
            form.appendChild(document.createElement('br'));
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