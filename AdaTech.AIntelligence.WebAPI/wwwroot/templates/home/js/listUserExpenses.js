document.addEventListener("DOMContentLoaded", function () {
    fetch('https://localhost:7016/api/expense/view-user-expenses')
        .then(response => {
            if (!response.ok) {
                throw new Error('Erro ao carregar despesas');
            }
            return response.json();
        })
        .then(data => {
            console.log(data);

            const expenses = data.$values; // Obtém o array de despesas

            const tableBody = document.querySelector('#expensesTable tbody');

            // Limpa o conteúdo da tabela antes de adicionar os novos dados
            tableBody.innerHTML = '';

            console.log(expenses);

            // Itera sobre cada despesa e cria uma nova linha na tabela
            expenses.forEach(expense => {
                const row = document.createElement('tr');

                // Adiciona os dados da despesa em cada célula da linha
                row.innerHTML = `
                        <td>${expense.id}</td>
                        <td>${expense.status}</td>
                        <td>${expense.description}</td>
                        <td>${expense.category}</td>
                        <td>${expense.totalValue}</td>
                        <td>${expense.isActive ? 'Yes' : 'No'}</td>
                        <td>${new Date(expense.creatAt).toLocaleString()}</td>
                    `;

                // Adiciona a linha à tabela
                tableBody.appendChild(row);
            });
        })
        .catch(error => console.error('Erro ao carregar despesas:', error.message));
});
