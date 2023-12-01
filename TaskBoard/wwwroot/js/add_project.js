//скрипт создания проекта

//вызывает метод создания и добавления в базу проекта
function addProject() {
    const project = {
        'name': document.getElementById('name').value,
        'description': document.getElementById('description').value
    };
    $.ajax({
        type: 'POST',
        url: '/api/Projects',
        data: project,
        success: function (response) {
            getProjects();
            getProjects();
            closeForm();
        },
        error: function (xhr, status, error) {
            var errorMessage = xhr.responseText || xhr.statusText;
            console.log('Error! ' + errorMessage);
        }
    });
}

//открывает специальную форму для создания проекта
function openFormForAddProject() {
    const popup = document.createElement('div');
    popup.className = 'popup';
    const formContainer = createFormForAddProject();
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

//создаёт форму для создания проекта
function createFormForAddProject() {
    const formContainer = document.createElement('div');
    formContainer.classList.add('form-container');

    const form = document.createElement('form');
    form.id = 'form';

    const heading = document.createElement('h2');
    heading.textContent = 'Add New Project';

    const nameLabel = document.createElement('label');
    nameLabel.setAttribute('for', 'name');
    nameLabel.textContent = 'Name';
    const nameInput = document.createElement('input');
    nameInput.setAttribute('name', 'name');
    nameInput.setAttribute('id', 'name');
    nameInput.setAttribute('type', 'text');
    nameInput.setAttribute('placeholder', 'Type Project Name')
    nameInput.setAttribute('autofocus', 'true');
    nameInput.setAttribute('required', 'true');

    const descLabel = document.createElement('label');
    descLabel.setAttribute('for', 'description');
    descLabel.textContent = 'Description';
    const descInput = document.createElement('input');
    descInput.setAttribute('name', 'description');
    descInput.setAttribute('id', 'description');
    descInput.setAttribute('type', 'text');
    descInput.setAttribute('placeholder', 'Type Project Description')
    descInput.setAttribute('required', 'true');

    const addButton = document.createElement('button');
    addButton.setAttribute('type', 'button');
    addButton.setAttribute('name', 'addButton');
    addButton.setAttribute('id', 'addButton');
    addButton.setAttribute('value', 'add');
    addButton.style.width = '300px';
    addButton.textContent = 'Add Project';
    addButton.addEventListener('click', addProject);

    form.appendChild(heading);
    form.appendChild(nameLabel);
    form.appendChild(nameInput);
    form.appendChild(document.createElement('br'));
    form.appendChild(document.createElement('br'));
    form.appendChild(descLabel);
    form.appendChild(descInput);
    form.appendChild(document.createElement('br'));
    form.appendChild(document.createElement('br'));
    form.appendChild(addButton);

    formContainer.appendChild(form);

    return formContainer;
}

//закрывает форму
function closeForm() {
    const popup = document.querySelector('.popup');
    const overlay = document.querySelector('.overlay');
    popup.remove();
    overlay.classList.remove('active');
}