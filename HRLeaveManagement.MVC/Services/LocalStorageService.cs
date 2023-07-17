using Hanssens.Net;
using HRLeaveManagement.MVC.Contracts;
using System.Collections.Generic;

namespace HRLeaveManagement.MVC.Services
{
	public class LocalStorageService : ILocalStorageService
	{
		private LocalStorage _localStorage;

        public LocalStorageService()
        {
			_localStorage = new LocalStorage(new LocalStorageConfiguration
			{
				AutoLoad = true,
				AutoSave = true,
				Filename = "HR.LEAVEMGMT"
			});
        }

		public void ClearStorage(List<string> keys)
		{
			foreach (string key in keys)
			{
				_localStorage.Remove(key);
			}
		}

		public void SetStorageValue<T>(string key, T value)
		{
			_localStorage.Store(key, value);
			_localStorage.Persist();
		}

		public T GetStorageValue<T>(string key) =>
			_localStorage.Get<T>(key);

		public bool Exists(string key) =>
			_localStorage.Exists(key);
	}
}
