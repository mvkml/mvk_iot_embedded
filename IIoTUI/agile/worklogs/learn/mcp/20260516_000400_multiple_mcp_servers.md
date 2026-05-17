# Learn Log — Multiple mcp_server.py Files: Do Each Need `mcp_app`?
**Date:** 2026-05-16
**Topic:** FastMCP — one vs multiple MCP server instances across files
**File:** `mvkapi/fastapi/ucmcpapi/`

---

## Question
If we have multiple `mcp_server.py` files, does each file need `mcp_app = mcp.streamable_http_app()`?

**Answer: It depends on whether you want one shared MCP server or multiple independent servers.**

---

## Option 1 — One MCP Server, Tools Split Across Multiple Files

All tool files share the **same `mcp` instance**. Only **one** `mcp_app` is needed.

### `mcp_server.py` — central server
```python
from mcp.server.fastmcp import FastMCP

mcp = FastMCP("ucmcpapi-mcp", stateless_http=True, streamable_http_path="/")
mcp_app = mcp.streamable_http_app()   # ← only here, once
```

### `math_tools.py` — imports shared `mcp`, registers tools
```python
from mcp_server import mcp            # ← import, not create

@mcp.tool()
def add_two_numbers(a: float, b: float) -> float:
    """Adds two numbers."""
    return a + b
```

### `hr_tools.py` — same pattern
```python
from mcp_server import mcp

@mcp.tool()
def get_employee(id: int) -> dict:
    """Gets employee by ID."""
    return {"id": id, "name": "John"}
```

### `main.py` — import tool files to trigger registration
```python
import mcp_server
import math_tools   # ← importing this file registers its tools onto mcp
import hr_tools

app.mount("/mcp", mcp_server.mcp_app)
```

**Result:** All tools appear on one endpoint — `POST /mcp`

---

## Option 2 — Multiple Independent MCP Servers

Each file is its own server with its own `mcp` instance and its own `mcp_app`.

### `math_server.py`
```python
from mcp.server.fastmcp import FastMCP

mcp = FastMCP("math-mcp", stateless_http=True, streamable_http_path="/")
mcp_app = mcp.streamable_http_app()   # ← own mcp_app

@mcp.tool()
def add_two_numbers(a: float, b: float) -> float:
    """Adds two numbers."""
    return a + b
```

### `hr_server.py`
```python
from mcp.server.fastmcp import FastMCP

mcp = FastMCP("hr-mcp", stateless_http=True, streamable_http_path="/")
mcp_app = mcp.streamable_http_app()   # ← own mcp_app

@mcp.tool()
def get_employee(id: int) -> dict:
    """Gets employee by ID."""
    return {"id": id, "name": "John"}
```

### `main.py` — mounted at different paths
```python
import math_server
import hr_server

app.mount("/mcp/math", math_server.mcp_app)
app.mount("/mcp/hr",   hr_server.mcp_app)
```

**Result:** Two separate endpoints — `POST /mcp/math` and `POST /mcp/hr`

---

## Comparison

| | Option 1 (Shared) | Option 2 (Independent) |
|---|---|---|
| `mcp` instances | 1 | 1 per file |
| `mcp_app` needed | 1 (in main server file only) | 1 per file |
| Mount point | Single `/mcp` | One per server |
| Tool discovery | All tools in one place | Tools per server |
| Use when | Tools are logically one service | Servers are truly separate domains |

---

## Recommendation for `ucmcpapi`

**Option 1** is the right pattern.
- `ucmcpapi` is one MCP service.
- Tools can be organized in multiple files (`math_tools.py`, `hr_tools.py`, etc.).
- All share the single `mcp` instance from `mcp_server.py`.
- Only one `mcp_app` is created and mounted at `/mcp`.

---

## Key Rule
```
One FastMCP instance  →  one mcp_app  →  one mounted endpoint
```
Tool files just **import** the shared `mcp` and use `@mcp.tool()` — they do NOT create their own `mcp_app`.

---

## Related Logs
- `20260516_000200_fastmcp_server_py.md` — Full mcp_server.py explanation
- `20260516_000300_streamable_http_app.md` — What is `mcp.streamable_http_app()`?
