using Octalines.Web;
using Volo.Abp;

var builder = WebApplication.CreateBuilder(args);

await builder.AddApplicationAsync<OctalinesWebModule>();

var app = builder.Build();

await app.InitializeApplicationAsync();

await app.RunAsync();
