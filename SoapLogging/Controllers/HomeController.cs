using SoapLogging.CalculatorServiceRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoapLogging.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ContentResult Index()
        {
            //Web servisi çağrısı yaparak gönderilen ve alınan verilerin loglamasını görebiliriz.
            var client = new CalculatorSoapClient();
            var result = client.Add(6, 3);

            return Content(result.ToString());
        }
    }
}