using UnityEngine;

namespace Assets.Scripts.Minigame.Impl
{
    public class RelationshipCursor : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _parent;

        [SerializeField]
        private RectTransform _marker;

        private float _baseSpeed;

        private bool _goLeft;

        private float _xMax = 455f;

        private void ResetSpeed()
        {
            _baseSpeed = Random.Range(255, 1500);
        }

        private void Update()
        {
            transform.Translate(Vector3.right * (_goLeft ? -1f : 1f) * _baseSpeed * Time.deltaTime);
            var rTransform = (RectTransform)transform;
            if (_goLeft && rTransform.anchoredPosition.x <= 0f)
            {
                rTransform.anchoredPosition = new(0f, rTransform.anchoredPosition.y);
                _goLeft = false;
                ResetSpeed();
            }
            else if (!_goLeft && rTransform.anchoredPosition.x >= _xMax)
            {
                rTransform.anchoredPosition = new(_xMax, rTransform.anchoredPosition.y);
                _goLeft = true;
            }
        }

        public void ResetCursor()
        {
            _goLeft = false;
            var rTransform = (RectTransform)transform;
            rTransform.anchoredPosition = new(0f, rTransform.anchoredPosition.y);
            _baseSpeed = Random.Range(255, 2000);
            _marker.gameObject.SetActive(false);
        }

        public bool IsPosOk()
        {
            var rTransform = (RectTransform)transform;
            _marker.gameObject.SetActive(true);
            _marker.anchoredPosition = rTransform.anchoredPosition;
            return rTransform.anchoredPosition.x >= 185 && rTransform.anchoredPosition.x <= 275;
        }
    }
}
