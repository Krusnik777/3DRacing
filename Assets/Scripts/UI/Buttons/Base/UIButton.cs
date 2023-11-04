using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Racing
{
    public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] protected bool m_interactible;

        public UnityEvent OnClick;

        public event UnityAction<UIButton> EventOnPointerEnter;
        public event UnityAction<UIButton> EventOnPointerExit;
        public event UnityAction<UIButton> EventOnPointerClick;

        private bool inFocus = false;
        public bool InFocus => inFocus;

        public virtual void SetFocus()
        {
            if (!m_interactible) return;

            inFocus = true;
        }

        public virtual void UnsetFocus()
        {
            if (!m_interactible) return;

            inFocus = false;
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (!m_interactible) return;

            EventOnPointerEnter?.Invoke(this);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (!m_interactible) return;

            EventOnPointerExit?.Invoke(this);

        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!m_interactible) return;

            EventOnPointerClick?.Invoke(this);
            OnClick?.Invoke();
        }

        public virtual void SetInteractible(bool state)
        {
            m_interactible = state;
        }
    }
}
