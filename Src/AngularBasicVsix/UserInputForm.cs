using Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AngularBasicVsix
{
    public partial class UserInputForm : Form 
    {
        ////////////////private Label label1;
        ////////////////private Button btnOk;
        ////////////////private Button btnCancel;

        public UserInputForm(string solutionDirectory, string tempDirectory, string generatorName, string regularProjectName)
        {
            var label1 = new Label();
            var btnOk = new Button();
            var btnCancel = new Button();

            SuspendLayout();

            label1.AutoSize = true;
            label1.Location = new Point(25, 22);
            label1.Name = "label1";
            label1.Size = new Size(35, 13);
            label1.TabIndex = 0;
            label1.Text = YoProcessor.GetLabelText(solutionDirectory, tempDirectory, generatorName, regularProjectName);

            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(464, 202);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 23);
            btnOk.TabIndex = 1;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += new EventHandler(btnOkCancel_Click);

            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(559, 202);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += new EventHandler(btnOkCancel_Click);

            ClientSize = new Size(646, 237);//gregt auto size this ?
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(label1);
            Name = "UserInputForm";
            ResumeLayout(false);
            PerformLayout();
        }

        private void btnOkCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //////////////private void btnOk_Click(object sender, EventArgs e)
        //////////////{
        //////////////    Close();
        //////////////}

        //////////////private void btnCancel_Click(object sender, EventArgs e)
        //////////////{
        //////////////    Close();
        //////////////}
    }
}