using System;
using MorePeaceful.Attributes;
using MorePeaceful.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using Winch.Core;

namespace MorePeaceful.Libs
{
    internal class CustomSceneManager : MonoBehaviour
    {
        public static Action GameSceneLoaded;
        public static Action MainMenuSceneLoaded;

        public void Awake()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
            OnSceneChanged(default, SceneManager.GetActiveScene());
        }

        public void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }

        private static void OnSceneChanged(Scene prev, Scene current)
        {
            var sceneManagerObject = new GameObject(nameof(CustomSceneManager) + "Objects");
            WinchCore.Log.Info(current.name);
            switch (current.name)
            {
                case Scenes.Game:
                    foreach (var type in TypeHelper.GetTypesWithAttribute(typeof(AddToGameSceneAttribute)))
                    {
                        sceneManagerObject.AddComponent(type);
                    }
                    GameSceneLoaded?.Invoke();
                    break;

                case Scenes.Title:
                    foreach (var type in TypeHelper.GetTypesWithAttribute(typeof(AddToMainMenuSceneAttribute)))
                    {
                        sceneManagerObject.AddComponent(type);
                    }
                    MainMenuSceneLoaded?.Invoke();
                    break;

            }
        }
    }
}
