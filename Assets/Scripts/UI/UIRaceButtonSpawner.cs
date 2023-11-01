using UnityEngine;

namespace Racing
{
    public class UIRaceButtonSpawner : MonoBehaviour
    {
        [SerializeField] private Transform m_parent;
        [SerializeField] private UIRaceButton m_prefab;
        [SerializeField] private RaceInfo[] m_properties;

        [ContextMenu(nameof(Spawn))]
        public void Spawn()
        {
            if (Application.isPlaying == true) return;

            GameObject[] allObjects = new GameObject[m_parent.childCount];

            for (int i = 0; i < m_parent.childCount; i++)
            {
                allObjects[i] = m_parent.GetChild(i).gameObject;
            }

            for (int i = 0; i < allObjects.Length; i++)
            {
                DestroyImmediate(allObjects[i]);
            }

            for (int i = 0; i < m_properties.Length; i++)
            {
                UIRaceButton button = Instantiate(m_prefab, m_parent);
                button.ApplyProperty(m_properties[i]);
            }
        }
    }
}
