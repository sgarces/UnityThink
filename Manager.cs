using System;
using System.Collections;
using UnityEngine;

namespace Think
{
	public class Manager : MonoBehaviour
	{
		void Awake()
		{
		}
		
		void Start()
		{
		}
		
		void Update()
		{
			Debug.Log("Hello");
		}
		
		void LateUpdate()
		{
		}
		
		static public Manager Instance { 
			get {
				if (quitting)
					return null;
				
				if (sInstance == null)
				{
					sInstance = (Manager) FindObjectOfType(typeof(Manager));
					if (sInstance == null)
					{
						GameObject obj = new GameObject();
						obj.hideFlags = HideFlags.HideAndDontSave;
						sInstance = obj.AddComponent<Manager>();
						DontDestroyOnLoad(obj);
					}
				}
				return sInstance;
			}
		}
		private static Manager sInstance;

		private static bool quitting = false;
		public void OnDestroy()
		{
		}
	}
}

