using UnityEngine;

namespace Racing
{
    [CreateAssetMenu]
    public class MusicInfo : ScriptableObject
    {
        [System.Serializable]
        public class MusicTrack
        {
            public AudioClip Track;
            public string Title;
            public string Artist;
        }

        public MusicTrack[] Tracks;

        public AudioClip[] GetClips()
        {
            AudioClip[] clips = new AudioClip[Tracks.Length];

            for (int i = 0; i < Tracks.Length; i++)
            {
                clips[i] = Tracks[i].Track;
            }

            return clips;
        }
    }
}
