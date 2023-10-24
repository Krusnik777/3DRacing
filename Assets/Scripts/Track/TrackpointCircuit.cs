using System;
using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public enum TrackType
    {
        Circular,
        Sprint
    }

    public class TrackpointCircuit : MonoBehaviour
    {
        [SerializeField] private TrackType m_type;
        public TrackType Type => m_type;

        public event UnityAction<TrackPoint> EventOnTrackPointTriggered;
        public event UnityAction<int> EventOnLapCompleted;

        private int lapsCompleted = -1;

        private TrackPoint[] m_points;

        private void Start()
        {
            BuildCircuit();

            for (int i=0; i< m_points.Length;i++)
            {
                m_points[i].EventOnTriggered += OnTrackPointTriggered;
            }

            m_points[0].AssignAsTarget();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < m_points.Length; i++)
            {
                m_points[i].EventOnTriggered -= OnTrackPointTriggered;
            }
        }

        [ContextMenu(nameof(BuildCircuit))]
        private void BuildCircuit()
        {
            m_points = TrackCircuitBuilder.Build(transform, m_type);
        }

        private void OnTrackPointTriggered(TrackPoint trackPoint)
        {
            if (trackPoint.IsTarget == false) return;

            trackPoint.Passed();
            trackPoint.Next?.AssignAsTarget();

            EventOnTrackPointTriggered?.Invoke(trackPoint);

            if (trackPoint.IsLast == true)
            {
                lapsCompleted++;

                if (m_type == TrackType.Sprint)
                    EventOnLapCompleted?.Invoke(lapsCompleted);

                if (m_type == TrackType.Circular)
                    if (lapsCompleted > 0)
                        EventOnLapCompleted?.Invoke(lapsCompleted);
            }
        }
    }
}
