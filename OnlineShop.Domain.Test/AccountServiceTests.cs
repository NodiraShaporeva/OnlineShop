using Bogus;
using Moq;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.RepositoriesInterfaces;
using OnlineShop.Domain.Services;

namespace OnlineShop.Domain.Test;

public class AccountServiceTests
{
    [Fact]
    private async void Register_new_user_succeeded()
    {
        var fakeUser = new Faker();
        
        var cartRepoMock = new Mock<ICartRepository>();
        var accountRepoMock = new Mock<IAccountRepository>();
        var uowMock = new Mock<IUnitOfWork>();
        var passwordHasherMock = new Mock<IPasswordHasherService>();
        var tokenServiceMock = new Mock<ITokenService>();

        uowMock.Setup(u => u.AccountRepository)
            .Returns(accountRepoMock.Object);
        uowMock.Setup(u => u.CartRepository)
            .Returns(cartRepoMock.Object);

        passwordHasherMock.Setup(x => x.HashPassword(It.IsAny<string>()))
            .Returns<string>(x => x);

        var accountService = new AccountService(
            accountRepoMock.Object, passwordHasherMock.Object, uowMock.Object, tokenServiceMock.Object);

        var (account, token) = await accountService.Register(
            fakeUser.Person.FullName, fakeUser.Person.Email, fakeUser.Internet.Password());
        
        accountRepoMock.Verify(
            x=>x.Add(It.Is<Account>(a=>a==account),default), Times.Once);
        
        cartRepoMock.Verify(
            x=>x.Add(It.Is<Cart>(c=>c.AccountId==account.Id),default), Times.Once);

        uowMock.Verify(x=>x.SaveChangesAsync(default), Times.AtLeastOnce);
    }
}