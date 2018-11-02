namespace SchnorrGUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonGenGroup = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxG = new System.Windows.Forms.TextBox();
            this.textBoxQ = new System.Windows.Forms.TextBox();
            this.textBoxP = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxY = new System.Windows.Forms.TextBox();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.buttonGenKeys = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxE = new System.Windows.Forms.TextBox();
            this.textBoxN = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxMsg = new System.Windows.Forms.TextBox();
            this.buttonSign = new System.Windows.Forms.Button();
            this.buttonVerify = new System.Windows.Forms.Button();
            this.buttonSignFile = new System.Windows.Forms.Button();
            this.buttonVerifyFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openSigDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveSigDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonGenGroup
            // 
            this.buttonGenGroup.Location = new System.Drawing.Point(452, 12);
            this.buttonGenGroup.Name = "buttonGenGroup";
            this.buttonGenGroup.Size = new System.Drawing.Size(75, 233);
            this.buttonGenGroup.TabIndex = 1;
            this.buttonGenGroup.Text = "Generate\r\ngroup";
            this.buttonGenGroup.UseVisualStyleBackColor = true;
            this.buttonGenGroup.Click += new System.EventHandler(this.buttonGenGroup_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxG);
            this.groupBox1.Controls.Add(this.textBoxQ);
            this.groupBox1.Controls.Add(this.textBoxP);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 233);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Group";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "g:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "q:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "p:";
            // 
            // textBoxG
            // 
            this.textBoxG.Location = new System.Drawing.Point(28, 132);
            this.textBoxG.Multiline = true;
            this.textBoxG.Name = "textBoxG";
            this.textBoxG.Size = new System.Drawing.Size(400, 84);
            this.textBoxG.TabIndex = 2;
            this.textBoxG.Text = resources.GetString("textBoxG.Text");
            // 
            // textBoxQ
            // 
            this.textBoxQ.Location = new System.Drawing.Point(28, 106);
            this.textBoxQ.Name = "textBoxQ";
            this.textBoxQ.Size = new System.Drawing.Size(400, 20);
            this.textBoxQ.TabIndex = 1;
            this.textBoxQ.Text = "752026659149698529193455616736653383516663559271";
            // 
            // textBoxP
            // 
            this.textBoxP.Location = new System.Drawing.Point(28, 16);
            this.textBoxP.Multiline = true;
            this.textBoxP.Name = "textBoxP";
            this.textBoxP.Size = new System.Drawing.Size(400, 84);
            this.textBoxP.TabIndex = 0;
            this.textBoxP.Text = resources.GetString("textBoxP.Text");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxY);
            this.groupBox2.Controls.Add(this.textBoxX);
            this.groupBox2.Location = new System.Drawing.Point(12, 251);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(434, 140);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Keys";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "y:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "x:";
            // 
            // textBoxY
            // 
            this.textBoxY.Location = new System.Drawing.Point(28, 42);
            this.textBoxY.Multiline = true;
            this.textBoxY.Name = "textBoxY";
            this.textBoxY.Size = new System.Drawing.Size(400, 84);
            this.textBoxY.TabIndex = 2;
            // 
            // textBoxX
            // 
            this.textBoxX.Location = new System.Drawing.Point(28, 16);
            this.textBoxX.Name = "textBoxX";
            this.textBoxX.Size = new System.Drawing.Size(400, 20);
            this.textBoxX.TabIndex = 0;
            // 
            // buttonGenKeys
            // 
            this.buttonGenKeys.Location = new System.Drawing.Point(452, 251);
            this.buttonGenKeys.Name = "buttonGenKeys";
            this.buttonGenKeys.Size = new System.Drawing.Size(75, 140);
            this.buttonGenKeys.TabIndex = 7;
            this.buttonGenKeys.Text = "Generate\r\nKeys";
            this.buttonGenKeys.UseVisualStyleBackColor = true;
            this.buttonGenKeys.Click += new System.EventHandler(this.buttonGenKeys_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.textBoxE);
            this.groupBox3.Controls.Add(this.textBoxN);
            this.groupBox3.Location = new System.Drawing.Point(533, 213);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(434, 74);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Signature";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "e:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "n:";
            // 
            // textBoxE
            // 
            this.textBoxE.Location = new System.Drawing.Point(28, 42);
            this.textBoxE.Name = "textBoxE";
            this.textBoxE.Size = new System.Drawing.Size(400, 20);
            this.textBoxE.TabIndex = 2;
            // 
            // textBoxN
            // 
            this.textBoxN.Location = new System.Drawing.Point(28, 16);
            this.textBoxN.Name = "textBoxN";
            this.textBoxN.Size = new System.Drawing.Size(400, 20);
            this.textBoxN.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxMsg);
            this.groupBox4.Location = new System.Drawing.Point(542, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(434, 166);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Message";
            // 
            // textBoxMsg
            // 
            this.textBoxMsg.Location = new System.Drawing.Point(6, 16);
            this.textBoxMsg.Multiline = true;
            this.textBoxMsg.Name = "textBoxMsg";
            this.textBoxMsg.Size = new System.Drawing.Size(419, 141);
            this.textBoxMsg.TabIndex = 0;
            this.textBoxMsg.Text = "Message to sign!";
            // 
            // buttonSign
            // 
            this.buttonSign.Location = new System.Drawing.Point(542, 184);
            this.buttonSign.Name = "buttonSign";
            this.buttonSign.Size = new System.Drawing.Size(75, 23);
            this.buttonSign.TabIndex = 9;
            this.buttonSign.Text = "Sign";
            this.buttonSign.UseVisualStyleBackColor = true;
            this.buttonSign.Click += new System.EventHandler(this.buttonSign_Click);
            // 
            // buttonVerify
            // 
            this.buttonVerify.Location = new System.Drawing.Point(623, 184);
            this.buttonVerify.Name = "buttonVerify";
            this.buttonVerify.Size = new System.Drawing.Size(75, 23);
            this.buttonVerify.TabIndex = 10;
            this.buttonVerify.Text = "Verify";
            this.buttonVerify.UseVisualStyleBackColor = true;
            this.buttonVerify.Click += new System.EventHandler(this.buttonVerify_Click);
            // 
            // buttonSignFile
            // 
            this.buttonSignFile.Location = new System.Drawing.Point(713, 184);
            this.buttonSignFile.Name = "buttonSignFile";
            this.buttonSignFile.Size = new System.Drawing.Size(121, 23);
            this.buttonSignFile.TabIndex = 11;
            this.buttonSignFile.Text = "Sign file";
            this.buttonSignFile.UseVisualStyleBackColor = true;
            this.buttonSignFile.Click += new System.EventHandler(this.buttonSignFile_Click);
            // 
            // buttonVerifyFile
            // 
            this.buttonVerifyFile.Location = new System.Drawing.Point(840, 184);
            this.buttonVerifyFile.Name = "buttonVerifyFile";
            this.buttonVerifyFile.Size = new System.Drawing.Size(121, 23);
            this.buttonVerifyFile.TabIndex = 12;
            this.buttonVerifyFile.Text = "Verify file";
            this.buttonVerifyFile.UseVisualStyleBackColor = true;
            this.buttonVerifyFile.Click += new System.EventHandler(this.buttonVerifyFile_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "File";
            // 
            // openSigDialog
            // 
            this.openSigDialog.FileName = "Signature";
            // 
            // saveSigDialog
            // 
            this.saveSigDialog.FileName = "Signature";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 403);
            this.Controls.Add(this.buttonVerifyFile);
            this.Controls.Add(this.buttonSignFile);
            this.Controls.Add(this.buttonVerify);
            this.Controls.Add(this.buttonSign);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonGenKeys);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonGenGroup);
            this.Name = "MainForm";
            this.Text = "Schnorr";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonGenGroup;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxG;
        private System.Windows.Forms.TextBox textBoxQ;
        private System.Windows.Forms.TextBox textBoxP;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxY;
        private System.Windows.Forms.TextBox textBoxX;
        private System.Windows.Forms.Button buttonGenKeys;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxE;
        private System.Windows.Forms.TextBox textBoxN;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxMsg;
        private System.Windows.Forms.Button buttonSign;
        private System.Windows.Forms.Button buttonVerify;
        private System.Windows.Forms.Button buttonSignFile;
        private System.Windows.Forms.Button buttonVerifyFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.OpenFileDialog openSigDialog;
        private System.Windows.Forms.SaveFileDialog saveSigDialog;
    }
}

