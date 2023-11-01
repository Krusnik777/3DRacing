using UnityEngine;

namespace Racing
{
    public class SceneDependenciesContainer : Dependency
    {
        [SerializeField] private RaceStateTracker m_raceStateTracker;
        [SerializeField] private RaceTimeTracker m_raceTimeTracker;
        [SerializeField] private RaceResultTime m_raceResultTime;
        [SerializeField] private CarInputControl m_carInputControl;
        [SerializeField] private TrackpointCircuit m_trackpointCircuit;
        [SerializeField] private Car m_car;
        [SerializeField] private CarCameraController m_carCameraController;

        protected override void BindAll(MonoBehaviour monoBehaviourInScene)
        {
            Bind<RaceStateTracker>(m_raceStateTracker, monoBehaviourInScene);
            Bind<RaceTimeTracker>(m_raceTimeTracker, monoBehaviourInScene);
            Bind<RaceResultTime>(m_raceResultTime, monoBehaviourInScene);
            Bind<CarInputControl>(m_carInputControl, monoBehaviourInScene);
            Bind<TrackpointCircuit>(m_trackpointCircuit, monoBehaviourInScene);
            Bind<Car>(m_car, monoBehaviourInScene);
            Bind<CarCameraController>(m_carCameraController, monoBehaviourInScene);
        }

        private void Awake()
        {
            FindAllObjectsToBind();
        }
    }
}
