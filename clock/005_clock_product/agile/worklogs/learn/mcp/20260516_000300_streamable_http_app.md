# Learn Log — What is `mcp_app = mcp.streamable_http_app()`?
**Date:** 2026-05-16
**Topic:** FastMCP — converting MCP server into a mountable ASGI web app
**File:** `mvkapi/fastapi/ucmcpapi/mcp_server.py`

---

## The Line

```python
mcp_app = mcp.streamable_http_app()
```

---

## Two Parts Explained

### `mcp.streamable_http_app()`

`mcp` is a `FastMCP` server — it knows about your tools, but it is **not yet a web app**.
It has no HTTP handling, no routes, nothing a web server can run.

`streamable_http_app()` is a method that **wraps** that MCP server into a **Starlette ASGI web application** that:
- Listens for HTTP POST requests
- Speaks the MCP Streamable HTTP protocol
- Parses incoming MCP messages and routes them to your registered tools

### `mcp_app = ...`

The result is stored in `mcp_app`. It is now a **runnable ASGI app object**.

---

## Why Do We Need This Step?

```
mcp (FastMCP)        →  just your tools + MCP logic (no HTTP)
mcp_app (Starlette)  →  HTTP server that wraps those tools
```

FastAPI is also an ASGI app. So you can **mount** one ASGI app inside another:

```python
app.mount("/mcp", mcp_app)
```

This tells FastAPI: *"any request to `/mcp` — hand it over to `mcp_app` to handle."*

---

## Analogy

| Real World | Code |
|---|---|
| A chef who knows recipes | `mcp` (FastMCP) |
| A restaurant that employs the chef and serves customers | `mcp_app` (Starlette ASGI app) |
| A food court with multiple restaurants | `app` (FastAPI) |
| "Go to stall /mcp for MCP food" | `app.mount("/mcp", mcp_app)` |

The chef alone cannot serve customers. The restaurant wraps the chef and handles the front-of-house. Same idea here.

---

## Key Concepts

| Term | Meaning |
|---|---|
| ASGI | Async Server Gateway Interface — Python standard for async web apps |
| Starlette | Lightweight ASGI framework — FastAPI is built on top of it |
| `streamable_http_app()` | Converts FastMCP into a Starlette ASGI app using Streamable HTTP transport |
| `app.mount()` | FastAPI method to embed one ASGI app inside another at a URL prefix |

---

## Related Logs
- `20260516_000200_fastmcp_server_py.md` — Full mcp_server.py explanation
- `20260516_000100_sse_server_sent_events.md` — SSE vs Streamable HTTP transport
