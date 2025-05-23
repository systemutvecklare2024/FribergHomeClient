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

// Custom services
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IRealEstateAgencyService, RealEstateAgencyService>();
builder.Services.AddScoped<IRealEstateAgentService, RealEstateAgentService>();
builder.Services.AddScoped<IMuncipalityService, MuncipalityService>();

// Auth
builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p=>p.GetRequiredService<ApiAuthenticationStateProvider>());
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped(sp => new HttpClient 
{ 
	BaseAddress = new Uri("https://localhost:7248")  
	
});

// Radzen
builder.Services.AddRadzenComponents();
builder.Services.AddScoped<NotificationService>();

await builder.Build().RunAsync();