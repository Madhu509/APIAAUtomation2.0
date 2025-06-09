using APIAutomation.FeatureSteps;
using APIAutomation.Utilities;
using APIAutomation.Config;
using Autofac;
using Microsoft.Extensions.Configuration;
using Reqnroll;
using WebAutomation.Config;
using APIAutomation.Models.Contexts;
using APIAutomation.Utilities.EndPoints;
using Reqnroll.Autofac;
using Reqnroll.Autofac.ReqnrollPlugin;

[Binding]
public sealed class GlobalHooks
{
    private static IConfiguration? Configuration;

    [GlobalDependencies]
    public static void CreateGlobalContainer(ContainerBuilder container)
    {
        // Load configuration
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<GlobalHooks>()
            .Build();

        // Register Configuration as a singleton
        container.RegisterInstance(Configuration).As<IConfiguration>().SingleInstance();

        container.RegisterType<EmployeeDataContexts>().AsSelf().SingleInstance();

        // Bind specific configuration sections to objects
        var environmentConfig = Configuration.GetSection("Environment").Get<EnvironmentConfig>() ?? throw new InvalidOperationException("Environment not set.");
        var connectionStringConfig = Configuration.GetSection("ConnectionStrings").Get<ConnectionStringConfig>() ?? throw new InvalidOperationException("ConnectionStrings not set.");
        var endpointConfig = Configuration.GetSection("Endpoints").Get<EndpointConfig>() ?? throw new InvalidOperationException("Endpoints not set.");
        var identityServerConfig = Configuration.GetSection("IdentityServer").Get<IdentityServerConfig>() ?? throw new InvalidOperationException("IdentityServer not set.");

        container.RegisterInstance(environmentConfig).SingleInstance();
        container.RegisterInstance(connectionStringConfig).SingleInstance();
        container.RegisterInstance(endpointConfig).SingleInstance();
        container.RegisterInstance(identityServerConfig).SingleInstance();

        container.RegisterType<EmployeeEndpoints>().AsSelf().SingleInstance();
        container.RegisterType<CompanyEndpoints>().AsSelf().SingleInstance();
    }

    [ScenarioDependencies]
    public static void SetupScenarioDependencies(ContainerBuilder container)
    {
        // Correct way to register step definition classes with Reqnroll.Autofac
        container.RegisterType<AuthenticationSteps>().AsSelf();
        container.RegisterType<EmployeeSteps>().AsSelf();
        // Assuming CompanyGetSteps was CompanyGETSteps based on earlier file listings
        container.RegisterType<CompanyGETSteps>().AsSelf();

        container.RegisterType<CommonSteps>().AsSelf();

    }
}