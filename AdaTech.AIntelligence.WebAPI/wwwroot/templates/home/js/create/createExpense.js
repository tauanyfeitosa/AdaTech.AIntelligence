document.addEventListener('DOMContentLoaded', () => {
    const createExpenseButton = document.getElementById('botao-criar-despesa');
    const form = document.getElementById('expenseForm');
    const urlInput = document.getElementById('url-input');
    const fileInput = document.getElementById('file-input');

    createExpenseButton.addEventListener('click', async (event) => {
        event.preventDefault();

        if (urlInput.value) {
            await createExpenseFromUrl(urlInput.value);
        } else if (fileInput.files.length > 0) {
            await createExpenseFromFile(fileInput.files[0]);
        } else {
            alert('Por favor, selecione uma URL ou um arquivo de imagem.');
        }
    });

    async function createExpenseFromUrl(url) {
        try {
            const response = await fetch(`https://localhost:7016/api/expense/create-expense-image-url?url=${encodeURIComponent(url)}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            if (response.ok) {
                const responseData = await response.text();
                console.log('Resposta do servidor:', responseData);

                alert('Despesa criada com sucesso!');
                form.reset();
            } else {
                const errorText = await response.text();
                console.error('Erro ao criar despesa:', response.statusText);
                alert('Erro ao criar despesa. Mensagem do servidor:', errorText);
            }
        } catch (error) {
            console.error('Erro ao fazer a requisição:', error);
            alert('Erro ao criar despesa. Por favor, tente novamente mais tarde.');
        }
    }


    async function createExpenseFromFile(file) {
        try {
            const formData = new FormData();
            formData.append('image', file);

            const response = await fetch('https://localhost:7016/api/expense/create-expense-image-file', {
                method: 'POST',
                body: formData,
            });

            if (response.ok) {
                const responseData = await response.text();
                console.log('Resposta do servidor:', responseData);

                alert('Despesa criada com sucesso!');
                form.reset();
            } else {
                const errorText = await response.text();
                console.error('Erro ao criar despesa:', response.statusText);
                alert('Erro ao criar despesa. Mensagem do servidor:', errorText);
            }
        } catch (error) {
            console.error('Erro ao fazer a requisição:', error);
            alert('Erro ao criar despesa. Por favor, tente novamente mais tarde.');
        }
    }
});
