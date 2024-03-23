async function login(event) {
    event.preventDefault();
    const formData = new FormData(event.target);
    const data = {
        Email: formData.get('email'),
        Senha: formData.get('password')
    };
    try {
        const response = await fetch('https://localhost:7016/api/userauth/login', {
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
        window.location.href = 'https://localhost:7016/templates/home/homepage.html';
    } catch (error) {
        alert('Login error: ' + error.message);
    }
}