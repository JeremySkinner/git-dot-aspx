namespace GitAspx.Lib {
	using System;
	using System.IO;
	using GitSharp.Core.Transport;

	public class Repository {
		private DirectoryInfo directory;

		public static Repository Open(DirectoryInfo directory) {
			if(GitSharp.Repository.IsValid(directory.FullName)) {
				return new Repository(directory);
			}

			return null;
		}

		public Repository(DirectoryInfo directory) {
			this.directory = directory;
		}

		public void AdvertiseUploadPack(Stream output) {
			using (var repository = GetRepository()) {
				var pack = new UploadPack(repository);
				pack.sendAdvertisedRefs(new RefAdvertiser.PacketLineOutRefAdvertiser(new PacketLineOut(output)));
			}
		}

		public void AdvertiseReceivePack(Stream output) {
			using (var repository = GetRepository()) {
				var pack = new ReceivePack(repository);
				pack.SendAdvertisedRefs(new RefAdvertiser.PacketLineOutRefAdvertiser(new PacketLineOut(output)));
			}
		}

		public void Receive(Stream inputStream, Stream outputStream) {
			using(var repository = GetRepository()) {
				var pack = new ReceivePack(repository);
				pack.setBiDirectionalPipe(false);
				pack.receive(inputStream, outputStream, outputStream);
			}
		}

		public void Upload(Stream inputStream, Stream outputStream) {
			using (var repository = GetRepository()) {
				using (var pack = new UploadPack(repository)) {
					pack.setBiDirectionalPipe(false);
					pack.Upload(inputStream, outputStream, outputStream);
				}
			}
		}

		public CommitInfo GetLatestCommit() {
			using(var repository = new GitSharp.Repository(FullPath)) {
				var commit = repository.Head.CurrentCommit;

				if(commit == null) {
					return null;
				}

				return new CommitInfo {
					Message = commit.Message,
					Date = commit.CommitDate.DateTime
				};
			}
		}

		private GitSharp.Core.Repository GetRepository() {
			return GitSharp.Core.Repository.Open(directory);
		}

		public string Name {
			get { return directory.Name; }
		}

		public string FullPath {
			get { return directory.FullName; }
		}
	}

	public class CommitInfo {
		public string Message { get; set; }
		public DateTime Date { get; set; }
	}
}