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
            //You can catch this client behaviors (outgoing/incoming xml) in SoapBehavior.cs
            var client = new CalculatorSoapClient();
            int a = 6;
            int b = 3;
            var result = client.Add(a, b);

            return Content($"{a}+{b}={result.ToString()} (Result come from Soap service.)");
        }
    }
}