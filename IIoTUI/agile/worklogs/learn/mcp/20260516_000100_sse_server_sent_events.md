# Learn — MCP: SSE (Server-Sent Events)
## Date: 2026-05-16
## Time: 00:01:00
## Topic: What is SSE in the context of MCP

---

## Definition

**SSE = Server-Sent Events**

A one-way persistent HTTP connection where the server streams events to the client.
The connection opens and stays open — unlike REST which closes after each response.

---

## How SSE Works in MCP

```
Client (MCP Agent)          Server (MCP Server)
        |                           |
        |--- GET /mcp/sse --------->|  connection opens, stays open
        |                           |
        |--- POST /mcp/messages --->|  client sends tool call
        |                           |
        |<-- event: tool result ----|  server pushes response back
        |                           |
        |<-- event: ... ------------|  server can keep pushing
        |                           |
```

---

## SSE vs REST vs WebSocket

| | REST | SSE | WebSocket |
|--|------|-----|-----------|
| Connection | Opens → closes | Opens → **stays open** | Opens → stays open |
| Direction | Request → Response | Server → Client (one-way push) | Bidirectional |
| Client sends | Each request | Via separate POST | Over same connection |
| MCP uses it? | No | **Yes** | No |

---

## Why MCP Uses SSE

- REST closes after each response — MCP needs a **persistent session**
- SSE is simpler than WebSocket — server pushes, client sends via separate POST
- Two endpoints work together:
  - `GET /mcp/sse` — holds the connection open (server → client stream)
  - `POST /mcp/messages` — client sends tool calls to server

---

## Code Reference

```python
# mcp_server.py

@router.get("/sse")           # SSE connection — stays open
async def handle_sse(...):
    async with sse_transport.connect_sse(...):
        await mcp.run(...)    # blocks here, keeps connection alive

@router.post("/messages")     # client sends tool calls here
async def handle_messages(...):
```

---

## Key Takeaway

`GET /mcp/sse` is NOT a REST endpoint.
It is a persistent SSE tunnel — the connection stays open for the duration of the MCP session.

---

## Related Topics
- `learn/mcp/` — MCP protocol overview
- `learn/fastapi/` — FastAPI routing and routers
