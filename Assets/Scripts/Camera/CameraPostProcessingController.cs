using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Racing
{
    [RequireComponent(typeof(PostProcessVolume), typeof(PostProcessLayer))]
    public class CameraPostProcessingController : MonoBehaviour
    {
        [SerializeField] private Car m_car;
        [SerializeField] private float m_minMotionBlurShutterAngle = 0.0f;
        [SerializeField] private float m_maxMotionBlurShutterAngle = 200.0f;
        [SerializeField] private float m_minVignetteValue = 0.0f;
        [SerializeField] private float m_maxVignetteValue = 0.5f;

        private PostProcessVolume postProcessVolume;

        private MotionBlur motionBlur;
        private Vignette vignette;

        private void Start()
        {
            postProcessVolume = GetComponent<PostProcessVolume>();
            postProcessVolume.profile.TryGetSettings(out motionBlur);
            postProcessVolume.profile.TryGetSettings(out vignette);
        }

        private void Update()
        {
            motionBlur.shutterAngle.value = Mathf.Lerp(m_minMotionBlurShutterAngle, m_maxMotionBlurShutterAngle, m_car.NormalizedLinearVelocity);
            vignette.intensity.value = Mathf.Lerp(m_minVignetteValue, m_maxVignetteValue, m_car.NormalizedLinearVelocity);
        }

    }
}
