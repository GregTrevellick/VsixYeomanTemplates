using Microsoft.VisualStudio.PlatformUI;
using System.Windows;

namespace AngularBasicVsix
{
    public partial class PopUpDialog : DialogWindow
    {
        public PopUpDialog(string solutionDirectory, string tempDirectory, string generatorName, string regularProjectName)
        {
            InitializeComponent();
            InitializePopUpDialog();

            //var label1 = new Label
            //{
            //    AutoSize = true,
            //    //Location = new Point(25, 22),//gregt auto-position ?
            //    Name = "label1",
            //    Text = DialogHelper.GetLabelText(solutionDirectory, tempDirectory, generatorName, regularProjectName)
            //};

            //var btnOk = new Button
            //{
            //    DialogResult = DialogResult.OK,
            //    Location = new Point(label1.Location.X, 400),//gregt auto-position ?
            //    Name = "btnOk",
            //    TabIndex = 0,
            //    Text = DialogHelper.Ok,
            //};

            //var btnCancel = new Button
            //{
            //    DialogResult = DialogResult.Cancel,
            //    Location = new Point(btnOk.Location.X + 77, btnOk.Location.Y),//gregt auto-position ?
            //    Name = "btnCancel",
            //    TabIndex = 1,
            //    Text = "Cancel",
            //};

            //// Button click events
            //btnOk.Click += new EventHandler(btnOkCancel_Click);
            //btnCancel.Click += new EventHandler(btnOkCancel_Click);
        }

        //private Size CommonButtonSize()
        //{
        //    return new Size(75, 23);
        //}

        private void InitializePopUpDialog()
        {
            HasMaximizeButton = true;
            HasMinimizeButton = true;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void BtnOkay_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow((DependencyObject)sender);

            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        ///////// <summary>
        ///////// AutoSizeMode.GrowAndShrink
        /////////  The control grows or shrinks to fit its contents.
        /////////  The control cannot be resized manually.
        ///////// AutoSizeMode.GrowOnly
        /////////  The control grows as much as necessary to fit its contents but does not shrink smaller than the value of its System.Windows.Forms.Control.Size property.
        /////////  The form can be resized, but cannot be made so small that any of its contained controls are hidden.
        ///////// </summary>
        //////private void DefineFormSettings()
        //////{
        //////    AutoSize = true;
        //////    AutoSizeMode = AutoSizeMode.GrowOnly;
        //////    ClientSize = new Size(200, 100);
        //////    Font = new Font(Font.FontFamily, Font.SizeInPoints + 1);
        //////    Name = "UserInputForm";
        //////}
    }
}
