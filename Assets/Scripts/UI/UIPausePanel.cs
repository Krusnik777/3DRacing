using UnityEngine;

namespace Racing
{
    public class UIPausePanel : MonoBehaviour, IDependency<Pauser>
    {
        [SerializeField] private GameObject m_panel;

        private Pauser m_pauser;
        public void Construct(Pauser pauser) => m_pauser = pauser;

        public void Unpause()
        {
            m_pauser.Unpause();
        }

        #region Private

        private void Start()
        {
            m_panel.SetActive(false);
            m_pauser.EventOnPauseStateChange += OnPauseStateChange;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) m_pauser.ChangePauseState();
        }

        private void OnDestroy()
        {
            m_pauser.EventOnPauseStateChange -= OnPauseStateChange;
        }

        private void OnPauseStateChange(bool isPause)
        {
            m_panel.SetActive(isPause);
        }

        #endregion

    }
}
