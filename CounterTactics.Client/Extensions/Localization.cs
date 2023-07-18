using System.Globalization;

using Blazored.LocalStorage;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace CounterTactics.Client.Extensions;

public static class Localization
{
    private const string DefaultCulture = "pl-PL";
    public const string BlazorCultureKey = "BlazorCulture";

    public static readonly CultureInfo[] SupportedCultures =
    {
        new("pl-PL"),
        new("en-US")
    };

    public static async Task<WebAssemblyHost> ConfigureLocalization(this WebAssemblyHost host)
    {
        var ls = host.Services.GetRequiredService<ILocalStorageService>();
        var result = await ls.GetItemAsStringAsync(BlazorCultureKey);
        var culture = new CultureInfo(DefaultCulture);
        if (!string.IsNullOrWhiteSpace(result))
        {
            culture = new CultureInfo(result);
        }
        else
        {
            await ls.SetItemAsStringAsync(BlazorCultureKey, culture.Name);
        }

        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        return host;
    }
}