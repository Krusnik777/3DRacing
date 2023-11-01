using UnityEngine;

namespace Racing
{
    public class SpawnObjectByPropertiesList : MonoBehaviour
    {
        [SerializeField] private Transform m_parent;
        [SerializeField] private GameObject m_prefab;
        [SerializeField] private ScriptableObject[] m_properties;

        [ContextMenu(nameof(SpawnInEditMode))]
        public void SpawnInEditMode()
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
                GameObject gameObject = Instantiate(m_prefab, m_parent);
                IScriptableObjectProperty scriptableObjectProperty = gameObject.GetComponent<IScriptableObjectProperty>();
                scriptableObjectProperty.ApplyProperty(m_properties[i]);
            }
        }
    }
}
