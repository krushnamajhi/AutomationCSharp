using Helpers.SeriLog;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
   public class RetreiveProperties
    {
        private static readonly ILogger logger = LoggerConfig.Logger;

        public Dictionary<string, string> GetProperties(string path)
        {
            try { 
            string fileData = "";
            using (StreamReader sr = new StreamReader(path))
            {
                fileData = sr.ReadToEnd().Replace("\r", "");
            }
            Dictionary<string, string> Properties = new Dictionary<string, string>();
            string[] kvp;
            string[] records = fileData.Split("\n".ToCharArray());
            foreach (string record in records)
            {
                bool isCorrectRow = true;

                    if (record.StartsWith("*"))
                    {
                        isCorrectRow = false;
                    }


                    if (record.StartsWith("#"))
                    {
                        isCorrectRow = false;
                    }

                    if (isCorrectRow)
                    {

                        if (record.Contains("="))
                        {
                            kvp = record.Split("=".ToCharArray(), 2);
                            if (Properties.ContainsKey(kvp[0].Trim()) == false)
                            {
                                Properties.Add(kvp[0].Trim(), kvp[1].Trim());
                            }
                        }
                    }
            }
            return Properties;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                logger.Here().Error(e.Message);
                logger.Here().Error(e.StackTrace);
                logger.Here().Error(e.InnerException.ToString());
                throw e;
            }
        }

    }
}
