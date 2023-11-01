using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Racing
{
    public class UISelectableButton : UIButton
    {
        [SerializeField] private Image m_selectImage;

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
    }
}
