# Learn Log — FastMCP: mcp_server.py Explained
**Date:** 2026-05-16
**Topic:** MCP Server — FastMCP, Streamable HTTP, Tool Registration
**File:** `mvkapi/fastapi/ucmcpapi/mcp_server.py`

---

## Full File

```python
from mcp.server.fastmcp import FastMCP

mcp = FastMCP("ucmcpapi-mcp", stateless_http=True, streamable_http_path="/")

@mcp.tool()
def add_two_numbers(a: float, b: float) -> float:
    """Adds two numbers and returns their sum."""
    return a + b

mcp_app = mcp.streamable_http_app()
```

---

## Line-by-Line Explanation

### Line 1 — Import
```python
from mcp.server.fastmcp import FastMCP
```
- `FastMCP` is a high-level class from the MCP SDK.
- It hides all low-level protocol wiring so you only write business logic.
- Package: `mcp` (installed via pip, version >= 1.0.0).

---

### Line 3 — Create the MCP Server
```python
mcp = FastMCP("ucmcpapi-mcp", stateless_http=True, streamable_http_path="/")
```

| Argument | Value | What it does |
|---|---|---|
| Server name | `"ucmcpapi-mcp"` | Name shown when an AI agent connects and asks "who are you?" |
| `stateless_http` | `True` | Each request is independent — no session kept between calls. Simpler and scales better. |
| `streamable_http_path` | `"/"` | Internal route inside this app. When mounted at `/mcp` in FastAPI, final URL = `http://localhost:8000/mcp` |

**Why `streamable_http_path="/"`?**
- Default internal path is `/mcp`.
- If we mounted at `/mcp` in FastAPI with default, the URL would become `/mcp/mcp` (double nesting).
- Setting it to `/` makes the final URL cleanly `/mcp`.

---

### Lines 6-8 — Register a Tool
```python
@mcp.tool()
def add_two_numbers(a: float, b: float) -> float:
    """Adds two numbers and returns their sum."""
    return a + b
```

| Part | Purpose |
|---|---|
| `@mcp.tool()` | Decorator — tells MCP to expose this function as a callable tool |
| Docstring | Becomes the tool description — the AI agent reads it to decide when to call this tool |
| Type hints (`float`) | Become the tool's input/output schema — MCP validates requests automatically |

- You do NOT write any HTTP code, routing, or validation manually.
- The MCP SDK handles everything from the decorator alone.

---

### Line 10 — Create the ASGI App
```python
mcp_app = mcp.streamable_http_app()
```
- Converts the `FastMCP` server into a **Starlette ASGI web app**.
- Uses the **Streamable HTTP transport** (modern MCP standard).
- This `mcp_app` object is mounted into FastAPI in `main.py` via `app.mount("/mcp", mcp_app)`.

---

## The Big Picture — How Pieces Connect

```
AI Agent (Claude, GPT, etc.)
    │
    │  POST http://localhost:8000/mcp
    ▼
FastAPI  (main.py)
    │  app.mount("/mcp", mcp_app)
    ▼
mcp_app  ←── mcp.streamable_http_app()
    │
    ▼
FastMCP  (mcp_server.py)
    │  @mcp.tool()
    ▼
add_two_numbers(a, b) → result sent back to AI agent
```

The AI agent sends a standard HTTP POST. The MCP layer parses the protocol, then calls your Python function. The result travels back through the same HTTP response.

---

## Key Concepts Summary

| Concept | Explanation |
|---|---|
| `FastMCP` | High-level MCP server class — decorator-based, no protocol code needed |
| `@mcp.tool()` | Registers a Python function as an MCP tool discoverable by AI agents |
| `stateless_http=True` | No session state — each request is self-contained |
| `streamable_http_path` | Internal URL path inside the Starlette app |
| `streamable_http_app()` | Returns an ASGI app object for mounting into FastAPI |
| Streamable HTTP | Modern MCP transport — single HTTP endpoint, replaces old SSE transport |

---

## Related Logs
- `20260516_000100_sse_server_sent_events.md` — SSE vs Streamable HTTP transport explained
- `main.py` — FastAPI lifespan wires `session_manager.run()` so the MCP server starts correctly
