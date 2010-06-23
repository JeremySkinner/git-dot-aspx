namespace GitAspx.Lib {
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using GitSharp.Core;

	public class RepositoryService {
		AppSettings appSettings = new AppSettings();

		public IEnumerable<DirectoryInfo> GetAllRepositories() {
			return appSettings.RepositoriesDirectory.GetDirectories();
		}

		public Repository GetRepository(string project) {
			var directory = Path.Combine(appSettings.RepositoriesDirectory.FullName, project);

			if(!Directory.Exists(directory)) {
				return null;
			}

			return Repository.Open(directory);
		}

		public DirectoryInfo GetRepositoriesDirectory() {
			return appSettings.RepositoriesDirectory;
		}
	}
}