using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Racing
{
    public class MusicPlayer : SingletonBase<MusicPlayer>
    {
        [SerializeField] private MusicInfo m_raceTracks;
        [SerializeField] private bool m_playInRandomOrder;

        public event UnityAction<MusicInfo,int> EventOnMusicTrackChange;

        private AudioSource m_audioSource;

        private AudioClip[] clips;
        private List<int> playedClips;

        private int audioNumber = 0;

        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();

            clips = m_raceTracks.GetClips();
            playedClips = new List<int>();
            Play(true);
        }

        private void Update()
        {
            if (m_audioSource.isPlaying == false)
            {
                Play(m_playInRandomOrder);
            }
        }

        private void Play(bool inRandomOrder)
        {
            if (inRandomOrder)
            {
                do
                {
                    audioNumber = Random.Range(0, clips.Length);
                }
                while (playedClips.Contains(audioNumber));

                playedClips.Add(audioNumber);

                if (playedClips.Count >= clips.Length)
                {
                    playedClips.Clear();
                    playedClips.Add(audioNumber);
                }
            }
            else
            {
                audioNumber++;
                if (audioNumber >= clips.Length) audioNumber = 0;
            }

            m_audioSource.PlayOneShot(clips[audioNumber]);
            EventOnMusicTrackChange?.Invoke(m_raceTracks, audioNumber);
        }
    }
}
