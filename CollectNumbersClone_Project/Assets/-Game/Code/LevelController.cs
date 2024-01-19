using System;
using System.Collections.Generic;
using _Game.Code.Utils;
using DG.Tweening;
using EventArch;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Code
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private LevelData levelData;
        [SerializeField] private TMP_Text moveCountText;
        [SerializeField] private Transform boardSpawnPoint;
        [SerializeField] private Transform canvas;
        private VisualConfig visualConfig;
        private BoardController boardController;
        private List<GoalIndicator> goalIndicators = new();
        private Player player;
        private int moveCount;

        public int MoveCount
        {
            get => moveCount;
            set
            {
                moveCount = value;
                moveCountText.text = moveCount.ToString();
            }
        }

        private void Awake()
        {
            visualConfig = Resources.Load<VisualConfig>("Configs/VisualConfig");
        }

        public void Setup(LevelData levelData)
        {
            this.levelData = levelData;
            BoardSetup();
            LevelSetup();
        }

        private void BoardSetup()
        {
            var boardGo = new GameObject("board");
            boardGo.transform.position = boardSpawnPoint.position;
            boardController = boardGo.AddComponent<BoardController>();
            boardController.Setup(levelData.boardConfig);
            boardController.onMatchesFound += SetMatches;
        }

        private void OnDisable()
        {
            boardController.onMatchesFound -= SetMatches;
        }

        private void LevelSetup()
        {
            foreach (var goal in levelData.goalConfig.goals)
            {
                var goalIndicator = Instantiate(levelData.goalConfig.goalIndicatorPrefab, canvas.transform);
                goalIndicator.Setup(goal);
                goalIndicators.Add(goalIndicator);
            }

            MoveCount = levelData.goalConfig.moveCount;
            player = new GameObject("Player").AddComponent<Player>();
            player.onCellClicked += OnCellClicked;
        }

        private void OnCellClicked(Cell arg0)
        {
            if (MoveCount > 0)
            {
                MoveCount--;
                boardController.Increase(arg0);
            }
            else
            {
                Events.onFinishGame.WinState = false;
                EventManager.Broadcast(Events.onFinishGame);
            }
        }

        private void CheckWinState()
        {
            var winState = true;
            if (moveCount <= 0)
            {
                winState = !GoalIndicatorCheck();
            }
            else if (GoalIndicatorCheck())
            {
                return;
            }

            Events.onFinishGame.WinState = winState;
            EventManager.Broadcast(Events.onFinishGame);
            boardController.gameObject.SetActive(false);
            enabled = false;
        }

        private bool GoalIndicatorCheck()
        {
            foreach (var goalIndicator in goalIndicators)
            {
                if (goalIndicator.Amount > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void SetMatches(List<Cell> matches)
        {
            for (int i = 0; i < matches.Count; i++)
            {
                var cell = matches[i];
                var item = (NormalItem)cell.Item;
                var goalIndicator = goalIndicators.Find(x => x.Goal.type == item.ItemType);
                if (goalIndicator != null)
                {
                    if (goalIndicator.Amount > 0)
                    {
                        var ps = visualConfig.goalParticles.Find(x => x.type == item.ItemType).ps;
                        var spawnedPs = Instantiate(ps, cell.transform.position, Quaternion.identity);
                        spawnedPs.Play();
                        spawnedPs.transform.DOMove(goalIndicator.transform.position, 0.5f).OnComplete(() =>
                        {
                            Destroy(spawnedPs.gameObject);
                            goalIndicator.Amount -= 1;
                        });
                    }
                }
            }

            DOTween.Sequence().AppendInterval(0.5f).AppendCallback(delegate { CheckWinState(); });
        }
    }
}