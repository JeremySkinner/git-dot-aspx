This is a simple implementation of git-http-backend written in ASP.NET that can be used to read/write git repositories on Windows with IIS.

Inspired by Grack (http://github.com/schacon/grack)

This is largely untested, but has been developed with IIS7.5 under Windows 7 x64. 

The version of GitSharp included is a custom build with some minor changes. Details to follow.

Requirements:
- VS2010 with .NET 4
- IIS7

Edit the web.config and change the "RepositoriesDirectory" app-setting to point to a directory containing git repositories.
