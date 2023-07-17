using System.Collections.Generic;

namespace HRLeaveManagement.MVC.Contracts
{
	public interface ILocalStorageService
	{
		public void ClearStorage(List<string> keys);

		public bool Exists(string key);

		public void SetStorageValue<T>(string key, T value);

		public T GetStorageValue<T>(string key);
	}
}
