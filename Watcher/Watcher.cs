using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Permissions;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Net;

namespace Watcher
{
    class Watcher
    {
        private long pos = 0;
        private string diretorio, nomeArquivo;
        public Watcher(string diretorio, string nomeArquivo)
        { 
            this.diretorio = diretorio;
            this.nomeArquivo = nomeArquivo;
            LeArquivoIni();
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = diretorio;
                watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.CreationTime;
                watcher.Changed += Processa;
                watcher.Created += Processa;
                watcher.Filter = nomeArquivo;
                watcher.EnableRaisingEvents = true;
                while (true)
                {
                    /* roda até usuário finalizar */
                }
            }
        }

        private void Processa(object source, FileSystemEventArgs e)
        {
            List<string> buffer = new List<string>();
            try
            {
                using (var fs = new FileStream(this.diretorio + Path.DirectorySeparatorChar + this.nomeArquivo, FileMode.Open, FileAccess.Read, FileShare.None))
                using (var sr = new StreamReader(fs, Encoding.Default))
                {
                    sr.BaseStream.Position = sr.BaseStream.Length < this.pos ? sr.BaseStream.Position : this.pos;
                    while (sr.Peek() > -1)
                        buffer.Add(sr.ReadLine());
                    this.pos = sr.BaseStream.Position;
                    if (buffer.Count != 0)
                        GravaPosIni();       
                }
                foreach (string s in buffer)
                    if (s.Contains("Accepted"))
                        IdentificaIp(s);
            }
            
            catch (Exception ex)
            {
                Gravalog("Erro ao processar alterações no arquivo de log: " + ex.Message);
            }
        }

        public static void IdentificaIp(string login)
        {
            Regex ip = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            MatchCollection result = ip.Matches(login);
            try
            {
                if (result.Count == 0)
                    return;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.ipstack.com/" + result[0].Value + "?access_key=821b5cce4fd0a6f22c3c003539d1549a");
                request.AutomaticDecompression = DecompressionMethods.GZip;
                string ret;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                    ret = reader.ReadToEnd();
                var ipInfo = JsonConvert.DeserializeObject<IpInfo>(ret);
                if (ipInfo.CountryName != "Brazil")
                    Gravalog(login, ipInfo);
            }
            catch (Exception ex)
            {
                Gravalog("Erro ao identificar ip: " + ex.Message);
            }
        }

        private void LeArquivoIni()
        {
            try
            {
                if (!File.Exists(diretorio + Path.DirectorySeparatorChar + nomeArquivo))
                {
                    this.pos = 0;
                    return;
                }
                var ini = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + nomeArquivo + ".ini";
                StreamReader sr = new StreamReader(ini);
                this.pos = Convert.ToInt32(sr.ReadLine().Split('=')[1].Trim());
            }
            catch (FileNotFoundException e)
            {
                this.pos = 0;
                Gravalog("Arquivo ini não encontrado, analisando novos registros.");
            }
        }

        private void GravaPosIni()
        {
            try
            {
                var ini = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + nomeArquivo + ".ini";
                File.WriteAllText(ini, "pos=" + this.pos.ToString());
            }
            catch (Exception e)
            {
                this.pos = 0;
                Gravalog("Erro ao gravar no arquivo ini: " + e.Message);
            }
        }

        private static void Gravalog(string msg, IpInfo ipInfo = null)
        {
            string appPath = Directory.GetCurrentDirectory();
            string arqPath = Path.DirectorySeparatorChar + "logs.log";
            string fullpath = appPath + arqPath;
            string dataHora = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
            StreamWriter sw = new StreamWriter(fullpath, true);
            string info = ipInfo is null ? "" : "Detectado acesso incomum, localização: " + ipInfo.City + ", " + ipInfo.CountryName + "." + Environment.NewLine;
            sw.WriteLine(Environment.NewLine + dataHora + Environment.NewLine + info + msg);
            sw.Close();
        }
    }
}
