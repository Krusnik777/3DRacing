using UnityEngine;

namespace Racing
{
    public class UIConfirmPanels : MonoBehaviour
    {
        [SerializeField] private GameObject m_resetPanelObject;
        [SerializeField] private GameObject m_quitPanelObject;
        [SerializeField] private GameObject m_backgroundObject;
        [Header("CancelButtons")]
        [SerializeField] private UIButton m_resetCancelButton;
        [SerializeField] private UIButton m_quitCancelButton;

        public bool IsConfirmPanelUp => m_backgroundObject.activeInHierarchy;

        [HideInInspector] public UIButton CancelButton;

        public void SetResetPanelActive(bool state)
        {
            m_backgroundObject.SetActive(state);
            m_resetPanelObject.SetActive(state);
        }

        public void SetQuitPanelActive(bool state)
        {
            m_backgroundObject.SetActive(state);
            m_quitPanelObject.SetActive(state);
        }

        public UISelectableButtonContainer GetActiveConfirmPanelContainer()
        {
            if (m_resetPanelObject.activeInHierarchy)
            {
                CancelButton = m_resetCancelButton;
                return m_resetPanelObject.GetComponent<UISelectableButtonContainer>();
            }

            if (m_quitPanelObject.activeInHierarchy)
            {
                CancelButton = m_quitCancelButton;
                return m_quitPanelObject.GetComponent<UISelectableButtonContainer>();
            }

            Debug.LogWarning("No active Confirm Panel");

            return null;
        }

        private void Start()
        {
            m_backgroundObject.SetActive(false);
            m_resetPanelObject.SetActive(false);
            m_quitPanelObject.SetActive(false);
        }

        

    }
}
