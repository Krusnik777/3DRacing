using UnityEngine;

namespace Racing
{
    public class MusicPlayer : SingletonBase<MusicPlayer>
    {
        [SerializeField] private AudioClip[] m_audioClips;

        private AudioSource m_audioSource;

        private int audioNumber = 0;

        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();
            m_audioSource.PlayOneShot(m_audioClips[audioNumber]);
        }

        private void Update()
        {
            if (m_audioSource.isPlaying == false)
            {
                m_audioSource.PlayOneShot(ChoosedAudio());
            }
        }

        private AudioClip ChoosedAudio()
        {
            audioNumber++;
            if (audioNumber >= m_audioClips.Length) audioNumber = 0;
            return m_audioClips[audioNumber];
        }
    }
}
