using System;
using System.Collections.Generic;
using Serilog.Events;

namespace Serilog.Sinks.SignalR.Data
{
    /// <summary>
    /// A wrapper class for <see cref="Events.LogEvent"/> that is sent as a message to SignalR clients.
    /// </summary>
    public class LogEvent
    {
        /// <summary>
        /// Construct a new <see cref="LogEvent"/>.
        /// </summary>
        public LogEvent()
        {    
        }

        /// <summary>
        /// Construct a new <see cref="LogEvent"/>.
        /// </summary>
        public LogEvent(Events.LogEvent logEvent, string renderedMessage)
        {
            Timestamp = logEvent.Timestamp;
            Exception = logEvent.Exception;
            MessageTemplate = logEvent.MessageTemplate.Text;
            Level = logEvent.Level;
            RenderedMessage = renderedMessage;
            Properties = new Dictionary<string, object>();
            foreach (var pair in logEvent.Properties)
            {
                Properties.Add(pair.Key, SignalRPropertyFormatter.Simplify(pair.Value));
            }
        }

        /// <summary>
        /// The time at which the event occurred.
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// The template that was used for the log message.
        /// </summary>
        public string MessageTemplate { get; set; }

        /// <summary>
        /// The level of the log.
        /// </summary>
        public LogEventLevel Level { get; set; }

        /// <summary>
        /// A string representation of the exception that was attached to the log (if any).
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// The rendered log message.
        /// </summary>
        public string RenderedMessage { get; set; }

        /// <summary>
        /// Properties associated with the event, including those presented in <see cref="Events.LogEvent.MessageTemplate"/>.
        /// </summary>
        public IDictionary<string, object> Properties { get; set; }
    }
}