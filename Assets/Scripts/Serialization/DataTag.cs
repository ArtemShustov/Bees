using System;
using System.Collections.Generic;

namespace Game.Serialization {
	[Serializable]
	public class DataTag {
		[Newtonsoft.Json.JsonProperty]
		private Dictionary<string, object> _data = new Dictionary<string, object>();

		public void Set<T>(string key, T value) {
			if (_data.ContainsKey(key)) {
				_data[key] = value;
			} else {
				_data.Add(key, value);
			}
		}
		public T Get<T>(string key) {
			return (T)_data[key];
		}
		public T Get<T>(string key, T defaultValue) {
			return (T)_data[key] ?? defaultValue;
		}
		public bool TryGet<T>(string key, out T value) {
			if (_data.TryGetValue(key, out var obj)) {
				if (obj is T v) {
					value = v;
					return true;
				}
			}
			value = default(T);
			return false;
		}

		public void SetLong(string key, long value) => Set<long>(key, value);
		public long GetLong(string key, long defaultValue) => Get<long>(key, defaultValue);
		public bool TryGetLong(string key, out long value) => TryGet<long>(key, out value);

		public void SetString(string key, string value) => Set<string>(key, value);
		public bool TryGetString(string key, out string value) => TryGet<string>(key, out value);

		public Guid GetGuid(string key, Guid defaultValue) => TryGetGuid(key, out var value) ? value : defaultValue;
		public bool TryGetGuid(string key, out Guid value) {
			if (TryGet(key, out value)) {
				return true;
			}
			if (TryGetString(key, out var str)) {
				if (Guid.TryParse(str, out value)) {
					return true;
				}
			}
			return false;
		}
		
		public void SetBool(string key, bool value) => Set<bool>(key, value);
		public bool GetBool(string key, bool defaultValue = false) => Get<bool>(key, defaultValue);
	}
}