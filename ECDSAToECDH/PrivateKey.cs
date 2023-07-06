using ECDSAToECDH.EllipticCurve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECDSAToECDH
{
    public class PrivateKeyHex
    {
        public PrivateKey PrivateKey { get; set; }
        public bool AddPrefix { get; set; }
        public string HexPrivateKey { get { return GenerateHexPrivateKeyFromPrivateKey(); } }
        public string HexPublicKey { get { return GenerateHexPublicKeyFromPrivateKey(); } }

        private string GenerateHexPrivateKeyFromPrivateKey()
        {
            var key = PrivateKey.secret.ToString("x");
            return key;
        }

        private string GenerateHexPublicKeyFromPrivateKey()
        {
            var pubKey = PrivateKey.publicKey();
            var pubKeyHex = AddPrefix ? ("04" + ByteToHex(pubKey.toString())) : ByteToHex(pubKey.toString());
            return pubKeyHex;
        }

        private static string ByteToHex(byte[] pubkey)
        {
            return Convert.ToHexString(pubkey).ToLower();
        }
    }
}
