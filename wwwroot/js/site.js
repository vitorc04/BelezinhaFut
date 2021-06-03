const url = 'https://localhost:5001/api/Times';
let times = [];


function getTimes(){
    fetch(url)
        .then((response) => response.json())
        .then(data => _displayTimes(data))
        .catch(error => console.error());

}

function addTime(){
    const addNameTextbox = document.getElementById('add-name');
    const addAbrevTextbox = document.getElementById('add-abrev');
    const addCidadeTextbox = document.getElementById('add-cidade');

    const time = {
        nome: addNameTextbox.value.trim(),
        abrev: addAbrevTextbox.value.trim(),
        cidade: addCidadeTextbox.value.trim()
    }
    fetch(url, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(time)
    })
    .then(response => response.json())
    .then(() => {
        getTimes();
        addNameTextbox.value = '';
        addAbrevTextbox.value = '';
        addCidadeTextbox.value = '';
    })
    .catch(error => console.error('Unable to add item.', error))
 }

function deleteTime(id){
    fetch(`${url}/${id}`, {
        method: 'DELETE'
    })
    .then(() => getTimes())
    .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id){
    const time = times.find(time => time.id === id);
    document.getElementById('edit-name').value = time.nome;
    document.getElementById('edit-id').value = time.id;
    document.getElementById('edit-abrev').value = time.abrev;
    document.getElementById('edit-cidade').value = time.cidade;
    document.getElementById('editForm').style.display = 'block';
}

function updateTime() {
    const timeId = document.getElementById('edit-id').value;

    const time = {
        id: parseInt(timeId, 10),
        nome: document.getElementById('edit-name').value.trim(),
        abrev : document.getElementById('edit-abrev').value.trim(),
        cidade : document.getElementById('edit-cidade').value.trim()
    };

    fetch(`${url}/${timeId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(time)
    })
    .then(() => getTimes())
    .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayTimes(data){
    const tBody = document.getElementById('times');
    let teams = data;
    tBody.innerHTML='';
    const button = document.createElement('button');

    teams.forEach(time => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${time.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute("onclick", `deleteTime(${time.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNome = document.createTextNode(time.nome);
        td1.appendChild(textNome);

        let td2 = tr.insertCell(1);
        let textAbrev = document.createTextNode(time.abrev);
        td2.appendChild(textAbrev);

        let td3 = tr.insertCell(2);
        let textCidade = document.createTextNode(time.cidade);
        td3.appendChild(textCidade);

        let td4 = tr.insertCell(3);
        td4.appendChild(editButton);

        let td5 = tr.insertCell(4);
        td5.appendChild(deleteButton);
    });

    times = data;

   

}

