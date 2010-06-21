namespace GitAspx.Controllers {
	using System;
	using System.IO;
	using GitSharp.Core.Transport;

	// Handles project/git-upload-pack and project/git-receive-pack
	public class RpcController : BaseController {
		RepositoryService repositories = new RepositoryService();

		public void UploadPack(string project) {
			Response.ContentType = "application/x-git-upload-pack-result";
			WriteNoCache();

			using (var repository = repositories.GetRepository(project))
			using (var pack = new UploadPack(repository)) {
				pack.setBiDirectionalPipe(false);
				pack.Upload(Request.InputStream, Response.OutputStream, Response.OutputStream);
			}
		}

		public void ReceivePack(string project) {
			Response.ContentType = "application/x-git-receive-pack-result";
			WriteNoCache();



			using (var repository = repositories.GetRepository(project)) {
				var pack = new ReceivePack(repository);
				pack.setBiDirectionalPipe(false);
				// Have to wrap the stream because GitSharp is naugty. 
				pack.receive(new WrappedStream(Request.InputStream, Response.OutputStream), Response.OutputStream);
			}
		}


		// GitSharp calls Flush on our read-only input stream
		private class WrappedStream : Stream {
			private Stream readStream;
			private Stream writeStream;

			public WrappedStream(Stream readStream, Stream writeStream) {
				this.readStream = readStream;
				this.writeStream = writeStream;
			}

			public override void Flush() {
				writeStream.Flush();
			}

			public override long Seek(long offset, SeekOrigin origin) {
				return readStream.Seek(offset, origin);
			}

			public override void SetLength(long value) {
				readStream.SetLength(value);
			}

			public override int Read(byte[] buffer, int offset, int count) {
				return readStream.Read(buffer, offset, count);
			}

			public override void Write(byte[] buffer, int offset, int count) {
				writeStream.Write(buffer, offset, count);
			}

			public override bool CanRead {
				get { return readStream.CanRead; }
			}

			public override bool CanSeek {
				get { return readStream.CanSeek; }
			}

			public override bool CanWrite {
				get { return readStream.CanWrite; }
			}

			public override long Length {
				get { return readStream.Length; }
			}

			public override long Position {
				get { return readStream.Position; }
				set { readStream.Position = value; }
			}
		}
	}
}