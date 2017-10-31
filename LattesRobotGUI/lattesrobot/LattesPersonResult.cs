using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Drawing;

namespace LattesRobotGUI.lattesrobot
{
    /// <summary>
    /// LattesPersonResult
    /// Model class for results on Person search
    /// @author: Renan Medina
    /// </summary>
    /// <param name="name">Person name ([String])</param>
    /// <param name="education">Education about text ([String])</param>
    /// <param name="rh_code">RH Code (Old lattes id) ([String])</param>
    /// <param name="cnpq_id">CNPQ ID (New lattes id) ([String])</param>
    class LattesPersonResult
    {
        public const String SEARCH_BASE_URL = "http://buscacv.cnpq.br/buscacv/rest/consultasimplificada";
        const String PHOTO_URL_BASE = "http://servicosweb.cnpq.br/wspessoa/servletrecuperafoto?tipo=1&id={0}&bcv=true";
        const String PROFILE_URL_BASE = "http://buscacv.cnpq.br/buscacv/#/espelho?nro_id_cnpq_cp_s={0}";
        const String OLD_PROFILE_URL_BASE = "http://buscatextual.cnpq.br/buscatextual/visualizacv.do?id={0}";

        public LattesPersonResult(String name, String education, String rh_code, String cnpq_id)
        {
            this.Name = name;
            this.Education = education;
            this.RHCode = rh_code;
            this.CNPQId = cnpq_id;
        }

        public String Name { get;set; }
        public String Education { get; set; }
        public String RHCode { get; set; }
        public String CNPQId { get; set; }

        public String getPhotoURL()
        {
            return String.Format(LattesPersonResult.PHOTO_URL_BASE, this.RHCode);
        }

        public Image getPhotoImage()
        {
            try
            {
                WebClient wc = new WebClient();
                byte[] bytes = wc.DownloadData(this.getPhotoURL());
                MemoryStream ms = new MemoryStream(bytes);
                Image img = Image.FromStream(ms);
                return img;
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

        public static String getProfileURL(String cnpq_id)
        {
            return String.Format(LattesPersonResult.PROFILE_URL_BASE, cnpq_id);
        }

        public static String getOldProfileURL(String k_id)
        {
            return String.Format(LattesPersonResult.OLD_PROFILE_URL_BASE, k_id);
        }

        public String toTXTLine()
        {
            return string.Format("{0} {1} {2} \"{3}\"\n", this.CNPQId, this.RHCode, this.Name, this.Education);
        }

        public String toCSVLine()
        {
            return string.Format("{0},{1},{2},\"{3}\"\n", this.CNPQId, this.RHCode, this.Name, this.Education);
        }

    }
}
