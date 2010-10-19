namespace GitAspx.Lib {
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.IO;
	using System.Net;
	using GitSharp.Core;
	using GitSharp.Core.Exceptions;
	using GitSharp.Core.Transport;
	using GitSharp.Core.Util;

	// Modified code from GitSharp

	public class MockRefWriter : RefWriter {
		private readonly global::GitSharp.Core.Repository _db;

		public MockRefWriter(global::GitSharp.Core.Repository db, IEnumerable<global::GitSharp.Core.Ref> refs)
			: base(refs) {
			_db = db;
		}

		protected override void writeFile(string file, byte[] content) {
			FileInfo p = PathUtil.CombineFilePath(_db.Directory, file);
			LockFile lck = new LockFile(p);
			if (!lck.Lock())
				throw new ObjectWritingException("Can't write " + p);
			try {
				lck.Write(content);
			}
			catch (IOException) {
				throw new ObjectWritingException("Can't write " + p);
			}
			if (!lck.Commit())
				throw new ObjectWritingException("Can't write " + p);
		}
	}

	public class HttpObjectDatabase : WalkRemoteObjectDatabase {

		public override URIish getURI() {
			throw new NotImplementedException();
		}

		public override ICollection<WalkRemoteObjectDatabase> getAlternates() {
			try {
				return readAlternates(INFO_HTTP_ALTERNATES);
			}
			catch (FileNotFoundException) {
				// Fall through.
			}

			try {
				return readAlternates(INFO_ALTERNATES);
			}
			catch (FileNotFoundException) {
				// Fall through.
			}

			return null;
		}

		public override WalkRemoteObjectDatabase openAlternate(string location) {
			throw new NotImplementedException();
		}

		public override ICollection<string> getPackNames() {
			var packs = new List<string>();
			try {
				using (StreamReader br = openReader(INFO_PACKS)) {
					while (true) {
						string s = br.ReadLine();
						if (string.IsNullOrEmpty(s)) break;

						if (!s.StartsWith("P pack-") || !s.EndsWith(IndexPack.PackSuffix)) {
							throw InvalidAdvertisement(s);
						}
						packs.Add(s.Substring(2));
					}

					return packs;
				}
			}
			catch (FileNotFoundException) {
				return packs;
			}
		}

		public override Stream open(string path) {
			return File.OpenRead(path);

		/*	Uri @base = _objectsUrl;
			var u = new Uri(@base, path);

			var c = (HttpWebRequest)WebRequest.Create(u);

			try {
				var response = (HttpWebResponse)c.GetResponse();
				switch (response.StatusCode) {
					case HttpStatusCode.OK:
						return response.GetResponseStream();

					default:
						throw new IOException(u + ": " + response.StatusDescription);
				}
			}
			catch (WebException e) {
				var response2 = ((HttpWebResponse)(e.Response));
				if (response2.StatusCode == HttpStatusCode.NotFound) {
					throw new FileNotFoundException(u.ToString());
				}

				throw new IOException(u + ": " + response2.StatusDescription, e);
			}*/

		}

		private static PackProtocolException InvalidAdvertisement(string n) {
			return new PackProtocolException("invalid advertisement of " + n);
		}

		public override void close() {
			// We do not maintain persistent connections.
		}
	}

}