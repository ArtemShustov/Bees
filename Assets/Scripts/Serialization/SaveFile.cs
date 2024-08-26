using System.IO;
using UnityEngine;

namespace Game.Serialization {
	public class SaveFile {
		public string Name { get; private set; }

		public string FullPath => Path.Combine(Application.persistentDataPath, $"{Name}.binsave");

		public SaveFile(string name = "default") {
			Name = name;
		}

		public bool Exists() {
			return File.Exists(FullPath);
		}
		public void WriteData(byte[] data) {
			using (var file = File.OpenWrite(FullPath)) {
				file.Write(data, 0, data.Length);
			}
		}
		public byte[] ReadData() {
			byte[] data;
			using (var file = File.OpenRead(FullPath)) {
				data = new byte[file.Length];
				file.Read(data, 0, data.Length);
			}
			return data;
		}
	}
}