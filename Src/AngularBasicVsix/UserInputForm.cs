using System;
using System.Windows.Forms;

namespace AngularBasicVsix
{
    public partial class UserInputForm : Form //gregt extract to common ???
    {
        private Label label1;
        private Button btnOk;
        private Button btnCancel;

        public UserInputForm(string solutionDirectory, string tempDirectory, string generatorName, string regularProjectName)
        {
            label1 = new Label();
            btnOk = new Button();
            btnCancel = new Button();
            SuspendLayout();

            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(25, 22);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(35, 13);
            label1.TabIndex = 0;
            label1.Text =
                $"The following will happen:" + Environment.NewLine +
                $" - A new project named '{regularProjectName}' will be created at {solutionDirectory}{Environment.NewLine}" +
                $" - A command prompt window will open and run the following commands{Environment.NewLine}" +
                $"    - npm install -g yo generator-{generatorName}{Environment.NewLine}" +
                $"    - yo {generatorName}{Environment.NewLine}" +
                $" - The new yeoman generated '{generatorName}' project will be launched in a NEW instance of Visual Studio{Environment.NewLine}" +
                $" - The '{regularProjectName}' project (which is actually just an empty folder) will be moved{Environment.NewLine}" +
                $" from{Environment.NewLine}" +
                $"      {solutionDirectory}{Environment.NewLine}" +
                $" to{Environment.NewLine}" +
                $"      {tempDirectory}{Environment.NewLine}" +
                $"{Environment.NewLine}Click OK to proceed.";//gregt extract to common

            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new System.Drawing.Point(464, 202);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(75, 23);
            btnOk.TabIndex = 1;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += new System.EventHandler(this.btnOk_Click);

            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(559, 202);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += new EventHandler(this.btnCancel_Click);

            ClientSize = new System.Drawing.Size(646, 237);//gregt auto size this ?
            Controls.Add(this.btnCancel);
            Controls.Add(this.btnOk);
            Controls.Add(this.label1);
            Name = "UserInputForm";
            ResumeLayout(false);
            PerformLayout();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}