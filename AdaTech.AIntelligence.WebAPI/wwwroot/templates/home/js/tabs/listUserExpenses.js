document.addEventListener("DOMContentLoaded", function () {
    fetch('https://localhost:7016/api/expense/view-user-expenses')
        .then(response => {
            if (!response.ok) {
                throw new Error('Erro ao carregar despesas');
            }
            return response.json();
        })
        .then(data => {

            const expenses = data.$values; // Obtém o array de despesas

            const tableBody = document.querySelector('#expensesUserTable tbody');

            // Limpa o conteúdo da tabela antes de adicionar os novos dados
            tableBody.innerHTML = '';


            const statusMap = {
                1: 'Submetida',
                2: 'Paga'
            }

            const categoryMap = {
                1: 'Alojamento',
                2: 'Transporte',
                3: 'Viagem',
                4: 'Alimentação',
                5: 'Outros'
            };

            // Itera sobre cada despesa e cria uma nova linha na tabela
            expenses.forEach(expense => {
                const row = document.createElement('tr');

                const statusText = statusMap[expense.status] || 'Desconhecido';
                const categoryText = categoryMap[expense.category] || 'Desconhecido';
                const totalFormatted = `R$ ${expense.totalValue.toFixed(2)}`;
                const isActiveText = expense.isActive ? 'Sim' : 'Não';


                // Adiciona os dados da despesa em cada célula da linha
                row.innerHTML = `
                        <td>${expense.id}</td>
                        <td>${statusText}</td>
                        <td>${expense.description}</td>
                        <td>${categoryText}</td>
                        <td>${totalFormatted}</td>
                        <td>${isActiveText}</td>
                        <td>${new Date(expense.createdAt).toLocaleString()}</td>
                    `;

                // Adiciona a linha à tabela
                tableBody.appendChild(row);
            });
        })
        .catch(error => console.error('Erro ao carregar despesas:', error.message));
});
