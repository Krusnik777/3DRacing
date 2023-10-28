using UnityEngine;

namespace Racing
{
    public class CarInputControl : MonoBehaviour, IDependency<Car>
    {
        [SerializeField] private AnimationCurve m_brakeCurve;
        [SerializeField] private AnimationCurve m_steerCurve;

        [SerializeField] [Range(0.0f, 1.0f)] private float m_autoBrakeStrength = 0.1f;

        private Car m_car;
        public void Construct(Car car) => m_car = car;

        private float wheelSpeed;
        private float verticalAxis;
        private float horizontalAxis;
        private float handBrakeAxis;

        #region Public

        public void Reset()
        {
            verticalAxis = 0;
            horizontalAxis = 0;
            handBrakeAxis = 0;

            m_car.ThrottleControl = 0;
            m_car.SteerControl = 0;
            m_car.BrakeControl = 0;
        }

        public void Stop()
        {
            Reset();

            m_car.BrakeControl = 1;
        }

        #endregion

        #region Private

        private void Update()
        {
            wheelSpeed = m_car.WheelSpeed;

            UpdateAxis();

            UpdateThrottleAndBrake();
            UpdateSteer();
            UpdateHandBrake();

            UpdateAutoBrake();

            //DEBUG
            if (Input.GetButtonDown("ShiftGearUp"))
                m_car.UpGear();
            if (Input.GetButtonDown("ShiftGearDown"))
                m_car.DownGear();
        }

        private void UpdateAxis()
        {
            verticalAxis = Input.GetAxis("Vertical");
            horizontalAxis = Input.GetAxis("Horizontal");
            handBrakeAxis = Input.GetAxis("HandBrake");
        }

        private void UpdateThrottleAndBrake()
        {
            if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
            {
                m_car.ThrottleControl = Mathf.Abs(verticalAxis);
                m_car.BrakeControl = 0;
            }
            else
            {
                m_car.ThrottleControl = 0;
                m_car.BrakeControl = m_brakeCurve.Evaluate(wheelSpeed / m_car.MaxSpeed);
            }

            if (verticalAxis < 0 && wheelSpeed > -0.5f && wheelSpeed <= 0.5f)
                m_car.ShiftToReverseGear();

            if (verticalAxis > 0 && wheelSpeed > -0.5f && wheelSpeed < 0.5f)
                m_car.ShiftToFirstGear();
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

        #endregion
    }
}
