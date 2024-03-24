document.addEventListener('DOMContentLoaded', () => {
    const logoutLink = document.getElementById('connection-gpt');

    logoutLink.addEventListener('click', async (e) => {
        e.preventDefault();
        try {
            const response = await fetch('https://localhost:7016/api/ChatGPT/check', {
                method: 'GET'
            });
            if (response.ok) {
                const data = await response.text();
                console.log(data);
                if (data == "Conexão com o chat GPT bem-sucedida!") {
                    alert('Conexão com o chat GPT bem-sucedida!')
                } else {
                    alert(data);
                }
            }
            else {
                console.error('Erro ao carregar dados do usuário:', response);
                alert(data);
            }
        } catch (error) {
            console.error('Sem resposta do servidor', error);
            alert('Impossível estabelecer conexão com o servidor.');
        }
    });
});