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

            for (int i =0; i < uIButtons.Length; i++)
            {
                uIButtons[i].EventOnPointerEnter += OnPointerEnter;
                uIButtons[i].EventOnPointerClick += OnPointerClick;
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < uIButtons.Length; i++)
            {
                uIButtons[i].EventOnPointerEnter -= OnPointerEnter;
                uIButtons[i].EventOnPointerClick -= OnPointerClick;
            }
        }

        private void OnPointerEnter(UIButton button)
        {
            m_audioSource.PlayOneShot(m_hoverSound);
        }

        private void OnPointerClick(UIButton button)
        {
            m_audioSource.PlayOneShot(m_clickSound);
        }
    }
}
