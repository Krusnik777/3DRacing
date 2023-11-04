using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public class RaceResultTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>, IDependency<GameCompletion>
    {
        [SerializeField] private float m_goldTime;
        public float GoldTime => m_goldTime;

        public event UnityAction EventOnUpdatedResults;

        private RaceStateTracker m_raceStateTracker;
        public void Construct(RaceStateTracker raceStateTracker) => m_raceStateTracker = raceStateTracker;

        private RaceTimeTracker m_raceTimeTracker;
        public void Construct(RaceTimeTracker raceTimeTracker) => m_raceTimeTracker = raceTimeTracker;

        private GameCompletion m_gameCompletion;
        public void Construct(GameCompletion gameCompletion) => m_gameCompletion = gameCompletion;

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

        private void Start()
        {
            Load();

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
            m_gameCompletion.LoadResult(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, ref playerRecordTime);

            //playerRecordTime = PlayerPrefs.GetFloat(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + SaveMark, 0f);
        }

        private void Save()
        {
            m_gameCompletion.SaveResult(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, playerRecordTime);

            //PlayerPrefs.SetFloat(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + SaveMark, playerRecordTime);
        }

        #endregion
    }
}
