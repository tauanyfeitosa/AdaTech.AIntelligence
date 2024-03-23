document.addEventListener('DOMContentLoaded', function () {
    fetch('https://localhost:7016/api/UserManagement/view-user-logged')
        .then(response => response.json())
        .then(userData => {
            const user = userData.values;
            const name = `${user.name} ${user.lastName}`;
            const cpf = formatarCPF(user.cpf);
            const userNameElement = document.createElement('p');
            const userCpfElement = document.createElement('p');
            userNameElement.textContent = `Nome: ${name}`;
            userCpfElement.textContent = `CPF: ${cpf}`;
            const userDetailsElement = document.querySelector('.user-details');
            userDetailsElement.appendChild(userNameElement);
            userDetailsElement.appendChild(userCpfElement);
        })
        .catch(error => console.error('Erro ao carregar dados do usuário:', error));

    fetch('https://localhost:7016/api/UserManagement/view-role-user-logged')
        .then(response => response.json())
        .then(rolesData => {
            if (Array.isArray(rolesData.values)) {
                const rolesList = rolesData.values.join(', ');
                const userRolesElement = document.createElement('p');
                userRolesElement.textContent = `Cargo(s): ${rolesList}`;
                const userDetailsElement = document.querySelector('.user-details');
                if (userDetailsElement) {
                    userDetailsElement.appendChild(userRolesElement);
                }
            } else {
                console.error('rolesData não é uma array:', rolesData);
            }
        })
        .catch(error => console.error('Erro ao carregar cargos do usuário:', error));
});

function formatarCPF(cpf) {
    cpf = cpf.replace(/\D/g, '');
    cpf = cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
    return cpf;
}