# Gather all the Nuget.csproj files and update the version information in the file
Get-ChildItem -Path ./*.Nuget.csproj -Recurse -Force | ForEach {
  (Get-Content $_ | ForEach {$_ -replace {{{Version}}}, $args[0]}) | Set-Content $_
}