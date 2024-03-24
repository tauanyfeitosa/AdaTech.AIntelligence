document.addEventListener('DOMContentLoaded', function () {
    const selectDeleteType = document.getElementById('optionDeleteUser');
    const selectUser = document.getElementById('optionUser');

    selectDeleteType.addEventListener('change', function () {

        const endpoint = this.value === 'hard' ? 'view-all-users' : 'view-all-users-active';
        loadUsers(endpoint);
    });

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

    loadUsers('view-all-users');
});

document.getElementById('modalDeleteUser').querySelector('.close').onclick = function () {
    document.getElementById('modalDeleteUser').style.display = 'none';
};
