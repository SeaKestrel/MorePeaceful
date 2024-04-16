using System;
using UnityEngine;
using Winch.Core;

namespace MorePeaceful
{
	public class MorePeaceful : MonoBehaviour
	{
        public bool Passive { get; set; }

		[NonSerialized]
		public Action OnGameModeChanged;

		public static MorePeaceful Instance { get; private set; }
		public void Awake()
        {
			Instance = this;

			OnGameModeChanged += GameModeChange; // Add GamemodeChanged event
			GameManager.Instance.OnGameStarted += GameStarted; // Add handler to OnGameStarted event

            WinchCore.Log.Debug($"{nameof(MorePeaceful)} has loaded!");
        }

        public void Update()
		{
			if (Instance == null) return;

			// GamemodeChanged event declencher
			if (GameManager.Instance._saveManager.activeSettingsData.CurrentGameMode() == GameMode.PASSIVE && !Passive) OnGameModeChanged?.Invoke();
			else if (GameManager.Instance._saveManager.activeSettingsData.CurrentGameMode() != GameMode.PASSIVE && Passive) OnGameModeChanged?.Invoke();
		}

		// GameModeChange handler
		public void GameModeChange()
		{
            if (!Passive) // if the gamemode changed to passive
			{
                WinchCore.Log.Info("Gamemode changed to passive, saved sanity: "+ GameManager.Instance.Player._sanity._sanity);
                GameManager.Instance._saveManager.activeSaveData.SetFloatVariable("CustomSanity", GameManager.Instance.Player._sanity._sanity); // Trying to save the sanity into the settings
                Passive = !Passive; // Change the passive bool
            } else // if the gamemode changed to normal
			{
                Passive = !Passive; // Change the passive bool
                WinchCore.Log.Info("Gamemode changed to normal, loaded sanity: " + GameManager.Instance._saveManager.activeSaveData.GetFloatVariable("CustomSanity", 1));
                GameManager.Instance.Player._sanity._sanity = GameManager.Instance._saveManager.activeSaveData.GetFloatVariable("CustomSanity", 1);
            }
        }

		public void GameWindow()
		{
            // If the gamemode is passive, init the var in the "main" at true, else false
            if (GameManager.Instance.SettingsSaveData.gameMode == 0) Passive = true;
            else Passive = false;
        }

		// GameStarted handler
		public void GameStarted()
		{
			WinchCore.Log.Info("Game started");
            if (GameManager.Instance._saveManager.activeSaveData.GetFloatVariable("CustomSanity", 2f) == 2f)
			{
				WinchCore.Log.Info("No sanity saved. Registering one");
                GameManager.Instance._saveManager.activeSaveData.SetFloatVariable("CustomSanity", 1f);
            }
		}
    }
}
