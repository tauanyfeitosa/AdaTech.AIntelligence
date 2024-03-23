async function loadUsers() {
    const response = await fetch('https://localhost:7016/api/usermanagement/view-all-users');
    if (response.ok) {
        const users = await response.json();
        const tableBody = document.getElementById('usersTable').querySelector('tbody');
        tableBody.innerHTML = '';

        for (const user of users) {
            // Get roles for each user
            const rolesResponse = await fetch(`https://localhost:7016/api/usermanagement/view-role-user?id=${user.id}`);
            const rolesData = await rolesResponse.json();
            console.log(rolesData);

            // Assume rolesData is an array and map to a string
            const roles = rolesData.values.join(', ');

            const row = tableBody.insertRow();
            const fullNameCell = row.insertCell();
            fullNameCell.textContent = `${user.name} ${user.lastName}`;

            const roleCell = row.insertCell();
            roleCell.textContent = roles; // Now we have the roles from the endpoint

            const cpfCell = row.insertCell();
            cpfCell.textContent = user.cpf;

            const isActiveCell = row.insertCell();
            isActiveCell.textContent = user.isActive ? 'Sim' : 'Não';
        }
    } else {
        console.error('Failed to load users:', response.status);
    }
}

loadUsers();
