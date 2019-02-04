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
    Write-Host "Pushing to C:\nuget (/nuget on macOS or Unix): $file"
	 
    if($PSVersionTable.Platform -eq "Unix")
    {
        Write-Host "macOS or Unix"
        $targetDir = "/nuget"
    }
    else
    {    
        Write-Host "Windows"        
        $targetDir = "C:\nuget"
    }    

    New-Item -ItemType Directory -Force -Path $targetDir
    
    $targetFile = Join-Path -Path $targetDir -ChildPath $file.Name
    Copy-Item $file $targetFile
}
Set-Location ..

Write-Host('')
[void](Read-Host 'Press ENTER to continue')