using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Game.Code.UI
{
    public class RetryButton : MonoBehaviour
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
            SceneManager.LoadScene(0);
        }
    }
}