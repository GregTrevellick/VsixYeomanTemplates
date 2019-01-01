using Common;
using Microsoft.VisualStudio.PlatformUI;
using System.Windows;

namespace AngularBasicVsix
{
    public partial class PopUpDialog : DialogWindow
    {
        public PopUpDialog(string solutionDirectory, string tempDirectory, string generatorName, string regularProjectName)
        {
            InitializeComponent();
            InitializePopUpDialog(solutionDirectory, tempDirectory, generatorName, regularProjectName);
            //btnOk.Click += new EventHandler(btnOkCancel_Click);
            //btnCancel.Click += new EventHandler(btnOkCancel_Click);
        }

        private void InitializePopUpDialog(string solutionDirectory, string tempDirectory, string generatorName, string regularProjectName)
        {
            Title = "gregt title";
            ResizeMode = ResizeMode.CanResize;
            HasMaximizeButton = true;
            HasMinimizeButton = true;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            AppTextBlockQuotation.Text = DialogHelper.GetLabelText(solutionDirectory, tempDirectory, generatorName, regularProjectName);

            this.Show();
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
    }
}
