var modal = document.getElementById("myModal");
var btn = document.getElementById("requestButton");
var span = document.getElementsByClassName("close")[0];

btn.onclick = function () {
    modal.style.display = "block";
}

span.onclick = function () {
    modal.style.display = "none";
}

window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

document.getElementById("promotionForm").onsubmit = function (event) {
    event.preventDefault();

    var select = document.getElementById("roles");
    var selectedOptions = Array.from(select.selectedOptions).map(option => option.value);
    var requestData = {
        roles: selectedOptions
    };

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
