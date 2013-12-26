param($installPath, $toolsPath, $package, $project)

$DTE.ItemOperations.Navigate("http://endpointmvc.bit13.com/Version/Install/" + $package.Id + "?v" + $package.Version)
