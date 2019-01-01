using Common;
using Common.Dtos;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.Generic;
using System.Windows;

namespace AngularBasicVsix
{
    public partial class PopUpDialog : DialogWindow
    {
        private string _generatorName;
        private YoProcessor _yoProcessor;
        private FileSystemDto _dto;

        public PopUpDialog(string generatorName, Dictionary<string, string> replacementsDictionary)
        {
            _generatorName = generatorName;
            _yoProcessor = new YoProcessor(_generatorName);
            _dto = _yoProcessor.Initialise(replacementsDictionary);

            InitializeComponent();
            InitializePopUpDialog();
        }

        private void InitializePopUpDialog()
        {
            AppTextBlock.Text = DialogHelper.GetLabelText(_dto.SolutionDirectory, _dto.TempDirectory, _generatorName, _dto.RegularProjectName);
            HasMaximizeButton = true;
            HasMinimizeButton = true;
            ResizeMode = ResizeMode.CanResize;
            SizeToContent = SizeToContent.WidthAndHeight;
            Title = "gregt title";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void BtnOkay_OnClick(object sender, RoutedEventArgs e)
        {
            _yoProcessor.Generate();
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            _yoProcessor.ArchiveRegularProject(_dto.SolutionDirectory, _dto.TempDirectory);
            Close();

            //...from trivial
            //var parentWindow = Window.GetWindow((DependencyObject)sender);
            //if (parentWindow != null)
            //{
            //    parentWindow.Close();
            //}
        }

        //private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        //{
        //    var parentWindow = Window.GetWindow((DependencyObject)sender);
        //    if (parentWindow != null)
        //    {
        //        parentWindow.Close();
        //    }
        //}
    }
}
