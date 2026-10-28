using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

static class Program
{
    const int PORT = 47831;
    static byte[] page;
    static long lastTicks = DateTime.UtcNow.Ticks;
    static volatile bool seen = false;

    [STAThread]
    static void Main()
    {
        try
        {
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("WatchParty.html"))
            using (MemoryStream ms = new MemoryStream())
            {
                s.CopyTo(ms);
                page = ms.ToArray();
            }

            string dir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TelaJunta");
            Directory.CreateDirectory(dir);
            string profile = Path.Combine(dir, "perfil");

            // O programa roda em http://localhost (necessario para a Twitch funcionar e para mic/tela liberarem sem avisos)
            string url = "http://localhost:" + PORT + "/";
            HttpListener l = new HttpListener();
            l.Prefixes.Add(url);
            bool serving = true;
            try { l.Start(); } catch (HttpListenerException) { serving = false; } // ja existe outra janela servindo

            Open(url, profile);
            if (!serving) return;

            ThreadPool.QueueUserWorkItem(delegate { Serve(l); });
            // fica vivo enquanto a pagina estiver aberta (ela avisa a cada 5s)
            while (new TimeSpan(DateTime.UtcNow.Ticks - Interlocked.Read(ref lastTicks)).TotalSeconds < (seen ? 300 : 120)) Thread.Sleep(2000);
            try { l.Stop(); } catch { }
        }
        catch (Exception e)
        {
            MessageBox.Show("Não foi possível abrir o WatchParty:\n\n" + e.Message,
                "WatchParty", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    static void Serve(HttpListener l)
    {
        while (l.IsListening)
        {
            try
            {
                HttpListenerContext c = l.GetContext();
                if (c.Request.Url.AbsolutePath == "/ping")
                {
                    Interlocked.Exchange(ref lastTicks, DateTime.UtcNow.Ticks); seen = true;
                    c.Response.StatusCode = 204;
                }
                else
                {
                    c.Response.ContentType = "text/html; charset=utf-8";
                    c.Response.AddHeader("Cache-Control", "no-store");
                    c.Response.ContentLength64 = page.Length;
                    c.Response.OutputStream.Write(page, 0, page.Length);
                }
                c.Response.Close();
            }
            catch { }
        }
    }

    static void Open(string url, string profile)
    {
        string args = "--app=\"" + url + "\" --user-data-dir=\"" + profile + "\" --no-first-run --disable-renderer-backgrounding --disable-backgrounding-occluded-windows --disable-features=CalculateNativeWinOcclusion --autoplay-policy=no-user-gesture-required --disable-background-mode";

        string pf = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        string pf86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        string[] browsers = new string[] {
            Path.Combine(pf,   @"Google\Chrome\Application\chrome.exe"),
            Path.Combine(pf86, @"Google\Chrome\Application\chrome.exe"),
            Path.Combine(local, @"Google\Chrome\Application\chrome.exe"),
            Path.Combine(pf86, @"Microsoft\Edge\Application\msedge.exe"),
            Path.Combine(pf,   @"Microsoft\Edge\Application\msedge.exe")
        };
        foreach (string b in browsers)
        {
            if (File.Exists(b)) { Process.Start(b, args); return; }
        }
        Process.Start(url); // sem Chrome nem Edge: navegador padrao
    }
}
