using Microsoft.AspNetCore.Mvc;
using KlingelnbergMachinesInformation.Api.Configurations;
using KlingelnbergMachinesInformation.Api.Middleware;
using Newtonsoft.Json;

using KlingelnbergMachinesInformation.Api.Services;
using System.Reflection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        
        builder.Services.Configure<KIPLDatabaseSetting>(
            builder.Configuration.GetSection("KIPLDatabase"));

        builder.Services.AddMvc(setupAction =>
        {
            setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
            setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));
            setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
            setupAction.Filters.Add(
                    new ProducesDefaultResponseTypeAttribute());
            setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));

           setupAction.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            var jsonOutputFormatter = setupAction.OutputFormatters.OfType<NewtonsoftJsonOutputFormatter>().FirstOrDefault();
            if (jsonOutputFormatter != null)
            {
                if (jsonOutputFormatter.SupportedMediaTypes.Contains("text/json"))
                {
                    jsonOutputFormatter.SupportedMediaTypes.Remove("text/json");
                }
            }
        });



        builder.Services.AddControllers(setupAction =>
        {
            setupAction.ReturnHttpNotAcceptable = true;
        }).AddNewtonsoftJson(setupAction =>{
            setupAction.SerializerSettings.ContractResolver=new CamelCasePropertyNamesContractResolver();
        }).AddXmlDataContractSerializerFormatters()
        ;
        
       
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(setupAction =>
        {
            setupAction.SwaggerDoc("KlingelnbergMachineDataApiSpecification",
                new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Klingelnberg Machine Data API",
                    Version = "v1",
                    Description = "Through this API you can access Klingelnberg Machine data with Machine name, Asset name and series number of asset.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Name = "Omkar Kadam",
                        Email = "omkar.kadam@klingelnberg,com",
                        Url = new Uri("https://klingelnberg.com/")
                    }
                });
            var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
            setupAction.IncludeXmlComments(xmlCommentsFullPath);
        });
        builder.Services.AddScoped<IKIPLMachineDataService,KIPLMachineDataService>();
        

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.InjectStylesheet("/UIStyle.css");
                
                setupAction.SwaggerEndpoint("/swagger/KlingelnbergMachineDataApiSpecification/swagger.json", "Klingelnberg Machine Data API");
                setupAction.DefaultModelExpandDepth(2);
                setupAction.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
                setupAction.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                setupAction.EnableDeepLinking();
                setupAction.DisplayOperationId();
            });
        }

        //app.UseHttpsRedirection();

       // app.UseMiddleware<ExceptionMiddleware>();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}