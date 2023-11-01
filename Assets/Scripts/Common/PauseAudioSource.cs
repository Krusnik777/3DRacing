using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(AudioSource))]
    public class PauseAudioSource : MonoBehaviour, IDependency<Pauser>
    {
        private AudioSource m_audioSource;

        private Pauser m_pauser;
        public void Construct(Pauser pauser) => m_pauser = pauser;

        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();

            m_pauser.EventOnPauseStateChange += OnPauseStateChange;
        }

        private void OnDestroy()
        {
            m_pauser.EventOnPauseStateChange -= OnPauseStateChange;
        }

        private void OnPauseStateChange(bool pause)
        {
            if (!pause) m_audioSource.Play();
            else m_audioSource.Stop();
        }
    }
}
