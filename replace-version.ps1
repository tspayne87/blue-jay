Get-ChildItem -Path ./*.Nuget.csproj -Recurse -Force | ForEach {
  (Get-Content $_ | ForEach {$_ -replace {{{Version}}}, '0.1.1'}) | Set-Content $_
}