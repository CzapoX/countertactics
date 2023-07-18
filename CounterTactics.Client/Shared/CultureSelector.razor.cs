using System.Globalization;

using Blazored.LocalStorage;

using CounterTactics.Client.Extensions;

using Microsoft.AspNetCore.Components;

namespace CounterTactics.Client.Shared;

public partial class CultureSelector
{
    [Inject] private NavigationManager Navigation { get; set; } = default!;
    [Inject] private ILocalStorageService LocalStorageService { get; set; } = default!;


    private async Task ChangeCulture(CultureInfo supportedCulture)
    {
        if (Equals(CultureInfo.CurrentCulture, supportedCulture))
            return;

        await LocalStorageService.SetItemAsStringAsync(Localization.BlazorCultureKey, supportedCulture.Name);

        Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
    }
}