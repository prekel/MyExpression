$env:rev = git rev-list HEAD --count
$env:assembly_version = "$env:appveyor_build_version.$env:rev"
Write-Host -Backgroundcolor Green -Foregroundcolor White "Assembly Version: $env:assembly_version"