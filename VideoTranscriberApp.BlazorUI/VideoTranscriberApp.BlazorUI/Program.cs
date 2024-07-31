using OpenAI.Extensions;
using VideoTranscriberApp.BlazorUI.Components;
using VideoTranscriberApp.BlazorUI.Client.Pages;
using VideoTranscriberApp.BlazorUI.Components;
using VideoTranscriberApp.BlazorUI.Sevices;
using BlazorDownloadFile;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5220/api/") });

builder.Services.AddControllers();

var openAIApiKey = builder
    .Configuration
    .GetSection("OpenAIApiKey")
    .Value;

builder.Services.AddOpenAIService(settings => settings.ApiKey = openAIApiKey);

builder.Services.AddScoped<TranscriptionManager>();

builder.Services.AddBlazorDownloadFile(ServiceLifetime.Scoped);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(VideoTranscriberApp.BlazorUI.Client._Imports).Assembly);

app.MapControllers();

app.Run();
