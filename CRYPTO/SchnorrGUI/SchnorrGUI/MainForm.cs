using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace SchnorrGUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonGenGroup_Click(object sender, EventArgs e)
        {
            BigInteger[] group = Schnorr.GenerateGroup();

            textBoxP.Text = group[0].ToString();
            textBoxQ.Text = group[1].ToString();
            textBoxG.Text = group[2].ToString();

            MessageBox.Show("Done!");
        }

        private void buttonGenKeys_Click(object sender, EventArgs e)
        {
            BigInteger[] keys = Schnorr.GenerateKeys(new BigInteger[] { BigInteger.Parse(textBoxP.Text), BigInteger.Parse(textBoxQ.Text) , BigInteger.Parse(textBoxG.Text) });

            textBoxX.Text = keys[0].ToString();
            textBoxY.Text = keys[1].ToString();
        }

        private void buttonSign_Click(object sender, EventArgs e)
        {
            BigInteger[] group = new BigInteger[] { BigInteger.Parse(textBoxP.Text), BigInteger.Parse(textBoxQ.Text), BigInteger.Parse(textBoxG.Text) };
            BigInteger[] keys = new BigInteger[] { BigInteger.Parse(textBoxX.Text), BigInteger.Parse(textBoxY.Text) };

            BigInteger[] sign = Schnorr.Sign(Encoding.UTF8.GetBytes(textBoxMsg.Text), group, keys);

            textBoxN.Text = sign[0].ToString();
            textBoxE.Text = sign[1].ToString();
        }

        private void buttonVerify_Click(object sender, EventArgs e)
        {
            BigInteger[] group = new BigInteger[] { BigInteger.Parse(textBoxP.Text), BigInteger.Parse(textBoxQ.Text), BigInteger.Parse(textBoxG.Text) };
            BigInteger[] keys = new BigInteger[] { BigInteger.Parse(textBoxX.Text), BigInteger.Parse(textBoxY.Text) };
            BigInteger[] sign = new BigInteger[] { BigInteger.Parse(textBoxN.Text), BigInteger.Parse(textBoxE.Text) };

            if (Schnorr.Verify(Encoding.UTF8.GetBytes(textBoxMsg.Text), group, keys, sign))
                MessageBox.Show("Signature is valid!", "Valid", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Signature is *NOT* valid!", "Not valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonSignFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string name = openFileDialog.FileName;
                    byte[] data = File.ReadAllBytes(name);

                    BigInteger[] group = new BigInteger[] { BigInteger.Parse(textBoxP.Text), BigInteger.Parse(textBoxQ.Text), BigInteger.Parse(textBoxG.Text) };
                    BigInteger[] keys = new BigInteger[] { BigInteger.Parse(textBoxX.Text), BigInteger.Parse(textBoxY.Text) };

                    BigInteger[] sign = Schnorr.Sign(data, group, keys);

                    string output = group[0].ToString() + Environment.NewLine;
                    output += group[1].ToString() + Environment.NewLine;
                    output += group[2].ToString() + Environment.NewLine;
                    output += keys[1].ToString() + Environment.NewLine;
                    output += sign[0].ToString() + Environment.NewLine;
                    output += sign[1].ToString() + Environment.NewLine;

                    if (saveSigDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(saveSigDialog.FileName, output);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!\r\n" + ex.Message);
            }
        }

        private void buttonVerifyFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (openSigDialog.ShowDialog() == DialogResult.OK)
                    {
                        byte[] fdata = File.ReadAllBytes(openFileDialog.FileName);
                        string[] data = File.ReadAllLines(openSigDialog.FileName);

                        BigInteger[] group = new BigInteger[] { BigInteger.Parse(data[0]), BigInteger.Parse(data[1]), BigInteger.Parse(data[2]) };
                        BigInteger[] keys = new BigInteger[] { BigInteger.Zero, BigInteger.Parse(data[3]) };
                        BigInteger[] sign = new BigInteger[] { BigInteger.Parse(data[4]), BigInteger.Parse(data[5]) };

                        if (Schnorr.Verify(fdata, group, keys, sign))
                            MessageBox.Show("Signature is valid!", "Valid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Signature is *NOT* valid!", "Not valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!\r\n" + ex.Message);
            }
        }
    }
}
