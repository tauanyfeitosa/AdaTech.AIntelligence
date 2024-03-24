document.addEventListener('DOMContentLoaded', () => {
    const logoutLink = document.getElementById('logoutLink');

    logoutLink.addEventListener('click', async (e) => {
        e.preventDefault(); 
        try {
            const response = await fetch('https://localhost:7016/api/userauth/logout', { 
                method: 'POST'
            });
            alert("Você será redirecionado para a página de login!"); 
            window.location.href = '/templates/authentication/login.html'; 
        } catch (error) {
            console.error('Logout failed', error);
            alert('Falha ao deslogar.');
        }
    });
});
