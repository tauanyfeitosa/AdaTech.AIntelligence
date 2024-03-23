// Quando o usuário clicar no botão, abrir o modal
var modal = document.getElementById("myModal");
var btn = document.getElementById("requestButton");
var span = document.getElementsByClassName("close")[0];

btn.onclick = function () {
    modal.style.display = "block";
}

// Quando o usuário clicar em (x), fechar o modal
span.onclick = function () {
    modal.style.display = "none";
}

// Quando o usuário clicar fora do modal, fechar
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

// Manipular o envio do formulário
document.getElementById("promotionForm").onsubmit = function (event) {
    event.preventDefault();

    var select = document.getElementById("roles");
    var selectedOptions = Array.from(select.selectedOptions).map(option => option.value);
    var requestData = {
        roles: selectedOptions
    };

    // Substitua 'endpointUrl' pela URL do seu endpoint
    fetch('endpointUrl', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestData)
    })
        .then(response => response.json())
        .then(data => {
            console.log(data);
            modal.style.display = "none";
        })
        .catch((error) => {
            console.error('Error:', error);
        });
};
