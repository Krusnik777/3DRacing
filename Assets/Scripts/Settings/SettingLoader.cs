using UnityEngine;

namespace Racing
{
    public class SettingLoader : MonoBehaviour
    {
        [SerializeField] private Setting[] m_allSettings;

        private void Awake()
        {
            for (int i = 0; i < m_allSettings.Length; i++)
            {
                m_allSettings[i].Load();
                m_allSettings[i].Apply();
            }
        }
    }
}
