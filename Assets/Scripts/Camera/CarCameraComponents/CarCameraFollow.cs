using UnityEngine;

namespace Racing
{
    public class CarCameraFollow : CarCameraComponent
    {
        [Header("Offset")]
        [SerializeField] private float m_viewHeight;
        [SerializeField] private float m_height;
        [SerializeField] private float m_distance;

        [Header("Damping")]
        [SerializeField] private float m_rotationDamping;
        [SerializeField] private float m_heightDamping;
        [SerializeField] private float m_speedThreshold;

        private Transform m_target;
        private Rigidbody m_rigidbody;

        public override void SetPropertioes(Car car, Camera camera)
        {
            base.SetPropertioes(car, camera);

            m_target = car.transform;
            m_rigidbody = car.Rigidbody;
        }

        private void FixedUpdate()
        {
            Vector3 velocity = m_rigidbody.velocity;
            Vector3 targetRotation = m_target.eulerAngles;

            if (velocity.magnitude > m_speedThreshold)
            {
                targetRotation = Quaternion.LookRotation(velocity, Vector3.up).eulerAngles;
            }

            // Lerp
            float currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetRotation.y, m_rotationDamping * Time.fixedDeltaTime);
            float currentHeight = Mathf.Lerp(transform.position.y, m_target.position.y + m_height, m_heightDamping * Time.fixedDeltaTime);

            // Position
            Vector3 positionOffset = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * m_distance;
            transform.position = m_target.position - positionOffset;
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

            // Rotation
            transform.LookAt(m_target.position + new Vector3(0, m_viewHeight, 0));
        } 
    }
}
