using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(UISelectableButtonContainer))]
    public class UIPausePanel : MonoBehaviour, IDependency<Pauser>, IDependency<RaceStateTracker>
    {
        [SerializeField] private GameObject m_panel;

        private Pauser m_pauser;
        public void Construct(Pauser pauser) => m_pauser = pauser;

        private RaceStateTracker m_raceStateTracker;
        public void Construct(RaceStateTracker raceStateTracker) => m_raceStateTracker = raceStateTracker;

        private UISelectableButtonContainer buttonContainer;

        public void Unpause()
        {
            m_pauser.Unpause();
        }

        #region Private

        private void Start()
        {
            buttonContainer = GetComponent<UISelectableButtonContainer>();

            m_panel.SetActive(false);
            m_pauser.EventOnPauseStateChange += OnPauseStateChange;
        }

        private void Update()
        {
            if (m_raceStateTracker.State == RaceState.Preparation) return;

            if (m_raceStateTracker.State == RaceState.Passed) enabled = false;

            if (Input.GetButtonDown("Pause")) m_pauser.ChangePauseState();

            ControlPauseMenu();
        }

        private void OnDestroy()
        {
            m_pauser.EventOnPauseStateChange -= OnPauseStateChange;
        }

        private void OnPauseStateChange(bool isPause)
        {
            m_panel.SetActive(isPause);
        }

        private void ControlPauseMenu()
        {
            if (!m_panel.activeInHierarchy) return;

            if (MenuControlButtons.IsDown) buttonContainer.SelectNext();
            if (MenuControlButtons.IsUp) buttonContainer.SelectPrevious();
            if (Input.GetButtonDown("Submit")) buttonContainer.ActivateButton();
        }

        #endregion

    }
}
