using UnityEngine;
using UnityEngine.UI;

namespace MiniJamGame16.Minigame.Impl
{
    public class CustomLineRenderer : Graphic
    {
        [SerializeField]
        private Vector3[] _positions;

        [SerializeField]
        private float thickness = 10f;

        public void SetPositions(Vector3[] positions)
        {
            _positions = positions;
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            foreach (var pos in _positions)
            {
                DrawVerticesForPoint(pos, vh);
            }

            for (var i = 0; i < _positions.Length; i++)
            {
                var index = i * 2;
                vh.AddTriangle(index, index + 1, index + 3);
                vh.AddTriangle(index + 3, index + 2, index);
            }
        }

        private void DrawVerticesForPoint(Vector3 point, VertexHelper vh)
        {
            var vertex = UIVertex.simpleVert;
            vertex.color = color;

            vertex.position = new(-thickness / 2f, 0f);
            vertex.position += new Vector3(point.x, point.y, point.z);
            vh.AddVert(vertex);

            vertex.position = new(thickness / 2f, 0f);
            vertex.position += new Vector3(point.x, point.y, point.z);
            vh.AddVert(vertex);
        }
    }
}
