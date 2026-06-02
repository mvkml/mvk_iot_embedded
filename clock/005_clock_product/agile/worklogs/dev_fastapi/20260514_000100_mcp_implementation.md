# 🚀 Dev FastAPI Agent — Work Log
## Date: 2026-05-14
## Time: 00:01:00
## Subject: MCP Server + OpenAI Agent — Initial Implementation

### What Was Done
- Created `ucmcpapi` FastAPI project under `mvkapi/fastapi/ucmcpapi/`
- Implemented two Python files in one FastAPI app:
  - `mcp_server.py` — MCP server with SSE transport, exposes `add_two_numbers` tool
  - `calculation_api.py` — Agent endpoint using Azure OpenAI with function calling
- Created `main.py` to wire both routers into one app
- Created `requirements.txt` and `.env` / `.env.example`
- Created `.vscode/launch.json` at workspace root for VS Code debugging

### Architecture
```
POST /calculate/add {a, b}
  → calculation_api.py
      → Azure OpenAI (gpt-4o-mini) with tool definition
          → calls add_two_numbers tool
              → mcp_server.py executes and returns result
          → OpenAI composes final answer
  → response with tool trace + answer
```

### Issues Encountered & Solutions

| # | Issue | Cause | Solution |
|---|-------|-------|----------|
| 1 | Used `AsyncOpenAI` initially | Assumed regular OpenAI | Switched to `AsyncAzureOpenAI` with `azure_endpoint`, `api_version`, `deployment` |
| 2 | Real API key placed in `.env.example` | User added key to wrong file | Created proper `.env` (gitignored), reset `.env.example` to placeholder |
| 3 | Port 8000 already in use (WinError 10048) | Background server from earlier test still running (PID 3024) | Killed process via PowerShell: `Stop-Process -Id 3024 -Force` |
| 4 | `launch.json` not picked up by VS Code | File placed inside `ucmcpapi/.vscode/` instead of workspace root `.vscode/` | Moved `launch.json` to `mvkhr_home/.vscode/launch.json` |
| 5 | No `.gitignore` at root | Project never had one | Created `.gitignore` blocking `.env`, `__pycache__`, `node_modules`, etc. |

### Key Learnings
- Azure OpenAI uses **deployment name** in the `model=` field, not model name
- `.env` must be gitignored — `.env.example` is the safe-to-commit template
- VS Code `launch.json` must live at the **workspace root** `.vscode/` to be detected
- Port conflicts on Windows: use `netstat -ano | findstr :<port>` then `Stop-Process -Id <PID> -Force`
- MCP tool logic lives in `mcp_server.py` — `calculation_api.py` delegates to it, not the other way

### Pending / Next Steps
- Split `mcp_server.py` and `calculation_api.py` into separate services on different ports
- Add more MCP tools (subtract, multiply, etc.) as the next learning step
- Connect MCP tools to real HR data (employee lookup, document search)
