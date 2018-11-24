using System;
using System.Text;
using System.Windows.Forms;
using System.Numerics;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace RSAGUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ParallelPrime.Size = 2048 / 2;
            ParallelPrime.Start();
        }

        private void buttonKeyGen_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            {
                string[] keys = RSA.KeyGen(2048);

                textBoxN.Text = keys[0];
                textBoxE.Text = keys[1];
                textBoxD.Text = keys[2];

                try
                {
                    BigInteger a = new BigInteger("1234567890");
                    BigInteger enc = RSA.Encrypt(a, BigInteger.Parse(keys[0]), BigInteger.Parse(keys[1]));
                    BigInteger dec = RSA.Decrypt(enc, BigInteger.Parse(keys[2]), BigInteger.Parse(keys[0]));
                    if (a != dec)
                    {
                        MessageBox.Show("Bad1");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Bad2");
                }

            }
            sw.Stop();



            MessageBox.Show("Keys generated in " + sw.ElapsedMilliseconds + "ms");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ParallelPrime.Stop();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            if(textBoxMsg.Text.Length == 0 || textBoxE.Text.Length == 0 || textBoxN.Text.Length == 0)
            {
                MessageBox.Show("Message and public key cannot be empty!");
                return;
            }

            BigInteger input = new BigInteger(Encoding.UTF8.GetBytes(textBoxMsg.Text + "\x01"));
            BigInteger n = BigInteger.Parse(textBoxN.Text);
            BigInteger ee = BigInteger.Parse(textBoxE.Text);

            BigInteger outp = RSA.Encrypt(input, n, ee);

            textBoxCode.Text = outp.ToString();
            textBoxMsg.Text = "";
        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            if (textBoxCode.Text.Length == 0 || textBoxD.Text.Length == 0 || textBoxN.Text.Length == 0)
            {
                MessageBox.Show("Code and private key cannot be empty!");
                return;
            }
            BigInteger input = BigInteger.Parse(textBoxCode.Text);
            BigInteger n = BigInteger.Parse(textBoxN.Text);
            BigInteger d = BigInteger.Parse(textBoxD.Text);

            BigInteger outp = RSA.Decrypt(input, d, n);

            byte[] outpb = outp.ToByteArray();

            for (int i = outpb.Length - 1; outpb[i] == 0; i--)
                Array.Resize(ref outpb, outpb.Length - 1);

            textBoxMsg.Text = Encoding.UTF8.GetString(outp.ToByteArray()).Substring(0, outpb.Length - 1);
            textBoxCode.Text = "";
        }

        private void buttonEncryptFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] input = File.ReadAllBytes(openFileDialog.FileName);
                    string output = "";

                    for (int i = 0; i < input.Length; i += 254)
                    {
                        byte[] data = input.Skip(i).Take(254).Concat(new byte[] { 1 }).ToArray();      

                        BigInteger n = BigInteger.Parse(textBoxN.Text);
                        BigInteger ee = BigInteger.Parse(textBoxE.Text);

                        BigInteger outp = RSA.Encrypt(new BigInteger(data), n, ee);
                        output += Convert.ToBase64String(outp.ToByteArray()) + Environment.NewLine;
                    }

                    File.WriteAllText(saveFileDialog.FileName, output);
                }
            }
        }

        private void buttonDecryptFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] input = File.ReadAllLines(openFileDialog.FileName);
                    List<byte> output = new List<byte>();

                    foreach(string line in input)
                    {
                        byte[] data = Convert.FromBase64String(line);

                        BigInteger n = BigInteger.Parse(textBoxN.Text);
                        BigInteger d = BigInteger.Parse(textBoxD.Text);

                        BigInteger outp = RSA.Decrypt(new BigInteger(data), d, n);

                        byte[] outpb = outp.ToByteArray();

                        for (int i = outpb.Length - 1; outpb[i] == 0; i--)
                            Array.Resize(ref outpb, outpb.Length - 1);

                        Array.Resize(ref outpb, outpb.Length - 1);

                        output.AddRange(outpb);
                    }

                    File.WriteAllBytes(saveFileDialog.FileName, output.ToArray());
                }
            }
        }

        private void poolTimer_Tick(object sender, EventArgs e)
        {
            int pool = ParallelPrime.GetPrimePoolSize();
            labelPool.Text = "Pool: " + pool.ToString();
        }
    }
}
