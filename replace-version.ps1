# Get the semver version from the argument list
$replace=$args[0];

# Gather all the Nuget.csproj files and update the version information in the file
Get-ChildItem -Path ./lib/*.csproj -Recurse -Force | ForEach-Object {
  # Log information to the host so we can check in the pipelines what is going on
  Write-Host "Updating File: $_ | Version ($replace)"

  # Get the content of the file, iterate over and replace all {{Version}} tokens, and write back the results of the replace
  # so that all versions of the nuget packages are in sync
  (Get-Content $_ | ForEach-Object {$_ -replace {<Version>1.0.0</Version>}, "<Version>$replace</Version>"}) | Set-Content $_
}