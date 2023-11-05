using UnityEngine;

namespace Racing
{
    public class UISelectableButtonContainer : MonoBehaviour
    {
        [SerializeField] private Transform m_buttonsContainer;

        public bool Interactable = true;
        public void SetInteractable(bool interactable) => Interactable = interactable;

        private UISelectableButton[] buttons;

        private int selectButtonIndex = 0;

        public UISelectableButton SelectedButton => buttons[selectButtonIndex];

        #region Public

        public void SelectNext()
        {
            var newButtonIndex = selectButtonIndex;
            newButtonIndex++;
            if (newButtonIndex >= buttons.Length) newButtonIndex = 0;
            SelectButton(buttons[newButtonIndex]);
        }

        public void SelectPrevious()
        {
            var newButtonIndex = selectButtonIndex;
            newButtonIndex--;
            if (newButtonIndex < 0) newButtonIndex = buttons.Length - 1;
            SelectButton(buttons[newButtonIndex]);
        }

        public void ActivateButton()
        {
            buttons[selectButtonIndex].OnButtonClick();
        }

        #endregion

        #region Private

        private void Start()
        {
            buttons = m_buttonsContainer.GetComponentsInChildren<UISelectableButton>();

            if (buttons == null) Debug.LogError("Button List is Empty!");

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].EventOnPointerEnter += OnPointerEnter;
            }

            if (!Interactable) return;

            buttons[selectButtonIndex].SetFocus();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].EventOnPointerEnter -= OnPointerEnter;
            }
        }

        private void OnPointerEnter(UIButton button)
        {
            SelectButton(button);
        }

        private void SelectButton(UIButton button)
        {
            if (!Interactable) return;

            buttons[selectButtonIndex].UnsetFocus();

            for (int i = 0; i < buttons.Length; i++)
            {
                if (button == buttons[i])
                {
                    selectButtonIndex = i;
                    button.SetFocus();
                    break;
                }
            }
        }

        #endregion

    }
}
