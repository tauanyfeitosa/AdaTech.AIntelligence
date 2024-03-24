
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

loadExpenses('https://localhost:7016/api/expense/view-expense-submitted', 'expensesTable');
loadExpenses('https://localhost:7016/api/expense/view-expense', 'expensesTableAll');
loadExpenses('https://localhost:7016/api/expense/view-expense-active', 'expensesTableActive');
