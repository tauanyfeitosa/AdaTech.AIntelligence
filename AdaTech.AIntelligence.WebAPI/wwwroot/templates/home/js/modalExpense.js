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
    fileOption.textContent = 'File';
    optionsSelect.appendChild(fileOption);

    const urlOption = document.createElement('option');
    urlOption.value = 'url';
    urlOption.textContent = 'URL';
    optionsSelect.appendChild(urlOption);

    optionsSelect.addEventListener('change', function () {
        toggleInputFields(this.value);
    });

    toggleInputFields(optionsSelect.value);
}

function toggleInputFields(selectedOption) {
    const fileInput = document.getElementById('file-input');
    const urlInput = document.getElementById('url-input');
    const fileLabel = document.getElementById('file-label');
    const urlLabel = document.getElementById('url-label');

    fileInput.style.display = selectedOption === 'file' ? 'block' : 'none';
    fileLabel.style.display = selectedOption === 'file' ? 'block' : 'none';
    urlInput.style.display = selectedOption === 'url' ? 'block' : 'none';
    urlLabel.style.display = selectedOption === 'url' ? 'block' : 'none';
}

