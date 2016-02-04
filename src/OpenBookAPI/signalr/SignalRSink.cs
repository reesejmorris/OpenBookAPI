using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Serilog.Sinks.PeriodicBatching;
using LogEvent = Serilog.Sinks.SignalR.Data.LogEvent;

namespace Serilog.Sinks.SignalR
{
    /// <summary>
    /// Writes log events as messages to a SignalR hub.
    /// </summary>
    public class SignalRSink : PeriodicBatchingSink
    {
        readonly IFormatProvider _formatProvider;
        readonly IHubContext _context;
        /// <summary>
        /// A reasonable default for the number of events posted in
        /// each batch.
        /// </summary>
        public const int DefaultBatchPostingLimit = 5;

        /// <summary>
        /// A reasonable default time to wait between checking for event batches.
        /// </summary>
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);

        /// <summary>
        /// Construct a sink posting to the specified database.
        /// </summary>
        /// <param name="context">The hub context.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        public SignalRSink(IHubContext context, int batchPostingLimit, TimeSpan period = default(TimeSpan), IFormatProvider formatProvider = null)
            : base(batchPostingLimit, period)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if(period == default(TimeSpan))
                period = TimeSpan.FromSeconds(2);
            _formatProvider = formatProvider;
            _context = context;
        }

        /// <summary>
        /// Emit a batch of log events, running asynchronously.
        /// </summary>
        /// <param name="events">The events to emit.</param>
        /// <remarks>Override either <see cref="PeriodicBatchingSink.EmitBatch"/> or <see cref="PeriodicBatchingSink.EmitBatchAsync"/>,
        /// not both.</remarks>
        protected override void EmitBatch(IEnumerable<Events.LogEvent> events)
        {
            // This sink doesn't use batching to send events, instead only using
            // PeriodicBatchingSink to manage the worker thread; requires some consideration.

            foreach (var logEvent in events)
            {
                _context.Clients.All.broadcastLogEvent(new LogEvent(logEvent, logEvent.RenderMessage(_formatProvider)));
            }
        }
    }
}