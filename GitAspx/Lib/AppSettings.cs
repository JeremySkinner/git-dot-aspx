namespace GitAspx.Lib {
	using System;
	using System.Configuration;
	using System.IO;

	public class AppSettings {
		public DirectoryInfo RepositoriesDirectory { get; set; }
		public bool UploadPack { get; set; }
		public bool ReceivePack { get; set; }

		public static AppSettings FromAppConfig() {
			var settings = new AppSettings();

			var path = ConfigurationManager.AppSettings["RepositoriesDirectory"];

			if (string.IsNullOrEmpty(path)) {
				throw new InvalidOperationException("The 'Repositories' AppSetting has not been initialised.");
			}

			if (!Directory.Exists(path)) {
				throw new DirectoryNotFoundException(string.Format("Could not find the directory '{0}' which is configured as the directory of repositories.", path));
			}

			settings.RepositoriesDirectory = new DirectoryInfo(path);


			var uploadPackRaw = ConfigurationManager.AppSettings["UploadPack"];
			var receivePackRaw = ConfigurationManager.AppSettings["ReceivePack"];

			bool uploadpack;
			bool receivePack;

			if(!string.IsNullOrEmpty(uploadPackRaw) && bool.TryParse(uploadPackRaw, out uploadpack)) {
				settings.UploadPack = uploadpack;
			}
			
			if(!string.IsNullOrEmpty(receivePackRaw) && bool.TryParse(receivePackRaw, out receivePack)) {
				settings.ReceivePack = receivePack;
			}

			return settings;
		}

	}

}