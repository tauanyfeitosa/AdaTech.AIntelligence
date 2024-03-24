document.addEventListener('DOMContentLoaded', async function () {
    try {
        const userResponse = await fetch('https://localhost:7016/api/UserManagement/view-user-logged');
        const userData = await userResponse.json();
        const user = userData.values;
        const name = `${user.name} ${user.lastName}`;
        const cpf = formatarCPF(user.cpf);

        const userDetailsElement = document.querySelector('.user-details');

        const userNameElement = document.createElement('p');
        userNameElement.innerHTML = `<span class="label">Nome:</span> ${name}`;
        userDetailsElement.appendChild(userNameElement);

        const userCpfElement = document.createElement('p');
        userCpfElement.innerHTML = `<span class="label">CPF:</span> ${cpf}`;
        userDetailsElement.appendChild(userCpfElement);

        const rolesResponse = await fetch('https://localhost:7016/api/UserManagement/view-role-user-logged');
        const rolesData = await rolesResponse.json();

        if (Array.isArray(rolesData.values)) {
            const rolesList = rolesData.values.join(', ');
            const userRolesElement = document.createElement('p');
            userRolesElement.innerHTML = `<span class="label">Cargo(s):</span> ${rolesList}`;
            userDetailsElement.appendChild(userRolesElement);
            userDetailsElement.appendChild(userRolesElement);
        } else {
            console.error('rolesData não é uma array:', rolesData);
        }
    } catch (error) {
        console.error('Erro ao carregar dados do usuário:', error);
    }
});

function formatarCPF(cpf) {
    cpf = cpf.replace(/\D/g, '');
    cpf = cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
    return cpf;
}
