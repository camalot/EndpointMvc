param($installPath, $toolsPath, $package, $project)

[Diagnostics.Process]::Start("http://endpointmvc.bit13.com/Version/Install/" + $package.Id + "?v=" + $package.Version,"")
