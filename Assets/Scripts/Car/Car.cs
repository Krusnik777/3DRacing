using UnityEngine;

namespace Racing
{ 
    [RequireComponent(typeof(CarChassis))]
    public class Car : MonoBehaviour
    {
        [SerializeField] private float m_maxSteerAngle;
        [SerializeField] private float m_maxBrakeTorque;

        [SerializeField] private AnimationCurve m_engineTorqueCurve;
        [SerializeField] private float m_maxMotorTorque;
        [SerializeField] private int m_maxSpeed;

        public float LinearVelocity => m_chassis.LinearVelocity;
        public float WheelSpeed => m_chassis.GetWheelSpeed();
        public float MaxSpeed => m_maxSpeed;

        public void ApplyHandBrake(bool isActive) => m_chassis.ApplyHandBrake(m_maxBrakeTorque, isActive);

        private CarChassis m_chassis;

        // DEBUG
        [SerializeField] private float m_linearVelocity;
        public float ThrottleControl;
        public float SteerControl;
        public float BrakeControl;

        private void Start()
        {
            m_chassis = GetComponent<CarChassis>();
        }

        private void Update()
        {
            m_linearVelocity = LinearVelocity;

            float engineTorque = m_engineTorqueCurve.Evaluate(LinearVelocity / m_maxSpeed) * m_maxMotorTorque;

            if (LinearVelocity >= m_maxSpeed) engineTorque = 0;

            m_chassis.MotorTorque = engineTorque * ThrottleControl;
            m_chassis.SteerAngle = m_maxSteerAngle * SteerControl;
            m_chassis.BrakeTorque = m_maxBrakeTorque * BrakeControl;
        }
    }
}
