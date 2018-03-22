using System;
using System.IO;
using Autofac;

namespace ST.Common.Logger
{
	public class LoggerModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType(typeof(LogHandler)).As(typeof(ILogger)).InstancePerLifetimeScope();
		}

		public static void RegisterConfigFile(string fileName)
		{
			var log4NetConfigDirectory = AppDomain.CurrentDomain.BaseDirectory;
			var log4NetConfigFilePath = Path.Combine(log4NetConfigDirectory, fileName);
			if (!File.Exists(log4NetConfigFilePath))
				throw new FileNotFoundException($"{fileName} not found. Please add the config file in {log4NetConfigDirectory}.");

			log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(log4NetConfigFilePath));
		}
	}
}
