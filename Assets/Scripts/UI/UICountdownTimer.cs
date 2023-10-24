using UnityEngine;
using UnityEngine.UI;


namespace Racing
{
    public class UICountdownTimer : MonoBehaviour, IDependency<RaceStateTracker>
    {
        [SerializeField] private Text m_text;

        private RaceStateTracker m_raceStateTracker;
        public void Construct(RaceStateTracker raceStateTracker) => m_raceStateTracker = raceStateTracker;

        private void Start()
        {
            m_raceStateTracker.EventOnPreparationStarted += OnRacePreparationStarted;
            m_raceStateTracker.EventOnStarted += OnRaceStarted;

            m_text.enabled = false;
        }

        private void Update()
        {
            m_text.text = m_raceStateTracker.CountdownTimer.Value.ToString("F0");

            if (m_text.text == "0") m_text.text = "GO!";
        }

        private void OnDestroy()
        {
            m_raceStateTracker.EventOnPreparationStarted -= OnRacePreparationStarted;
            m_raceStateTracker.EventOnStarted -= OnRaceStarted;
        }

        private void OnRacePreparationStarted()
        {
            m_text.enabled = true;
            enabled = true;
        }

        private void OnRaceStarted()
        {
            m_text.enabled = false;
            enabled = false;
        }
    }
}
