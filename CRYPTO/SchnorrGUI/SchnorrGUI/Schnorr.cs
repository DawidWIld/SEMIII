using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace SchnorrGUI
{
    class Schnorr
    {
        static int sizeQ = 160;
        static int sizeP = 1024;

        static BigInteger hash(byte[] a, byte[]b)
        {
            using (MD5 md5 = MD5.Create())
            {
                List<byte> data = md5.ComputeHash(a.Concat(b).ToArray()).ToList();
                data.Add(0); // make sure that biginteger will be positive
                BigInteger big = new BigInteger(data.ToArray());
                return big;
            }
        }

        public static BigInteger[] GenerateGroup()
        {
            BigInteger q = Prime.RandomPrime(sizeQ);
            BigInteger p;
            BigInteger g;

            BigInteger r;
            BigInteger min = BigInteger.One << (sizeP - 1);
   
            do
            {
                // (p - 1) / q = r
                // (p - 1)     = r * q
                //  p          = r * q + 1
                r = Prime.RandomBig(sizeP);
                p = r * q + 1;

            } while (!(Prime.IsPrime(p, sizeP) && p > min));

            do
            {
                BigInteger a = Prime.RandomBig(2, q);

                g = BigInteger.ModPow(a, r, p);

            } while (g == 1);

            return new BigInteger[] { p, q, g };
        }

        public static BigInteger[] GenerateKeys(BigInteger[] group)
        {
            BigInteger p = group[0];
            BigInteger q = group[1];
            BigInteger g = group[2];

            BigInteger x = Prime.RandomBig(1, q);

            BigInteger y = BigInteger.ModPow(g, x, p);

            return new BigInteger[] { x, y };
        }


        public static BigInteger[] Sign(byte[] data, BigInteger[] group, BigInteger[] keys)
        {
            BigInteger p = group[0];
            BigInteger q = group[1];
            BigInteger g = group[2];
            BigInteger x = keys[0];

        Start:
            BigInteger k = Prime.RandomBig(2, q);

            BigInteger r = BigInteger.ModPow(g, k, p);
            if (r == 0)
                goto Start;

            BigInteger e = hash(r.ToByteArray(), data);
            BigInteger s = (k - (x * e) % q) % q;
            if (s <= 0)
                goto Start;

            return new BigInteger[] { s, e };
        }

        public static bool Verify(byte[] data, BigInteger[] group, BigInteger[] keys, BigInteger[] sign)
        {
            BigInteger p = group[0];
            BigInteger g = group[2];
            BigInteger y = keys[1];
            BigInteger s = sign[0];
            BigInteger e = sign[1];

            BigInteger rv = (BigInteger.ModPow(g, s, p) * BigInteger.ModPow(y, e, p)) % p;
            BigInteger ev = hash(rv.ToByteArray(), data);
            if (ev == e)
                return true;
            return false;
        }
    }
}
