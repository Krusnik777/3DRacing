using UnityEngine;

namespace Racing
{
    public class CarRespawner : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<Car>, IDependency<CarInputControl>
    {
        [SerializeField] private float m_respawnHeight;

        private RaceStateTracker m_raceStateTracker;
        public void Construct(RaceStateTracker raceStateTracker) => m_raceStateTracker = raceStateTracker;

        private Car m_car;
        public void Construct(Car car) => m_car = car;

        private CarInputControl m_carControl;
        public void Construct(CarInputControl carControl) => m_carControl = carControl;

        private TrackPoint respawnTrackPoint;

        #region Public

        public void Respawn()
        {
            if (respawnTrackPoint == null) return;

            if (m_raceStateTracker.State != RaceState.Race) return;

            m_car.Respawn(respawnTrackPoint.transform.position + respawnTrackPoint.transform.up * m_respawnHeight, respawnTrackPoint.transform.rotation);

            m_carControl.Reset();
        }

        #endregion

        #region Private

        private void Start()
        {
            m_raceStateTracker.EventOnTrackPointPassed += OnTrackPointPassed;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Respawn();
            }
        }

        private void OnDestroy()
        {
            m_raceStateTracker.EventOnTrackPointPassed -= OnTrackPointPassed;
        }
        private void OnTrackPointPassed(TrackPoint point)
        {
            respawnTrackPoint = point;
        }

        #endregion
    }
}
