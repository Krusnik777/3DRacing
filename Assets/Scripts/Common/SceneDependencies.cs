using UnityEngine;

namespace Racing
{
    public interface IDependency<T>
    {
        void Construct(T obj);
    }


    public class SceneDependencies : MonoBehaviour
    {
        [SerializeField] private RaceStateTracker m_raceStateTracker;
        [SerializeField] private RaceTimeTracker m_raceTimeTracker;
        [SerializeField] private RaceResultTime m_raceResultTime;
        [SerializeField] private CarInputControl m_carInputControl;
        [SerializeField] private TrackpointCircuit m_trackpointCircuit;
        [SerializeField] private Car m_car;
        [SerializeField] private CarCameraController m_carCameraController;

        private void Bind(MonoBehaviour mono)
        {
            if (mono is IDependency<RaceStateTracker>) (mono as IDependency<RaceStateTracker>).Construct(m_raceStateTracker);
            if (mono is IDependency<RaceTimeTracker>) (mono as IDependency<RaceTimeTracker>).Construct(m_raceTimeTracker);
            if (mono is IDependency<RaceResultTime>) (mono as IDependency<RaceResultTime>).Construct(m_raceResultTime);
            if (mono is IDependency<CarInputControl>) (mono as IDependency<CarInputControl>).Construct(m_carInputControl);
            if (mono is IDependency<TrackpointCircuit>) (mono as IDependency<TrackpointCircuit>).Construct(m_trackpointCircuit);
            if (mono is IDependency<Car>) (mono as IDependency<Car>).Construct(m_car);
            if (mono is IDependency<CarCameraController>) (mono as IDependency<CarCameraController>).Construct(m_carCameraController);
        }

        private void Awake()
        {
            MonoBehaviour[] allMonoInScene = FindObjectsOfType<MonoBehaviour>();

            for (int i = 0; i < allMonoInScene.Length; i++)
            {
                Bind(allMonoInScene[i]);
            }
        }
    }
}
