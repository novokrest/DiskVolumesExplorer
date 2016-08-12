using Prism.Mvvm;

namespace DiskVolumesExplorer.Client.Base
{
    class CleanUpViewModel : BindableBase
    {
        private bool _cancelClose;
        private bool _cleanUp;

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

        public bool CleanUp
        {
            get { return _cleanUp; }
            set
            {
                if (_cleanUp != value)
                {
                    _cleanUp = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
