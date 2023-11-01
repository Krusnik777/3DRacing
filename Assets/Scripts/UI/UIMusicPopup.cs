using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UIMusicPopup : MonoBehaviour
    {
        [SerializeField] private MusicPlayer m_musicPlayer;
        [SerializeField] private Animator m_animator;
        [SerializeField] private Text m_titleText;
        [SerializeField] private Text m_performerText;

        private void Start()
        {
            m_titleText.text = "";
            m_performerText.text = "";

            m_musicPlayer.EventOnMusicTrackChange += OnMusicTrackChange;
        }

        private void OnDestroy()
        {
            m_musicPlayer.EventOnMusicTrackChange -= OnMusicTrackChange;
        }

        private void OnMusicTrackChange(MusicInfo musicTracks, int index)
        {
            if (m_musicPlayer.InMenu) return;

            m_titleText.text = musicTracks.Tracks[index].Title;
            m_performerText.text = musicTracks.Tracks[index].Artist;
            m_animator.SetTrigger("AppearTrigger");
        }
    }
}
