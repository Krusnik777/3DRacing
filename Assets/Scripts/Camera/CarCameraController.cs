using UnityEngine;

namespace Racing
{
    public class CarCameraController : MonoBehaviour, IDependency<Car>, IDependency<RaceStateTracker>
    {
        [SerializeField] private Camera m_camera;
        [SerializeField] private CarCameraFollow m_follower;
        [SerializeField] private CarCameraShaker m_shaker;
        [SerializeField] private CarCameraFovCorrector m_fovCorrector;
        [SerializeField] private CarCameraPostProcessingCorrector m_postProcessingCorrector;
        [SerializeField] private CameraPathFollower m_pathFollower;

        private Car m_car;
        public void Construct(Car car) => m_car = car;

        private RaceStateTracker m_raceStateTracker;
        public void Construct(RaceStateTracker raceStateTracker) => m_raceStateTracker = raceStateTracker;

        private void Awake()
        {
            m_follower.SetPropertioes(m_car, m_camera);
            m_shaker.SetPropertioes(m_car, m_camera);
            m_fovCorrector.SetPropertioes(m_car, m_camera);
            m_postProcessingCorrector.SetPropertioes(m_car, m_camera);
        }

        private void Start()
        {
            m_raceStateTracker.EventOnPreparationStarted += OnPreparationStarted;
            m_raceStateTracker.EventOnCompleted += OnCompleted;

            m_follower.enabled = false;
            m_pathFollower.enabled = true;
        }

        private void OnDestroy()
        {
            m_raceStateTracker.EventOnPreparationStarted -= OnPreparationStarted;
            m_raceStateTracker.EventOnCompleted -= OnCompleted;
        }

        private void OnPreparationStarted()
        {
            m_follower.enabled = true;
            m_pathFollower.enabled = false;
        }

        private void OnCompleted()
        {
            m_pathFollower.enabled = true;
            m_pathFollower.StartMovementToNearestPoint();
            m_pathFollower.SetLookTarget(m_car.transform);

            m_follower.enabled = false;
        }

    }
}