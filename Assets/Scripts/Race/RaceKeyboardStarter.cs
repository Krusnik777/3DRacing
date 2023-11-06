using UnityEngine;

namespace Racing
{
    public class RaceKeyboardStarter : MonoBehaviour, IDependency<RaceStateTracker>
    {
        private RaceStateTracker m_raceStateTracker;
        public void Construct(RaceStateTracker raceStateTracker) => m_raceStateTracker = raceStateTracker;

        private void Update()
        {
            if (Input.GetButtonDown("Submit"))
            {
                m_raceStateTracker.LaunchPreparationStart();
                enabled = false;
            }
        }
    }
}
