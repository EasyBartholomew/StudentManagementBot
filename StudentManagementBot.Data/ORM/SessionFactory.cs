using System.Reflection;

using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Validator.Cfg;
using NHibernate.Validator.Cfg.Loquacious;
using NHibernate.Validator.Engine;
using NHibernate.Validator.Event;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using FluentConfiguration = NHibernate.Validator.Cfg.Loquacious.FluentConfiguration;
using NEnvironment = NHibernate.Validator.Cfg.Environment;


namespace StudentManagementBot.Data.ORM
{
    public static class SessionFactory
    {
        public static ISessionFactory Create(bool schemaExport = false)
        {
            var validatorConfiguration = new FluentConfiguration();

            validatorConfiguration
                .SetDefaultValidatorMode(ValidatorMode.UseAttribute)
                .Register(Assembly.GetExecutingAssembly().ValidationDefinitions())
                .IntegrateWithNHibernate
                .ApplyingDDLConstraints()
                .And
                .RegisteringListeners();

            var provider = new NHibernateSharedEngineProvider();
            NEnvironment.SharedEngineProvider = provider;

            var engine = provider.GetEngine();
            engine.Configure(validatorConfiguration);

            var fluentConfiguration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(c => c.FromConnectionStringWithKey("StudentManagementBotDataDB"))
                    .UseReflectionOptimizer())
                .Mappings(m =>
                    m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .ExposeConfiguration(cfg =>
                    new SchemaExport(cfg).Create(false, schemaExport));

            fluentConfiguration.BuildConfiguration().Initialize(engine);

            return fluentConfiguration.BuildSessionFactory();
        }
    }
}
