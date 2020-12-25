using MaskingService.Utils;
using Microsoft.AspNetCore.Http;
using NLog;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MaskingService
{

    public class MaskManager
    {
        public MaskResult Execute(IEnumerable<string> lines)
        {
            Guard.ArgumentNotNullOrEmpty(lines,nameof(lines));

            var mapEngine = new MapEngine(lines);
            var mappedIP = mapEngine.Execute();

            var maskEngine = new MaskEngine(lines, mappedIP);
            var result = maskEngine.Execute();
            return result;
        }
    }
}
