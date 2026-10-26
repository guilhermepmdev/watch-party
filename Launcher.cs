using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

static class Program
{
    [STAThread]
    static void Main()
    {
        try
        {
            string dir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TelaJunta");
            Directory.CreateDirectory(dir);

            // O programa (HTML) vai embutido dentro do .exe; aqui ele é extraído a cada abertura
            string html = Path.Combine(dir, "WatchParty.html");
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("WatchParty.html"))
            using (FileStream f = File.Create(html))
            {
                s.CopyTo(f);
            }

            string url = new Uri(html).AbsoluteUri;
            string profile = Path.Combine(dir, "perfil");
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
                if (File.Exists(b))
                {
                    Process.Start(b, args);
                    return;
                }
            }

            // Sem Chrome nem Edge: abre no navegador padrão
            Process.Start(html);
        }
        catch (Exception e)
        {
            MessageBox.Show("Não foi possível abrir o WatchParty:\n\n" + e.Message,
                "WatchParty", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
