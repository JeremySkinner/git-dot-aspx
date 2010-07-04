solution_file = "GitAspx.sln"
project_file = "GitAspx/GitAspx.csproj"

target default, (init, compile, test, deploy, package):
  pass
  
target init:
  rm("build")

target compile:
  msbuild(file: solution_file, configuration: "release", version: "4")

target test:
  pass # Need to implement

target deploy:
  msbuild(file: project_file, configuration: "release", version: "4", targets: ("_CopyWebApplication",), properties: { "OutDir": "../build/" }, verbosity: "normal")

target package:
  pass