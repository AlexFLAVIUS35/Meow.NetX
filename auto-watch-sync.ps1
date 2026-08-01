$watcher = New-Object System.IO.FileSystemWatcher
$watcher.Path = (Get-Location).Path
$watcher.IncludeSubdirectories = $true
$watcher.EnableRaisingEvents = $true

$action = {
    Start-Sleep -Seconds 2

    git add .

    if (!(git diff --cached --quiet)) {
        git commit -m "Auto sync $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')"
        git push
    }
}

Register-ObjectEvent $watcher Changed -Action $action
Register-ObjectEvent $watcher Created -Action $action
Register-ObjectEvent $watcher Renamed -Action $action

Write-Host "Watching for changes..."
while ($true) {
    Start-Sleep 1
}
