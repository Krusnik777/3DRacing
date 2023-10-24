using UnityEngine;

namespace Racing
{
    public class CarCameraFovCorrector : CarCameraComponent
    {
        [SerializeField] private float m_minFieldOfView;
        [SerializeField] private float m_maxFieldOfView;

        private float defaultFov;

        private void Start()
        {
            m_camera.fieldOfView = defaultFov;
        }

        private void Update()
        {
            m_camera.fieldOfView = Mathf.Lerp(m_minFieldOfView, m_maxFieldOfView, m_car.NormalizedLinearVelocity);
        }
    }
}
