using StyletIoC;
using System;

namespace Eto.Forms.Plus
{
	public class WindowManager
	{
		private readonly IContainer _container;
		private readonly ViewFactory _viewFactory;
		private readonly Application _application;

		public WindowManager(IContainer container, ViewFactory viewFactory,
			Application application)
		{
			_container = container;
			_viewFactory = viewFactory;
			_application = application;
		}

        public void Exit()
        {
            _application.Quit();
        }

		public void RunOnUIThread(Action action)
		{
			_application.Invoke(action);
		}

		public T RunOnUIThread<T>(Func<T> func)
		{
			return _application.Invoke(func);
		}

		public TResult ShowDialog<TViewModel, TResult>(Control owner = null)
		{
			var control = CreateAndBind<TViewModel>() as Dialog<TResult>;
			if (control == null)
				return default(TResult);
			if (owner != null)
				return control.ShowModal(owner);
			else
				return control.ShowModal();
		}

		public TResult ShowDialog<TViewModel, TResult>(TViewModel viewModel, Control owner = null)
		{
			var control = _viewFactory.GetAndBind(viewModel) as Dialog<TResult>;
			if (control == null)
				return default(TResult);
			if (owner != null)
				return control.ShowModal(owner);
			else
				return control.ShowModal();
		}

		public void ShowDialog<TViewModel>(Control owner = null)
		{
			var control = CreateAndBind<TViewModel>() as Dialog;
			if (control == null)
				return;
			if (owner != null)
				control.ShowModal(owner);
			else
				control.ShowModal();
		}

		public void ShowDialog<TViewModel>(TViewModel viewModel, Control owner = null)
		{
			var control = _viewFactory.GetAndBind(viewModel) as Dialog;
			if (control == null)
				return;
			if (owner != null)
				control.ShowModal(owner);
			else
				control.ShowModal();
		}

		public void ShowForm<TViewModel>()
		{
			var control = CreateAndBind<TViewModel>() as Form;
			control?.Show();
		}

		public Control CreateAndBind<TViewModel>()
		{
			var viewModel = _container.Get<TViewModel>();
			return _viewFactory.GetAndBind(viewModel) as Control;
		}
	}
}
