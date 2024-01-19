using System;
using EventArch;
using UnityEngine;

namespace _Game.Code.UI
{
    public class InGame : MonoBehaviour
    {
        public GameObject content;
        private void OnEnable()
        {
            content.SetActive(true);
            EventManager.AddListener<OnFinishGame>(HideMenu);
            EventManager.AddListener<OnStartGame>(ShowMenu);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<OnFinishGame>(HideMenu);
            EventManager.RemoveListener<OnStartGame>(ShowMenu);
        }

        private void HideMenu(OnFinishGame finishGame)
        {
           content.SetActive(false);
        }

        private void ShowMenu(OnStartGame startGame)
        {
            content.SetActive(true);
        }
    }
}