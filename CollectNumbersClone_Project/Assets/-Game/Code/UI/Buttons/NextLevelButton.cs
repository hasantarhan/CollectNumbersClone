using Game.Base;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class NextLevelButton : MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(Click);
        }

        private void Click()
        {
            DataManager.Player.Level++;
            DataManager.Player.Save();
            SceneManager.LoadScene(0);
        }
    }
}