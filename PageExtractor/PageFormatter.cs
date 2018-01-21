using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageExtractor
{
    public class PageFormatter
    {
        public string OriginalText { get; set; }

        public string FixCommonOCRErrors()
        {
            var result = new StringBuilder(OriginalText);

            var missedOCR = new Dictionary<string, string>
            {
                { " acceffit", " accessit" },
                { " clarifsime", " clarissime" },
                { " effe ", " esse " },
                { " Eft ", " Est "},
                { " fcriptore ", " scriptore " },
                { " fcriptis ", " scriptis " },
                { " eft ", " est "},
                { " eft,", " est,"},
                { " eft:", " est:"},
                { " fæculum ", " sæculum " },
                { " fe ", " se " },
                { " fed ", " sed " },
                { " fextus ", " sextus " },
                { " fingulare ", " singulare "} ,
                { " fi ", " si "} ,
                { " fi.", " si."} ,
                { " fitus ", " situs " },
                { " fortaffè.", " fortasse."} ,
                { " ftudium ", " studium " },
                { " fub ", " sub " },
                { " fummum,", " summum," },
                { " fuiffè,", " fuisse," },
                { " ipfi ", " ipfi " },
                { " menfes ", " menses "},
                { " menfis ", " mensis "},
                { " noftro", " nostro" },
                { " noftrum ", " nostrum " },
                { " poft ", " post "},
                { " pofteris ", " posteris " },
                { " poftquam ", " postquam " },
                { " poffumus ", " possumus "},
                { " Scd ", " Sed "},
                { "Created with EO.Pdf for .NET trial version. http://www.essentialobjects.com.", "" },
                { "Iosephi Scaligeri ... Opus de emendatione temporum: hac postrema editione, ex auctoris ipsius manuscripto, emendatius, magnaque accessione auctius. Addita veterum Graecorum fragmenta selecta, quibus loci aliquot obscurissimi chronologiae sacrae, & Bibliorum illustrantur: cum notis eiusdem Scaligeri", "" },
                { "\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n", "" }
            };

            foreach(var kvp in missedOCR)
            {
                result = result.Replace(kvp.Key, kvp.Value);
            }

            return result.ToString();
        }

        public string[] Format()
        {
            var result = new List<string>();
            return result.ToArray();
        }
    }
}
