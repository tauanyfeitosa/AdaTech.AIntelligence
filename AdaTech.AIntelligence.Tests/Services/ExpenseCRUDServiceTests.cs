using AdaTech.AIntelligence.DbLibrary.Repository;
using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Exceptions;
using AdaTech.AIntelligence.Service.Services.DeleteStrategyService;
using AdaTech.AIntelligence.Service.Services.ExpenseServices;
using AdaTech.AIntelligence.Tests.Mock;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace AdaTech.AIntelligence.Tests.Services
{
    public class ExpenseCRUDServiceTests
    {
        private readonly IAIntelligenceRepository<Expense> _repository;
        private readonly GenericDeleteService<Expense> _deleteService;
        private readonly ExpenseCRUDService _expenseService;
        private readonly UserManager<UserInfo> _userManager = new UserInfoMock();

        public ExpenseCRUDServiceTests()
        {
            _repository = Substitute.For<IAIntelligenceRepository<Expense>>();
            _deleteService = Substitute.For<GenericDeleteService<Expense>>(_userManager);
            _expenseService = new ExpenseCRUDService(_repository, _deleteService);
        }


        [Fact]
        public async Task CreateExpense_Success_ReturnsTrue()
        {
            //Arrange
            var expense = new Expense();

            _repository.Create(expense).Returns(true);

            //Act
            var result = await _expenseService.CreateExpense(expense);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateExpense_Success_ReturnsTrue()
        {
            //Arrange
            var expense = new Expense();
            _repository.Update(expense).Returns(true);

            //Act
            var result = await _expenseService.UpdateExpense(expense);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void GetOne_Expense_Null_Returns_ThrowNotFoundException()
        {
            //Arange
            _repository.GetOne(Arg.Any<int>()).Returns(Task.FromResult<Expense>(null));

            //Act
            var action = async () => await _expenseService.GetOne(1);

            //Assert
            action.Should().ThrowExactlyAsync<NotFoundException>();
        }

        [Fact]
        public void GetOne_Expense_IsInactive_Returns_ThrowNotFoundException()
        {
            //Arange
            var expenseInactive = new Expense { IsActive = false };
            _repository.GetOne(Arg.Any<int>()).Returns(Task.FromResult<Expense>(expenseInactive));

            //Act
            var action = async () => await _expenseService.GetOne(1);

            //Assert
            action.Should().ThrowExactlyAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetOne_ExpenseFoundAndActive_ReturnsExepnse()
        {
            //Arrange
            var expenseFound = new Expense { IsActive = true };
            _repository.GetOne(Arg.Any<int>()).Returns(expenseFound);

            //Act
            var result = await _expenseService.GetOne(1);

            //Assert
            result.Should().NotBeNull();
            result.Should().Be(expenseFound);
        }

        [Fact]
        public async Task GetAllSubmitted_NotSubmittedExpenses_ReturnsEmptyList()
        {
            //Arange
            var expense = new List<Expense> { new Expense { Status = ExpenseStatus.PAID }, new Expense { Status = ExpenseStatus.PAID } };
            _repository.GetAll().Returns(expense);

            //Act
            var result = await _expenseService.GetAllSubmitted();

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllSubmitted_WithSubmittedExpenses_ReturnSubmittedExpenses()
        {
            //Arange
            var submittedExpense = new Expense { Status = ExpenseStatus.SUBMITTED, IsActive = true };
            var expense = new List<Expense> { new Expense { Status = ExpenseStatus.PAID }, submittedExpense, submittedExpense };
            _repository.GetAll().Returns(expense);

            //Act
            var result = await _expenseService.GetAllSubmitted();

            //Assert
            result.Should().Contain(submittedExpense).And.HaveCount(2);
        }

        [Fact]
        public async Task GetAllSubmitted_WithInactiveSubmittedExpenses_ReturnsEmptyList()
        {
            //Arange
            var submittedExpense = new Expense { Status = ExpenseStatus.SUBMITTED, IsActive = false };
            var expense = new List<Expense> { submittedExpense, submittedExpense };
            _repository.GetAll().Returns(expense);

            //Act
            var result = await _expenseService.GetAllSubmitted();

            //Assert
            result.Should().BeEmpty();
        }
        [Fact]
        public async Task GetAllActive_NoActiveExpense_ReturnsEmptyList()
        {

            //Arange
            var expense = new List<Expense> { new Expense { IsActive = false }, new Expense { IsActive = false} };
            _repository.GetAll().Returns(expense);

            //Act
            var result = await _expenseService.GetAllActive();

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllActive_WithActiveExpenses_ReturnActiveExpenses()
        {
            //Arange
            var submittedExpense = new Expense { IsActive = true };
            var expense = new List<Expense> { new Expense { IsActive = false }, submittedExpense, submittedExpense, submittedExpense };
            _repository.GetAll().Returns(expense);

            //Act
            var result = await _expenseService.GetAllActive();

            //Assert
            result.Should().Contain(submittedExpense).And.HaveCount(3);
        }

        [Fact]
        public async Task GetAll_ReturnAllExpenses()
        {
            //Arrange
            var expense = new List<Expense>{
                new Expense { IsActive = true },
                new Expense { IsActive = false },
                new Expense {Status = ExpenseStatus.PAID },
                new Expense {Status = ExpenseStatus.SUBMITTED} 
            };
            _repository.GetAll().Returns(expense);

            //Act
            var result = await _expenseService.GetAll();

            //Assert
            result.Should().Contain(expense);
        }
    }

}
