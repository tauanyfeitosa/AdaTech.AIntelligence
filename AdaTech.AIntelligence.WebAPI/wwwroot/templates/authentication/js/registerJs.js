document.getElementById('registrationForm').addEventListener('submit', async function (event) {
    event.preventDefault();

    const formData = new FormData(event.target);
    const data = {
        Email: formData.get('email'),
        Nome: formData.get('nome'),
        Sobrenome: formData.get('sobrenome'),
        Data_Nascimento: formData.get('dataNascimento'),
        Senha: formData.get('senha'),
        CPF: formData.get('cpf')
    };

    try {
        const response = await fetch('https://localhost:7016/api/userauth/create-user', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
        });

        if (!response.ok) {
            const errorBody = await response.text();
            throw new Error(errorBody || response.statusText);
        }

        const result = await response.json();
        alert('Usuário criado com sucesso!');
        window.location.href = 'https://localhost:7016/templates/authentication/login.html';
    } catch (error) {
        alert('Erro no registro: ' + error.message);
    }
});