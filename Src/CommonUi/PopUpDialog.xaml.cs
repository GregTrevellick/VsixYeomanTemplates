using CommonYo;
using CommonYo.Dtos;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace CommonUi
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
            BtnOkay.Focus();
            HasMaximizeButton = true;
            HasMinimizeButton = true;
            ResizeMode = ResizeMode.CanResize;
            SizeToContent = SizeToContent.WidthAndHeight;
            Title = _generatorName;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void BtnOkay_OnClick(object sender, RoutedEventArgs e)
        {
            _yoProcessor.Generate();
            //gregt get this working async (i.e. dont close & delete regular til new Yo created)
            //Task.Run(() => _yoProcessor.Generate()).GetAwaiter().GetResult();
            Close();
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)//corner x to also do archgive
        {
            Close(); 
        }

        /// <summary>
        /// Invoked by Close();
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            _yoProcessor.ArchiveRegularProject(_dto.SolutionDirectory, _dto.TempDirectory);
        }
    }
}
