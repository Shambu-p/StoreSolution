using StoreSolution.Application.ItemsModule.command;
using StoreSolution.Application.StoreItemModule.command;
using StoreSolution.Application.StoreModule.command;
using StoreSolution.Application.UserModule.command;
using StoreSolution.Domain.Entity;

namespace AppTest.MyTests;
using static Testing;
public class StoreTest
{

    [SetUp]
    public async Task SetUp()
    {
        await ResetState();
    }

    [Test]
    public async Task StoreCreationTest()
    {
        string name = "test store";
        string user_name = "test user";
        string email = "testuser@test.com";
        byte role = 0;
        string password = "password";
        
        CreateUser user_command = new CreateUser(user_name, email, role, password);
        User store_keeper = await SendAsync(user_command);
        
        CreateStore command = new CreateStore(name, store_keeper.Id);
        Store created_store = await SendAsync(command);
        Assert.IsNotNull(created_store);
        
    }

    [Test]
    public async Task StoreItemCreation()
    {
        string name = "test store";
        string user_name = "test user";
        string email = "testuser@test.com";
        byte role = 0;
        string password = "password";
        
        CreateUser user_command = new CreateUser(user_name, email, role, password);
        User store_keeper = await SendAsync(user_command);
        
        CreateStore command = new CreateStore(name, store_keeper.Id);
        Store created_store = await SendAsync(command);

        CreateItem item_command = new CreateItem("test item", 1.0);
        Item created_item = await SendAsync(item_command);

        CreateStoreItem store_item_command = new CreateStoreItem(created_store.Id, created_item.Id, 3);
        StoreItem created_store_item = await SendAsync(store_item_command);
        
        Assert.IsNotNull(created_store_item);
        
    }
    
}