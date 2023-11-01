using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Racing
{
    public class MusicPlayer : SingletonBase<MusicPlayer>
    {
        private const string MainMenuSceneName = "MainMenu";

        [SerializeField] private MusicInfo m_menuTracks;
        [SerializeField] private MusicInfo m_raceTracks;
        [SerializeField] private bool m_playInRandomOrder;

        public event UnityAction<MusicInfo,int> EventOnMusicTrackChange;

        private AudioSource m_audioSource;

        private AudioClip[] clips;
        private List<int> playedClips;

        private int audioNumber = 0;

        public bool InMenu => activeSceneName == MainMenuSceneName;

        private string activeSceneName => SceneManager.GetActiveScene().name;
        private string prevActiveSceneName = "";

        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();
            playedClips = new List<int>();

            SceneManager.sceneLoaded += OnSceneLoaded;

            PlayBasedOnScene(SceneManager.GetActiveScene());
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
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

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == prevActiveSceneName) return;

            m_audioSource.Stop();
            playedClips.Clear();
            PlayBasedOnScene(scene);
        }

        private void PlayBasedOnScene(Scene scene)
        {
            prevActiveSceneName = scene.name;

            if (scene.name == MainMenuSceneName)
            {
                clips = m_menuTracks.GetClips();
                Play(true);
            }
            else
            {
                clips = m_raceTracks.GetClips();
                Play(true);
            }
        }
    }
}
