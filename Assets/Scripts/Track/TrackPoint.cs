using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public class TrackPoint : MonoBehaviour
    {
        public event UnityAction<TrackPoint> EventOnTriggered;

        public TrackPoint Next;
        public bool IsFirst;
        public bool IsLast;

        protected bool isTarget;
        public bool IsTarget => isTarget;

        protected virtual void OnPassed() { }
        protected virtual void OnAssignAsTarget() { }

        #region Public

        public void Passed()
        {
            isTarget = false;
            OnPassed();
        }

        public void AssignAsTarget()
        {
            isTarget = true;
            OnAssignAsTarget();
        }

        public void Reset()
        {
            Next = null;
            IsFirst = false;
            IsLast = false;
        }

        #endregion

        #region Private

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.GetComponent<Car>() == null) return;

            EventOnTriggered?.Invoke(this);
        }

        #endregion
    }
}
