using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UITrackTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>
    {
        [SerializeField] private Text m_text;

        private RaceStateTracker m_raceStateTracker;
        public void Construct(RaceStateTracker raceStateTracker) => m_raceStateTracker = raceStateTracker;

        private RaceTimeTracker m_raceTimeTracker;
        public void Construct(RaceTimeTracker raceTimeTracker) => m_raceTimeTracker = raceTimeTracker;

        private void Start()
        {
            m_raceStateTracker.EventOnStarted += OnRaceStarted;
            m_raceStateTracker.EventOnCompleted += OnRaceCompleted;

            m_text.enabled = false;
        }

        private void Update()
        {
            m_text.text = StringTime.SecondToTimeString(m_raceTimeTracker.CurrentTime);
        }

        private void OnDestroy()
        {
            m_raceStateTracker.EventOnStarted -= OnRaceStarted;
            m_raceStateTracker.EventOnCompleted -= OnRaceCompleted;
        }

        private void OnRaceStarted()
        {
            m_text.enabled = true;
            enabled = true;
        }

        private void OnRaceCompleted()
        {
            m_text.enabled = false;
            enabled = false;
        }
    }
}
