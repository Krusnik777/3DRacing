using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UIRaceRecordTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceResultTime>
    {
        [SerializeField] private GameObject m_goldRecordObject;
        [SerializeField] private GameObject m_playerRecordObject;
        [SerializeField] private Text m_goldRecordTimeText;
        [SerializeField] private Text m_playerRecordTimeText;
        [Header("Results")]
        [SerializeField] private GameObject m_resultsObject;
        [SerializeField] private GameObject m_recordTimeObject;
        [SerializeField] private GameObject m_newRecordObject;
        [SerializeField] private Text m_playerCurrentTimeText;
        [SerializeField] private Text m_standardTimeText;
        [SerializeField] private Text m_recordTimeText;

        private RaceStateTracker m_raceStateTracker;
        public void Construct(RaceStateTracker raceStateTracker) => m_raceStateTracker = raceStateTracker;

        private RaceResultTime m_raceResultTime;
        public void Construct(RaceResultTime raceResultTime) => m_raceResultTime = raceResultTime;

        private void Start()
        {
            m_raceStateTracker.EventOnStarted += OnRaceStarted;
            m_raceStateTracker.EventOnCompleted += OnRaceCompleted;

            m_raceResultTime.EventOnUpdatedResults += OnUpdatedResults;

            m_goldRecordObject.SetActive(false);
            m_playerRecordObject.SetActive(false);

            m_resultsObject.SetActive(false);
        }

        private void OnDestroy()
        {
            m_raceStateTracker.EventOnStarted -= OnRaceStarted;
            m_raceStateTracker.EventOnCompleted -= OnRaceCompleted;

            m_raceResultTime.EventOnUpdatedResults -= OnUpdatedResults;
        }

        private void OnRaceStarted()
        {
            if (m_raceResultTime.PlayerRecordTime > m_raceResultTime.GoldTime || !m_raceResultTime.RecordWasSet)
            {
                m_goldRecordObject.SetActive(true);
                m_goldRecordTimeText.text = StringTime.SecondToTimeString(m_raceResultTime.GoldTime);
            }

            if (m_raceResultTime.RecordWasSet)
            {
                m_playerRecordObject.SetActive(true);
                m_playerRecordTimeText.text = StringTime.SecondToTimeString(m_raceResultTime.PlayerRecordTime);
            }
        }

        private void OnRaceCompleted()
        {
            m_goldRecordObject.SetActive(false);
            m_playerRecordObject.SetActive(false);
        }

        private void OnUpdatedResults()
        {
            m_resultsObject.SetActive(true);

            m_playerCurrentTimeText.text = StringTime.SecondToTimeString(m_raceResultTime.CurrentTime);
            m_standardTimeText.text = StringTime.SecondToTimeString(m_raceResultTime.GoldTime);

            if (!m_raceResultTime.RecordWasSet)
            {
                m_newRecordObject.SetActive(false);
                m_recordTimeObject.SetActive(false);
            }
            else
            {
                if (m_raceResultTime.CurrentTime <= m_raceResultTime.PlayerRecordTime)
                {
                    SetRecordObjectsByResult(true);
                }
                else
                {
                    SetRecordObjectsByResult(false);
                }
            }
        }

        private void SetRecordObjectsByResult(bool newRecordWasSet)
        {
            m_newRecordObject.SetActive(newRecordWasSet);
            m_recordTimeObject.SetActive(!newRecordWasSet);

            if (!newRecordWasSet) m_recordTimeText.text = StringTime.SecondToTimeString(m_raceResultTime.PlayerRecordTime);
        }

    }
}
