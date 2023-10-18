using UnityEngine;

namespace Racing
{
    public class CameraFovCorrector : MonoBehaviour
    {
        [SerializeField] private Car m_car;
        [SerializeField] private float m_minFieldOfView;
        [SerializeField] private float m_maxFieldOfView;

        private Camera m_camera;
        private float defaultFov;

        private void Start()
        {
            m_camera = GetComponent<Camera>();
            m_camera.fieldOfView = defaultFov;
        }

        private void Update()
        {
            m_camera.fieldOfView = Mathf.Lerp(m_minFieldOfView, m_maxFieldOfView, m_car.NormalizedLinearVelocity);
        }
    }
}
