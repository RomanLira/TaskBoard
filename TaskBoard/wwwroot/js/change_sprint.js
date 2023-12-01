//скрипт изменения спринта

//вызывает метод изменения спринта
function changeSprint() {
    const sprint = {
        'id': document.getElementById('sprint_header').value,
        'name': document.getElementById('name').value,
        'description': document.getElementById('description').value,
        'startDate': document.getElementById('start').value,
        'endDate': document.getElementById('end').value,
        'comment': document.getElementById('comment').value
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
                type: 'PUT',
                url: '/api/Sprints',
                contentType: 'application/json',
                data: JSON.stringify(sprintModel),

                success: function (response) {
                    getSprints();
                    getSprintData(sprint.id);
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

//открывает форму для изменения спринта
function openFormForChangeSprint() {
    const popup = document.createElement('div');
    popup.className = 'popup';
    const formContainer = createFormForChangeSprint();
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

//создаёт форму для изменения спринта
function createFormForChangeSprint() {
    const formContainer = document.createElement('div');
    formContainer.classList.add('form-container');
    var id = document.getElementById('sprint_header').value;
    $.ajax({
        type: 'GET',
        url: `/api/Sprints/Sprint/${id}`,
        success: function (response) {
            const form = document.createElement('form');
            form.id = 'form';

            const heading = document.createElement('h2');
            heading.textContent = 'Change Sprint';

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
            
            const startDate = new Date(response.startDate);
            const endDate = new Date(response.endDate);
            
            const startLabel = document.createElement('label');
            startLabel.setAttribute('for', 'start');
            startLabel.textContent = 'Start Date';
            const startInput = document.createElement('input');
            startInput.setAttribute('name', 'start');
            startInput.setAttribute('id', 'start');
            startInput.setAttribute('type', 'date');
            startInput.setAttribute('required', 'true');
            startInput.setAttribute('min', new Date().toISOString().split('T')[0]);
            startDate.setDate(startDate.getDate() + 1);
            startInput.value = startDate.toISOString().split('T')[0];
            
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
            endInput.setAttribute('required', 'true');
            endInput.setAttribute('min', new Date().toISOString().split('T')[0]);
            endDate.setDate(endDate.getDate() + 1);
            endInput.value = endDate.toISOString().split('T')[0];

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
            commInput.setAttribute('required', 'true');
            commInput.value =  response.comment;

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
                success: function (usersResponse) {
                    $.ajax({
                        type: 'GET',
                        url: `/api/Sprints/Sprint/${id}/Users`,
                        success: function (response) {
                            usersResponse.forEach(data => {
                                const userOption = document.createElement('option');
                                userOption.value = data.id;
                                userOption.textContent = data.login;
                                if(response.find(item => item.id === data.id)) {
                                    userOption.setAttribute('selected', 'selected');
                                }
                                users.appendChild(userOption);
                            })
                            $(users).select2({
                                placeholder: 'Select options',
                                closeOnSelect: false,
                                allowClear: true,
                                dropdownParent: $('.popup')
                            });
                        }
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
            saveButton.addEventListener('click', changeSprint);

            const deleteButton = document.createElement('button');
            deleteButton.setAttribute('type', 'button');
            deleteButton.setAttribute('name', 'deleteButton');
            deleteButton.setAttribute('id', 'deleteButton');
            deleteButton.setAttribute('value', 'delete');
            deleteButton.style.width = '300px';
            deleteButton.textContent = 'Delete Sprint';
            deleteButton.addEventListener('click', deleteSprint);

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
            form.appendChild(usersLabel);
            form.appendChild(users);
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