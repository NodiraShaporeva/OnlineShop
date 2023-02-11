using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using OnlineShop.HttpApiClient;

#pragma warning disable CS8618

namespace OnlineShop.BlazorClient.Pages;

public abstract class AppComponentBase : ComponentBase
{
    [Inject] protected IShopClient BaseShopClient { get; private set; }
    [Inject] protected ILocalStorageService LocalStorage { get; private set; }

    protected bool IsTokenChecked { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (!IsTokenChecked)
        {
            IsTokenChecked = true;
            var token = await LocalStorage.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
            {
                BaseShopClient.SetAuthToken(token);
            }
        }
    }
}