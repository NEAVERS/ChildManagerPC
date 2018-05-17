using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace YCF.Common
{
    class Decrypt
    {
        private static string prikey = "<RSAKeyValue><Modulus>0Rjmp+mEEQi1Ywo3OwrNV6Ineb1eqHl9IBJlKQsIcmN3E64fuOrVguZOrM6v7jhf1l5OnHf70O7P8DKaBECg7rAlLO1LUsn5yvrZLgqRM4XNm61xCoRfaDLkWoJ+RcqEutDIQfNVdUaporxstqQhBwat0u+2WpUMRPxfXWSqwmM=</Modulus><Exponent>AQAB</Exponent><P>/sufy0B45Woqg1LYm/0PBpJxuowL0jjcVIuCj159wk74UW54mObGPXmm4r5pUPO92rgr9MiysRN5yJ5WFwnCww==</P><Q>0hX4CdwIIsyr37XZSWzxOKKo1AYIs3iNKpg7ShR1HP9Kusz6W94inhO43WPu25AybvbG8LIQ36Ar/QhoaXvH4Q==</Q><DP>5m41V9Y5ABHh3N6h84ELg8ARhsp9LmQqL3P0YcDtLzvIK60i9/VNt+87VLkr+gBV5WbGqhyQLsEUVLGN3kv/jQ==</DP><DQ>NvaccCfq0P/vL5YqBPXFnmWf+eiiOiId/LjbOiRkB3QbHwglsAdL00Ohp/pPMY5mQ3W40pwjof4LxCWA+6fwwQ==</DQ><InverseQ>xPsC8WfVdaUXME9rfVScK6RpJOb6FOeVzGhhgyWIVWyyeeF4d9X0NuC4A7pd9L9WPdhBnxryzmfU7779nInt2g==</InverseQ><D>e+qs7z/Vk568mFg6iRQKw2+Gw3/1tBRjkfk02FEAjYHi1NfxVs5dAHlqkMDgxGXGbi1vmw0EQDr3IltqqTB/7kEWNSqivI5+8iNyW9BM3fdtc8AO1Gw9Q1/Vu0H3QwnAOtRRum5kzcLAefY4QKnUf+A7kBygWgwqVRjB6vA8GkE=</D></RSAKeyValue>";

        public Decrypt()
        {

        }

        public static string RSADecrypt(string content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(prikey);
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);
            return Encoding.UTF8.GetString(cipherbytes);

        }
    }
}
