using Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AngularBasicVsix
{
    public partial class UserInputForm : Form 
    {
        //////////////////////////////////////////////private Label label1;
        //////////////////////////////////////////////private Button btnOk;
        //////////////////////////////////////////////private Button btnCancel;

        ////////////////////////////////////////////private void btnOk_Click(object sender, EventArgs e)
        ////////////////////////////////////////////{
        ////////////////////////////////////////////    Close();
        ////////////////////////////////////////////}

        ////////////////////////////////////////////private void btnCancel_Click(object sender, EventArgs e)
        ////////////////////////////////////////////{
        ////////////////////////////////////////////    Close();
        ////////////////////////////////////////////}

        public UserInputForm(string solutionDirectory, string tempDirectory, string generatorName, string regularProjectName)
        {
            SuspendLayout();

            #region Add form controls
            #region Form text
            var label1 = new Label();
            label1.AutoSize = true;
            label1.Location = new Point(25, 22);
            label1.Name = "label1";
            //label1.Size = new Size(35, 13);
            //label1.TabIndex = 0;
            label1.Text = DialogHelper.GetLabelText(solutionDirectory, tempDirectory, generatorName, regularProjectName);
            Controls.Add(label1);
            #endregion

            #region Okay button
            var btnOk = new Button();
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(464, 202);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 23);
            btnOk.TabIndex = 0;/////////////////// 1;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += new EventHandler(btnOkCancel_Click);
            Controls.Add(btnOk);
            #endregion

            #region Cancel button
            var btnCancel = new Button();
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(559, 202);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;///////////////////// 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += new EventHandler(btnOkCancel_Click);
            Controls.Add(btnCancel);
            #endregion
            #endregion

            #region Define form settings
            AutoSize = true;
            //ClientSize = new Size(646, 237);//gregt auto size this ?
            Name = "UserInputForm";
            ResumeLayout(false);
            #endregion

            PerformLayout();
        }

        private void btnOkCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}