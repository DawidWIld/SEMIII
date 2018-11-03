using System.Numerics;

namespace RSAGUI
{
    class RSA
    {
        public static BigInteger Encrypt(BigInteger data, BigInteger n, BigInteger e)
        {
            return BigInteger.ModPow(data, e, n);
        }

        public static BigInteger Decrypt(BigInteger data, BigInteger d, BigInteger n)
        {
            return BigInteger.ModPow(data, d, n);
        }

        public static string[] KeyGen(int size)
        {   
            //e = first half of public key 
            BigInteger e = 65537, lambda = 1, p = 1, q = 1; 

             do
             {
                 p = ParallelPrime.GetPrime();
                 q = ParallelPrime.GetPrime();
                 lambda = lcm(p - 1, q - 1);            //lmc - least common multiply
                 //lambda = (p - 1) * (q - 1);

             } while (BigInteger.GreatestCommonDivisor(e, lambda) != 1 || BigInteger.Abs(p - q) < BigInteger.Pow(2, size / 2 - 100));

            BigInteger n = p * q;                       //half of private/public key
            BigInteger d = modInverse(e, lambda);       //second half of private key 

            return new string[] { n.ToString(), e.ToString(), d.ToString() };
        }

        static BigInteger lcm(BigInteger a, BigInteger b)
        {
            return (a / BigInteger.GreatestCommonDivisor(a, b)) * b;
        }

        static BigInteger modInverse(BigInteger a, BigInteger n)
        {
            BigInteger i = n, v = 0, d = 1;
            while (a > 0)
            {
                BigInteger t = i / a;
                BigInteger x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0)
                v = (v + n) % n;
            return v;
        }
    }
}
