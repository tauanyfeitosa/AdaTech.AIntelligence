document.addEventListener('DOMContentLoaded', () => {
  const optionDeleteSelect = document.getElementById('optionDelete');
  const optionsExpensesDeleteSelect = document.getElementById('optionsExpensesDelete');

  optionDeleteSelect.addEventListener('change', function() {
    optionsExpensesDeleteSelect.innerHTML = '';
    const endpoint = this.value === 'hard' ? 'view-expense' : 'view-expense-active';
    loadExpensesOptions(endpoint);
  });

  async function loadExpensesOptions(endpoint) {
    try {
      const response = await fetch(`https://localhost:7016/api/expense/${endpoint}`);
      if (response.ok) {
        const expenses = await response.json();
        expenses.forEach(expense => {

            const option = document.createElement('option');
            option.value = expense.id;
            option.textContent = `ID: ${expense.id} - Valor Total: ${expense.totalValue}`; 
            optionsExpensesDeleteSelect.appendChild(option);
        });
      } else {
        console.error('Erro ao carregar despesas:', response.statusText);
      }
    } catch (error) {
      console.error('Erro ao fazer a requisição:', error);
    }
  }

  loadExpensesOptions('view-expense');
});
