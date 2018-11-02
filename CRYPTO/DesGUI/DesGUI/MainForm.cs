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

namespace DesGUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            if (textBoxKey.Text.Length == 0 || textBoxMsg.Text.Length == 0)
            {
                MessageBox.Show("Key and Message cannot be empty.");
                return;
            }
            try
            {
                byte[] msg = Encoding.UTF8.GetBytes(textBoxMsg.Text);
                ulong key = ulong.Parse(textBoxKey.Text, System.Globalization.NumberStyles.HexNumber);

                byte[] code = DES.Encrypt(key, msg);

                textBoxCode.Text = Convert.ToBase64String(code);
                textBoxMsg.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!\r\n" + ex.Message);
            }
        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            if (textBoxKey.Text.Length == 0 || textBoxCode.Text.Length == 0)
            {
                MessageBox.Show("Key and Code cannot be empty.");
                return;
            }
            try
            {
                byte[] code = Convert.FromBase64String(textBoxCode.Text);
                ulong key = ulong.Parse(textBoxKey.Text, System.Globalization.NumberStyles.HexNumber);

                byte[] msg = DES.Decrypt(key, code);

                textBoxMsg.Text = Encoding.UTF8.GetString(msg);
                textBoxCode.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!\r\n" + ex.Message);
            }
        }

        private void buttonEncryptFile_Click(object sender, EventArgs e)
        {
            if (textBoxKey.Text.Length == 0)
            {
                MessageBox.Show("Key cannot be empty.");
                return;
            }
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string name = openFileDialog.FileName;
                    byte[] data = File.ReadAllBytes(name);
                    ulong key = ulong.Parse(textBoxKey.Text, System.Globalization.NumberStyles.HexNumber);

                    byte[] output = DES.Encrypt(key, data);

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(saveFileDialog.FileName, output);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!\r\n" + ex.Message);
            }
        }

        private void buttonDecryptFile_Click(object sender, EventArgs e)
        {
            if (textBoxKey.Text.Length == 0)
            {
                MessageBox.Show("Key cannot be empty.");
                return;
            }
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string name = openFileDialog.FileName;
                    byte[] data = File.ReadAllBytes(name);
                    ulong key = ulong.Parse(textBoxKey.Text, System.Globalization.NumberStyles.HexNumber);

                    byte[] output = DES.Decrypt(key, data);

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(saveFileDialog.FileName, output);
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
