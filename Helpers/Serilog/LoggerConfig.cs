using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.SeriLog
{
    public class LoggerConfig
    {
        public static readonly String FileName = "../../../Logs/Log_.log";

        static readonly String outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.ffff} [ThreadNumber:{ThreadNumber}] | {Level:u3} | {ClassName}.{Method}() [Line:{LineNumber}] | => {Message} {NewLine}";

        static LoggingLevelSwitch levelSwitch = new LoggingLevelSwitch(LogEventLevel.Verbose);

        public readonly static ILogger Logger  = 
           new LoggerConfiguration().MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.File(FileName,
                outputTemplate: outputTemplate,
                rollingInterval: RollingInterval.Hour, retainedFileCountLimit: 70).CreateLogger();
    }
}
