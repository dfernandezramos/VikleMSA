// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Formatting.Json;

namespace Common.Domain
{
    /// <summary>
    /// This class contains the implementation of the logger service.
    /// </summary>
    public class Log : ILog
    {
        private ILogger _logger;

        public Log()
        {
            _logger = new LoggerConfiguration().WriteTo.Console(new JsonFormatter(renderMessage: true)).CreateLogger();
        }

        internal Log(ILogger logger)
        {
            _logger = logger;
        }

        public void InitForDebug()
        {
            _logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }

        public void Info(string message)
        {
            _logger.Information(message);
        }

        public void Info(object obj)
        {
            (var prepedMessage, var prepedParams) = PrepMessageAndParams(string.Empty, obj, obj.GetType().Name);
            _logger.Information(prepedMessage, prepedParams);
        }

        public void Info(string message, object obj)
        {
            (var prepedMessage, var prepedParams) = PrepMessageAndParams(string.Empty, new { Message = message, Payload = obj }, "Info");
            _logger.Information(prepedMessage, prepedParams);
        }

        public void Warning(string message)
        {
            _logger.Warning(message);
        }

        public void Warning(object obj)
        {
            (var prepedMessage, var prepedParams) = PrepMessageAndParams(string.Empty, obj, obj.GetType().Name);
            _logger.Warning(prepedMessage, prepedParams);
        }

        public void Warning(string message, object obj)
        {
            (var prepedMessage, var prepedParams) = PrepMessageAndParams(string.Empty, new { Message = message, Payload = obj }, "Warning");
            _logger.Warning(prepedMessage, prepedParams);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(object obj)
        {
            (var prepedMessage, var prepedParams) = PrepMessageAndParams(string.Empty, obj, obj.GetType().Name);
            _logger.Error(prepedMessage, prepedParams);
        }

        public void Error(string message, object obj)
        {
            (var prepedMessage, var prepedParams) = PrepMessageAndParams(string.Empty, new { Message = message, Payload = obj }, "Error");
            _logger.Error(prepedMessage, prepedParams);
        }


        private static (string Message, object[] @params) PrepMessageAndParams(string message, object obj, string type = null)
        {
            if (obj == null) return ("Object passed for Logging was null\nORIGINAL MESSAGE:" + message, null);
            if (obj is string) return ("Object passed for Logging was a string\nORIGINAL MESSAGE:" + message + " with string: {string}", new[] { obj });

            var msg = new StringBuilder();
            var objs = new List<object>();

            var stringJoiner = new List<string>();

            if (!string.IsNullOrEmpty(type))
            {
                stringJoiner.Add("{@Type}");
                objs.Add(type);
            }

            if (!string.IsNullOrEmpty(message))
            {
                stringJoiner.Add(message);
            }

            msg.Append(string.Join(": ", stringJoiner));

            msg.Append(" with content:");
            foreach (var prop in obj.GetType().GetProperties())
            {
                msg.Append(" {@");
                msg.Append(prop.Name);
                msg.Append("}");
                objs.Add(prop.GetValue(obj));
            }

            return (msg.ToString(), objs.ToArray());
        }
    }
}