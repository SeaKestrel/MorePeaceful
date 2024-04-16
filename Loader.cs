using HarmonyLib;
using MorePeaceful.Libs;
using MorePeaceful.UI;
using UnityEngine;

namespace MorePeaceful
{
	public class Loader
	{
		/// <summary>
		/// This method is run by Winch to initialize your mod
		/// </summary>
		public static void Initialize()
		{
			var gameObject = new GameObject(nameof(MorePeaceful));
			gameObject.AddComponent<MorePeaceful>();
			gameObject.AddComponent<CustomSceneManager>();
			gameObject.AddComponent<UIHelper>();
            GameObject.DontDestroyOnLoad(gameObject);
			GameManager.Instance._prodGameConfigData.hourDurationInSeconds = 24;
            new Harmony("com.dredge.moredredge").PatchAll();
        }
	}
}