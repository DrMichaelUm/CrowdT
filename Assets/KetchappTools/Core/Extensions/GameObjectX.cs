using UnityEngine;

namespace KetchappTools.Core.Extensions
{
    public static class GameObjectX
    {
		//TODO: Add summaries.

		public static T					AddComponent<T>(this GameObject gameObject, T componentToCopy) where T : Component
        {
            return gameObject.AddComponent<T>().CopyFieldsOf(componentToCopy) as T;
        }

        public static T					GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();

            if (!component)
                component = gameObject.AddComponent<T>();

            return component;
        }

        public static void				SetLayer(this GameObject gameObject, int layer, bool recursively = false)
        {
            gameObject.layer = layer;

            if (recursively)
                foreach (Transform iChild in gameObject.transform)
                    iChild.gameObject.SetLayer(layer, recursively);
        }

        public static PolygonCollider2D	AddPolygonCollider2D(this GameObject gameObject, float offset, float simplify)
        {
            LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
            PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>();

            int pointsCount = polygonCollider.points.Length;
            Vector3[] points = new Vector3[pointsCount];
            Transform transform = gameObject.transform;
            for (int i = 0; i < pointsCount; i++)
                points[i] = transform.TransformPoint(polygonCollider.points[i]);
            lineRenderer.positionCount = pointsCount;
            lineRenderer.SetPositions(points);
            lineRenderer.widthMultiplier = offset * 2f;
            lineRenderer.Simplify(simplify);

            // Set first position similar to last
            lineRenderer.loop = true;
            lineRenderer.positionCount--;
            lineRenderer.alignment = LineAlignment.TransformZ;
            Mesh mesh = new Mesh();
            lineRenderer.BakeMesh(mesh);
            GameObject.Destroy(lineRenderer);

            Vector3[] meshPoints = mesh.vertices;
            System.Collections.Generic.List<Vector2> colliderPoints = new System.Collections.Generic.List<Vector2>();
            for (int i = 0; i < meshPoints.Length; i++)
            {
                if (polygonCollider.OverlapPoint(meshPoints[i]))
                    colliderPoints.Add(transform.InverseTransformPoint(meshPoints[i]));
            }
            polygonCollider.points = colliderPoints.ToArray();

            return polygonCollider;
        }

		public static T					FindClosest<T>(Vector3 center) where T : Component
		{
			T closest = null;
			T[] array = GameObject.FindObjectsOfType<T>();
			if (array.Length < 0)
				return null;

			float dist = float.PositiveInfinity;
			for (int i = 0; i < array.Length; i++)
			{
				float d = Vector3.SqrMagnitude(center - array[i].transform.position);
				if (d < dist)
				{
					dist = d;
					closest = array[i];
				}
			}
			return closest;
		}
	}
}