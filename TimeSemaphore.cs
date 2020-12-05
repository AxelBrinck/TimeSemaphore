using System;
using System.Collections.Generic;
using System.Threading;

namespace TimeSemaphore
{
    public class TimeSemaphore
    {
        public int MaximumCalls { get; }
        public TimeSpan TimeWindow { get; }

        private LinkedList<DateTime> _calls = new LinkedList<DateTime>();

        /// <summary>
        /// Instantiate the TimeSemaphore with a maximum call allowed for a time span.
        /// </summary>
        /// <param name="maximumCalls">The number of executions to allow.</param>
        /// <param name="timeWindow">The time window to look up.</param>
        public TimeSemaphore(int maximumCalls, TimeSpan timeWindow)
        {
            MaximumCalls = maximumCalls;
            TimeWindow = timeWindow;
        }

        /// <summary>
        /// Will remove all the expired calls from the buffer.
        /// </summary>
        private void RemoveExpiredCalls()
        {
            if (_calls.Count == 0) return;

            var currentCall = _calls.Last;

            while(_calls.Count > 1 && currentCall.Value < DateTime.Now - TimeWindow)
            {
                currentCall = currentCall.Previous;
                _calls.RemoveLast();
            }
        }

        /// <summary>
        /// Waits for space for an additional execution.
        /// </summary>
        public void Wait()
        {
            RemoveExpiredCalls();

            if (_calls.Count < MaximumCalls)
            {
                _calls.AddFirst(DateTime.Now);
                return;
            }
                
            var timeToWait = TimeWindow - (DateTime.Now - _calls.Last.Value);

            Thread.Sleep(timeToWait);
            RemoveExpiredCalls();

            _calls.AddFirst(DateTime.Now);
        }

        /// <summary>
        /// Ask if there is space available for an additional call.
        /// </summary>
        /// <returns></returns>
        public bool IsGreen()
        {
            RemoveExpiredCalls();

            if (_calls.Count >= MaximumCalls)
                return false;

            _calls.AddFirst(DateTime.Now);

            return true;
        }
    }
}