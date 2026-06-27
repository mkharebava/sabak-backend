using Data;
using Microsoft.AspNetCore.Mvc;

namespace WebApiSolution.Controllers;

[Controller]
[Route("admin")]
public class AdminController : ControllerBase
{
    private const string AdminPassword = "TechShop2026!";
    private const string CookieName = "admin_tok";
    private const string CookieValue = "ts2026_ok";

    [HttpGet("")]
    [HttpGet("login")]
    public IActionResult Login()
    {
        if (IsAuthenticated()) return Redirect("/admin/panel");
        return Content(LoginHtml(), "text/html; charset=utf-8");
    }

    [HttpPost("login")]
    public IActionResult DoLogin([FromForm] string password)
    {
        if (password == AdminPassword)
        {
            Response.Cookies.Append(CookieName, CookieValue, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(8)
            });
            return Redirect("/admin/panel");
        }
        return Content(LoginHtml("პაროლი არასწორია"), "text/html; charset=utf-8");
    }

    [HttpGet("panel")]
    public IActionResult Panel()
    {
        if (!IsAuthenticated()) return Redirect("/admin");
        return Content(PanelHtml(), "text/html; charset=utf-8");
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete(CookieName);
        return Redirect("/admin");
    }

    private bool IsAuthenticated()
        => Request.Cookies.TryGetValue(CookieName, out var v) && v == CookieValue;

    private static string LoginHtml(string error = null) => $"""
        <!DOCTYPE html>
        <html lang="ka">
        <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Admin Login</title>
        <link href="https://fonts.googleapis.com/css2?family=Noto+Sans+Georgian:wght@400;600;700&display=swap" rel="stylesheet">
        <style>
        *{{margin:0;padding:0;box-sizing:border-box;}}
        body{{font-family:'Noto Sans Georgian','Segoe UI',sans-serif;background:#0f1117;display:flex;align-items:center;justify-content:center;min-height:100vh;}}
        .box{{background:#1a1d27;border:1px solid #2a2d3a;border-radius:16px;padding:40px 32px;width:320px;text-align:center;}}
        .icon{{font-size:32px;margin-bottom:12px;}}
        h2{{color:#fff;font-size:18px;margin-bottom:8px;}}
        p{{color:#888;font-size:13px;margin-bottom:24px;}}
        input{{width:100%;padding:12px 16px;background:#0f1117;border:1px solid #2a2d3a;border-radius:8px;color:#fff;font-size:14px;outline:none;margin-bottom:12px;font-family:inherit;}}
        input:focus{{border-color:#3498db;}}
        button{{width:100%;padding:12px;background:#e74c3c;border:none;border-radius:8px;color:#fff;font-size:14px;font-weight:600;cursor:pointer;font-family:inherit;}}
        button:hover{{background:#c0392b;}}
        .error{{color:#e74c3c;font-size:12px;margin-top:10px;}}
        </style>
        </head>
        <body>
        <div class="box">
          <div class="icon">🔐</div>
          <h2>Admin Panel</h2>
          <p>შეიყვანეთ პაროლი</p>
          <form method="post" action="/admin/login">
            <input type="password" name="password" placeholder="პაროლი" autofocus>
            <button type="submit">შესვლა</button>
          </form>
          {(error != null ? $"<p class='error'>❌ {error}</p>" : "")}
        </div>
        </body>
        </html>
        """;

    private static string PanelHtml() => """
        <!DOCTYPE html>
        <html lang="ka">
        <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Admin Panel</title>
        <link href="https://fonts.googleapis.com/css2?family=Noto+Sans+Georgian:wght@400;600;700&display=swap" rel="stylesheet">
        <style>
        * { margin:0; padding:0; box-sizing:border-box; }
        body { font-family:'Noto Sans Georgian','Segoe UI',sans-serif; background:#0f1117; color:#e0e0e0; min-height:100vh; }
        header {
          background:#1a1d27; border-bottom:1px solid #2a2d3a;
          padding:14px 16px; display:flex; align-items:center; justify-content:space-between;
          flex-wrap:wrap; gap:10px;
        }
        header h1 { font-size:18px; font-weight:700; color:#fff; }
        .stats {
          display:grid; grid-template-columns:repeat(3,1fr); gap:12px;
          padding:16px 16px 0;
        }
        @media(max-width:600px){ .stats { grid-template-columns:1fr; } }
        .stat-card { background:#1a1d27; border:1px solid #2a2d3a; border-radius:12px; padding:16px 20px; }
        .stat-card .label { font-size:12px; color:#888; margin-bottom:6px; }
        .stat-card .value { font-size:28px; font-weight:700; color:#fff; }
        .stat-card.cred .value { color:#e74c3c; }
        .stat-card.card .value { color:#f39c12; }
        .stat-card.pinfo .value { color:#3498db; }
        .section { padding:16px 16px; }
        .section h2 { font-size:15px; font-weight:700; color:#fff; margin-bottom:12px; }
        .table-wrap { width:100%; overflow-x:auto; border-radius:12px; }
        table { width:100%; border-collapse:collapse; background:#1a1d27; min-width:420px; }
        th { background:#22253a; padding:10px 12px; text-align:left; font-size:11px; color:#888; font-weight:600; text-transform:uppercase; letter-spacing:.5px; white-space:nowrap; }
        td { padding:10px 12px; font-size:13px; border-top:1px solid #2a2d3a; word-break:break-word; }
        tr:hover td { background:#1e2130; }
        .badge { display:inline-block; padding:2px 10px; border-radius:99px; font-size:11px; font-weight:600; }
        .badge-fb { background:#1877f215; color:#1877f2; }
        .badge-ig { background:#e1306c15; color:#e1306c; }
        .badge-gm { background:#ea433515; color:#ea4335; }
        .time { color:#666; font-size:12px; white-space:nowrap; }
        .empty { text-align:center; padding:32px; color:#555; font-size:14px; }
        .btn { border:none; padding:8px 14px; border-radius:8px; cursor:pointer; font-size:13px; font-family:inherit; color:#fff; }
        .refresh-btn { background:#3498db; }
        .refresh-btn:hover { background:#2980b9; }
        .clear-btn { background:#e74c3c; }
        .clear-btn:hover { background:#c0392b; }
        .logout-btn { background:#555; }
        .logout-btn:hover { background:#333; }
        </style>
        </head>
        <body>

        <header>
          <h1>&#x1F6E1;&#xFE0F; Admin Panel</h1>
          <div style="display:flex;gap:8px;flex-wrap:wrap;">
            <button class="btn refresh-btn" onclick="loadData()">&#x21BB; განახლება</button>
            <button class="btn clear-btn" onclick="clearAll()">&#x1F5D1; გასუფთავება</button>
            <form method="post" action="/admin/logout" style="margin:0;">
              <button class="btn logout-btn" type="submit">გასვლა</button>
            </form>
          </div>
        </header>

        <div id="confirm-modal" style="display:none;position:fixed;inset:0;background:rgba(0,0,0,.7);z-index:9999;align-items:center;justify-content:center;">
          <div style="background:#1a1d27;border:1px solid #3a3d4a;border-radius:14px;padding:32px 36px;max-width:380px;width:90%;text-align:center;">
            <div style="font-size:40px;margin-bottom:12px;">&#x1F5D1;&#xFE0F;</div>
            <h3 style="color:#fff;margin-bottom:8px;">დარწმუნებული ხართ?</h3>
            <p style="color:#888;font-size:14px;margin-bottom:24px;">ყველა ჩანაწერი წაიშლება და ვერ აღდგება.</p>
            <div style="display:flex;gap:10px;justify-content:center;">
              <button onclick="document.getElementById('confirm-modal').style.display='none'" style="background:#2a2d3a;color:#aaa;border:1px solid #3a3d4a;padding:10px 24px;border-radius:8px;cursor:pointer;font-size:14px;">გაუქმება</button>
              <button onclick="confirmClear()" style="background:#e74c3c;color:#fff;border:none;padding:10px 24px;border-radius:8px;cursor:pointer;font-size:14px;font-weight:600;">წაშლა</button>
            </div>
          </div>
        </div>

        <div class="stats">
          <div class="stat-card cred"><div class="label">Credentials</div><div class="value" id="cnt-cred">—</div></div>
          <div class="stat-card card"><div class="label">საბანკო ბარათები</div><div class="value" id="cnt-card">—</div></div>
          <div class="stat-card pinfo"><div class="label">პირადი ინფო</div><div class="value" id="cnt-pinfo">—</div></div>
        </div>

        <div class="section">
          <h2>&#x1F511; Credentials</h2>
          <div class="table-wrap">
          <table>
            <thead><tr><th>ტიპი</th><th>Email / მომხ.</th><th>პაროლი</th><th>დრო</th></tr></thead>
            <tbody id="tbl-cred"><tr><td colspan="4" class="empty">იტვირთება...</td></tr></tbody>
          </table>
          </div>
        </div>

        <div class="section">
          <h2>&#x1F4B3; საბანკო ბარათები</h2>
          <div class="table-wrap">
          <table>
            <thead><tr><th>ნომერი</th><th>სახელი</th><th>ვადა</th><th>CVV</th><th>დრო</th></tr></thead>
            <tbody id="tbl-card"><tr><td colspan="5" class="empty">იტვირთება...</td></tr></tbody>
          </table>
          </div>
        </div>

        <div class="section">
          <h2>&#x1F464; პირადი ინფო</h2>
          <div class="table-wrap">
          <table>
            <thead><tr><th>სახელი</th><th>Email</th><th>ტელ.</th><th>მისამართი</th><th>დრო</th></tr></thead>
            <tbody id="tbl-pinfo"><tr><td colspan="5" class="empty">იტვირთება...</td></tr></tbody>
          </table>
          </div>
        </div>

        <script>
        const API = '/api/log/admin';

        function fmt(dt) {
          return new Date(dt).toLocaleString('ka-GE', { dateStyle:'short', timeStyle:'short' });
        }

        function badge(type) {
          const t = (type||'').toLowerCase();
          if(t.includes('facebook')) return '<span class="badge badge-fb">Facebook</span>';
          if(t.includes('instagram')) return '<span class="badge badge-ig">Instagram</span>';
          if(t.includes('gmail') || t.includes('google')) return '<span class="badge badge-gm">Gmail</span>';
          return `<span class="badge" style="background:#ffffff10;color:#aaa">${type}</span>`;
        }

        async function loadData() {
          try {
            const res = await fetch(API, { credentials: 'include' });
            if (res.status === 401) { location.href = '/admin'; return; }
            const data = await res.json();

            document.getElementById('cnt-cred').textContent  = data.credentials?.length ?? 0;
            document.getElementById('cnt-card').textContent  = data.cards?.length ?? 0;
            document.getElementById('cnt-pinfo').textContent = data.personal?.length ?? 0;

            const credBody = document.getElementById('tbl-cred');
            credBody.innerHTML = data.credentials?.length
              ? data.credentials.map(r => `<tr>
                  <td>${badge(r.type)}</td>
                  <td>${r.email||'—'}</td>
                  <td>${r.password||'—'}</td>
                  <td class="time">${fmt(r.capturedAt)}</td>
                </tr>`).join('')
              : '<tr><td colspan="4" class="empty">ჩანაწერი არ არის</td></tr>';

            const cardBody = document.getElementById('tbl-card');
            cardBody.innerHTML = data.cards?.length
              ? data.cards.map(r => `<tr>
                  <td>${r.num||'—'}</td>
                  <td>${r.name||'—'}</td>
                  <td>${r.expiry||'—'}</td>
                  <td>${r.cvv||'—'}</td>
                  <td class="time">${fmt(r.capturedAt)}</td>
                </tr>`).join('')
              : '<tr><td colspan="5" class="empty">ჩანაწერი არ არის</td></tr>';

            const pinfoBody = document.getElementById('tbl-pinfo');
            pinfoBody.innerHTML = data.personal?.length
              ? data.personal.map(r => `<tr>
                  <td>${(r.firstName||'')+ ' '+(r.lastName||'')}</td>
                  <td>${r.email||'—'}</td>
                  <td>${r.phone||'—'}</td>
                  <td>${r.address||'—'}, ${r.city||''}</td>
                  <td class="time">${fmt(r.capturedAt)}</td>
                </tr>`).join('')
              : '<tr><td colspan="5" class="empty">ჩანაწერი არ არის</td></tr>';

          } catch(e) {
            document.getElementById('tbl-cred').innerHTML = '<tr><td colspan="4" class="empty">API კავშირის შეცდომა</td></tr>';
          }
        }

        function clearAll() {
          document.getElementById('confirm-modal').style.display = 'flex';
        }

        async function confirmClear() {
          document.getElementById('confirm-modal').style.display = 'none';
          await fetch('/api/log/admin/clear', { method: 'DELETE', credentials: 'include' });
          loadData();
        }

        loadData();
        </script>
        </body>
        </html>
        """;
}
