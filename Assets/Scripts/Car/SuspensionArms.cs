using UnityEngine;

namespace Racing
{
    public class SuspensionArms : MonoBehaviour
    {
        [SerializeField] private Transform m_target;
        [SerializeField] private float m_factor;

        private float baseOffset;

        private void Start()
        {
            baseOffset = m_target.localPosition.y;
        }

        private void Update()
        {
            transform.localEulerAngles = new Vector3(0, 0, (m_target.localPosition.y - baseOffset) * m_factor);
        }
    }
}
