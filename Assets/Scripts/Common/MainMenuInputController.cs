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
        [Header("RacePanels")]
        [SerializeField] private RaceMenu[] m_RacesMenus;

        private int activeRacesMenuIndex = -1;

        public void ExitGame()
        {
            Application.Quit();
        }

        private void Update()
        {
            ControlRacesMenu();
            ControlMenuItemsBar();
            ControlSeasonsMenu();
            ControlSettingsMenu();
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
                activeRacesMenuIndex = GetActiveRacesMenuIndex();
            }
        }

        private void ControlSettingsMenu()
        {
            if (!m_settingsButtonContainer.gameObject.activeInHierarchy) return;

            if (MenuControlButtons.IsUp) m_settingsButtonContainer.SelectPrevious();
            if (MenuControlButtons.IsDown) m_settingsButtonContainer.SelectNext();
            if (MenuControlButtons.IsLeft) (m_settingsButtonContainer.SelectedButton as UISettingButton).SetPreviousValueSetting();
            if (MenuControlButtons.IsRight) (m_settingsButtonContainer.SelectedButton as UISettingButton).SetNextValueSetting();
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

        private int GetActiveRacesMenuIndex()
        {
            for (int i = 0; i < m_RacesMenus.Length; i++)
            {
                if (m_RacesMenus[i].RacesButtonContrainer.gameObject.activeInHierarchy)
                    return i;
            }

            return -1;
        }
    }
}
