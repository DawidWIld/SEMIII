using System.Numerics;

namespace RSAGUI
{
    class RSA
    {
        public static BigInteger Encrypt(BigInteger data, BigInteger n, BigInteger e)
        {
            return Big.ModPow(data, e, n);
        }

        public static BigInteger Decrypt(BigInteger data, BigInteger d, BigInteger n)
        {
            return Big.ModPow(data, d, n);
        }

        public static string[] KeyGen(int size)
        {
            //e = first half of public key 
            BigInteger e = 65537, lambda = 1, p = 1, q = 1;

            do
            {
                p = ParallelPrime.GetPrime();
                q = ParallelPrime.GetPrime();
                lambda = Big.LeastCommonMultiple(p - 1, q - 1);            //lmc - least common multiply
                                                                                 //lambda = (p - 1) * (q - 1);
                                                                                 
            } while (Big.GreatestCommonDivisor(e, lambda) != 1);

            BigInteger n = p * q;                       //half of private/public key
            BigInteger d = Big.ModInv(e, lambda);       //second half of private key 

            return new string[] { n.ToString(), e.ToString(), d.ToString() };
        }
    }
}
