using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaskingService.Utils
{
    public static class Guard
    {
        public static void ArgumentNotNullOrEmpty(IEnumerable<string> argumentValue,string argumentName)
        {
            if (argumentValue == null || !argumentValue.Any())
            {
                var message = $"The '{nameof(argumentName)}' parameter must be initialized and should contain at least single item";
                throw (new ArgumentNullException(message));
            }
        }
    }
}
