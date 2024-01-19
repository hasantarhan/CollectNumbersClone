using Game.Base;
using TMPro;
using UnityEngine;

namespace _Game.UI.Texts
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LevelText : MonoBehaviour
    {
        private readonly string header = "LEVEL ";
        private TextMeshProUGUI text;

        private void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
            text.text = header + (DataManager.Player.Level + 1);
        }
    }
}