using System;

namespace GovApp.Models
{
    public class ErrorModel
    {
        public string Url { get; set; }

        public string errMsg { get; set; }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
