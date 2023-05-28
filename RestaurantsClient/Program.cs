using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RestaurantsClient;
using RestaurantsClient.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
 
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Enable CORS for API endpoint
builder.Services.AddCors(options =>  
{  
    options.AddDefaultPolicy(  
        policy =>  
        {  
            policy.WithOrigins("http://localhost:5000").AllowAnyHeader();  //set the allowed origin  
        });  
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<CartManager>();

await builder.Build().RunAsync();
