using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoapLogging.Database
{
    public class ServiceLog
    {
        public long Id { get; set; }
        public string Module { get; set; }
        public string ActionName { get; set; }
        public string OutgoingXml { get; set; }
        public string IncomingXml { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public string HintKey { get; set; }
    }
}