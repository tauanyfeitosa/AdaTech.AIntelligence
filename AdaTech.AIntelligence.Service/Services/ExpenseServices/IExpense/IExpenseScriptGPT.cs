namespace AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense
{
    public interface IExpenseScriptGPT
    {
        Task<StringContent> ExpenseScriptPrompt(string imagem);
    }
}
