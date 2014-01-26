call "C:\Program Files (x86)\Microsoft Visual Studio 12.0\VC\vcvarsall.bat" x86

REM Builds the projects making sure they are up to date.

msbuild Build.msbuild /p:Project=EndpointMvc.Mvc3 /verbosity:Diagnostic
msbuild Build.msbuild /p:Project=EndpointMvc.Mvc4 /verbosity:Diagnostic
msbuild Build.msbuild /p:Project=EndpointMvc.Mvc5 /verbosity:Diagnostic
msbuild Build.msbuild /p:Project=EndpointMvc /verbosity:Diagnostic

REM this will only build the package
msbuild NugetPublish.msbuild /p:PublishMode=NuGet /p:Project=EndpointMvc.Mvc3 /p:ProjectSharedName=EndpointMvc /p:NuSpecFile=EndpointMvc.Mvc3.nuspec /p:LocalDeploy=E:\Development\deploy\nuget\ /verbosity:Diagnostic
pause
msbuild NugetPublish.msbuild /p:PublishMode=NuGet /p:Project=EndpointMvc.Mvc4 /p:ProjectSharedName=EndpointMvc /p:NuSpecFile=EndpointMvc.Mvc4.nuspec /p:LocalDeploy=E:\Development\deploy\nuget\ /verbosity:Diagnostic
msbuild NugetPublish.msbuild /p:PublishMode=NuGet /p:Project=EndpointMvc.Mvc5 /p:ProjectSharedName=EndpointMvc /p:NuSpecFile=EndpointMvc.Mvc5.nuspec  /p:LocalDeploy=E:\Development\deploy\nuget\ /verbosity:Diagnostic
REM this is the "reference package" because I changed the nuget id.
msbuild NugetPublish.msbuild /p:NoBinaries=TRUE /p:PublishMode=NuGet /p:Project=EndpointMvc /p:NuSpecFile=EndpointMvc.nuspec  /p:LocalDeploy=E:\Development\deploy\nuget\ /verbosity:Diagnostic
pause