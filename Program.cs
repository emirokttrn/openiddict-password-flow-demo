using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using openiddictAPI.data;
using static OpenIddict.Abstractions.OpenIddictConstants;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options=>{
options.UseInMemoryDatabase("openiddict-demo");
options.UseOpenIddict();
}
);
builder.Services.AddOpenIddict()
.AddCore(options=>
{
    options.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>();
}).AddServer( options =>
{
    //token en pointini aciyor
    options.SetTokenEndpointUris("connect/token");
    
    //pasword flowunu aktif ediyor(username/password ile token alma)
    options.AllowPasswordFlow();

//gelistirme sertifikalri(test icin)
    options.AddDevelopmentEncryptionCertificate()
    .AddDevelopmentSigningCertificate();
//asp.net hostunu kaydeder
    options.UseAspNetCore().
    EnableTokenEndpointPassthrough()
    .DisableTransportSecurityRequirement(); 

}

);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
await using (var scope = app.Services.CreateAsyncScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.EnsureCreatedAsync();

    var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

    if (await manager.FindByClientIdAsync("test-client") is null)
    {
        await manager.CreateAsync(new OpenIddictApplicationDescriptor
        {
            ClientId = "test-client",
            ClientSecret = "test-secret",
            Permissions =
            {
                Permissions.Endpoints.Token,
                Permissions.GrantTypes.Password
            }
        });
    }
}

app.Run();

