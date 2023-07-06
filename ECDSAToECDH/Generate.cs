using ECDSAToECDH.EllipticCurve;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ECDSAToECDH
{
    public class Generate
    {
        public static string GenerateSharedKey(string privateKey, string publicKey, string sharedPublicKey)
        {
            BigInteger a1 = BigInteger.Parse(privateKey, NumberStyles.AllowHexSpecifier);//converts hex private key into big int.
            BigInteger a2 = BigInteger.Parse(publicKey, NumberStyles.AllowHexSpecifier);//converts hex private key into big int.
            BigInteger b2 = BigInteger.Parse(sharedPublicKey, NumberStyles.AllowHexSpecifier);//converts hex private key into big int.

            BigInteger P, G, a, x;

            P = a2;
            G = b2;
            a = a1;

            x = power(G, a, P);

            var generatedKey = x.ToString("X");

            return generatedKey;
        }
        public static string GenerateSharedKey(PrivateKey privateKey, string sharedPublicKey, bool addPrefix = true)
        {
            var privKeySecretHex = privateKey.secret.ToString("x");
            var pubKey = privateKey.publicKey();
            var pubKeyHex = addPrefix ? ("04" + ByteToHex(pubKey.toString())) : ByteToHex(pubKey.toString());

            BigInteger a1 = BigInteger.Parse(privKeySecretHex, NumberStyles.AllowHexSpecifier);//converts hex private key into big int.
            BigInteger a2 = BigInteger.Parse(pubKeyHex, NumberStyles.AllowHexSpecifier);//converts hex private key into big int.
            BigInteger b2 = BigInteger.Parse(sharedPublicKey, NumberStyles.AllowHexSpecifier);//converts hex private key into big int.

            BigInteger P, G, a, x;

            P = a2;
            G = b2;
            a = a1;

            x = power(G, a, P);

            var generatedKey = x.ToString("X");

            return generatedKey;
        }
        private static BigInteger power(BigInteger a, BigInteger b, BigInteger P)
        {
            return BigInteger.ModPow(a, b, P);
        }
        private static string ByteToHex(byte[] pubkey)
        {
            return Convert.ToHexString(pubkey).ToLower();
        }

    }
}
