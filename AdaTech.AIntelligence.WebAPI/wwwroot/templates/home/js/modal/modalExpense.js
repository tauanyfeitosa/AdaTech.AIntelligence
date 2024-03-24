document.addEventListener('DOMContentLoaded', function () {

    setupOptions();
    setupModal('all-expense', 'modalExpenseAll');
    setupModal('active-expense', 'modalExpenseActive');

    function setupModal(buttonId, modalId) {
        const btn = document.getElementById(buttonId);
        const modal = document.getElementById(modalId);
        const closeSpan = modal.querySelector('.close');

        btn.onclick = function () {
            modal.style.display = 'block';
        };

        closeSpan.onclick = function () {
            modal.style.display = 'none';
        };
    }

    window.onclick = function (event) {
        if (event.target.classList.contains('modal')) {
            event.target.style.display = 'none';
        }
    };
});

function setupOptions() {
    const optionsSelect = document.getElementById('options');
    optionsSelect.innerHTML = '';

    const fileOption = document.createElement('option');
    fileOption.value = 'file';
    fileOption.textContent = 'Enviar uma imagem';
    optionsSelect.appendChild(fileOption);

    const urlOption = document.createElement('option');
    urlOption.value = 'url';
    urlOption.textContent = 'Inserir uma URL';
    optionsSelect.appendChild(urlOption);

    optionsSelect.addEventListener('change', function () {
        toggleInputFields(this.value);
        if (this.value === 'url') {
            document.getElementById('file-input').value = '';
            document.getElementById('file-label-2').textContent = 'Nenhum arquivo selecionado';
            document.getElementById('botao-criar-despesa').disabled = true;
            document.getElementById('url-input').focus();
        }
        if (this.value === 'file') {
            document.getElementById('url-input').value = '';
            document.getElementById('botao-criar-despesa').disabled = true;
        }
    });


    toggleInputFields(optionsSelect.value);
}

function toggleInputFields(selectedOption) {
    const fileInput = document.getElementById('file-input');
    const urlInput = document.getElementById('url-input');
    const fileLabel = document.getElementById('file-label');
    const fileLabel2 = document.getElementById('file-label-2');
    const urlLabel = document.getElementById('url-label');

    if (selectedOption === 'url') {
        fileLabel.style.display = 'none';
        fileLabel2.style.display = 'none';
        urlInput.style.display = 'block';
        urlLabel.style.display = 'block';
    }
    else {
        fileLabel.style.display = 'block';
        fileLabel2.style.display = 'block';
        urlInput.style.display = 'none';
        urlLabel.style.display = 'none';
    }
}


const btnCriarDespesa = document.getElementById('botao-criar-despesa');

document.getElementById('file-input').addEventListener('change', function () {
    const fileLabel2 = document.querySelector('#file-label-2');
    if (this.value) {
        fileLabel2.textContent = this.files[0].name;
    } else {
        fileLabel2.textContent = 'Nenhum arquivo selecionado';
    }
});

document.getElementById('file-input').addEventListener('change', function () {
    if (this.files.length > 0 || this.value) {
        btnCriarDespesa.disabled = false;
    } else {
        btnCriarDespesa.disabled = true;
    }
});

document.getElementById('url-input').addEventListener('input', function () {
    if (this.value.trim() !== '') {
        btnCriarDespesa.disabled = false;
    } else {
        btnCriarDespesa.disabled = true;
    }
});