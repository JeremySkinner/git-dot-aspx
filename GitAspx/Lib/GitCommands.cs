using System;
using System.Diagnostics;
using System.IO;

namespace GitAspx.Lib
{
	public class GitCommands
	{
		public string Invoke(string commandLineFormat, params object[] args)
		{
			var process = InvokeProcess(commandLineFormat, args);
			process.WaitForExit();
			return process.StandardOutput.ReadToEnd();
		}

		public Process InvokeProcess(string commandLineFormat, params object[] args)
		{
			var commandLine = string.Format(commandLineFormat, args);

			var psi = new ProcessStartInfo("c:\\program files (x86)\\git\\bin\\git.exe", commandLine) {
				WorkingDirectory = "c:\\projects\\gittest\\simplegit",
				UseShellExecute = false,
				RedirectStandardError = true,
				RedirectStandardOutput = true,
				RedirectStandardInput = true,
				CreateNoWindow = true
			};

			var process = Process.Start(psi);
			return process;
		}
	}
}