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
            if (!m_interactible) return;

            base.OnPointerClick(eventData);

            if (m_raceInfo == null) return;

            SceneManager.LoadScene(m_raceInfo.SceneName);
        }

        private void Start()
        {
            ApplyProperty(m_raceInfo);
        }
    }
}
