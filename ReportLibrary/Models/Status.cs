using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportLibrary.Models
{
    public sealed class Status
    {
        public static String PASS => "PASS";
        public static String FAIL => "FAIL";
        public static String DONE => "DONE";
        public static String ERROR => "ERROR";
    }
}
