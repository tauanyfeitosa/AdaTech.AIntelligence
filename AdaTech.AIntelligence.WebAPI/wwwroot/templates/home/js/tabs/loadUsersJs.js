﻿function formatarCPF(cpf) {
    cpf = cpf.replace(/\D/g, '');
    cpf = cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
    return cpf;
}

async function loadUsers() {
    const response = await fetch('https://localhost:7016/api/usermanagement/view-all-users');
    if (response.ok) {
        const users = await response.json();
        const tableBody = document.getElementById('usersTable').querySelector('tbody');
        tableBody.innerHTML = '';

        for (const user of users) {
            const rolesResponse = await fetch(`https://localhost:7016/api/usermanagement/view-role-user?id=${user.id}`);
            const rolesData = await rolesResponse.json();
            const roles = rolesData.values.join(', ');

            const row = tableBody.insertRow();
            const fullNameCell = row.insertCell();
            fullNameCell.textContent = `${user.name} ${user.lastName}`;

            const roleCell = row.insertCell();
            roleCell.textContent = roles; 

            const cpfCell = row.insertCell();
            cpfCell.textContent = formatarCPF(user.cpf);

            const isActiveCell = row.insertCell();
            isActiveCell.textContent = user.isActive ? 'Sim' : 'Não';
        }
    } else {
        console.error('Failed to load users:', response.status);
    }
}

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

async function init() {
    const roles = await getUserRoles();
    const isAdmin = roles.includes('Admin');

    if (isAdmin) {
        loadUsers();
    }
}

init();

