using UnityEngine;

namespace Racing
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private Car m_car;
        [SerializeField] [Range(0.0f, 1.0f)] private float m_normalizedSpeedShake;
        [SerializeField] private float m_shakeAmount;

        private void Update()
        {
            if (m_car.NormalizedLinearVelocity >= m_normalizedSpeedShake)
                transform.localPosition += Random.insideUnitSphere * m_shakeAmount * Time.deltaTime;
        }
    }
}
