using UnityEngine;

namespace Racing
{
    public class RaceInputController : MonoBehaviour, IDependency<CarInputControl>, IDependency<RaceStateTracker>
    {
        private CarInputControl m_carControl;
        public void Construct(CarInputControl carControl) => m_carControl = carControl;

        private RaceStateTracker m_raceStateTracker;
        public void Construct(RaceStateTracker raceStateTracker) => m_raceStateTracker = raceStateTracker;

        private void Start()
        {
            m_raceStateTracker.EventOnStarted += OnRaceStarted;
            m_raceStateTracker.EventOnCompleted += OnRaceFinished;

            m_carControl.enabled = false;
        }

        private void OnDestroy()
        {
            m_raceStateTracker.EventOnStarted -= OnRaceStarted;
            m_raceStateTracker.EventOnCompleted -= OnRaceFinished;
        }

        private void OnRaceStarted()
        {
            m_carControl.enabled = true;
        }

        private void OnRaceFinished()
        {
            m_carControl.Stop();
            m_carControl.enabled = false;
        }
    }
}
