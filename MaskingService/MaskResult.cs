using System.Collections.Generic;

namespace MaskingService
{
    public class MaskResult
    {
        public IEnumerable<string> Lines { get; set; }
        public IEnumerable<IPSubmission> Summery  { get; set; }
    }
}
