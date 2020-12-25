using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MaskingService.Utils
{
    public static class FileExtensionMethods
    {
        public static IEnumerable<string> ReadLines(this IFormFile file)
        {
            var result = new List<string>();
            if (file != null && file.Length > 0)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                    {
                        result.Add(reader.ReadLine());
                    }
                }
            }
            return result;
        }
    }
}
