param($installPath, $toolsPath, $package, $project)

[Diagnostics.Process]::Start("http://endpointmvc.bit13.com/Version/Install/?id=" + $package.Id + "&v=" + $package.Version,"")
