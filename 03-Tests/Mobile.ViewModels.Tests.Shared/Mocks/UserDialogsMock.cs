using System;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Splat;

namespace Mobile.ViewModels.Tests.Shared.Mocks
{
    public class UserDialogsMock : IUserDialogs
    {
        public IDisposable ActionSheet(ActionSheetConfig config)
        {
            return new DisposableMock();
        }

        public Task<string> ActionSheetAsync(string title, string cancel, string destructive, CancellationToken? cancelToken = default(CancellationToken?), params string[] buttons)
        {
            return Task.FromResult("");
        }

        public IDisposable Alert(string message, string title = null, string okText = null)
        {
            return new DisposableMock();
        }

        public IDisposable Alert(AlertConfig config)
        {
            return new DisposableMock();
        }

        public Task AlertAsync(string message, string title = null, string okText = null, CancellationToken? cancelToken = default(CancellationToken?))
        {
            return Task.CompletedTask;
        }

        public Task AlertAsync(AlertConfig config, CancellationToken? cancelToken = default(CancellationToken?))
        {
            return Task.CompletedTask;
        }

        public IDisposable Confirm(ConfirmConfig config)
        {
            return new DisposableMock();
        }

        public Task<bool> ConfirmAsync(string message, string title = null, string okText = null, string cancelText = null, CancellationToken? cancelToken = default(CancellationToken?))
        {
            return Task.FromResult(true);
        }

        public Task<bool> ConfirmAsync(ConfirmConfig config, CancellationToken? cancelToken = default(CancellationToken?))
        {
            return Task.FromResult(true);
        }

        public IDisposable DatePrompt(DatePromptConfig config)
        {
            return new DisposableMock();
        }

        public Task<DatePromptResult> DatePromptAsync(DatePromptConfig config, CancellationToken? cancelToken = default(CancellationToken?))
        {
            return Task.FromResult(new DatePromptResult(true, DateTime.Now));
        }

        public Task<DatePromptResult> DatePromptAsync(string title = null, DateTime? selectedDate = default(DateTime?), CancellationToken? cancelToken = default(CancellationToken?))
        {
            return Task.FromResult(new DatePromptResult(true, DateTime.Now));
        }

        public void HideLoading()
        {
            return;
        }

        public IProgressDialog Loading(string title = null, Action onCancel = null, string cancelText = null, bool show = true, MaskType? maskType = default(MaskType?))
        {
            return new ProgressDialogMock();
        }

        public IDisposable Login(LoginConfig config)
        {
            return new DisposableMock();
        }

        public Task<LoginResult> LoginAsync(string title = null, string message = null, CancellationToken? cancelToken = default(CancellationToken?))
        {
            return Task.FromResult(new LoginResult(true, "", ""));
        }

        public Task<LoginResult> LoginAsync(LoginConfig config, CancellationToken? cancelToken = default(CancellationToken?))
        {
            return Task.FromResult(new LoginResult(true, "", ""));
        }

        public IProgressDialog Progress(ProgressDialogConfig config)
        {
            return new ProgressDialogMock();
        }

        public IProgressDialog Progress(string title = null, Action onCancel = null, string cancelText = null, bool show = true, MaskType? maskType = default(MaskType?))
        {
            return new ProgressDialogMock();
        }

        public IDisposable Prompt(PromptConfig config)
        {
            return new DisposableMock();
        }

        public Task<PromptResult> PromptAsync(string message, string title = null, string okText = null, string cancelText = null, string placeholder = "", InputType inputType = InputType.Default, CancellationToken? cancelToken = default(CancellationToken?))
        {
            return Task.FromResult(new PromptResult(true, ""));
        }

        public Task<PromptResult> PromptAsync(PromptConfig config, CancellationToken? cancelToken = default(CancellationToken?))
        {
            return Task.FromResult(new PromptResult(true, ""));
        }

        public void ShowError(string message, int timeoutMillis = 2000)
        {
            return;
        }

        public void ShowImage(IBitmap image, string message, int timeoutMillis = 2000)
        {
            return;
        }

        public void ShowLoading(string title = null, MaskType? maskType = default(MaskType?))
        {
            return;
        }

        public void ShowSuccess(string message, int timeoutMillis = 2000)
        {
            return;
        }

        public IDisposable TimePrompt(TimePromptConfig config)
        {
            return new DisposableMock();
        }

        public Task<TimePromptResult> TimePromptAsync(TimePromptConfig config, CancellationToken? cancelToken = default(CancellationToken?))
        {
            return Task.FromResult(new TimePromptResult(true, DateTime.Now.TimeOfDay));
        }

        public Task<TimePromptResult> TimePromptAsync(string title = null, TimeSpan? selectedTime = default(TimeSpan?), CancellationToken? cancelToken = default(CancellationToken?))
        {
            return Task.FromResult(new TimePromptResult(true, DateTime.Now.TimeOfDay));
        }

        public IDisposable Toast(string title, TimeSpan? dismissTimer = default(TimeSpan?))
        {
            return new DisposableMock();
        }

        public IDisposable Toast(ToastConfig cfg)
        {
            return new DisposableMock();
        }
    }
}
