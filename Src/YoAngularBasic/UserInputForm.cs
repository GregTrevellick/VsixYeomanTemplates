using System;
using System.Windows.Forms;

namespace YoAngularBasic
{
    public partial class UserInputForm : Form //gregt rename class ?
    {
        private Label label1;
        private Button btnOk;
        private Button btnCancel;

        public UserInputForm(string solutionDirectory, string tempDirectory, string generatorName)
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
                $" - A new temporary project will be created at {solutionDirectory}{Environment.NewLine}" +
                $" - A command prompt window will open and run the following commands{Environment.NewLine}" +
                $"    - npm install -g yo generator-{generatorName}{Environment.NewLine}" +
                $"    - yo {generatorName}{Environment.NewLine}" +
                $" - The new yeoman generated '{generatorName}' project will be launched in a NEW instance of Visual Studio{Environment.NewLine}" +
                $" - The temporary project (which is actually just an empty folder) will be moved{Environment.NewLine}" +
                $" from{Environment.NewLine}" +
                $"      {solutionDirectory}{Environment.NewLine}" +
                $" to{Environment.NewLine}" +
                $"      {tempDirectory}{Environment.NewLine}" +
                $"{Environment.NewLine}Click OK to proceed.";

            btnOk.Location = new System.Drawing.Point(464, 202);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(75, 23);
            btnOk.TabIndex = 1;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += new System.EventHandler(this.btnOk_Click);

            btnCancel.Location = new System.Drawing.Point(559, 202);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += new EventHandler(this.btnCancel_Click);

            ClientSize = new System.Drawing.Size(646, 237);
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



//public UserInputForm()
//{
//    //Size = new Size(200, 150);

//    //var label1 = new Label();
//    //label1.Location = new Point(10, 10);
//    //label1.Text = $"Will open cmd" + Environment.NewLine 
//    //    +
//    //    $"Click OK to proceed.";
//    //Controls.Add(label1);

//    //var button1 = new Button();
//    //button1.Location = new Point(10, 50);
//    //button1.Size = new Size(50, 25);
//    //button1.Text = "OK";
//    //button1.Click += button1_Click;
//    //Controls.Add(button1);

//    //var button2 = new Button();
//    //button2.Location = new Point(10, 50);
//    //button2.Size = new Size(50, 25);
//    //button2.Text = "CANCEL";
//    //button2.Click += button1_Click;
//    //Controls.Add(button2);

//}
