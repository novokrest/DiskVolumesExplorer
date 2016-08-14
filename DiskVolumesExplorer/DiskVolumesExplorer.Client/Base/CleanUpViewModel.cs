using Prism.Mvvm;

namespace DiskVolumesExplorer.Client.Base
{
    class CleanUpViewModel : BindableBase
    {
        private bool _cancelClose;
        private bool _isProcessing;
        private string _processingMessage;

        public CleanUpViewModel()
        {
            CancelClose = true;
        }

        public bool CancelClose
        {
            get { return _cancelClose; }
            set
            {
                if (_cancelClose != value)
                {
                    _cancelClose = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsProcessing
        {
            get { return _isProcessing; }
            set
            {
                if (_isProcessing != value)
                {
                    _isProcessing = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ProcessingMessage
        {
            get { return _processingMessage; }
            set
            {
                if (_processingMessage != value)
                {
                    _processingMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public void StartProcessing(string message)
        {
            ProcessingMessage = message;
            IsProcessing = true;
        }

        public void StopProcessing()
        {
            IsProcessing = false;
            ProcessingMessage = null;
        }
    }
}
