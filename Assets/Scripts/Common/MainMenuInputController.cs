using UnityEngine;

namespace Racing
{
    public class MainMenuInputController : MonoBehaviour
    {
        [System.Serializable]
        private class RaceMenu
        {
            public UISelectableButtonContainer RacesButtonContrainer;
            public UIButton CloseButton;
        }

        [SerializeField] private UISelectableButtonContainer m_menuItemsBarContainer;
        [Header("MainPanels")]
        [SerializeField] private UISelectableButtonContainer m_seasonsButtonContainer;
        [SerializeField] private UISelectableButtonContainer m_settingsButtonContainer;
        [SerializeField] private UICreditsPanel m_creditsPanel;
        [Header("RacePanels")]
        [SerializeField] private RaceMenu[] m_RacesMenus;
        [Header("ConfirmPanels")]
        [SerializeField] private UIConfirmPanels m_confirmPanels;
        [Header("ExitButton")]
        [SerializeField] private UIButton m_exitButton;

        private int activeRacesMenuIndex = -1;

        private UISelectableButton[] seasonButtons;

        private void Start()
        {
            seasonButtons = m_seasonsButtonContainer.ButtonsContainer.GetComponentsInChildren<UISelectableButton>();

            foreach (var seasonButton in seasonButtons)
            {
                seasonButton.OnClick.AddListener(GetActiveRacesMenuIndex);
            }
        }

        private void OnDestroy()
        {
            foreach (var seasonButton in seasonButtons)
            {
                seasonButton.OnClick.RemoveListener(GetActiveRacesMenuIndex);
            }
        }

        private void Update()
        {
            if (m_confirmPanels.IsConfirmPanelUp)
            {
                ControlConfirmPanels();
            }
            else
            {
                ControlRacesMenu();
                ControlMenuItemsBar();
                ControlSeasonsMenu();
                ControlSettingsMenu();
                ControlCreditsPanel();

                if (Input.GetButtonDown("Exit")) m_exitButton.OnButtonClick();
            }
        }

        private void ControlConfirmPanels()
        {
            var confirmContainer = m_confirmPanels.GetActiveConfirmPanelContainer();

            if (MenuControlButtons.IsLeft) confirmContainer.SelectPrevious();
            if (MenuControlButtons.IsRight) confirmContainer.SelectNext();

            if (Input.GetButtonDown("Submit"))
            {
                confirmContainer.SelectedButton.OnButtonClick();
            }

            if (Input.GetButtonDown("Exit") || Input.GetButtonDown("Cancel"))
            {
                m_confirmPanels.CancelButton.OnButtonClick();
            }
        }

        private void GetActiveRacesMenuIndex()
        {
            for (int i = 0; i < m_RacesMenus.Length; i++)
            {
                if (m_RacesMenus[i].RacesButtonContrainer.gameObject.activeInHierarchy)
                {
                    activeRacesMenuIndex = i;
                    return;
                }
            }

            activeRacesMenuIndex = -1;
        }

        private void ControlRacesMenu()
        {
            if (m_seasonsButtonContainer.gameObject.activeInHierarchy || activeRacesMenuIndex == -1) return;

            if (MenuControlButtons.IsUp) m_RacesMenus[activeRacesMenuIndex].RacesButtonContrainer.SelectUp();
            if (MenuControlButtons.IsDown) m_RacesMenus[activeRacesMenuIndex].RacesButtonContrainer.SelectDown();
            if (MenuControlButtons.IsLeft) m_RacesMenus[activeRacesMenuIndex].RacesButtonContrainer.SelectLeft();
            if (MenuControlButtons.IsRight) m_RacesMenus[activeRacesMenuIndex].RacesButtonContrainer.SelectRight();

            /*
            if (Input.GetKeyDown(KeyCode.D))
            {
                m_RacesMenus[activeRacesMenuIndex].RacesButtonContrainer.SelectNext();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                m_RacesMenus[activeRacesMenuIndex].RacesButtonContrainer.SelectPrevious();
            }*/

            if (Input.GetButtonDown("Submit"))
            {
                m_RacesMenus[activeRacesMenuIndex].RacesButtonContrainer.ActivateButton();
            }

            if (Input.GetButtonDown("Cancel"))
            {
                m_RacesMenus[activeRacesMenuIndex].CloseButton.OnButtonClick();
                activeRacesMenuIndex = -1;
            }
        }

        private void ControlMenuItemsBar()
        {
            if (Input.GetButtonDown("NextPage"))
            {
                m_menuItemsBarContainer.SelectNext();
            }

            if (Input.GetButtonDown("PrevPage"))
            {
                m_menuItemsBarContainer.SelectPrevious();
            }
        }

        private void ControlSeasonsMenu()
        {
            if (!m_seasonsButtonContainer.gameObject.activeInHierarchy) return;

            if (MenuControlButtons.IsUp) m_seasonsButtonContainer.SelectUp();
            if (MenuControlButtons.IsDown) m_seasonsButtonContainer.SelectDown();
            if (MenuControlButtons.IsLeft) m_seasonsButtonContainer.SelectLeft();
            if (MenuControlButtons.IsRight) m_seasonsButtonContainer.SelectRight();

            /*
            if (Input.GetKeyDown(KeyCode.D))
            {
                m_seasonsButtonContainer.SelectNext();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                m_seasonsButtonContainer.SelectPrevious();
            }*/

            if (Input.GetButtonDown("Submit"))
            {
                m_seasonsButtonContainer.ActivateButton();
            }
        }

        private void ControlSettingsMenu()
        {
            if (!m_settingsButtonContainer.gameObject.activeInHierarchy) return;

            if (MenuControlButtons.IsUp) m_settingsButtonContainer.SelectPrevious();
            if (MenuControlButtons.IsDown) m_settingsButtonContainer.SelectNext();
            if (MenuControlButtons.IsLeft && m_settingsButtonContainer.SelectedButton is UISettingButton) (m_settingsButtonContainer.SelectedButton as UISettingButton).SetPreviousValueSetting();
            if (MenuControlButtons.IsRight && m_settingsButtonContainer.SelectedButton is UISettingButton) (m_settingsButtonContainer.SelectedButton as UISettingButton).SetNextValueSetting();

            if (Input.GetButtonDown("Submit"))
            {
                if (!(m_settingsButtonContainer.SelectedButton is UISettingButton)) m_settingsButtonContainer.SelectedButton.OnButtonClick();
            }  
        }

        private void ControlCreditsPanel()
        {
            if (m_creditsPanel.isActiveAndEnabled)
            {
                var verticalAxis = Input.GetAxis("MenuVertical");

                if (verticalAxis != 0) m_creditsPanel.ControlCreditsScrollBar(Input.GetAxis("MenuVertical"));
            }
        }
    }
}
