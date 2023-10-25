using UnityEngine;

namespace Racing
{
    public class RaceTimeTracker : MonoBehaviour, IDependency<RaceStateTracker>
    {
        private RaceStateTracker m_raceStateTracker;
        public void Construct(RaceStateTracker raceStateTracker) => m_raceStateTracker = raceStateTracker;

        private float currentTime;
        public float CurrentTime => currentTime;

        private void Start()
        {
            m_raceStateTracker.EventOnStarted += OnRaceStarted;
            m_raceStateTracker.EventOnCompleted += OnRaceCompleted;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
        }

        private void OnDestroy()
        {
            m_raceStateTracker.EventOnStarted -= OnRaceStarted;
            m_raceStateTracker.EventOnCompleted -= OnRaceCompleted;
        }

        private void OnRaceStarted()
        {
            enabled = true;
            currentTime = 0;
        }

        private void OnRaceCompleted()
        {
            enabled = false;
        }

        
    }
}
