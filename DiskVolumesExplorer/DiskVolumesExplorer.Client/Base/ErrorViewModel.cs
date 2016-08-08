using Prism.Mvvm;

namespace DiskVolumesExplorer.Client.Base
{
    internal abstract class ErrorViewModel : BindableBase
    {
        private bool _isError;
        private string _errorMessage;

        public bool IsError
        {
            get { return _isError; }
            set
            {
                if (_isError != value)
                {
                    _isError = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        protected void SetError(string errorMessage)
        {
            SetIsError(true, errorMessage);
        }

        protected void DisableError()
        {
            SetIsError(false);
        }

        private void SetIsError(bool isError, string errorMessage = null)
        {
            IsError = isError;
            ErrorMessage = errorMessage;
        }
    }
}
