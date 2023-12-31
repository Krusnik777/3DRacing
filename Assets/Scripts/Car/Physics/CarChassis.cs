using System;
using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarChassis : MonoBehaviour
    {
        [SerializeField] private WheelAxle[] m_wheelAxles;
        [SerializeField] private float m_wheelBaseLength;

        [SerializeField] private Transform m_centerOfMass;

        [Header("AngularDrag")]
        [SerializeField] private float m_angularDragMin;
        [SerializeField] private float m_angularDragMax;
        [SerializeField] private float m_angularDragFactor;

        [Header("DownForce")]
        [SerializeField] private float m_downForceMin;
        [SerializeField] private float m_downForceMax;
        [SerializeField] private float m_downForceFactor;

        // DEBUG
        public float MotorTorque;
        public float BrakeTorque;
        public float SteerAngle;

        public float LinearVelocity => rigidbody.velocity.magnitude * 3.6f;

        private new Rigidbody rigidbody;
        public Rigidbody Rigidbody => rigidbody == null ? GetComponent<Rigidbody>() : rigidbody;

        private bool isHandBraked;

        #region Public

        public float GetAverageRpm()
        {
            float sum = 0;

            for (int i = 0; i < m_wheelAxles.Length; i++)
            {
                sum += m_wheelAxles[i].GetAverageRpm();
            }

            return sum / m_wheelAxles.Length;
        }

        public float GetWheelSpeed()
        {
            return GetAverageRpm() * m_wheelAxles[0].GetRadius() * 2 * 0.1885f;
        }

        public void ApplyHandBrake(float maxBrakeTorque, bool isActive)
        {
            isHandBraked = isActive;
            if (isHandBraked) m_wheelAxles[1].ApplyBrakeTorque(maxBrakeTorque);
        }

        public void Reset()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        #endregion

        #region Private

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();

            if (m_centerOfMass != null)
                rigidbody.centerOfMass = m_centerOfMass.localPosition;

            for (int i = 0; i < m_wheelAxles.Length;i++)
            {
                m_wheelAxles[i].ConfigureVehicleSubsteps(50, 50, 50);
            }
        }

        private void FixedUpdate()
        {
            UpdateAngularDrag();

            UpdateDownForce();

            UpdateWheelAxles();
        }

        private void UpdateAngularDrag()
        {
            rigidbody.angularDrag = Mathf.Clamp(m_angularDragFactor * LinearVelocity, m_angularDragMin, m_angularDragMax);
        }

        private void UpdateDownForce()
        {
            float downForce = Mathf.Clamp(m_downForceFactor * LinearVelocity, m_downForceMin, m_downForceMax);
            rigidbody.AddForce(-transform.up * downForce);
        }

        private void UpdateWheelAxles()
        {
            int amountMotorWheel = 0;

            for (int i = 0; i < m_wheelAxles.Length; i++)
            {
                if (m_wheelAxles[i].IsMotor)
                    amountMotorWheel += 2;
            }

            for (int i = 0; i < m_wheelAxles.Length; i++)
            {
                m_wheelAxles[i].Update();

                m_wheelAxles[i].ApplyMotorTorque(MotorTorque / amountMotorWheel);
                m_wheelAxles[i].ApplySteerAngle(SteerAngle, m_wheelBaseLength);
                if (!(isHandBraked && i == 1)) m_wheelAxles[i].ApplyBrakeTorque(BrakeTorque);
            }
        }

        #endregion
    }
}
