$items = Get-ChildItem -Recurse -Include AssemblyInfo.*
foreach ($i in $items) { (Get-Content $i) -replace "111.111.111.111", $env:assembly_version | Set-Content $i }
foreach ($i in $items) { (Get-Content $i) -replace "222.222.222.222", $env:assembly_version | Set-Content $i }