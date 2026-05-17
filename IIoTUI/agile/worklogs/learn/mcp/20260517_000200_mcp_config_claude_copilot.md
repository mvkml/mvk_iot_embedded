# Learn: MCP Configuration — Claude Code & GitHub Copilot

**Date:** 2026-05-17
**Topic:** How to register an MCP server for Claude Code and GitHub Copilot (VS Code)

---

## What We Built First — The MCP Server

```
mvkhr_home/
  mvkapi/fastapi/ucmcpapi/
    mcp_server.py     ← shared FastMCP instance (the "hub")
    mpc_add.py        ← add_two_numbers tool
    mcp_sub.py        ← subtract_two_numbers tool
    mcp_mul.py        ← multiply_two_numbers tool
    mcp_div.py        ← divide_two_numbers tool
    main.py           ← FastAPI app, mounts MCP at /mcp
```

**Running server URL:** `http://127.0.0.1:8000/mcp`

---

## Two Clients — Two Different Config Files

We have two AI tools that can USE this MCP server:

| AI Client | Config File | Location |
|---|---|---|
| Claude Code (CLI) | `.mcp.json` + `settings.local.json` | project root + `.claude/` |
| GitHub Copilot (VS Code) | `.vscode/mcp.json` | `.vscode/` folder |

These are SEPARATE configs for SEPARATE tools. Both point to the same server URL.

---

## Config 1: GitHub Copilot → `.vscode/mcp.json`

**File path:** `mvkhr_home/.vscode/mcp.json`

```json
{
  "servers": {
    "ucmcpapi": {
      "type": "http",
      "url": "http://127.0.0.1:8000/mcp"
    }
  }
}
```

**Key:** `"servers"` (plural) — this is GitHub Copilot's format.

**How it works:**
```
VS Code opens project
  → GitHub Copilot extension reads .vscode/mcp.json
  → connects to http://127.0.0.1:8000/mcp
  → discovers: add, subtract, multiply, divide tools
  → tools appear in Copilot Chat (Agent mode)
```

**How to test:**
1. Open Copilot Chat (Ctrl+Shift+I)
2. Switch to Agent mode (dropdown at bottom)
3. Click tools icon (🔧) — you should see "ucmcpapi"
4. Type: *"Add 10 and 20 using ucmcpapi"*

---

## Config 2: Claude Code → `.mcp.json`

**File path:** `mvkhr_home/.mcp.json`

```json
{
  "mcpServers": {
    "ucmcpapi": {
      "type": "http",
      "url": "http://127.0.0.1:8000/mcp"
    }
  }
}
```

**Key:** `"mcpServers"` (camelCase) — this is Claude Code's format.

**How it works:**
```
Claude Code (CLI) starts in this project folder
  → reads .mcp.json from project root
  → reads enabledMcpjsonServers from .claude/settings.local.json
  → connects to http://127.0.0.1:8000/mcp
  → discovers: add, subtract, multiply, divide tools
  → tools are available directly in your Claude conversation
```

---

## Config 3: Claude Code Approval → `.claude/settings.local.json`

**File path:** `mvkhr_home/.claude/settings.local.json`

```json
{
  "permissions": {
    "allow": [
      "Bash(npm --version)",
      "Bash(ng version *)"
    ]
  },
  "enabledMcpjsonServers": ["ucmcpapi"]
}
```

**Why is this needed?**
Claude Code requires explicit approval before connecting to any MCP server from `.mcp.json`.
Without `"enabledMcpjsonServers": ["ucmcpapi"]`, Claude Code would prompt:
> *"Do you want to allow ucmcpapi MCP server?"* — every single session.

Adding it to `settings.local.json` pre-approves it so it connects automatically.

**Why `settings.local.json` and not `settings.json`?**

| File | Scope | Git | Use for |
|---|---|---|---|
| `.claude/settings.json` | Team (all members) | Committed | Shared team rules |
| `.claude/settings.local.json` | Personal only | NOT committed (gitignored) | Personal approvals, local URLs |

Since `http://127.0.0.1:8000` only works on YOUR machine, it belongs in `settings.local.json`.

---

## Full Picture — All 3 Config Files

```
mvkhr_home/
  │
  ├── .mcp.json                        ← Claude Code: "here is the MCP server"
  │     mcpServers.ucmcpapi.url = http://127.0.0.1:8000/mcp
  │
  ├── .claude/
  │     └── settings.local.json        ← Claude Code: "I approve ucmcpapi"
  │           enabledMcpjsonServers = ["ucmcpapi"]
  │
  └── .vscode/
        └── mcp.json                   ← GitHub Copilot: "here is the MCP server"
              servers.ucmcpapi.url = http://127.0.0.1:8000/mcp
```

---

## Why Two Separate Formats?

```
.mcp.json (Claude Code)          .vscode/mcp.json (GitHub Copilot)
─────────────────────────        ────────────────────────────────
"mcpServers": { }                "servers": { }
  ↑ camelCase, plural              ↑ lowercase, plural

Claude Code format               VS Code extension format
Made by Anthropic                Made by Microsoft/GitHub
```

Both are JSON. Both point to the same URL. But each tool has its own key names.

---

## Testing Checklist

### Before testing — start the server:
```powershell
Set-Location "mvkapi\fastapi\ucmcpapi"
C:\Python314\python.exe -m uvicorn main:app --port 8000 --reload
```
You should see: `INFO: Application startup complete.`

### Test with Claude Code (this tool):
Ask in chat:
> *"Use ucmcpapi to divide 100 by 4"*
> *"Multiply 7 and 8 using the MCP tools"*

### Test with GitHub Copilot (VS Code):
1. Ctrl+Shift+I → Agent mode → tools icon
2. Ask: *"Add 15 and 27 using ucmcpapi"*

---

## What is MCP? (One Paragraph)

**Model Context Protocol (MCP)** is an open standard (by Anthropic) that lets AI assistants call external tools over HTTP. Instead of hardcoding "add two numbers" into the AI model, you expose a tool via an MCP server. The AI discovers the tool, understands its inputs/outputs from the schema, and calls it when needed. This is why the SAME server (`http://127.0.0.1:8000/mcp`) can be used by BOTH Claude Code AND GitHub Copilot — they both speak the MCP protocol.

---

## Summary Table

| What | File | Key field | Who reads it |
|---|---|---|---|
| MCP server definition (Claude) | `.mcp.json` | `mcpServers` | Claude Code CLI |
| MCP server approval (Claude) | `.claude/settings.local.json` | `enabledMcpjsonServers` | Claude Code CLI |
| MCP server definition (Copilot) | `.vscode/mcp.json` | `servers` | GitHub Copilot VS Code |
| Debug/run config | `.vscode/launch.json` | `configurations` | VS Code F5 |
