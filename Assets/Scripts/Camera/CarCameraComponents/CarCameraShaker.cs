using UnityEngine;

namespace Racing
{
    public class CarCameraShaker : CarCameraComponent
    {
        [SerializeField] [Range(0.0f, 1.0f)] private float m_normalizedSpeedShake;
        [SerializeField] private float m_shakeAmount;

        private void Update()
        {
            if (m_car.NormalizedLinearVelocity >= m_normalizedSpeedShake)
                transform.localPosition += Random.insideUnitSphere * m_shakeAmount * Time.deltaTime;
        }
    }
}
