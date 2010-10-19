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
}