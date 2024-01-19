
using Game.Configs;
using TMPro;
using UnityEngine;


namespace Game.Entities
{
    public class GoalIndicator : MonoBehaviour
    {
        public GameObject done;
        private int amount;
        private TMP_Text amountText;
        private ColorView goalColorView;

        public Goal Goal { get; set; }

        public int Amount
        {
            get => amount;
            set
            {
                amount = value;
                amountText = GetComponentInChildren<TMP_Text>();

                amountText.text = amount.ToString();
                if (amount <= 0)
                {
                    amountText.gameObject.SetActive(false);
                    done.SetActive(true);
                }
            }
        }
        public void Setup(Goal goal)
        {
            goalColorView = GetComponentInChildren<ColorView>();
            Goal = goal;
            Amount = goal.amount;
            goalColorView.SetType(goal.type);
        }
    }
}