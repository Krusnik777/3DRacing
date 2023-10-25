using UnityEngine;

namespace Racing
{
    public class ActivatedTrackPoint : TrackPoint
    {
        [SerializeField] private GameObject m_hint;

        private void Start()
        {
            m_hint.SetActive(isTarget);
        }

        protected override void OnPassed()
        {
            m_hint.SetActive(false);
        }

        protected override void OnAssignAsTarget()
        {
            m_hint.SetActive(true);
        }
    }
}
