using Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AngularBasicVsix
{
    public partial class zUserInputForm : Form 
    {
        public zUserInputForm(string solutionDirectory, string tempDirectory, string generatorName, string regularProjectName)
        {
            SuspendLayout();
            AddFormControls(solutionDirectory, tempDirectory, generatorName, regularProjectName);
            DefineFormSettings();
            ResumeLayout(false);
            PerformLayout();
        }

        private void AddFormControls(string solutionDirectory, string tempDirectory, string generatorName, string regularProjectName)
        {
            var label1 = new Label
            {
                AutoSize = true,
                //Location = new Point(25, 22),//gregt auto-position ?
                Name = "label1",
                Text = DialogHelper.GetLabelText(solutionDirectory, tempDirectory, generatorName, regularProjectName)
            };

            var btnOk = new Button
            {
                DialogResult = DialogResult.OK,
                Location = new Point(label1.Location.X, 400),//gregt auto-position ?
                Name = "btnOk",
                TabIndex = 0,
                Text = DialogHelper.Ok,
            };

            var btnCancel = new Button
            {
                DialogResult = DialogResult.Cancel,
                Location = new Point(btnOk.Location.X+77, btnOk.Location.Y),//gregt auto-position ?
                Name = "btnCancel",
                TabIndex = 1,
                Text = "Cancel",
            };

            // Button click events
            btnOk.Click += new EventHandler(btnOkCancel_Click);
            btnCancel.Click += new EventHandler(btnOkCancel_Click);

            // Shared
            btnOk.Size = btnCancel.Size = CommonButtonSize();
            btnOk.UseVisualStyleBackColor = btnCancel.UseVisualStyleBackColor = true;
           
            // Add controls to form
            Controls.Add(label1);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
        }

        private Size CommonButtonSize()
        {
            return new Size(75, 23);
        }

        private void btnOkCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// AutoSizeMode.GrowAndShrink
        ///  The control grows or shrinks to fit its contents.
        ///  The control cannot be resized manually.
        /// AutoSizeMode.GrowOnly
        ///  The control grows as much as necessary to fit its contents but does not shrink smaller than the value of its System.Windows.Forms.Control.Size property.
        ///  The form can be resized, but cannot be made so small that any of its contained controls are hidden.
        /// </summary>
        private void DefineFormSettings()
        {            
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowOnly;
            ClientSize = new Size(200, 100);
            Font = new Font(Font.FontFamily, Font.SizeInPoints + 1);
            Name = "UserInputForm";
        }
    }
}