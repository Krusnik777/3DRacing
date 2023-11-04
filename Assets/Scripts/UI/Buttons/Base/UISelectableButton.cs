using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Racing
{
    public class UISelectableButton : UIButton
    {
        [SerializeField] private Image m_selectImage;
        [SerializeField] private GameObject m_lockerObject;
        [SerializeField] private Text m_lockMessage;

        public UnityEvent OnSelect;
        public UnityEvent OnUnselect;

        public override void SetFocus()
        {
            base.SetFocus();

            m_selectImage.enabled = true;
            OnSelect?.Invoke();
        }

        public override void UnsetFocus()
        {
            base.UnsetFocus();

            m_selectImage.enabled = false;
            OnUnselect?.Invoke();
        }

        public virtual void SetLocker(int racesAmount)
        {
            if (!m_lockerObject) return;

            m_lockerObject.SetActive(true);
            m_lockMessage.text = $"Clear {racesAmount} Races";
        }

        public virtual void SetLocker(string message)
        {
            if (!m_lockerObject) return;

            m_lockerObject.SetActive(true);
            m_lockMessage.text = message;
        }

        public virtual void SetLockerOff()
        {
            if (!m_lockerObject) return;

            m_lockerObject.SetActive(false);
        }
    }
}
