using System.Numerics;

namespace RSAGUI
{
    class RSA
    {
        public static BigInteger Encrypt(BigInteger data, BigInteger n, BigInteger e)
        {
            return BigInteger.ModPow(data, e, n);  //napisac recznie
        }

        public static BigInteger Decrypt(BigInteger data, BigInteger d, BigInteger n)
        {
            return BigInteger.ModPow(data, d, n);  //napisac recznie
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

        static BigInteger modInverse(BigInteger value, BigInteger modulo)
        {
            BigInteger left = value;
            BigInteger right = modulo;
            BigInteger leftFactor = 0;
            BigInteger rightFactor = 1;
            BigInteger u = 1;
            BigInteger v = 0;
            BigInteger gcd = 0;

            while (left != 0)
            {
                BigInteger q = right / left;
                BigInteger r = right % left;

                BigInteger m = leftFactor - u * q;
                BigInteger n = rightFactor - v * q;

                right = left;
                left = r;
                leftFactor = u;
                rightFactor = v;
                u = m;
                v = n;

                gcd = right;
            }

            if (gcd != 1)
                throw new System.Exception("Not primes!");

            if (leftFactor < 0)
                leftFactor += modulo;

            return leftFactor % modulo;
        }
    }
}
