using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Extensions
{
    public static class IFormFileExtension
    {
        public static byte[] ToByteArray(this IFormFile file)
        {
            byte[] bytes;
            var stream = file.OpenReadStream();
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }
            return bytes;
        }

        public static string GetExtention(this IFormFile file)
        =>  new Regex(@"[Jj][Pp][Ee]?[Gg]|[Pp][Nn][Gg]|[Gg][Ii][Ff]$", RegexOptions.IgnoreCase).Match(file.FileName).Value;
    }
}
