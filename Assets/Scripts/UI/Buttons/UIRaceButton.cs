using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Racing
{
    public class UIRaceButton : UISelectableButton, IScriptableObjectProperty
    {
        [SerializeField] private RaceInfo m_raceInfo;
        [SerializeField] private Image m_icon;
        [SerializeField] private Text m_title;
        [SerializeField] private Text m_laps;
        [Header("ClearedMark")]
        [SerializeField] private GameObject m_clearedMarkObject;
        [SerializeField] private Text m_bestTimeText;

        public string RaceKey => m_raceInfo.SceneName;

        public void ApplyProperty(ScriptableObject property)
        {
            if (property == null || !(property is RaceInfo)) return;

            m_raceInfo = property as RaceInfo;

            m_icon.sprite = m_raceInfo.Icon;
            m_title.text = m_raceInfo.Title;
            m_laps.text = m_raceInfo.Laps <= 0 ? "Sprint" : "Laps: " + m_raceInfo.Laps.ToString();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!m_interactable) return;

            base.OnPointerClick(eventData);

            if (m_raceInfo == null) return;

            SceneManager.LoadScene(m_raceInfo.SceneName);
        }

        public override void OnButtonClick()
        {
            if (!m_interactable) return;

            base.OnButtonClick();

            if (m_raceInfo == null) return;

            SceneManager.LoadScene(m_raceInfo.SceneName);
        }

        public virtual void SetMark(float time)
        {
            if (!m_clearedMarkObject) return;

            m_clearedMarkObject.SetActive(true);
            m_bestTimeText.text = StringTime.SecondToTimeString(time);
        }

        public virtual void SetMarkOff()
        {
            if (!m_clearedMarkObject) return;

            m_clearedMarkObject.SetActive(false);
        }

        private void Start()
        {
            ApplyProperty(m_raceInfo);
        }
    }
}
