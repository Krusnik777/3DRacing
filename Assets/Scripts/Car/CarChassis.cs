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

        public float LinearVelocity => rigidBody.velocity.magnitude * 3.6f;

        private new Rigidbody rigidBody;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();

            if (m_centerOfMass != null)
                rigidBody.centerOfMass = m_centerOfMass.localPosition;
        }

        private void FixedUpdate()
        {
            UpdateAngularDrag();

            UpdateDownForce();

            UpdateWheelAxles();
        }

        private void UpdateAngularDrag()
        {
            rigidBody.angularDrag = Mathf.Clamp(m_angularDragFactor * LinearVelocity, m_angularDragMin, m_angularDragMax);
        }

        private void UpdateDownForce()
        {
            float downForce = Mathf.Clamp(m_downForceFactor * LinearVelocity, m_downForceMin, m_downForceMax);
            rigidBody.AddForce(-transform.up * downForce);
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
                m_wheelAxles[i].ApplyBrakeTorque(BrakeTorque);

            }
        }
    }
}
