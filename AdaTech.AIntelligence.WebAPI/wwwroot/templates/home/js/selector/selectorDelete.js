async function getUserRoles() {
    try {
        const response = await fetch('https://localhost:7016/api/UserManagement/view-role-user-logged');
        if (response.ok) {
            const roles = await response.json();
            console.log('Roles:', roles.values);
            return roles.values;
        } else {
            console.error('Erro ao carregar roles:', response.statusText);
        }
    } catch (error) {
        console.error('Erro ao fazer a requisição para obter roles:', error);
    }
    return [];
}

async function setupDeleteOptions() {
    const userRoles = await getUserRoles();
    const optionDeleteSelect = document.getElementById('optionDelete');

    if (userRoles.includes('Admin') && userRoles.includes('Finance')) {
        optionDeleteSelect.innerHTML = `
      <option value="hard">HardDelete</option>
      <option value="soft">SoftDelete</option>
    `;
    } else if (userRoles.includes('Finance')) {
        optionDeleteSelect.innerHTML = `
      <option value="soft">SoftDelete</option>
    `;
    } else {
        optionDeleteSelect.innerHTML = '<option value="">Não autorizado</option>';
    }
}

setupDeleteOptions();
