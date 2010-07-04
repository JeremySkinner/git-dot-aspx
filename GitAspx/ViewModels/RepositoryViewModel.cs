namespace GitAspx.ViewModels {
	using System;
	using GitAspx.Lib;

	public class RepositoryViewModel {
		private Repository repository;
		private CommitInfo latestCommit;

		public RepositoryViewModel(Repository repository) {
			this.repository = repository;
			this.latestCommit = repository.GetLatestCommit();
		}

		public string Name {
			get { return repository.Name; }
		}

		private string CommitDate {
			get { return latestCommit != null ? latestCommit.Date.ToPrettyDateString() : string.Empty; }
		}

		private string Message {
			get {
				if(latestCommit != null) {
					string message = latestCommit.Message;

					if(message.Length > 60) {
						return message.Substring(0, 57) + "...";
					}

					return message;
				}
				return string.Empty;
			}
		}

		public string LatestCommitInfo {
			get {return Message + " - " + CommitDate; }
		}
	}
}