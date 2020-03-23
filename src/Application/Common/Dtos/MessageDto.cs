using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Dtos
{
    public class MessageDto
    {
        public MessageDto(string to, string subject, string htmlBody)
            => (To, Subject, HtmlBody) = (to, subject, htmlBody);

        public string To { get; set; }

        public string Subject { get; set; }

        public  string HtmlBody { get; set; }
    }
}
