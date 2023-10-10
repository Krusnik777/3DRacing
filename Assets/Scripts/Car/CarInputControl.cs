using UnityEngine;

namespace Racing
{
    public class CarInputControl : MonoBehaviour
    {
        [SerializeField] private Car m_car;

        private void Update()
        {
            m_car.ThrottleControl = Input.GetAxis("Vertical");
            m_car.BrakeControl = Input.GetAxis("Jump");
            m_car.SteerControl = Input.GetAxis("Horizontal");
        }
    }
}
