using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Serialization.Json {
	public class Vector3Converter: JsonConverter<Vector3> {
		public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer) {
			writer.WriteStartObject();
			writer.WritePropertyName("$type");
			writer.WriteValue("UnityEngine.Vector3, UnityEngine.CoreModule");
			writer.WritePropertyName("x");
			writer.WriteValue(value.x);
			writer.WritePropertyName("y");
			writer.WriteValue(value.y);
			writer.WritePropertyName("z");
			writer.WriteValue(value.z);
			writer.WriteEndObject();
		}

		public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer) {
			float x = 0, y = 0, z = 0;
			
			if (reader.TokenType == JsonToken.StartObject) {
				reader.Read();

				while (reader.TokenType == JsonToken.PropertyName) {
					string propertyName = reader.Value.ToString();
					reader.Read();

					switch (propertyName) {
						case "x":
							x = Convert.ToSingle(reader.Value);
							break;
						case "y":
							y = Convert.ToSingle(reader.Value);
							break;
						case "z":
							z = Convert.ToSingle(reader.Value);
							break;
					}

					reader.Read();
				}
			}

			return new Vector3(x, y, z);
		}
	}
}