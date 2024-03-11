
namespace AdaTech.AIntelligence.Service.Services.ExpenseServices
{
    public interface IExpenseScriptGPT
    {
        Task<StringContent> ExpenseScriptPrompt (string imagem);
    }
}
