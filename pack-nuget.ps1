New-Item -ItemType directory -Force -Path nupkgs
Set-Location nupkgs
Get-ChildItem -Path . -Include *.nupkg -Recurse | ForEach-Object { $_.Delete()}
Set-Location ..
#dotnet pack --no-build --output ../../nupkgs
dotnet pack --output ../../nupkgs