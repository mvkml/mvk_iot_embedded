# Learn: What is the `.vscode` Folder?

**Date:** 2026-05-17  
**Topic:** VS Code workspace configuration files

---

## What is `.vscode`?

`.vscode` is a **hidden folder** that VS Code automatically reads when you open a workspace/project.  
It stores **project-level settings** that tell VS Code how to behave for *this specific project only*.

Think of it like a "control panel" for VS Code — scoped to your project.

```
mvkhr_home/
  .vscode/
    launch.json   ← how to RUN / DEBUG the app
    mcp.json      ← which MCP servers to connect (for Copilot Agent)
```

---

## File 1: `launch.json` — Debug Configuration

**Purpose:** Tells VS Code how to launch and debug your app with F5.

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": "UC MCP API - Debug",      // label shown in the debug dropdown
      "type": "debugpy",                 // Python debugger
      "request": "launch",              // start a new process (not attach to existing)
      "module": "uvicorn",              // equivalent to: python -m uvicorn
      "args": ["main:app", "--port", "8000"],  // arguments passed to uvicorn
      "justMyCode": true,               // skip stepping into library code
      "cwd": "${workspaceFolder}/mvkapi/fastapi/ucmcpapi",  // working directory
      "envFile": "${workspaceFolder}/mvkapi/fastapi/ucmcpapi/.env",  // loads .env
      "console": "integratedTerminal"   // output goes to VS Code terminal
    }
  ]
}
```

### What happens when you press F5?
```
VS Code reads launch.json
  → sets working dir to ucmcpapi/
  → loads .env file (API keys, config)
  → runs: python -m uvicorn main:app --port 8000
  → attaches Python debugger
  → you can set breakpoints and step through code
```

### Key variables
| Variable | Meaning |
|---|---|
| `${workspaceFolder}` | Root of your project (where `.vscode` lives) |
| `type: debugpy` | Use Python debugger extension |
| `request: launch` | Start fresh (vs `attach` to running process) |
| `justMyCode: true` | Only debug YOUR code, skip library internals |

---

## File 2: `mcp.json` — MCP Server for GitHub Copilot

**Purpose:** Tells GitHub Copilot Agent mode which MCP servers to connect to.

```json
{
  "servers": {
    "ucmcpapi": {               // name you give this server (any label)
      "type": "http",           // transport: http = Streamable HTTP protocol
      "url": "http://127.0.0.1:8000/mcp"  // your running FastAPI MCP server
    }
  }
}
```

### What happens when Copilot Agent reads this?
```
VS Code opens workspace
  → Copilot reads .vscode/mcp.json
  → connects to http://127.0.0.1:8000/mcp
  → discovers tools: add, subtract, multiply, divide
  → makes them available in Copilot Chat (Agent mode)
```

### Transport types
| type | When to use |
|---|---|
| `http` | Streamable HTTP — modern, stateless (what we use) |
| `sse` | Server-Sent Events — older MCP transport |
| `stdio` | Local process launched by VS Code (no server needed) |

---

## Why is `.vscode` a folder, not a single file?

Because different tools write different files into it:

| File | Written by | Purpose |
|---|---|---|
| `launch.json` | You / VS Code | Run & debug configs |
| `mcp.json` | You | MCP server connections for Copilot |
| `settings.json` | VS Code | Workspace-level editor settings |
| `tasks.json` | You / VS Code | Build/run tasks (npm build, etc.) |
| `extensions.json` | You | Recommended extensions for teammates |

---

## Scope: Where settings apply

```
Global (~/.vscode or settings.json)   ← applies to ALL projects
  └── Workspace (.vscode/settings.json) ← applies to THIS project only
        └── Folder (multi-root workspaces) ← applies to one folder in workspace
```

`.vscode/mcp.json` and `.vscode/launch.json` are **workspace-scoped** — they only activate when you open this project.

---

## Summary

| File | One-line purpose |
|---|---|
| `launch.json` | Press F5 → start uvicorn with debugger attached |
| `mcp.json` | Copilot Agent → connect to your MCP tools automatically |

The `.vscode` folder is **safe to commit to git** — it helps teammates get the same debug and tool setup instantly when they clone the repo.
