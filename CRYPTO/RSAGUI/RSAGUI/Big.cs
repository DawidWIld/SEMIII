using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace RSAGUI
{
    public class Big
    {
        public static BigInteger ModPow(BigInteger b, BigInteger e, BigInteger n)
        {
            BigInteger s = 1;
            BigInteger t = b;
            BigInteger u = e;

            while (u != 0)
            {
                if ((u & 1) == 1) s = (s * t) % n;
                u = u >> 1;
                t = (t * t) % n;
            }

            return s;
        }

        public static BigInteger[] EGCD(BigInteger left, BigInteger right)
        {
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

            return new BigInteger[] { gcd, leftFactor, rightFactor };
        }

        public static BigInteger GreatestCommonDivisor(BigInteger a, BigInteger b)
        {
            return EGCD(a, b)[0];
        }

        public static BigInteger LeastCommonMultiple(BigInteger a, BigInteger b)
        {
            return (a / GreatestCommonDivisor(a, b)) * b;
        }

        public static BigInteger ModInv(BigInteger value, BigInteger modulo)
        {
            BigInteger[] arr = EGCD(value, modulo);

            if (arr[0] != 1)
                throw new Exception("Not primes!");

            if (arr[1] < 0)
                arr[1] += modulo;

            return arr[1] % modulo;
        }
    }
}
