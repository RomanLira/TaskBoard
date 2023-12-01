//скрипт создания пользователя другим пользователем (админом)

//вызывает метод создания и добавления в базу пользователя
function addUser() {
    const user = {
        'login': document.getElementById('login').value,
        'password': document.getElementById('password').value
    };
    $.ajax({
        type: 'POST',
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

//открывает форму для создания пользователя
function openFormForAddUser() {
    const popup = document.createElement('div');
    popup.className = 'popup';
    const formContainer = createFormForAddUser();
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

//создаёт форму для создания пользователя
function createFormForAddUser() {
    const formContainer = document.createElement('div');
    formContainer.classList.add('form-container');

    const form = document.createElement('form');
    form.id = 'form';

    const heading = document.createElement('h2');
    heading.textContent = 'Add New User';

    const nameLabel = document.createElement('label');
    nameLabel.setAttribute('for', 'login');
    nameLabel.textContent = 'Login';
    const nameInput = document.createElement('input');
    nameInput.setAttribute('name', 'login');
    nameInput.setAttribute('id', 'login');
    nameInput.setAttribute('type', 'text');
    nameInput.setAttribute('placeholder', 'Type User Login')
    nameInput.setAttribute('autofocus', 'true');
    nameInput.setAttribute('required', 'true');

    const passLabel = document.createElement('label');
    passLabel.setAttribute('for', 'password');
    passLabel.textContent = 'Password';
    const passInput = document.createElement('input');
    passInput.setAttribute('name', 'password');
    passInput.setAttribute('id', 'password');
    passInput.setAttribute('type', 'password');
    passInput.setAttribute('placeholder', 'Type User Password')
    passInput.setAttribute('required', 'true');

    const addButton = document.createElement('button');
    addButton.setAttribute('type', 'button');
    addButton.setAttribute('name', 'addButton');
    addButton.setAttribute('id', 'addButton');
    addButton.setAttribute('value', 'add');
    addButton.style.width = '300px';
    addButton.textContent = 'Add User';
    addButton.addEventListener('click', addUser);

    form.appendChild(heading);
    form.appendChild(nameLabel);
    form.appendChild(nameInput);
    form.appendChild(document.createElement('br'));
    form.appendChild(document.createElement('br'));
    form.appendChild(passLabel);
    form.appendChild(passInput);
    form.appendChild(document.createElement('br'));
    form.appendChild(document.createElement('br'));
    form.appendChild(addButton);

    formContainer.appendChild(form);

    return formContainer;
}