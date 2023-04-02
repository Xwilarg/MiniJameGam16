using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiniJamGame16.Minigame.Impl
{
    public class Minesweeper : AMinigame
    {
        [SerializeField]
        private Transform _parent;

        [SerializeField]
        private GameObject _container;

        [SerializeField]
        private GameObject _tile;

        private MineData[,] _data;

        public bool _isInit;

        private int _size = 5;
        private int _baseNbOfMines = 3;

        public override void Init()
        {
            for (int i = 0; i < _parent.childCount; i++) Destroy(_parent.GetChild(i).gameObject);
            _data = new MineData[_size, _size];
            for (int y = 0; y < _size; y++)
            {
                var go = Instantiate(_container, _parent);
                for (int x = 0; x < _size; x++)
                {
                    var vy = y;
                    var vx = x;
                    var tile = Instantiate(_tile, go.transform);
                    _data[y, x] = new() { Text = tile.GetComponentInChildren<TMP_Text>(), Button = tile.GetComponent<Button>() };
                    _data[y, x].Button.onClick.AddListener(new(() =>
                    {
                        Click(vy, vx);
                    }));
                }
            }
            _isInit = false;
        }

        private Color[] _colors = new[] {
          new Color(0, 1f / 255f, 253f / 255f), // 1
          new Color(1f / 255f, 126f / 255f, 0), // 2
          new Color(254f / 255f, 0, 0), // 3
          new Color(1f / 255f, 0, 130f / 255f), // 4
          new Color(131f / 255f, 0, 3f / 255f), // 5
          new Color(0, 128f / 255f, 128f / 255f), // 6
          Color.black, // 7
          new Color(128, 128, 128) // 8
        };

        private bool IsInBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x < _size && y < _size;
        }

        private int GetMines(int x, int y)
        {
            var adjMines = 0;
            for (int dy = -1; dy < 2; dy++)
            {
                for (int dx = -1; dx < 2; dx++)
                {
                    if (IsInBounds(x + dx, y + dy) && _data[y + dy, x + dx].IsMine)
                    {
                        adjMines++;
                    }
                }
            }
            return adjMines;
        }

        private void CleanTile(int x, int y)
        {
            for (int dy = -1; dy < 2; dy++)
            {
                for (int dx = -1; dx < 2; dx++)
                {
                    if (IsInBounds(x + dx, y + dy) && _data[y + dy, x + dx].Button.interactable)
                    {
                        _data[y + dy, x + dx].Button.interactable = false;
                        var count = GetMines(x + dx, y + dy);
                        if (count == 0)
                        {
                            CleanTile(x + dx, y + dy);
                        }
                        else
                        {
                            _data[y + dy, x + dx].Text.text = $"{count}";
                            _data[y + dy, x + dx].Text.color = _colors[count - 1];
                        }
                    }
                }
            }
        }

        private void ShowAllMines()
        {
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    _data[y, x].Button.interactable = false;
                    if (_data[y, x].IsMine)
                    {
                        _data[y, x].Text.text = "X";
                        _data[y, x].Text.color = Color.red;
                    }
                }
            }
        }

        private IEnumerator WaitAndReset()
        {
            yield return new WaitForSeconds(2f);
            Init();
        }

        public void Click(int y, int x)
        {
            _data[y, x].Button.interactable = false;
            if (_data[y, x].IsMine)
            {
                ShowAllMines();
                StartCoroutine(WaitAndReset());
                return;
            }
            if (!_isInit)
            {
                var mineCount = _baseNbOfMines;
                _isInit = true;
                while (mineCount > 0)
                {
                    var randX = Random.Range(0, _size);
                    var randY = Random.Range(0, _size);
                    if (_data[randY, randX].Button.interactable && !_data[randY, randX].IsMine)
                    {
                        _data[randY, randX].IsMine = true;
                        mineCount--;
                    }
                }
            }
            var adjMines = GetMines(x, y);
            if (adjMines > 0)
            {
                _data[y, x].Text.text = $"{adjMines}";
                _data[y, x].Text.color = _colors[adjMines - 1];
            }
            else
            {
                CleanTile(x, y);
            }
            if (_data.Cast<MineData>().All(x => x.IsMine || !x.Button.interactable))
            {
                Complete();
            }
        }
    }
}
