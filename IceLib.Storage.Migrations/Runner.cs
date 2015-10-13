using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.Model.Migrations
{
    public class Runner
    {
        public static void MigrateUp<TMigrationProcessor>(string connectionStringName, Assembly migrationAssembly)
            where TMigrationProcessor : MigrationProcessorFactory, new()
        {
            var connectionSettigns = ConfigurationManager.ConnectionStrings[connectionStringName];

            if (connectionSettigns == null) throw new InvalidOperationException(string.Format("Connection string '{0}' not found.", connectionStringName));

            var connectionString = connectionSettigns.ConnectionString;

            Announcer announcer = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));
            announcer.ShowSql = true;

            IRunnerContext migrationContext = new RunnerContext(announcer);

            var options = new ProcessorOptions
            {
                PreviewOnly = false,  // set to true to see the SQL
                Timeout = 60
            };

            try
            {
                var factory = new TMigrationProcessor(); 

                var processor = factory.Create(connectionString, announcer, options);

                var runner = new MigrationRunner(migrationAssembly, migrationContext, processor);
                    runner.MigrateUp(true);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("connectionString: " + connectionString, ex);
            }

        }
    }
}
