namespace ESAPrizes.Models {
    public class ErrorModel
    {
        internal int StatusCode;

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}