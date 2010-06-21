This is a simple implementation of git-http-backend written in ASP.NET that can be used to read/write git repositories on Windows with IIS.

Inspired by Grack (http://github.com/schacon/grack)

This is largely untested, but has been developed with IIS7.5 under Windows 7 x64. 

The version of GitSharp included is a custom build with some minor changes. Details to follow.

Requirements:
- VS2010 with .NET 4
- ASP.NET MVC2
- IIS7+

Edit the web.config and change the "RepositoriesDirectory" app-setting to point to a directory containing git repositories.

Assuming that your repositories directory looks like this:

C:\Repositories\Repo1.git

...and the RepositoriesDirectory app-setting is configured to be C:\Repositories:

<appSettings>
		<add key="RepositoriesDirectory" value="C:\Repositories"/>
</appSettings>
	
...and the application is configured under IIS7 on port 8000, then issuing the following command will cone the Repo1.git repository:

git clone http://localhost:8000/Repo1.git

Once cloned, push/pull work as expected.

There are currently no tests (something I hope to rectify soon). If you run into a problem, the best way to troubleshoot is by using Fiddler to see the raw request/response data.