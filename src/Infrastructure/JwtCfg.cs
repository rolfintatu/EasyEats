using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class JwtCfg
    {
        public JwtCfg(string secretKey)
        {
            SecretKey = secretKey;
        }

        public string SecretKey { get; }
    }
}
