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

        document.getElementById('loadingIndicator').style.display = 'block';
        try {
            const response = await fetch(`https://localhost:7016/api/expense/create-expense-image-url?url=${encodeURIComponent(url)}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            if (response.ok) {
                const responseData = await response.text();
                if (responseData == "Despesa criada com sucesso!") {
                    document.getElementById('modalCreateExpenseSuccess').style.display = 'block';
                    document.getElementById('successMessageTitle').textContent = "Despesa cadastrada";
                    document.getElementById('successMessage').textContent = responseData;
                    form.reset();
                }
                else {
                    document.getElementById('modalCreateExpenseError').style.display = 'block';
                    document.getElementById('errorMessageTitle').textContent = "Erro";
                    document.getElementById('errorMessage').textContent = "Erro ao cadastrar despesa";
                }
            } else {
                const errorText = await response.text();
                console.log("errinho");
                //console.error('Erro ao criar despesa:', response.statusText);
                document.getElementById('modalCreateExpenseError').style.display = 'block';
                document.getElementById('errorMessageTitle').textContent = "Erro";
                document.getElementById('errorMessage').textContent = "Erro ao cadastrar despesa";
            }
        } catch (error) {
            //console.error('Erro ao fazer a requisição:', error);
            console.log("errinho");
            document.getElementById('modalCreateExpenseError').style.display = 'block';
            document.getElementById('errorMessageTitle').textContent = "Erro";
            document.getElementById('errorMessage').textContent = "Erro ao estabelecer conexão com o servidor";
        } finally {
            document.getElementById('loadingIndicator').style.display = 'none';
        }
    }

    async function createExpenseFromFile(file) {
        document.getElementById('loadingIndicator').style.display = 'block';
        try {
            const formData = new FormData();
            formData.append('image', file);

            const response = await fetch('https://localhost:7016/api/expense/create-expense-image-file', {
                method: 'POST',
                body: formData,
            });

            if (response.ok) {
                const responseData = await response.text();
                if (responseData == "Despesa criada com sucesso!") {
                    document.getElementById('modalCreateExpenseSuccess').style.display = 'block';
                    document.getElementById('successMessageTitle').textContent = "Despesa cadastrada";
                    document.getElementById('successMessage').textContent = responseData;
                    form.reset();
                }
                else {
                    document.getElementById('modalCreateExpenseError').style.display = 'block';
                    document.getElementById('errorMessageTitle').textContent = "Erro";
                    document.getElementById('errorMessage').textContent = "Erro ao cadastrar despesa";
                }
            } else {
                const errorText = await response.text();
                console.log("errinho");
                //console.error('Erro ao criar despesa:', response.statusText);
                document.getElementById('modalCreateExpenseError').style.display = 'block';
                document.getElementById('errorMessageTitle').textContent = "Erro";
                document.getElementById('errorMessage').textContent = "Erro ao cadastrar despesa";
            }
        } catch (error) {
            //console.error('Erro ao fazer a requisição:', error);
            console.log("errinho");
            document.getElementById('modalCreateExpenseError').style.display = 'block';
            document.getElementById('errorMessageTitle').textContent = "Erro";
            document.getElementById('errorMessage').textContent = "Erro ao estabelecer conexão com o servidor";
        } finally {
            document.getElementById('loadingIndicator').style.display = 'none';
        }
    }


    // Adiciona o evento de clique no botão "OK"
    document.getElementById('okButton').addEventListener('click', () => {
        location.reload(); // Recarrega a página
    });
});
