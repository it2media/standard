New-Item -ItemType directory -Force -Path nupkgs
Set-Location nupkgs
Get-ChildItem -Path . -Include *.nupkg -Recurse | ForEach-Object { $_.Delete()}
Set-Location ..
#dotnet pack --no-build --output ../../nupkgs
dotnet pack --output ../../nupkgs
Set-Location nupkgs
$files = Get-ChildItem *.nupkg
foreach ($file in $files)
{
    Write-Host "Pushing to it2m14141.it2mtech.de: $file"

	$sourceNugetExe = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
	$targetNugetExe = "../tools/nuget.exe"
	Invoke-WebRequest $sourceNugetExe -OutFile $targetNugetExe
	
    $nugetexe = Resolve-Path ../tools/nuget.exe
    Write-Host $nugetexe
	 
    if($PSVersionTable.Platform -eq "Unix")
    {
        Write-Host "macOS or Unix"        
        mono $nugetexe push $file mqPY01tzBV -Source http://it2m14141.it2mtech.de/IT2media.NuGet.Server/api/v2/package
    }
    else
    {
        Write-Host "Windows"
        ../tools/nuget.exe push $file mqPY01tzBV -Source http://it2m14141.it2mtech.de/IT2media.NuGet.Server/api/v2/package
    }
    
}
Set-Location ..

Write-Host('')
[void](Read-Host 'Press ENTER to continue')