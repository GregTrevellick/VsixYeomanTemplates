using Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AngularBasicVsix
{
    public partial class UserInputForm : Form 
    {
        public UserInputForm(string solutionDirectory, string tempDirectory, string generatorName, string regularProjectName)
        {
            SuspendLayout();

            AddFormControls(solutionDirectory, tempDirectory, generatorName, regularProjectName);

            DefineFormSettings();

            PerformLayout();
        }

        private void DefineFormSettings()
        {
            AutoSize = true;
            ////////////////////////////////////////////////////////ClientSize = new Size(646, 237);
            Name = "UserInputForm";
            ResumeLayout(false);
        }

        private void AddFormControls(string solutionDirectory, string tempDirectory, string generatorName, string regularProjectName)
        {
            var btnCancel = new Button
            {
                DialogResult = DialogResult.Cancel,
                Location = new Point(559, 202),//gregt auto-position ?
                Name = "btnCancel",
                Size = CommonButtonSize(),
                TabIndex = 1,
                Text = "Cancel",
                UseVisualStyleBackColor = true
            };

            var btnOk = new Button
            {
                DialogResult = DialogResult.OK,
                Location = new Point(464, 202),//gregt auto-position ?
                Name = "btnOk",
                Size = CommonButtonSize(),
                TabIndex = 0,
                Text = "OK",
                UseVisualStyleBackColor = true
            };

            var label1 = new Label
            {
                AutoSize = true,
                Location = new Point(25, 22),//gregt auto-position ?
                Name = "label1",
                ////////////////////////////////////////////////////////////////////////////////////////////////label1.Size = new Size(35, 13);
                Text = DialogHelper.GetLabelText(solutionDirectory, tempDirectory, generatorName, regularProjectName)
            };

            // Button click events
            btnCancel.Click += new EventHandler(btnOkCancel_Click);
            btnOk.Click += new EventHandler(btnOkCancel_Click);

            // Add controls to form
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(label1);
        }

        private Size CommonButtonSize()
        {
            return new Size(75, 23);
        }

        private void btnOkCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}