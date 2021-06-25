# Get the semver version from the argument list
$replace=$args[0];

# Gather all the Nuget.csproj files and update the version information in the file
Get-ChildItem -Path ./*.Nuget.csproj -Recurse -Force | ForEach {
  # Log information to the host so we can check in the pipelines what is going on
  Write-Host "Updating File: $_ | Version ($replace)"

  # Get the content of the file, iterate over and replace all {{Version}} tokens, and write back the results of the replace
  # so that all versions of the nuget packages are in sync
  (Get-Content $_ | ForEach {$_ -replace {{{Version}}}, $replace}) | Set-Content $_
}