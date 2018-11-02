﻿using System;
using System.Collections.Generic;

namespace DesGUI
{
    class DES
    {
        static readonly byte[] pc1 = {
            57, 49, 41, 33, 25, 17, 9,
            1, 58, 50, 42, 34, 26, 18,
            10, 2, 59, 51, 43, 35, 27,
            19, 11, 3, 60, 52, 44, 36,
            63, 55, 47, 39, 31, 23, 15,
            7, 62, 54, 46, 38, 30, 22,
            14, 6, 61, 53, 45, 37, 29,
            21, 13, 5, 28, 20, 12, 4
        };
        static readonly byte[] pc2 = {
            14, 17, 11, 24, 1, 5,
            3, 28, 15, 6, 21, 10,
            23, 19, 12, 4, 26, 8,
            16, 7, 27, 20, 13, 2,
            41, 52, 31, 37, 47, 55,
            30, 40, 51, 45, 33, 48,
            44, 49, 39, 56, 34, 53,
            46, 42, 50, 36, 29, 32
        };
        static readonly byte[] ip = {
            58, 50, 42, 34, 26, 18, 10, 2,
            60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6,
            64, 56, 48, 40, 32, 24, 16, 8,
            57, 49, 41, 33, 25, 17, 9, 1,
            59, 51, 43, 35, 27, 19, 11, 3,
            61, 53, 45, 37, 29, 21, 13, 5,
            63, 55, 47, 39, 31, 23, 15, 7
        };
        static readonly byte[] invip = {
            40, 8, 48, 16, 56, 24, 64, 32,
            39, 7, 47, 15, 55, 23, 63, 31,
            38, 6, 46, 14, 54, 22, 62, 30,
            37, 5, 45, 13, 53, 21, 61, 29,
            36, 4, 44, 12, 52, 20, 60, 28,
            35, 3, 43, 11, 51, 19, 59, 27,
            34, 2, 42, 10, 50, 18, 58, 26,
            33, 1, 41, 9, 49, 17, 57, 25
        };
        static readonly byte[] e = {
            32, 1, 2, 3, 4, 5,
            4, 5, 6, 7, 8, 9,
            8, 9, 10, 11, 12,13,
            12, 13, 14, 15, 16, 17,
            16, 17,18, 19, 20,21,
            20, 21, 22, 23, 24, 25,
            24, 25, 26, 27, 28, 29,
            28, 29, 30, 31, 32, 1
        };
        static readonly byte[,,] sbox = {
            {
                { 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
                { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 },
                { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 },
                { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 }
            },
            {
                { 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 },
                { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 },
                { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 },
                { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 }
            },
            {
                { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
                { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 },
                { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
                { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 }
            },
            {
                { 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 },
                { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 },
                { 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 },
                { 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14 }
            },
            {
                { 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 },
                { 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 },
                { 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 },
                { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 }
            },
            {
                { 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 },
                { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 },
                { 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 },
                { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 }
            },
            {
                { 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 },
                { 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 },
                { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 },
                { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 }
            },
            {
                { 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 },
                { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 },
                { 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 },
                { 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 }
            }
        };
        static readonly byte[] p = {
            16, 7, 20, 21, 29, 12, 28, 17,
            1, 15, 23, 26, 5, 18, 31, 10,
            2, 8, 24, 14, 32, 27, 3, 9,
            19, 13, 30, 6, 22, 11, 4, 25
        };
        static void bin(ulong a)
        {
            for (int i = 7; i >= 0; i--)
            {
                Console.Write(Convert.ToString((byte)(a >> (i * 8)), 2).PadLeft(8, '0'));
                Console.Write(" ");
            }
            Console.WriteLine();
        }

        static ulong Permutate(ulong input, byte[] table)
        {
            ulong output = 0;
            foreach (byte b in table)
            {
                output = (output << 1) + ((input >> (64 - b)) & 1);
            }
            return output;
        }

        static readonly byte[] shifts = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

        static ulong[] GenSubKeys(ulong key)
        {
            uint[] c = new uint[17];
            uint[] d = new uint[17];
            ulong[] subkeys = new ulong[16];
            key = Permutate(key, pc1);
            d[0] = (uint)(key & 0xfffffff);
            c[0] = (uint)((key >> 28) & 0xfffffff);

            for (int i = 0; i < 16; i++)
            {
                c[i + 1] = c[i];
                d[i + 1] = d[i];
                for (int j = 0; j < shifts[i]; j++)
                {
                    c[i + 1] <<= 1;
                    c[i + 1] += ((c[i + 1] >> 28) & 1);
                    d[i + 1] <<= 1;
                    d[i + 1] += ((d[i + 1] >> 28) & 1);
                }
                c[i + 1] &= 0xfffffff;
                d[i + 1] &= 0xfffffff;
                bin(c[i + 1]);
                subkeys[i] = (ulong)c[i + 1] << 28 | d[i + 1];
                subkeys[i] = Permutate(subkeys[i], pc2);
            }

            return subkeys;
        }

        static uint F(uint a, ulong key)
        {
            ulong ker = key ^ Permutate(a, e);
            uint output = 0;
            for (int i = 0; i < 8; i++)
            {
                byte b = (byte)(ker & 0x3f); // take 6bits
                ker = ker >> 6; // shifts 6bits
                byte row = (byte)((b & 1) | (((b >> 5) & 1) << 1)); // a????b -> ab
                byte col = (byte)((b >> 1) & 0xf); // ?abcd? -> abcd
                byte s = sbox[i, row, col];
                output = (output << 4) | s; // combine every 4bits
            }
            output = (uint)Permutate(output, p);
            return output;
        }

        static ulong Des(ulong key, ulong msg, bool decrypt)
        {
            ulong[] keys = GenSubKeys(key);

            if (decrypt)
                for (int i = 0; i < 8; i++)
                {
                    ulong tmp = keys[i];
                    keys[i] = keys[15 - i];
                    keys[15 - i] = tmp;
                }

            ulong ipp = Permutate(msg, ip);

            uint[] l = new uint[17];
            uint[] r = new uint[17];
            l[0] = (uint)(ipp >> 32);
            r[0] = (uint)ipp;
            for (int i = 0; i < 16; i++)
            {
                l[i + 1] = r[i];
                r[i + 1] = l[i] ^ F(r[i], keys[i]);
            }

            ulong rev = ((ulong)r[16] << 32) | l[16];

            return Permutate(rev, invip);
        }

        public static ulong Encrypt(ulong key, ulong msg)
        {
            return Des(key, msg, false);
        }

        public static ulong Decrypt(ulong key, ulong msg)
        {
            return Des(key, msg, true);
        }

        public static byte[] Encrypt(ulong key, byte[] data)
        {
            byte padding = (byte)(8 - data.Length % 8);
            if (padding == 8) padding = 0;
            List<byte> inputPadded = new List<byte>();
            inputPadded.AddRange(data);
            for (int i = 0; i < padding; i++)
                inputPadded.Add(padding);

            data = inputPadded.ToArray();

            List<byte> output = new List<byte>();

            for (int i = 0; i < (data.Length + padding) / 8; i++)
            {
                ulong msg = BitConverter.ToUInt64(data, i * 8);
                byte[] dat = BitConverter.GetBytes(Encrypt(key, msg));
                output.AddRange(dat);
            }
            return output.ToArray();
        }

        public static byte[] Decrypt(ulong key, byte[] data)
        {
            List<byte> output = new List<byte>();

            for (int i = 0; i < data.Length / 8; i++)
            {
                ulong msg = BitConverter.ToUInt64(data, i * 8);
                byte[] dat = BitConverter.GetBytes(Decrypt(key, msg));
                output.AddRange(dat);
            }

            byte last_byte = output[output.Count - 1];
            int padding = last_byte;
            if (last_byte > 8)
                padding = 0;
            else
            {
                for (int i = 0; i < last_byte; i++)
                {
                    if(output[output.Count - 1 - i] != last_byte)
                    {
                        padding = 0;
                        break;
                    }
                }
            }
            if(padding != 0)
                output.RemoveRange(output.Count - padding, padding);
            return output.ToArray();
        }
    }
}
