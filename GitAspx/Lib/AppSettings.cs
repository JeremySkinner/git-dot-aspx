namespace GitAspx.Lib {
	using System;
	using System.Configuration;
	using System.IO;

	public class AppSettings {
		public DirectoryInfo RepositoriesDirectory {
			get {
				var path = ConfigurationManager.AppSettings["RepositoriesDirectory"];
				if(string.IsNullOrEmpty(path)) {
					throw new InvalidOperationException("The 'Repositories' AppSetting has not been initialised.");
				}

				if(! Directory.Exists(path)) {
					throw new DirectoryNotFoundException("Could not find the directory '{0}' which is configured as the directory of repositories.");
				}

				return new DirectoryInfo(path);
			}
		}
	}
}