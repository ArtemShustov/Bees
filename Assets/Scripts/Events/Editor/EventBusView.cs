using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Game.Events.Editor {
	public class EventBusView: UnityEditor.EditorWindow {
		private Vector2 _scrollPosition;
		private Type[] _types;
		
		private void Awake() {
			_types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(assembly => assembly.GetTypes())
				.Where(type => type.GetInterfaces().Contains(typeof(IGameEvent)))
				.Select(type => typeof(EventBus<>).MakeGenericType(type))
				.ToArray();
		}

		[MenuItem("Window/Event Bus View")]
		public static void ShowWindow() {
			GetWindow<EventBusView>("Event Bus View");
		}

		private void OnGUI() {
			GUILayout.Label("Events:", EditorStyles.boldLabel);
			
			EditorGUILayout.BeginScrollView(_scrollPosition, false, true);
			foreach (var type in _types) {
				GUILayout.Label($"{type.GetGenericArguments()[0]}");
			}
			EditorGUILayout.EndScrollView();
		}
	}
}