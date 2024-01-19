using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Code
{
    public class GoalIndicator : MonoBehaviour
    {
        private TMP_Text amountText;
        private ColorView goalColorView;
        public GameObject done;
        private Goal goal;
        public Goal Goal
        {
            get => goal;
            set => goal = value;
        }
        private int amount;
        public int Amount
        {
            get
            {
                return amount;
            }
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
        private void Awake()
        {
        }

        public void Setup(Goal goal)
        {
            goalColorView = GetComponentInChildren<ColorView>();

            this.Goal = goal;
            Amount = goal.amount;
            goalColorView.SetType(goal.type);
        }
    }
}