using Assets.Scripts.Minigame.Impl;
using MiniJamGame16.Translation;
using System.Collections;
using System.Diagnostics.SymbolStore;
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

        private int _size = 10;

        public override void Init()
        {
            for (int i = 0; i < _parent.childCount; i++) Destroy(_parent.GetChild(i).gameObject);
            _data = new MineData[_size, _size];
            for (int y = 0; y < _size; y++)
            {
                var go = Instantiate(_container, _parent);
                for (int x = 0; x < _size; x++)
                {
                    var tile = Instantiate(_tile, go.transform);
                    _data[y, x] = new() { Text = tile.GetComponentInChildren<TMP_Text>(), Button = tile.GetComponent<Button>() };
                    _data[y, x].Button.onClick.AddListener(new(() =>
                    {
                        Click(y, x);
                    }));
                }
            }
            _isInit = false;
        }

        public void Click(int y, int x)
        {
            _data[y, x].Button.interactable = false;
            if (_data[y, x].IsMine)
            {
                Debug.Log("lol you lost");
                return;
            }
            if (!_isInit)
            {
                var mineCount = 20;
                _isInit = true;
                while (mineCount > 0)
                {
                    var randX = Random.Range(0, _size);
                    var randY = Random.Range(0, _size);
                    if (_data[randY, randX].Button.interactable && !_data[randY, randX].IsMine)
                    {
                        _data[randY, randX].IsMine = true;
                    }
                }
            }
            var adjMines = 0;
            for (int dy = -1; dy < 2; dy++)
            {
                for (int dx = -1; dx < 2; dx++)
                {
                    if (_data[y + dy, x + dx].IsMine)
                    {
                        adjMines++;
                    }
                }
            }
            if (adjMines > 0)
            {

            }
        }
    }
}
