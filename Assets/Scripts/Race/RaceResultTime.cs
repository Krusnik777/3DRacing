using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public class RaceResultTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>
    {
        public const string SaveMark = "_player_best_time";

        [SerializeField] private float m_goldTime;
        public float GoldTime => m_goldTime;

        public event UnityAction EventOnUpdatedResults;

        private RaceStateTracker m_raceStateTracker;
        public void Construct(RaceStateTracker raceStateTracker) => m_raceStateTracker = raceStateTracker;

        private RaceTimeTracker m_raceTimeTracker;
        public void Construct(RaceTimeTracker raceTimeTracker) => m_raceTimeTracker = raceTimeTracker;

        private float playerRecordTime;
        public float PlayerRecordTime => playerRecordTime;
        private float currentTime;
        public float CurrentTime => currentTime;

        public bool RecordWasSet => playerRecordTime != 0;

        #region Public

        public float GetAbsoluteRecord()
        {
            if (playerRecordTime < m_goldTime && playerRecordTime != 0)
                return playerRecordTime;
            else
                return m_goldTime;
        }

        #endregion

        #region Private

        private void Awake()
        {
            Load();
        }

        private void Start()
        {
            m_raceStateTracker.EventOnCompleted += OnRaceCompleted;
        }

        private void OnDestroy()
        {
            m_raceStateTracker.EventOnCompleted -= OnRaceCompleted;
        }

        private void OnRaceCompleted()
        {
            float absoluteRecord = GetAbsoluteRecord();

            if (m_raceTimeTracker.CurrentTime < absoluteRecord)
            {
                playerRecordTime = m_raceTimeTracker.CurrentTime;

                Save();
            }

            currentTime = m_raceTimeTracker.CurrentTime;

            EventOnUpdatedResults?.Invoke();
        }

        private void Load()
        {
            playerRecordTime = PlayerPrefs.GetFloat(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + SaveMark, 0f);
        }

        private void Save()
        {
            PlayerPrefs.SetFloat(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + SaveMark, playerRecordTime);
        }

        #endregion
    }
}
