solution_file = "GitAspx.sln"
project_file = "GitAspx/GitAspx.csproj"

target default, (init, compile, package):
  pass
  
target init:
  rm("build")

target compile:
   msbuild(file: solution_file, configuration: "release", version: "4", targets: ("Build",))
   msbuild(file: project_file, configuration: "release", version: "4", targets: ("Build", "_CopyWebApplication"), properties: { "OutDir": "../build/output/" })

target package:
  zip("Build/Output/_PublishedWebsites/Gitaspx", "Build/GitAspx.zip")

target test, (compile):
  nunit(assembly: "GitAspx.Tests/bin/release/GitAspx.tests.dll")