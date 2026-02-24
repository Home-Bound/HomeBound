using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using InmoCierres;
using Blazored.LocalStorage; // <--- 1. AGREGAMOS ESTO
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// <--- 2. AGREGAMOS ESTA LÍNEA (Registramos el servicio)
builder.Services.AddBlazoredLocalStorage(); 
builder.Services.AddAuthorizationCore();
// Le decimos que nuestro FirebaseAuthStateProvider es quien manda en las sesiones
builder.Services.AddScoped<AuthenticationStateProvider, FirebaseAuthStateProvider>();

await builder.Build().RunAsync();