using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StoreSolution.Application.Auth.command;
using FluentAssertion;
using NUnit.Framework;

namespace AppTest;

using static Testing;

public class LoginTest {

    // [SetUp]
    // public async Task setup()
    // {
    //     await ResetState();
    // }
    
    [Test]
    public async Task LoginParameterRequirementTest() {
        LoginCommand command = new LoginCommand("", "");
        Assert.Throws<Exception>(async () =>
        {
            await SendAsync(command);
        });
    }
    
}