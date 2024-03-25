document.addEventListener('DOMContentLoaded', async function () {
    const selectDeleteType = document.getElementById('optionDeleteUser');
    const selectUser = document.getElementById('optionUser');
    const userRoles = await getUserRoles();

    if (userRoles.includes('Admin')) { 
        selectDeleteType.addEventListener('change', function () {
            const endpoint = this.value === 'hard' ? 'view-all-users' : 'view-all-users-active';
            loadUsers(endpoint);
        });

        loadUsers('view-all-users');
    } else {
        console.log('Acesso negado. Apenas administradores podem gerenciar usuários.');
    }

    async function loadUsers(endpoint) {
        const response = await fetch(`https://localhost:7016/api/usermanagement/${endpoint}`);
        if (response.ok) {
            const users = await response.json();
            selectUser.innerHTML = ''; 
            users.forEach(user => {
                const option = document.createElement('option');
                option.value = user.id; 
                option.textContent = `${user.name} ${user.lastName} - ${user.cpf}`; 
                selectUser.appendChild(option);
            });
        } else {
            console.error('Failed to load users:', response.status);
        }
    }
});

async function getUserRoles() {
    try {
        const response = await fetch('https://localhost:7016/api/UserManagement/view-role-user-logged');
        if (response.ok) {
            const data = await response.json();
            return data.values;
        } else {
            console.error('Erro ao carregar roles:', response.statusText);
        }
    } catch (error) {
        console.error('Erro ao fazer a requisição para obter roles:', error);
    }
    return [];
}

document.getElementById('modalDeleteUser').querySelector('.close').onclick = function () {
    document.getElementById('modalDeleteUser').style.display = 'none';
};
