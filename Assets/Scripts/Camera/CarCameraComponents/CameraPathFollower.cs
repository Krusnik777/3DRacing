using UnityEngine;

namespace Racing
{
    public class CameraPathFollower : CarCameraComponent
    {
        [SerializeField] private Transform m_path;
        [SerializeField] private Transform m_lookTarget;
        [SerializeField] private float m_movementSpeed;

        private Vector3[] points;
        private int pointIndex;

        #region Public

        public void SetLookTarget(Transform target)
        {
            m_lookTarget = target;
        }

        public void StartMovementToNearestPoint()
        {
            float minDistance = float.MaxValue;

            for (int i = 0; i < points.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, points[i]);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    pointIndex = i;
                }
            }
        }

        #endregion

        #region Private

        private void Start()
        {
            points = new Vector3[m_path.childCount];

            for (int i = 0; i < points.Length; i++)
                points[i] = m_path.GetChild(i).position;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, points[pointIndex], m_movementSpeed * Time.deltaTime);

            if (transform.position == points[pointIndex])
            {
                if (pointIndex == points.Length - 1) pointIndex = 0;
                else pointIndex++;
            }

            transform.LookAt(m_lookTarget);
        }

        #endregion
    }
}
