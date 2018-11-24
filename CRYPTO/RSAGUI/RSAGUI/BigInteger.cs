using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography;

namespace RSAGUI
{
    public class BigInteger
    {
        private const int maxLength = 128;
        private const uint signMask = 1U << (32 - 1);

        private uint[] data;
        public int Length;

        public BigInteger()
        {
            data = new uint[maxLength];
            Length = 1;
        }

        public BigInteger(long value)
        {
            data = new uint[maxLength];
            Length = 0;
            while (value != 0 && Length < maxLength)
            {
                data[Length] = (uint)(value);
                value >>= 32;
                Length++;
            }
            if (Length == 0)
                Length = 1;
        }

        public BigInteger(ulong value)
        {
            data = new uint[maxLength];
            Length = 0;
            while (value != 0 && Length < maxLength)
            {
                data[Length] = (uint)(value);
                value >>= 32;
                Length++;
            }
            if (Length == 0)
                Length = 1;
        }

        public BigInteger(BigInteger b)
        {
            data = new uint[maxLength];
            Length = b.Length;
            b.data.CopyTo(data, 0);
        }

        public static BigInteger Parse(string input)
        {
            return new BigInteger(input, 10);
        }

        public BigInteger(string value, int radix=16)
        {
            BigInteger multiplier = new BigInteger(1);
            BigInteger result = new BigInteger();
            value = value.ToUpper().Trim();
            int limit = 0;

            if (value[0] == '-')
                limit = 1;

            for (int i = value.Length - 1; i >= limit; i--)
            {
                int posVal = (int)value[i];

                if (posVal >= '0' && posVal <= '9')
                    posVal -= '0';
                else if (posVal >= 'A' && posVal <= 'Z')
                    posVal = (posVal - 'A') + 10;
                else
                    posVal = 9999999;


                if (posVal >= radix)
                    throw new ArithmeticException("Invalid character");
                else
                {
                    if (value[0] == '-')
                        posVal = -posVal;

                    result = result + (multiplier * posVal);

                    if ((i - 1) >= limit)
                        multiplier = multiplier * radix;
                }
            }

            data = new uint[maxLength];
            for (int i = 0; i < result.Length; i++)
                data[i] = result.data[i];

            Length = result.Length;
        }

        public BigInteger(byte[] inData)
        {
            if (inData.Length % 4 != 0)
                inData = inData.Concat(Enumerable.Repeat<byte>(0, 4 - inData.Length % 4)).ToArray();

            Length = inData.Length / 4;
            data = new uint[maxLength];
            Buffer.BlockCopy(inData, 0, data, 0, inData.Length);
        }

        public BigInteger(uint[] inData)
        {
            Length = inData.Length;
            data = new uint[maxLength];

            Buffer.BlockCopy(inData, 0, data, 0, inData.Length * 4);

            while (Length > 1 && data[Length - 1] == 0)
                Length--;
        }

        public static implicit operator BigInteger(long value)
        {
            return (new BigInteger(value));
        }

        public static implicit operator BigInteger(ulong value)
        {
            return (new BigInteger(value));
        }

        public static implicit operator BigInteger(int value)
        {
            return (new BigInteger((long)value));
        }

        public static implicit operator BigInteger(uint value)
        {
            return (new BigInteger((ulong)value));
        }

        public static BigInteger operator +(BigInteger a, BigInteger b)
        {
            BigInteger result = new BigInteger()
            {
                Length = (a.Length > b.Length) ? a.Length : b.Length
            };

            long carry = 0;
            for (int i = 0; i < result.Length; i++)
            {
                long sum = (long)a.data[i] + (long)b.data[i] + carry;
                carry = sum >> 32;
                result.data[i] = (uint)(sum);
            }

            if (carry != 0 && result.Length < maxLength)
            {
                result.data[result.Length] = (uint)(carry);
                result.Length++;
            }

            while (result.Length > 1 && result.data[result.Length - 1] == 0)
                result.Length--;


            // overflow check
            int lastPos = maxLength - 1;
            if ((a.data[lastPos] & signMask) == (b.data[lastPos] & signMask) &&
               (result.data[lastPos] & signMask) != (a.data[lastPos] & signMask))
            {
                throw new OverflowException();
            }

            return result;
        }

        public static BigInteger operator -(BigInteger a, BigInteger b)
        {
            BigInteger result = new BigInteger()
            {
                Length = (a.Length > b.Length) ? a.Length : b.Length
            };

            long carryIn = 0;
            for (int i = 0; i < result.Length; i++)
            {
                long diff;

                diff = (long)a.data[i] - (long)b.data[i] - carryIn;
                result.data[i] = (uint)(diff);

                if (diff < 0)
                    carryIn = 1;
                else
                    carryIn = 0;
            }

            if (carryIn != 0)
            {
                for (int i = result.Length; i < maxLength; i++)
                    result.data[i] = 0xFFFFFFFF;
                result.Length = maxLength;
            }

            while (result.Length > 1 && result.data[result.Length - 1] == 0)
                result.Length--;

            // overflow check
            int lastPos = maxLength - 1;
            if ((a.data[lastPos] & signMask) != (b.data[lastPos] & signMask) &&
               (result.data[lastPos] & signMask) != (a.data[lastPos] & signMask))
            {
                throw new OverflowException();
            }

            return result;
        }

        public static BigInteger operator *(BigInteger a, BigInteger b)
        {
            int lastPos = maxLength - 1;
            bool aNeg = false;
            bool bNeg = false;

            if ((a.data[lastPos] & signMask) != 0)  // a negative
            {
                aNeg = true;
                a = -a;
            }
            if ((b.data[lastPos] & signMask) != 0) // b negative
            {
                bNeg = true;
                b = -b;
            }

            BigInteger result = new BigInteger();

            try
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a.data[i] == 0) continue;

                    ulong mcarry = 0;
                    for (int j = 0, k = i; j < b.Length; j++, k++)
                    {
                        // k = i + j
                        ulong val = ((ulong)a.data[i] * (ulong)b.data[j]) +
                                     (ulong)result.data[k] + mcarry;

                        result.data[k] = (uint)(val);
                        mcarry = val >> 32;
                    }

                    if (mcarry != 0)
                        result.data[i + b.Length] = (uint)mcarry;
                }
            }
            catch (Exception)
            {
                throw new OverflowException();
            }

            result.Length = a.Length + b.Length;
            if (result.Length > maxLength)
                result.Length = maxLength;

            while (result.Length > 1 && result.data[result.Length - 1] == 0)
                result.Length--;

            // overflow check
            if ((result.data[lastPos] & signMask) != 0)
            {
                if (aNeg != bNeg && result.data[lastPos] == signMask) // different sign
                {
                    if (result.Length == 1)
                        return result;
                    else
                    {
                        bool isMaxNeg = true;
                        for (int i = 0; i < result.Length - 1 && isMaxNeg; i++)
                        {
                            if (result.data[i] != 0)
                                isMaxNeg = false;
                        }

                        if (isMaxNeg)
                            return result;
                    }
                }

                throw new OverflowException();
            }

            if (aNeg != bNeg)
                return -result;

            return result;
        }

        public static BigInteger operator <<(BigInteger a, int shiftVal)
        {
            BigInteger result = new BigInteger(a);
            result.Length = shiftLeft(result.data, shiftVal);
            return result;
        }

        private static int shiftLeft(uint[] buffer, int shiftVal)
        {
            int shiftAmount = 32;
            int bufLen = buffer.Length;

            while (bufLen > 1 && buffer[bufLen - 1] == 0)
                bufLen--;

            for (int count = shiftVal; count > 0;)
            {
                if (count < shiftAmount)
                    shiftAmount = count;

                ulong carry = 0;
                for (int i = 0; i < bufLen; i++)
                {
                    ulong val = ((ulong)buffer[i]) << shiftAmount;
                    val |= carry;

                    buffer[i] = (uint)(val);
                    carry = val >> 32;
                }

                if (carry != 0)
                {
                    if (bufLen + 1 <= buffer.Length)
                    {
                        buffer[bufLen] = (uint)carry;
                        bufLen++;
                    }
                }
                count -= shiftAmount;
            }
            return bufLen;
        }

        public static BigInteger operator >>(BigInteger a, int shiftVal)
        {
            BigInteger result = new BigInteger(a);
            result.Length = shiftRight(result.data, shiftVal);

            if ((a.data[maxLength - 1] & signMask) != 0) // negative
            {
                for (int i = maxLength - 1; i >= result.Length; i--)
                    result.data[i] = 0xFFFFFFFF;

                uint mask = signMask;
                for (int i = 0; i < 32; i++)
                {
                    if ((result.data[result.Length - 1] & mask) != 0)
                        break;

                    result.data[result.Length - 1] |= mask;
                    mask >>= 1;
                }
                result.Length = maxLength;
            }

            return result;
        }

        private static int shiftRight(uint[] buffer, int shiftVal)
        {
            int shiftAmount = 32;
            int invShift = 0;
            int bufLen = buffer.Length;

            while (bufLen > 1 && buffer[bufLen - 1] == 0)
                bufLen--;

            for (int count = shiftVal; count > 0;)
            {
                if (count < shiftAmount)
                {
                    shiftAmount = count;
                    invShift = 32 - shiftAmount;
                }

                ulong carry = 0;
                for (int i = bufLen - 1; i >= 0; i--)
                {
                    ulong val = ((ulong)buffer[i]) >> shiftAmount;
                    val |= carry;

                    carry = (((ulong)buffer[i]) << invShift) & 0xFFFFFFFF;
                    buffer[i] = (uint)(val);
                }

                count -= shiftAmount;
            }

            while (bufLen > 1 && buffer[bufLen - 1] == 0)
                bufLen--;

            return bufLen;
        }

        public static BigInteger operator -(BigInteger a)
        {
            if (a.Length == 1 && a.data[0] == 0)
                return (new BigInteger());

            BigInteger result = new BigInteger(a);

            for (int i = 0; i < maxLength; i++)
                result.data[i] = (uint)(~(a.data[i]));

            long val, carry = 1;
            int index = 0;

            while (carry != 0 && index < maxLength)
            {
                val = (long)(result.data[index]);
                val++;

                result.data[index] = (uint)(val);
                carry = val >> 32;

                index++;
            }

            if ((a.data[maxLength - 1] & signMask) == (result.data[maxLength - 1] & signMask))
                throw new OverflowException();

            result.Length = maxLength;

            while (result.Length > 1 && result.data[result.Length - 1] == 0)
                result.Length--;
        
            return result;
        }

        public static bool operator ==(BigInteger a, BigInteger b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(BigInteger a, BigInteger b)
        {
            return !(a.Equals(b));
        }

        public override bool Equals(object o)
        {
            BigInteger bi = (BigInteger)o;

            if (this.Length != bi.Length)
                return false;

            for (int i = 0; i < this.Length; i++)
            {
                if (this.data[i] != bi.data[i])
                    return false;
            }
            return true;
        }


        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public static bool operator >(BigInteger a, BigInteger b)
        {
            return !(a < b || a == b);
        }

        public static bool operator <(BigInteger a, BigInteger b)
        {
            int pos = maxLength - 1;

            // a is negative, b is positive
            if ((a.data[pos] & signMask) != 0 && (b.data[pos] & signMask) == 0)
                return true;

            // a is positive, b is negative
            else if ((a.data[pos] & signMask) == 0 && (b.data[pos] & signMask) != 0)
                return false;

            // same sign
            int len = (a.Length > b.Length) ? a.Length : b.Length;
            for (pos = len - 1; pos >= 0 && a.data[pos] == b.data[pos]; pos--) ;

            if (pos >= 0)
            {
                if (a.data[pos] < b.data[pos])
                    return true;
                return false;
            }
            return false;
        }

        public static bool operator >=(BigInteger a, BigInteger b)
        {
            return (a == b || a > b);
        }

        public static bool operator <=(BigInteger a, BigInteger b)
        {
            return (a == b || a < b);
        }

        private static void multiDivide(BigInteger a, BigInteger b, BigInteger outQuotient, BigInteger outRemainder)
        {
            uint[] result = new uint[maxLength];

            int remainderLen = a.Length + 1;
            uint[] remainder = new uint[remainderLen];

            uint mask = signMask;
            uint val = b.data[b.Length - 1];
            int shift = 0, resultPos = 0;

            while (mask != 0 && (val & mask) == 0)
            {
                shift++; mask >>= 1;
            }

            for (int i = 0; i < a.Length; i++)
                remainder[i] = a.data[i];
            shiftLeft(remainder, shift);
            b = b << shift;

            int j = remainderLen - b.Length;
            int pos = remainderLen - 1;

            ulong firstDivisorByte = b.data[b.Length - 1];
            ulong secondDivisorByte = b.data[b.Length - 2];

            int divisorLen = b.Length + 1;
            uint[] dividendPart = new uint[divisorLen];

            while (j > 0)
            {
                ulong dividend = ((ulong)remainder[pos] << 32) + (ulong)remainder[pos - 1];

                ulong q_hat = dividend / firstDivisorByte;
                ulong r_hat = dividend % firstDivisorByte;

                bool done = false;
                while (!done)
                {
                    done = true;

                    if (q_hat == 0x100000000 ||
                       (q_hat * secondDivisorByte) > ((r_hat << 32) + remainder[pos - 2]))
                    {
                        q_hat--;
                        r_hat += firstDivisorByte;

                        if (r_hat < 0x100000000)
                            done = false;
                    }
                }

                for (int h = 0; h < divisorLen; h++)
                    dividendPart[divisorLen - h - 1] = remainder[pos - h];

                BigInteger kk = new BigInteger(dividendPart);
                BigInteger ss = b * (long)q_hat;

                while (ss > kk)
                {
                    q_hat--;
                    ss -= b;
                }
                BigInteger yy = kk - ss;

                for (int h = 0; h < divisorLen; h++)
                    remainder[pos - h] = yy.data[b.Length - h];

                result[resultPos++] = (uint)q_hat;

                pos--;
                j--;
            }

            outQuotient.Length = resultPos;
            int y = 0;
            for (int x = outQuotient.Length - 1; x >= 0; x--, y++)
                outQuotient.data[y] = result[x];
            for (; y < maxLength; y++)
                outQuotient.data[y] = 0;

            while (outQuotient.Length > 1 && outQuotient.data[outQuotient.Length - 1] == 0)
                outQuotient.Length--;

            if (outQuotient.Length == 0)
                outQuotient.Length = 1;

            outRemainder.Length = shiftRight(remainder, shift);

            for (y = 0; y < outRemainder.Length; y++)
                outRemainder.data[y] = remainder[y];
            for (; y < maxLength; y++)
                outRemainder.data[y] = 0;
        }

        private static void singleDivide(BigInteger a, BigInteger b, BigInteger outQuotient, BigInteger outRemainder)
        {
            uint[] result = new uint[maxLength];
            int resultPos = 0;

            for (int i = 0; i < maxLength; i++)
                outRemainder.data[i] = a.data[i];
            outRemainder.Length = a.Length;

            while (outRemainder.Length > 1 && outRemainder.data[outRemainder.Length - 1] == 0)
                outRemainder.Length--;

            ulong divisor = (ulong)b.data[0];
            int pos = outRemainder.Length - 1;
            ulong dividend = (ulong)outRemainder.data[pos];

            if (dividend >= divisor)
            {
                ulong quotient = dividend / divisor;
                result[resultPos++] = (uint)quotient;

                outRemainder.data[pos] = (uint)(dividend % divisor);
            }
            pos--;

            while (pos >= 0)
            {
                dividend = ((ulong)outRemainder.data[pos + 1] << 32) + (ulong)outRemainder.data[pos];
                ulong quotient = dividend / divisor;
                result[resultPos++] = (uint)quotient;

                outRemainder.data[pos + 1] = 0;
                outRemainder.data[pos--] = (uint)(dividend % divisor);
            }

            outQuotient.Length = resultPos;
            int j = 0;
            for (int i = outQuotient.Length - 1; i >= 0; i--, j++)
                outQuotient.data[j] = result[i];
            for (; j < maxLength; j++)
                outQuotient.data[j] = 0;

            while (outQuotient.Length > 1 && outQuotient.data[outQuotient.Length - 1] == 0)
                outQuotient.Length--;

            if (outQuotient.Length == 0)
                outQuotient.Length = 1;

            while (outRemainder.Length > 1 && outRemainder.data[outRemainder.Length - 1] == 0)
                outRemainder.Length--;
        }

        public static BigInteger operator /(BigInteger a, BigInteger b)
        {
            BigInteger quotient = new BigInteger();
            BigInteger remainder = new BigInteger();

            bool divisorNeg = false;
            bool dividendNeg = false;

            if ((a.data[maxLength - 1] & signMask) != 0) // a negative
            {
                a = -a;
                dividendNeg = true;
            }
            if ((b.data[maxLength - 1] & signMask) != 0) // b negative
            {
                b = -b;
                divisorNeg = true;
            }

            if (a < b)
            {
                return quotient;
            }
            else
            {
                if (b.Length == 1)
                    singleDivide(a, b, quotient, remainder);
                else
                    multiDivide(a, b, quotient, remainder);

                if (dividendNeg != divisorNeg)
                    return -quotient;

                return quotient;
            }
        }

        public static BigInteger operator %(BigInteger a, BigInteger b)
        {
            BigInteger quotient = new BigInteger();
            BigInteger remainder = new BigInteger(a);

            bool dividendNeg = false;

            if ((a.data[maxLength - 1] & signMask) != 0) // a negative
            {
                a = -a;
                dividendNeg = true;
            }
            if ((b.data[maxLength - 1] & signMask) != 0)  // b negative
                b = -b;

            if (a < b)
            {
                return remainder;
            }
            else
            {
                if (b.Length == 1)
                    singleDivide(a, b, quotient, remainder);
                else
                    multiDivide(a, b, quotient, remainder);

                if (dividendNeg)
                    return -remainder;

                return remainder;
            }
        }

        public static BigInteger operator &(BigInteger a, BigInteger b)
        {
            BigInteger result = new BigInteger();

            int len = (a.Length > b.Length) ? a.Length : b.Length;

            for (int i = 0; i < len; i++)
            {
                uint sum = (uint)(a.data[i] & b.data[i]);
                result.data[i] = sum;
            }

            result.Length = maxLength;

            while (result.Length > 1 && result.data[result.Length - 1] == 0)
                result.Length--;

            return result;
        }

        public static BigInteger operator |(BigInteger a, BigInteger b)
        {
            BigInteger result = new BigInteger();

            int len = (a.Length > b.Length) ? a.Length : b.Length;

            for (int i = 0; i < len; i++)
            {
                uint sum = (uint)(a.data[i] | b.data[i]);
                result.data[i] = sum;
            }

            result.Length = maxLength;

            while (result.Length > 1 && result.data[result.Length - 1] == 0)
                result.Length--;

            return result;
        }

        public override string ToString()
        {
            return ToString(10);
        }

        public string ToString(int radix)
        {
            if (Length == 1 && data[0] == 0)
                return "0";

            string result = "";

            BigInteger a = this;

            bool negative = false;
            if ((a.data[maxLength - 1] & signMask) != 0)
            {
                negative = true;
                a = -a;
            }

            BigInteger quotient = new BigInteger();
            BigInteger remainder = new BigInteger();

            while (a.Length > 1 || (a.Length == 1 && a.data[0] != 0))
            {
                singleDivide(a, 10, quotient, remainder);
                result = remainder.data[0] + result;
                a = quotient;
            }
            if (negative)
                result = "-" + result;

            return result;
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[Length * 4];
            Buffer.BlockCopy(data, 0, result, 0, Length * 4);
            return result;
        }
    }
}