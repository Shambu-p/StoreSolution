using StoreSolution.Application.Auth.command;
using StoreSolution.Application.common.Models;
using StoreSolution.Application.UserModule.command;
using StoreSolution.Domain.Entity;

namespace AppTest.MyTests;

using static Testing;

public class LoginTest {

    [SetUp]
    public async Task SetUp()
    {
        await ResetState();
    }

    /*
     * this test is designed to fail
     */
    [Test]
    public async Task LoginParameterRequirementTest() {
        LoginCommand command = new LoginCommand("", "");
        
        Assert.IsNotNull(await SendAsync(command));
        
        // the following assertion will make it to pass
        // Assert.Throws<Exception>(() =>
        // {
        //     await SendAsync(command);
        // });
    }

    [Test]
    public async Task CheckTokenReturn()
    {
        const string name = "test_user";
        const string email = "test_user@test.com";
        const byte role = 0;
        const string password = "password";
        
        CreateUser user_command = new CreateUser(name, email, role, password);
        User saved_user = await SendAsync(user_command);
        
        LoginCommand command = new LoginCommand(email, password);
        UserAuthentication logged_user = await SendAsync(command);

        Assert.NotNull(logged_user.Token);
        Assert.AreEqual(saved_user.Id, logged_user.Id);
        
    }

}