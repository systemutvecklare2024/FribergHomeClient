using Blazored.LocalStorage;
using FribergHomeClient;
using FribergHomeClient.Providers;
using FribergHomeClient.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<ApiAuthenticationStateProvider>();

builder.Services.AddScoped<AuthenticationStateProvider>(p=>p.GetRequiredService<ApiAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();


builder.Services.AddScoped(sp => new HttpClient 
{ 
	BaseAddress = new Uri("https://localhost:7248")  
	
});
builder.Services.AddRadzenComponents();
builder.Services.AddScoped<NotificationService>();

await builder.Build().RunAsync();