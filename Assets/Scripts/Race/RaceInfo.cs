using UnityEngine;

namespace Racing
{
    [CreateAssetMenu]
    public class RaceInfo : ScriptableObject
    {
        [SerializeField] private string m_sceneName;
        [SerializeField] private Sprite m_icon;
        [SerializeField] private string m_title;
        [Header("Number of laps (0 = Sprint)")]
        [SerializeField] private int m_laps;

        public string SceneName => m_sceneName;
        public Sprite Icon => m_icon;
        public string Title => m_title;
        public int Laps => m_laps;
    }
}
