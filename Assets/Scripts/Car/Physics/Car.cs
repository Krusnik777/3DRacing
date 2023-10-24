using UnityEngine;
using System;

namespace Racing
{ 
    [RequireComponent(typeof(CarChassis))]
    public class Car : MonoBehaviour
    {
        [SerializeField] private float m_maxSteerAngle;
        [SerializeField] private float m_maxBrakeTorque;

        [Header("Engine")]
        [SerializeField] private AnimationCurve m_engineTorqueCurve;
        [SerializeField] private float m_engineMaxTorque;
        // DEBUG
        [SerializeField] private float m_engineTorque;
        [SerializeField] private float m_engineRpm;

        [SerializeField] private float m_engineMinRpm;
        [SerializeField] private float m_engineMaxRpm;

        [Header("Gearbox")]
        [SerializeField] private float[] m_gears;
        [SerializeField] private float m_finalDriveRatio;
        // DEBUG
        [SerializeField] private int m_selectedGearIndex;
        [SerializeField] private float m_selectedGear;
        [SerializeField] private float m_rearGear;

        [SerializeField] private float m_upShiftEngineRpm;
        [SerializeField] private float m_downShiftEngineRpm;

        [SerializeField] private int m_maxSpeed;

        public event Action<string> EventOnGearChanged;

        public float LinearVelocity => m_chassis.LinearVelocity;
        public float NormalizedLinearVelocity => m_chassis.LinearVelocity / m_maxSpeed;
        public float WheelSpeed => m_chassis.GetWheelSpeed();
        public float MaxSpeed => m_maxSpeed;
        public float EngineRpm => m_engineRpm;
        public float EngineMaxRpm => m_engineMaxRpm;

        public void ApplyHandBrake(bool isActive) => m_chassis.ApplyHandBrake(m_maxBrakeTorque, isActive);

        private CarChassis m_chassis;
        public Rigidbody Rigidbody => m_chassis == null ? GetComponent<CarChassis>().Rigidbody : m_chassis.Rigidbody;

        // DEBUG
        [SerializeField] private float m_linearVelocity;
        public float ThrottleControl;
        public float SteerControl;
        public float BrakeControl;

        #region Public

        public void UpGear()
        {
            ShiftGear(m_selectedGearIndex + 1);
        }

        public void DownGear()
        {
            ShiftGear(m_selectedGearIndex - 1);
        }

        public void ShiftToReverseGear()
        {
            m_selectedGear = m_rearGear;
            EventOnGearChanged?.Invoke(GetSelectedGearName());
        }

        public void ShiftToFirstGear()
        {
            ShiftGear(0);
        }

        public void ShiftNeutral()
        {
            m_selectedGear = 0;

            EventOnGearChanged?.Invoke(GetSelectedGearName());
        }

        public string GetSelectedGearName()
        {
            if (m_selectedGear == m_rearGear) return "R";
            if (m_selectedGear == 0) return "N";

            return (m_selectedGearIndex + 1).ToString();
        }

        #endregion

        #region Private

        private void Start()
        {
            m_chassis = GetComponent<CarChassis>();
        }

        private void Update()
        {
            m_linearVelocity = LinearVelocity;

            UpdateEngineTorque();

            AutoGearShift();

            if (LinearVelocity >= m_maxSpeed) m_engineTorque = 0;

            m_chassis.MotorTorque = m_engineTorque * ThrottleControl;
            m_chassis.SteerAngle = m_maxSteerAngle * SteerControl;
            m_chassis.BrakeTorque = m_maxBrakeTorque * BrakeControl;
        }

        private void ShiftGear(int gearIndex)
        {
            if (gearIndex >= m_gears.Length) return;

            gearIndex = Mathf.Clamp(gearIndex, 0, m_gears.Length - 1);
            m_selectedGear = m_gears[gearIndex];
            m_selectedGearIndex = gearIndex;
            EventOnGearChanged?.Invoke(GetSelectedGearName());
        }

        private void AutoGearShift()
        {
            if (m_selectedGear < 0) return;

            if (m_linearVelocity < 10)
            {
                if (m_selectedGear != 0) ShiftNeutral();
                return;
            }

            if (m_engineRpm >= m_upShiftEngineRpm)
                UpGear();
            if (m_engineRpm < m_downShiftEngineRpm)
                DownGear();
        }

        private void UpdateEngineTorque()
        {
            m_engineRpm = m_engineMinRpm + Mathf.Abs(m_chassis.GetAverageRpm() * m_selectedGear * m_finalDriveRatio);
            m_engineRpm = Mathf.Clamp(m_engineRpm, m_engineMinRpm, m_engineMaxRpm);

            m_engineTorque = m_engineTorqueCurve.Evaluate(m_engineRpm / m_engineMaxRpm) * m_engineMaxTorque * m_finalDriveRatio * Mathf.Sign(m_selectedGear) * m_gears[0];
        }
        #endregion

    }
}
