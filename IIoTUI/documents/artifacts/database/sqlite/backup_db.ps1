# backup_db.ps1
# Copies the live SQLite DB from AppData into the artifacts folder with a version name.
# Usage: .\documents\artifacts\database\sqlite\backup_db.ps1 -Version 1

param(
    [Parameter(Mandatory=$true)]
    [int]$Version
)

$source = "$env:LOCALAPPDATA\MariVshApp\marivshapp.db3"
$destDir = "$PSScriptRoot"
$destFile = Join-Path $destDir "marivshapp_v$Version.db3"

if (-not (Test-Path $source)) {
    Write-Host "ERROR: DB file not found at: $source"
    Write-Host "Launch the app and navigate to Sign Up page first to create the DB."
    exit 1
}

if (Test-Path $destFile) {
    Write-Host "WARNING: $destFile already exists. Overwrite? (y/n)"
    $confirm = Read-Host
    if ($confirm -ne 'y') { exit 0 }
}

Copy-Item -Path $source -Destination $destFile
Write-Host "Backup saved: $destFile"
