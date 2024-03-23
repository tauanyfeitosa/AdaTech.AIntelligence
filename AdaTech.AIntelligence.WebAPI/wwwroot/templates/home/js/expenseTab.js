const expenseStatusMap = {
    1: 'Submetida',
    2: 'Paga'
};

function formatarCPF(cpf) {
    cpf = cpf.replace(/\D/g, '');
    cpf = cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
    return cpf;
}

async function loadExpenses() {
    const response = await fetch('https://localhost:7016/api/expense/view-expense-submitted');
    if (response.ok) {
        const expensesData = await response.json();
        const tableBody = document.getElementById('expensesTable').querySelector('tbody');
        tableBody.innerHTML = ''; 

        for (const expense of expensesData) {

            let userInfo = { name: 'Desconhecido', lastName: '', cpf: 'Desconhecido' }
            const userResponse = await fetch(`https://localhost:7016/api/usermanagement/view-user?id=${expense.userInfoId}`);
            if (userResponse.ok) {
                userInfo = await userResponse.json();
            }

            const row = tableBody.insertRow();
            const fullNameCell = row.insertCell();
            const cpfCell = row.insertCell();
            const descriptionCell = row.insertCell();
            const valueCell = row.insertCell();
            const statusCell = row.insertCell();

            fullNameCell.textContent = `${userInfo.name} ${userInfo.lastName}`;
            cpfCell.textContent = formatarCPF(userInfo.cpf);
            descriptionCell.textContent = expense.description || 'No description';
            valueCell.textContent = expense.totalValue.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
            statusCell.textContent = expenseStatusMap[expense.status] || 'No Status';
        };
    } else {
        console.error('Failed to load expenses:', response.status);
    }
}

loadExpenses();
