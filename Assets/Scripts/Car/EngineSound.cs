using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(AudioSource))]
    public class EngineSound : MonoBehaviour
    {
        [SerializeField] private float m_pitchModifier;
        [SerializeField] private float m_volumeModifier;
        [SerializeField] private float m_rpmModifier;

        [SerializeField] private float m_basePitch = 1.0f;
        [SerializeField] private float m_baseVolume = 0.4f;

        private Car car;
        private AudioSource engineAudioSource;

        private void Start()
        {
            car = transform.root.GetComponent<Car>();
            engineAudioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            engineAudioSource.pitch = m_basePitch + m_pitchModifier * ((car.EngineRpm / car.EngineMaxRpm) * m_rpmModifier);
            engineAudioSource.volume = m_baseVolume + m_volumeModifier * (car.EngineRpm / car.EngineMaxRpm);
        }
    }
}
