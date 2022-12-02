using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.Toast;
using OnlineShop.BlazorClient.Services;
using OnlineShop.BlazorClient;
using OnlineShop.HttpApiClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton<ILocalStorage, LocalStorage>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient
    { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) } 
);
builder.Services.AddSingleton<IShopClient>(new ShopClient());
//builder.Services.AddSingleton<IShopClient>(new ShopClientFake());
builder.Services.AddScoped<CartService>();
builder.Services.AddBlazoredToast();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();