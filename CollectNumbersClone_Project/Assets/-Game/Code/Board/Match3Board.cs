using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game.Code;
using UnityEngine;

public class Match3Board
{
    private int boardSizeX;
    private int boardSizeY;
    private Cell[,] cells;
    private Transform root;
    private int matchMin;
    private List<StartItem> startItems;
    public Match3Board(Transform rootTransform, BoardConfig boardConfig)
    {
        root = rootTransform;
        matchMin = boardConfig.matchesMin;
        startItems = boardConfig.startItems;
        this.boardSizeX = boardConfig.boardSizeX;
        this.boardSizeY = boardConfig.boardSizeY;
        cells = new Cell[boardSizeX, boardSizeY];
        CreateBoard();
    }

    private void CreateBoard()
    {
        var origin = new Vector3(-boardSizeX * 0.5f + 0.5f, -boardSizeY * 0.5f + 0.5f, 0f);
        var prefabBG = Resources.Load<GameObject>(CONSTANTS.PREFAB_CELL_BACKGROUND);
        for (int x = 0; x < boardSizeX; x++)
        {
            for (int y = 0; y < boardSizeY; y++)
            {
                var go = GameObject.Instantiate(prefabBG);
                go.transform.position = origin + new Vector3(x, y+root.transform.position.y, 0f);
                go.transform.SetParent(root);

                var cell = go.GetComponent<Cell>();
                cell.Setup(x, y);

                cells[x, y] = cell;
            }
        }

        for (int x = 0; x < boardSizeX; x++)
        {
            for (int y = 0; y < boardSizeY; y++)
            {
                if (y + 1 < boardSizeY) cells[x, y].NeighbourUp = cells[x, y + 1];
                if (x + 1 < boardSizeX) cells[x, y].NeighbourRight = cells[x + 1, y];
                if (y > 0) cells[x, y].NeighbourBottom = cells[x, y - 1];
                if (x > 0) cells[x, y].NeighbourLeft = cells[x - 1, y];
            }
        }

    }

    internal void Fill()
    {
        for (int x = 0; x < boardSizeX; x++)
        {
            for (int y = 0; y < boardSizeY; y++)
            {
                var cell = cells[x, y];
                var item = new NormalItem();

                var types = new List<NormalItem.itemType>();
                if (cell.NeighbourBottom != null)
                {
                    var nitem = cell.NeighbourBottom.Item as NormalItem;
                    if (nitem != null)
                    {
                        types.Add(nitem.ItemType);
                    }
                }

                if (cell.NeighbourLeft != null)
                {
                    var nitem = cell.NeighbourLeft.Item as NormalItem;
                    if (nitem != null)
                    {
                        types.Add(nitem.ItemType);
                    }
                }
                item.SetType(Match3Utilities.GetRandomNormalTypeExcept(types.ToArray()));
                item.SetView();
                item.SetViewRoot(root);
                cell.Assign(item);
                cell.ApplyItemPosition(false);
            }
        }

        for (int i = 0; i < startItems.Count; i++)
        {
            var staticValue = startItems[i];
            var cell = cells[staticValue.x, staticValue.y];
            var item = new NormalItem();
            item.SetType(staticValue.value);
            item.SetView();
            item.SetViewRoot(root);
            cell.Clear();
            cell.Assign(item);
            cell.ApplyItemPosition(false);
        }
    }

    internal void Shuffle()
    {
       var list = new List<Item>();
        for (int x = 0; x < boardSizeX; x++)
        {
            for (int y = 0; y < boardSizeY; y++)
            {
                list.Add(cells[x, y].Item);
                cells[x, y].Free();
            }
        }

        for (int x = 0; x < boardSizeX; x++)
        {
            for (int y = 0; y < boardSizeY; y++)
            {
                int rnd = UnityEngine.Random.Range(0, list.Count);
                cells[x, y].Assign(list[rnd]);
                cells[x, y].ApplyItemMoveToPosition();

                list.RemoveAt(rnd);
            }
        }
    }
    internal void FillGapsWithNewItems()
    {
        for (int x = 0; x < boardSizeX; x++)
        {
            for (int y = 0; y < boardSizeY; y++)
            {
                var cell = cells[x, y];
                if (!cell.IsEmpty) continue;

                var item = new NormalItem();

                item.SetType(Match3Utilities.GetRandomNormalType());
                item.SetView();
                item.SetViewRoot(root);

                cell.Assign(item);
                cell.ApplyItemPosition(true);
            }
        }
    }

    internal void ExplodeAllItems()
    {
        for (int x = 0; x < boardSizeX; x++)
        {
            for (int y = 0; y < boardSizeY; y++)
            {
                var cell = cells[x, y];
                cell.ExplodeItem();
            }
        }
    }

    public void IncreaseItem(Cell cell)
    {
        var item = cell.Item as NormalItem;
        if (item == null) return;
        cell.Free();
        item.Increase();
        cell.Assign(item);
        cell.ApplyItemPosition(true);
    }

    public List<Cell> GetHorizontalMatches(Cell cell)
    {
        var list = new List<Cell>();
        list.Add(cell);

       
        var newCell = cell;
        while (true)
        {
            var neighbourRight = newCell.NeighbourRight;
            if (neighbourRight == null) break;

            if (neighbourRight.IsSameType(cell))
            {
                list.Add(neighbourRight);
                newCell = neighbourRight;
            }
            else break;
        }

        newCell = cell;
        while (true)
        {
            var neighbourLeft = newCell.NeighbourLeft;
            if (neighbourLeft == null) break;

            if (neighbourLeft.IsSameType(cell))
            {
                list.Add(neighbourLeft);
                newCell = neighbourLeft;
            }
            else break;
        }

        return list;
    }


    public List<Cell> GetVerticalMatches(Cell cell)
    {
        var list = new List<Cell>();
        list.Add(cell);

        var newCell = cell;
        while (true)
        {
            var neighbourUp = newCell.NeighbourUp;
            if (neighbourUp == null) break;

            if (neighbourUp.IsSameType(cell))
            {
                list.Add(neighbourUp);
                newCell = neighbourUp;
            }
            else break;
        }

        newCell = cell;
        while (true)
        {
            var neighbourBottom = newCell.NeighbourBottom;
            if (neighbourBottom == null) break;

            if (neighbourBottom.IsSameType(cell))
            {
                list.Add(neighbourBottom);
                newCell = neighbourBottom;
            }
            else break;
        }

        return list;
    }
    internal List<Cell> FindFirstMatch()
    {
        var list = new List<Cell>();

        for (int x = 0; x < boardSizeX; x++)
        {
            for (int y = 0; y < boardSizeY; y++)
            {
                var cell = cells[x, y];

                var listhor = GetHorizontalMatches(cell);
                if (listhor.Count >= matchMin)
                {
                    list = listhor;
                    break;
                }

                var listvert = GetVerticalMatches(cell);
                if (listvert.Count >= matchMin)
                {
                    list = listvert;
                    break;
                }
            }
        }

        return list;
    }
    
    internal void ShiftDownItems()
    {
        for (int x = 0; x < boardSizeX; x++)
        {
            int shifts = 0;
            for (int y = 0; y < boardSizeY; y++)
            {
                var cell = cells[x, y];
                if (cell.IsEmpty)
                {
                    shifts++;
                    continue;
                }

                if (shifts == 0) continue;

                var holder = cells[x, y - shifts];

                var item = cell.Item;
                cell.Free();

                holder.Assign(item);
                item.View.DOMove(holder.transform.position, 0.3f);
            }
        }
    }

    public void Clear()
    {
        for (int x = 0; x < boardSizeX; x++)
        {
            for (int y = 0; y < boardSizeY; y++)
            {
                var cell = cells[x, y];
                cell.Clear();

                GameObject.Destroy(cell.gameObject);
                cells[x, y] = null;
            }
        }
    }
}
