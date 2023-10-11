using UnityEngine;

namespace Racing
{
    public class CarInputControl : MonoBehaviour
    {
        [SerializeField] private Car m_car;
        [SerializeField] private AnimationCurve m_brakeCurve;
        [SerializeField] private AnimationCurve m_steerCurve;

        [SerializeField] [Range(0.0f, 1.0f)] private float m_autoBrakeStrength = 0.1f;

        private float wheelSpeed;
        private float verticalAxis;
        private float horizontalAxis;
        private float handBrakeAxis;

        private void Update()
        {
            wheelSpeed = m_car.WheelSpeed;

            UpdateAxis();

            UpdateThrottleAndBrake();
            UpdateSteer();
            UpdateHandBrake();

            UpdateAutoBrake();
        }

        private void UpdateAxis()
        {
            verticalAxis = Input.GetAxis("Vertical");
            horizontalAxis = Input.GetAxis("Horizontal");
            handBrakeAxis = Input.GetAxis("Jump");
        }

        private void UpdateThrottleAndBrake()
        {
            if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
            {
                m_car.ThrottleControl = verticalAxis;
                m_car.BrakeControl = 0;
            }
            else
            {
                m_car.ThrottleControl = 0;
                m_car.BrakeControl = m_brakeCurve.Evaluate(wheelSpeed / m_car.MaxSpeed);
            }
        }

        private void UpdateSteer()
        {
            m_car.SteerControl = m_steerCurve.Evaluate(wheelSpeed / m_car.MaxSpeed) * horizontalAxis;
        }

        private void UpdateHandBrake()
        {
            m_car.ApplyHandBrake(handBrakeAxis == 1);
        }

        private void UpdateAutoBrake()
        {
            if (verticalAxis == 0)
            {
                m_car.BrakeControl = m_brakeCurve.Evaluate(wheelSpeed / m_car.MaxSpeed) * m_autoBrakeStrength;
            }
        }
    }
}
