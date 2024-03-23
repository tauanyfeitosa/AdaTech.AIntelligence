document.addEventListener("DOMContentLoaded", function () {
    fetch('https://localhost:7016/api/userauth/check-authentication')
        .then(response => {
            if (response.status === 200) {
                // Se a resposta for bem-sucedida (status 200), você pode continuar com o fluxo normal da página
                console.log("Usuário autenticado.");
            } else if (response.status === 401) {
                // Se o status for 401 (Não autorizado), redirecione para a página de login
                window.location.href = '/templates/authentication/login.html'; // Redireciona para a página de login
            } else {
                // Se o status for diferente de 200 e 401, algo inesperado aconteceu
                console.error('Erro ao verificar autenticação:', response.statusText);
            }
        })
        .catch(error => {
            console.error('Erro ao verificar autenticação:', error);
        });
});