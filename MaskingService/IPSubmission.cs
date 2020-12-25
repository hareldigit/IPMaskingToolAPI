namespace MaskingService
{
    public class IPSubmission
    {
        public string OriginalIP { get; private set; }
        public string MaskedIP { get; private set; }

        public IPSubmission(string originalIP,string maskedIP)
        {
            OriginalIP = originalIP;
            MaskedIP = maskedIP;
        }
    }
}
