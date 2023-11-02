using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    [RequireComponent(typeof(Animator))]
    public class UIRaceStartHint : MonoBehaviour, IDependency<RaceStateTracker>
    {
        private RaceStateTracker m_raceStateTracker;
        public void Construct(RaceStateTracker raceStateTracker) => m_raceStateTracker = raceStateTracker;

        private Text m_text;
        private Animator m_animator;

        private void Start()
        {
            m_text = GetComponent<Text>();
            m_animator = GetComponent<Animator>();

            m_raceStateTracker.EventOnPreparationStarted += OnRacePreparationStarted;
        }

        private void OnDestroy()
        {
            m_raceStateTracker.EventOnPreparationStarted -= OnRacePreparationStarted;
        }

        private void OnRacePreparationStarted()
        {
            m_text.enabled = false;
            m_animator.enabled = false;
            enabled = false;
        }

    }
}
