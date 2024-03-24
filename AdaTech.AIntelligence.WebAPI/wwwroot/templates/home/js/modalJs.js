var modal = document.getElementById("myModal");
var btn = document.getElementById("requestButton");
var span = document.getElementsByClassName("close")[0];

btn.onclick = function () {
    modal.style.display = "block";
}

span.onclick = function () {
    modal.style.display = "none";
}


document.getElementById('register-expense').onclick = function () {
    document.getElementById('modalExpense').style.display = 'block';
    updateForm(document.getElementById('fileOrUrl').value);
};

document.getElementById('delete-expense').onclick = function () {
    document.getElementById('modalDeleteExpense').style.display = 'block';
};

document.getElementById('delete-user').onclick = function () {
    document.getElementById('modalDeleteUser').style.display = 'block';
};

document.getElementsByClassName('close')[0].onclick = function () {
    document.getElementById('modalExpense').style.display = 'none';
};


window.onclick = function (event) {
    if (event.target == document.getElementById('myModal')) {
        document.getElementById('myModal').style.display = 'none';
    } else if (event.target == document.getElementById('modalExpense')) {
        document.getElementById('modalExpense').style.display = 'none';
    } else if (event.target == document.getElementById('modalDeleteExpense')) {
        document.getElementById('modalDeleteExpense').style.display = 'none';
    } else if (event.target == document.getElementById('modalDeleteUser')) {
        document.getElementById('modalDeleteUser').style.display = 'none';
    }
};


