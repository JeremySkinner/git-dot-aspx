namespace GitAspx.ViewModels {
	using System.Collections.Generic;
	using System.IO;

	public class DirectoryListViewModel {
		public string RepositoriesDirectory { get; set; }
		public IEnumerable<DirectoryInfo> Directories { get; set; }
	}
}