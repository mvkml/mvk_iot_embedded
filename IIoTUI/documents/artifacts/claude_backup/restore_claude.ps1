# restore_claude.ps1
# Restores Claude Code context from a versioned backup folder.
#
# Usage:
#   .\restore_claude.ps1 -Version 1
#
# What it does:
#   Copies contents of claude_backup_v<N>\ to C:\Users\<you>\.claude\
#   Then renames the project memory folder to match the current project path.

param(
    [Parameter(Mandatory=$true)]
    [int]$Version
)

$ScriptDir   = Split-Path -Parent $MyInvocation.MyCommand.Path
$BackupDir   = Join-Path $ScriptDir "claude_backup_v$Version"
$ClaudeDir   = "$env:USERPROFILE\.claude"

if (-not (Test-Path $BackupDir)) {
    Write-Error "Backup folder not found: $BackupDir"
    exit 1
}

Write-Host "Restoring from: $BackupDir"
Write-Host "Target        : $ClaudeDir"
Write-Host ""

# Copy backup into .claude (overwrite)
Copy-Item "$BackupDir\*" $ClaudeDir -Recurse -Force

Write-Host "Done. Claude context restored from v$Version."
Write-Host ""
Write-Host "IMPORTANT — if your project folder path changed:"
Write-Host "  1. Open: $ClaudeDir\projects\"
Write-Host "  2. Find the old encoded folder name (e.g. c--v-v-...IIoTUI)"
Write-Host "  3. Rename it to match your NEW project path encoding"
Write-Host "  4. Then open Claude Code from your new project folder"
