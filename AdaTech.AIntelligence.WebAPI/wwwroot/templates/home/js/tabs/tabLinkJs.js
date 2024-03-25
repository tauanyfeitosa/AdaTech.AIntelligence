let globalRoles = [];

async function getUserRolesTab() {
    const response = await fetch('https://localhost:7016/api/UserManagement/view-role-user-logged');
    if (response.ok) {
        const roles = await response.json();
        globalRoles = roles.values;
    }
}

function openTab(evt, tabName) {
    var i, tabcontent, tablinks;

    console.log('Roles in tab:', globalRoles);

    console.log('Tab name:', tabName);

    if (tabName === 'Admin' && !globalRoles.includes('Admin')) {
        alert('Você não tem permissão para acessar essa aba.');
        return;
    }

    if (tabName === 'Finance' && !globalRoles.includes('Finance')) {
        alert('Você não tem permissão para acessar essa aba.');
        return;
    }

    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(tabName).style.display = "block";
    evt.currentTarget.className += " active";
}

async function initTabs() {
    try {
        await getUserRolesTab();
        console.log('Função getUserRolesTab foi chamada com sucesso.', globalRoles);

        const fakeEvent = { currentTarget: document.getElementById("My_expense") };
        openTab(fakeEvent, 'My_expense');
    } catch (error) {
        console.error('Ocorreu um erro ao chamar getUserRolesTab:', error);
    }
}

initTabs();