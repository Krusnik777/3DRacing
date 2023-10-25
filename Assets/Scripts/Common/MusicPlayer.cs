using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public class MusicPlayer : SingletonBase<MusicPlayer>
    {
        [SerializeField] private AudioClip[] m_audioClips;
        [SerializeField] private bool m_playInRandomOrder;

        private AudioSource m_audioSource;

        private int audioNumber = 0;

        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();

            audioNumber = Random.Range(0, m_audioClips.Length);

            m_audioSource.PlayOneShot(m_audioClips[audioNumber]);
        }

        private void Update()
        {
            if (m_audioSource.isPlaying == false)
            {
                m_audioSource.PlayOneShot(ChoosedAudio(m_playInRandomOrder));
            }
        }

        private AudioClip ChoosedAudio(bool inRandomOrder)
        {
            if (inRandomOrder) 
                audioNumber = Random.Range(0, m_audioClips.Length);
            else
            {
                audioNumber++;
                if (audioNumber >= m_audioClips.Length) audioNumber = 0;
            }
            return m_audioClips[audioNumber];
        }
    }
}
