document.addEventListener('DOMContentLoaded', init);

async function init() {
    const optionDeleteSelect = document.getElementById('optionDelete');
    const optionsExpensesDeleteSelect = document.getElementById('optionsExpensesDelete');

    async function getUserRoles() {
        try {
            const response = await fetch('https://localhost:7016/api/UserManagement/view-role-user-logged');
            if (response.ok) {
                const roles = await response.json();
                return roles.values;
            } else {
                console.error('Erro ao carregar roles:', response.statusText);
                return [];
            }
        } catch (error) {
            console.error('Erro ao fazer a requisição para obter roles:', error);
            return [];
        }
    }

    async function loadExpensesOptions(endpoint) {
        try {
            const response = await fetch(`https://localhost:7016/api/expense/${endpoint}`);
            if (response.ok) {
                const expenses = await response.json();
                expenses.forEach(expense => {
                    const status = expense.status === 1 ? 'Submetida' : 'Paga';
                    const valor = expense.totalValue.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
                    const option = document.createElement('option');
                    option.value = expense.id;
                    option.textContent = `ID: ${expense.id} - Valor Total: ${valor} - Status: ${status} - Ativo: ${expense.isActive ? 'Sim' : 'Não'}`;
                    optionsExpensesDeleteSelect.appendChild(option);
                });
            } else {
                throw new Error(`Erro ao carregar despesas: ${response.statusText}`);
            }
        } catch (error) {
            console.error('Erro ao fazer a requisição:', error);
        }
    }

    const userRoles = await getUserRoles();
    if (userRoles.includes('Admin') && userRoles.includes('Finance')) {
        optionDeleteSelect.innerHTML = `
            <option value="hard">Excluir permanentemente</option>
            <option value="soft">Tornar inativo</option>
        `;
    } else if (userRoles.includes('Finance')) {
        optionDeleteSelect.innerHTML = `
            <option value="soft">SoftDelete</option>
        `;
    } else {
        optionDeleteSelect.innerHTML = '<option value="">Não autorizado</option>';
    }

    let defaultEndpoint = userRoles.includes('Admin') ? 'view-expense' : 'view-expense-submitted';
    loadExpensesOptions(defaultEndpoint);

    optionDeleteSelect.addEventListener('change', async function () {
        optionsExpensesDeleteSelect.innerHTML = '';
        let endpoint = '';

        if (userRoles.includes('Admin') && userRoles.includes('Finance')) {
            endpoint = optionDeleteSelect.value === 'hard' ? 'view-expense' : 'view-expense-active';
        } else if (userRoles.includes('Finance')) {
            endpoint = 'view-expense-submitted';
        }
        if (endpoint) {
            loadExpensesOptions(endpoint);
        }
    });
}
