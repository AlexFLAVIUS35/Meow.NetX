while ($true) {
    git add .

    $changes = git diff --cached --name-only

    if ($changes) {
        git commit -m "Auto sync $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')"
        git push
    }

    Start-Sleep -Seconds 10
}
