using System;
using Acr.UserDialogs;

namespace Mobile.ViewModels.Tests.Shared.Mocks
{
    public class ProgressDialogMock : IProgressDialog
    {
        public string Title { get; set; }
        public int PercentComplete { get; set; }

        public bool IsShowing => true;

        public void Dispose()
        {
            return;
        }

        public void Hide()
        {
            return;
        }

        public void Show()
        {
            return;
        }
    }
}
