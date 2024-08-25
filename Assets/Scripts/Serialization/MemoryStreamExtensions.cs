using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Game.Serialization {
	public static class MemoryStreamExtensions {
		public static void WriteInt32(this MemoryStream stream, int value) {
			stream.Write(BitConverter.GetBytes(value));
		}
		public static void WriteStringWithLength(this MemoryStream stream, string value) {
			if (value == null) {
				stream.WriteInt32(0);
				return;
			}
			var array = Encoding.UTF8.GetBytes(value);
			stream.WriteInt32(array.Length);
			if (array.Length > 0) {
				stream.Write(array);
			}
		}
		public static void WriteBool(this MemoryStream stream, bool value) {
			stream.Write(BitConverter.GetBytes(value));
		}
		public static int WriteGuid(this MemoryStream stream, Guid value) {
			var array = value.ToByteArray();
			stream.Write(array);
			return array.Length;
		}
		public static void WriteFloat(this MemoryStream stream, float value) {
			stream.Write(BitConverter.GetBytes(value));
		}
		public static void WriteVector2(this MemoryStream stream, Vector2 value) {
			stream.WriteFloat(value.x);
			stream.WriteFloat(value.y);
		}

		public static int ReadInt32(this MemoryStream stream) {
			var array = new byte[4];
			stream.Read(array);
			return BitConverter.ToInt32(array, 0);
		}
		public static string ReadString(this MemoryStream stream, int length) {
			if (length <= 0) {
				return null;
			}
			var array = new byte[length];
			stream.Read(array, 0, length);
			return Encoding.UTF8.GetString(array);
		}
		public static string ReadStringWithLength(this MemoryStream stream) {
			var len = stream.ReadInt32();
			return stream.ReadString(len);
		}
		public static bool ReadBool(this MemoryStream stream) {
			return BitConverter.ToBoolean(new byte[] { (byte)stream.ReadByte() });
		}
		public static Guid ReadGuid(this MemoryStream stream) {
			var array = new byte[16];
			stream.Read(array);
			return new Guid(array);
		}
		public static float ReadFloat(this MemoryStream stream) {
			var array = new byte[4];
			stream.Read(array, 0, 4);
			return BitConverter.ToSingle(array, 0);
		}
		public static Vector2 ReadVector2(this MemoryStream stream) {
			var x = stream.ReadFloat();
			var y = stream.ReadFloat();
			return new Vector2(x, y);
		}
	}
}