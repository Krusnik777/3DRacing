using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UICreditsPanel : MonoBehaviour
    {
        [SerializeField] private Scrollbar m_scrollbar;
        [SerializeField] private float m_scrollStep = 0.1f;

        public void ControlCreditsScrollBar(float inputAxis)
        {
            m_scrollbar.value += inputAxis * m_scrollStep;
        }

        private void OnEnable()
        {
            m_scrollbar.value = 1.0f;
        }
    }
}
