using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(AudioSource))]
    public class UIButtonSound : MonoBehaviour
    {
        [SerializeField] private AudioClip m_clickSound;
        [SerializeField] private AudioClip m_hoverSound;

        private AudioSource m_audioSource;

        private UIButton[] uIButtons;

        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();

            uIButtons = GetComponentsInChildren<UIButton>(true);

            for (int i = 0; i < uIButtons.Length; i++)
            {
                //uIButtons[i].EventOnPointerEnter += OnPointerEnter;
                if (uIButtons[i] is UISelectableButton) (uIButtons[i] as UISelectableButton).OnSelect.AddListener(OnSelectButton);

                //uIButtons[i].EventOnPointerClick += OnPointerClick;
                uIButtons[i].OnClick.AddListener(OnPointerClick);
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < uIButtons.Length; i++)
            {
                //uIButtons[i].EventOnPointerEnter -= OnPointerEnter;
                if (uIButtons[i] is UISelectableButton) (uIButtons[i] as UISelectableButton).OnSelect.RemoveListener(OnSelectButton);

                //uIButtons[i].EventOnPointerClick -= OnPointerClick;
                uIButtons[i].OnClick.RemoveListener(OnPointerClick);
            }
        }

        private void OnPointerEnter(UIButton button)
        {
            m_audioSource.PlayOneShot(m_hoverSound);
        }

        private void OnSelectButton()
        {
            m_audioSource.PlayOneShot(m_hoverSound);
        }

        private void OnPointerClick(UIButton button)
        {
            m_audioSource.PlayOneShot(m_clickSound);
        }

        private void OnPointerClick()
        {
            m_audioSource.PlayOneShot(m_clickSound);
        }
    }
}
