using System;
using EventArch;
using UnityEngine;

namespace _Game.Code.UI
{
    public class EndScreen : MonoBehaviour
    {
        public GameObject content;
        public GameObject win;
        public GameObject fail;
        private void OnEnable()
        {
            EventManager.AddListener<OnFinishGame>(ShowMenu);
            content.SetActive(false);
            win.SetActive(false);
            fail.SetActive(false);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<OnFinishGame>(ShowMenu);
        }

        private void ShowMenu(OnFinishGame finishGame)
        {
            content.SetActive(true);
            win.SetActive(finishGame.WinState);
            fail.SetActive(!finishGame.WinState);
        }
    }
}