using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;
using System.Threading;
using System.ComponentModel;
using System.IO;

namespace lattesrobot
{
    /// <summary>
    /// LattesRobot
    /// Robot class that download files from given ID's
    /// @author: Renan Medina
    /// </summary>
    /// <param name="name">worker ([BackgroundWorker])</param>
    public class LattesRobot
    {
        const String DOWNLOAD_URL = "http://buscacv.cnpq.br/buscacv/rest/download/curriculo/";
        const String BASE_URL = "http://buscacv.cnpq.br/buscacv/rest/espelhocurriculo/";
        const String DOWNLOAD_ID_REGEX = "(K[a-zA-Z0-9]{9})";

        private List<String> _ids = new List<String>();
        private List<String> _downloadeds = new List<String>();
        private List<String> _errors = new List<String>();

        private LattesFileMeta _cur_zip;

        private Char separator = ',';
        private String _idstxt;
        private WebClient _client = new WebClient();
        private String _cur_id = null;
        private int _idcount = 0;
        private BackgroundWorker _bgworker;
        private Boolean _stopped = false;

        public LattesRobot(BackgroundWorker worker)
        {
            this._client.Proxy = null;
            this._bgworker = worker;
        }

        public void StopDownloading()
        {
            this._stopped = true;
        }

        public Boolean isStopped{
            get{
                return this._stopped;
            }

            set{
              this._stopped = value;
            }
        }

        public void continueDownloads()
        {
            this.isStopped = false;
            this.processNext();
        }

        public void resetRobot()
        {
            this.isStopped = false;
            this._downloadeds.Clear();
            this._idcount = 0;
        }

        public String OutputFolder{ get; set;}
        public String SeparatorString { get;set; } = ","; // default value for property is ','
        public Boolean OutputXML{ get; set;}

        public List<String> parseIds(String txt_ids)
        {
            return Regex.Split(txt_ids, this.SeparatorString).Distinct<String>().ToList<String>();
        }

        public List<String> parseIds(String txt_ids, String sep)
        {
            return Regex.Split(txt_ids, sep).Distinct<String>().ToList<String>();
        }

        public void startFromText(String txt)
        {
           this._ids = this.parseIds(txt);
           this.processNext();
        }

        public void startFromFile(String file_path)
        {
            using(StreamReader reader = new StreamReader(file_path))
            {
                this.startFromText(reader.ReadToEnd());
            }
        }

        public void startFromList(List<String> ids)
        {
            this._ids = ids;
            this.processNext();
        }

        void processNext()
        {
            if (this._idcount > this._ids.Count - 1 || this._stopped)
                return;

            this._cur_id = this._ids[this._idcount];
            this._client = new WebClient();
            
            try
            {
               String d_id = this.getDownloadID();
               if (this.downloadCurriculumZip(d_id)){
                    if (this.OutputXML)
                        this.outputAsXML();
                   this._downloadeds.Add(this._cur_zip.CurriculumID);
                }
               // report downloaded successfully
               //this._bgworker.ReportProgress(0, this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                this._idcount = this._idcount + 1;
                float c = (float) this._downloadeds.Count;
                float ct = (float)this._ids.Count;
                float perc = (c / ct) * 100;
                this._bgworker.ReportProgress((int) perc, this);
                this.processNext();
            }
        }

        public int getDownloadedCount()
        {
            return this._downloadeds.Count;
        }

        public int getCurrentIDCount()
        {
            return this._idcount;
        }

        public String getCurrentID()
        {
            return this._cur_id;
        }

        public String getNextID()
        {
            return this._ids[this._idcount];
        }

        String getDownloadID()
        {
            String result = this._client.DownloadString(String.Format(LattesRobot.BASE_URL + "{0}", this._cur_id));
            Match matches = Regex.Match(result, LattesRobot.DOWNLOAD_ID_REGEX);
            return (String) matches.Value;
        }

        Boolean downloadCurriculumZip(String did)
        {
            try
            {
                // create the URL to download the zip file from Lattes Server
                String d_url = String.Format(LattesRobot.DOWNLOAD_URL + "{0}", did);
                // create a Meta object to be used later if needed
                this._cur_zip = new LattesFileMeta(this.OutputFolder, this._cur_id, did);
                // downloads the file from Lattes server and save on the output folder
                this._client.DownloadFile(d_url, this._cur_zip.OutputPath);
                // dispose the current connection
                this._client.Dispose();
                return true;
            }
            catch(WebException e){
                Console.WriteLine(e.ToString());
                this._errors.Add(this._cur_id);
                return false;
            }
        }

        void outputAsXML()
        {
            System.IO.Compression.ZipFile.ExtractToDirectory(this._cur_zip.OutputPath, this.OutputFolder);
            // check if extraction worked properly
            if (File.Exists(this._cur_zip.DefaultUnzipPath)){
                // rename the file "curriculo.xml" => "{lattesid}.xml" (8314328143251742.xml)
                File.Move(this._cur_zip.DefaultUnzipPath, this._cur_zip.XMLOutputPath);
                // delete the zip file "{lattesid}.zip" (8314328143251742.zip)
                File.Delete(this._cur_zip.OutputPath);
            }
                
        }
    }

    class LattesFileMeta
    {
        public LattesFileMeta(String out_folder, String id, String d_id)
        {
            this.CurriculumID = id;
            this.DownloadID = d_id;
            this.OutputPath = String.Format("{0}\\{1}.zip", out_folder, id);
            this.XMLOutputPath = String.Format("{0}\\{1}.xml", out_folder, id);
            this.DefaultUnzipPath = String.Format("{0}\\curriculo.xml", out_folder);
        }

        public String XMLOutputPath{ get; set; }
        public String DefaultUnzipPath{ get; set; }
        public String OutputPath { get; set; }
        public String CurriculumID{ get; set;}
        public String DownloadID{ get; set;}
    }
}
