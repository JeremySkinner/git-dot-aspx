namespace GitAspx.Lib {
	using System.IO;
	using GitSharp.Core;

	public class RepositoryService {
		AppSettings appSettings = new AppSettings();

		public Repository GetRepository(string project) {
			var directory = Path.Combine(appSettings.RepositoriesDirectory.FullName, project);

			if(!Directory.Exists(directory)) {
				throw new DirectoryNotFoundException(string.Format("Could not find the directory '{0}'", directory));
			}

			var directoryWithDotGit = Path.Combine(directory, ".git");

			if(Directory.Exists(directoryWithDotGit)) {
				directory = directoryWithDotGit;
			}
			
			return new Repository(new DirectoryInfo(directory));
		}
	}
}