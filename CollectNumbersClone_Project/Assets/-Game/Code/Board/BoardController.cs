using System.Collections.Generic;
using DG.Tweening;
using Game.Configs;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Board
{
    public class BoardController : MonoBehaviour
    {
        private Match3Board board;
        private BoardConfig boardConfig;
        public UnityAction<List<Cell>> onMatchesFound;

        private void OnDisable()
        {
            board.Clear();
        }

        public void Setup(BoardConfig boardConfig)
        {
            this.boardConfig = boardConfig;
            board = new Match3Board(transform, boardConfig);
            board.Fill();
            FindMatchesAndCollapse();
        }

        private void FindMatchesAndCollapse()
        {
            var matches = board.FindFirstMatch();
            onMatchesFound?.Invoke(matches);
            if (matches.Count > 0) CollapseMatches(matches, null);
        }

        private void CollapseMatches(List<Cell> matches, Cell cellEnd)
        {
            for (int i = 0; i < matches.Count; i++) matches[i].ExplodeItem();

            ShiftDownItems();
        }

        public void Increase(Cell cell)
        {
            board.IncreaseItem(cell);
            DOTween.Sequence()
                .AppendInterval(0.2f)
                .AppendCallback(() => FindMatchesAndCollapse());
        }

        private void ShiftDownItems()
        {
            board.ShiftDownItems();
            DOTween.Sequence()
                .AppendInterval(0.2f)
                .AppendCallback(() => board.FillGapsWithNewItems())
                .AppendInterval(0.2f)
                .AppendCallback(() => FindMatchesAndCollapse());
        }
    }
}