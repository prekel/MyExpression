New-Item -Path 'bin' -ItemType "directory" -Force | Out-Null
$bins = "MyExpression.Core.dll", "MyExpression.Console.exe", "MyExpression.Wpf.exe"
foreach ($i in $bins)
{
    $j = $i.Substring(0, $i.Length - 4)
    Copy-Item -Path "$j\bin\Release\$i" -Destination "bin\$i"
}