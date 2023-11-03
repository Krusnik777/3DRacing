using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UITrackPoints : MonoBehaviour, IDependency<RaceStateTracker>
    {
        [SerializeField] private GameObject m_panel;
        [SerializeField] private GameObject m_sprintLabel;
        [SerializeField] private GameObject m_lapsLabel;
        [SerializeField] private Text m_lapsValueText;
        [SerializeField] private Text m_pointsValueText;

        private RaceStateTracker m_raceStateTracker;
        public void Construct(RaceStateTracker raceStateTracker) => m_raceStateTracker = raceStateTracker;

        private int points = 0;
        private int laps = 0;

        private void Start()
        {
            m_panel.SetActive(false);

            m_raceStateTracker.EventOnStarted += OnRaceStarted;
            m_raceStateTracker.EventOnCompleted += OnRaceCompleted;
        }

        private void OnDestroy()
        {
            m_raceStateTracker.EventOnStarted -= OnRaceStarted;
            m_raceStateTracker.EventOnCompleted -= OnRaceCompleted;

            m_raceStateTracker.EventOnTrackPointPassed -= OnTrackPointPassed;
            if (m_raceStateTracker.RaceType == TrackType.Circular) m_raceStateTracker.EventOnLapCompleted -= OnLapCompleted;
        }

        private void OnRaceStarted()
        {
            m_panel.SetActive(true);

            m_raceStateTracker.EventOnTrackPointPassed += OnTrackPointPassed;

            m_pointsValueText.text = points.ToString() + "/" + m_raceStateTracker.TrackPointsAmount.ToString();

            if (m_raceStateTracker.RaceType == TrackType.Sprint)
            {
                m_sprintLabel.SetActive(true);
                m_lapsLabel.SetActive(false);
            }

            if (m_raceStateTracker.RaceType == TrackType.Circular)
            {
                m_sprintLabel.SetActive(false);
                m_lapsLabel.SetActive(true);
                m_lapsValueText.text = laps.ToString() + "/" + m_raceStateTracker.LapsToComplete.ToString();
                m_raceStateTracker.EventOnLapCompleted += OnLapCompleted;
            }
        }

        private void OnTrackPointPassed(TrackPoint trackPoint)
        {
            points++;
            m_pointsValueText.text = points.ToString() + "/" + m_raceStateTracker.TrackPointsAmount.ToString();
        }

        private void OnLapCompleted(int lapAmount)
        {
            points = 1;
            m_pointsValueText.text = points.ToString() + "/" + m_raceStateTracker.TrackPointsAmount.ToString();

            laps = lapAmount;
            m_lapsValueText.text = laps.ToString() + "/" + m_raceStateTracker.LapsToComplete.ToString();
        }

        private void OnRaceCompleted()
        {
            m_panel.SetActive(false);
        }
    }
}
