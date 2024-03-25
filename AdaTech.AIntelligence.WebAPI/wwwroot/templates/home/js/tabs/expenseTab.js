
let allExpenses = [];
const itemsPerPage = 10;
let currentPage = 1;

const expenseStatusMap = {
    1: 'Submetida',
    2: 'Paga'
};

function formatarCPF(cpf) {
    cpf = cpf.replace(/\D/g, '');
    cpf = cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
    return cpf;
}

async function loadExpenses(url, tabela) {
    const response = await fetch(url);
    if (response.ok) {
        const expensesData = await response.json();
        const tableBody = document.getElementById(tabela).querySelector('tbody');
        tableBody.innerHTML = '';

        for (const expense of expensesData) {

            let userInfo = { name: 'Desconhecido', lastName: '', cpf: 'Desconhecido' }
            const userResponse = await fetch(`https://localhost:7016/api/usermanagement/view-user?id=${expense.userInfoId}`);
            if (userResponse.ok) {
                userInfo = await userResponse.json();
            }

            const row = tableBody.insertRow();
            const idCell = row.insertCell();
            const fullNameCell = row.insertCell();
            const cpfCell = row.insertCell();
            const descriptionCell = row.insertCell();
            const valueCell = row.insertCell();
            const statusCell = row.insertCell();
            const activeCell = row.insertCell();

            if (tabela === 'expensesTable') {
                const actionCell = row.insertCell();
                const actionButton = document.createElement('button');
                actionButton.textContent = 'Aprovar';
                actionButton.onclick = async function () {
                    try {
                        const response = await fetch(`https://localhost:7016/api/expense/update-status-expense?idExpense=${expense.id}`, {
                            method: 'PATCH',
                            headers: {
                                'Content-Type': 'application/json',
                            },
                        });

                        if (!response.ok) {
                            throw new Error(`Erro ao atualizar status da despesa: ${response.statusText}`);
                        }

                        location.reload();

                    } catch (error) {
                        console.error('Falha ao fazer a requisição:', error);
                    }
                };
                actionCell.appendChild(actionButton);
            }


            idCell.textContent = expense.id;
            fullNameCell.textContent = `${userInfo.name} ${userInfo.lastName}`;
            cpfCell.textContent = formatarCPF(userInfo.cpf);
            descriptionCell.textContent = expense.description || 'No description';
            valueCell.textContent = expense.totalValue.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
            statusCell.textContent = expenseStatusMap[expense.status] || 'No Status';
            activeCell.textContent = expense.isActive ? 'Sim' : 'Não';
        };
    } else {
        console.error('Failed to load expenses:', response.status);
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
    const isAdminAndFinance = roles.includes('Admin') && roles.includes('Finance');

    loadExpenses('https://localhost:7016/api/expense/view-expense-submitted', 'expensesTable');

    if (isAdminAndFinance) {
        loadExpenses('https://localhost:7016/api/expense/view-expense', 'expensesTableAll');
        loadExpenses('https://localhost:7016/api/expense/view-expense-active', 'expensesTableActive');
    }
}

init();
