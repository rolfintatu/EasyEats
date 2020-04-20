using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Table.Queries.TableAvailability
{
    public class AvailabilityResponse
    {

        public AvailabilityResponse() {
            this.availableIntervals = new Dictionary<int, int>();
        }

        public AvailabilityResponse(Dictionary<int, int> intervals)
            => (this.availableIntervals) = (intervals);

        public Dictionary<int,int> availableIntervals { get; set; }
    }
}
