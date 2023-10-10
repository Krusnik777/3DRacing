using System;
using UnityEngine;

namespace Racing
{
    [System.Serializable]
    public class WheelAxle
    {
        [SerializeField] private WheelCollider m_leftWheelCollider;
        [SerializeField] private WheelCollider m_rightWheelCollider;

        [SerializeField] private Transform m_leftWheelMesh;
        [SerializeField] private Transform m_rightWheelMesh;

        [SerializeField] private bool m_isMotor;
        public bool IsMotor => m_isMotor;
        [SerializeField] private bool m_isSteer;
        public bool IsSteer => m_isSteer;

        [SerializeField] private float m_wheelWidth;

        [SerializeField] private float m_antiRollForce;

        [SerializeField] private float m_additionalWheelDownForce;

        [SerializeField] private float m_baseForwardStiffness = 1.5f;
        [SerializeField] private float m_stabilityForwardFactor = 1.0f;

        [SerializeField] private float m_baseSidewaysStiffness = 2.0f;
        [SerializeField] private float m_stabilitySidewaysFactor = 1.0f;

        private WheelHit leftWheelHit;
        private WheelHit rightWheelHit;

        #region Public

        public void Update()
        {
            UpdateWheelHits();

            ApplyAntiRoll();
            ApplyDownForce();
            CorrectStiffness();

            SyncMeshTransform();
        }

        public void ApplySteerAngle(float steerAngle, float wheelBaseLength)
        {
            if (!m_isSteer) return;

            float radius = Mathf.Abs(wheelBaseLength * Mathf.Tan(Mathf.Deg2Rad * (90 - Mathf.Abs(steerAngle))));
            float angleSign = Mathf.Sign(steerAngle);

            if (steerAngle > 0)
            {
                m_leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + (m_wheelWidth * 0.5f))) * angleSign;
                m_rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - (m_wheelWidth * 0.5f))) * angleSign;
            }
            else if (steerAngle < 0)
            {
                m_leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - (m_wheelWidth * 0.5f))) * angleSign;
                m_rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + (m_wheelWidth * 0.5f))) * angleSign;
            }
            else
            {
                m_leftWheelCollider.steerAngle = 0;
                m_rightWheelCollider.steerAngle = 0;
            }
        }

        public void ApplyMotorTorque(float motorTorque)
        {
            if (!m_isMotor) return;

            m_leftWheelCollider.motorTorque = motorTorque;
            m_rightWheelCollider.motorTorque = motorTorque;
        }

        public void ApplyBrakeTorque(float brakeTorque)
        {
            m_leftWheelCollider.brakeTorque = brakeTorque;
            m_rightWheelCollider.brakeTorque = brakeTorque;
        }

        #endregion

        #region Private

        private void UpdateWheelHits()
        {
            m_leftWheelCollider.GetGroundHit(out leftWheelHit);
            m_rightWheelCollider.GetGroundHit(out rightWheelHit);
        }

        private void ApplyAntiRoll()
        {
            float travelL = 1.0f;
            float travelR = 1.0f;

            if (m_leftWheelCollider.isGrounded)
                travelL = (-m_leftWheelCollider.transform.InverseTransformPoint(leftWheelHit.point).y - m_leftWheelCollider.radius) / m_leftWheelCollider.suspensionDistance;

            if (m_rightWheelCollider.isGrounded)
                travelR = (-m_rightWheelCollider.transform.InverseTransformPoint(rightWheelHit.point).y - m_rightWheelCollider.radius) / m_rightWheelCollider.suspensionDistance;

            float forceDir = (travelL - travelR);

            if (m_leftWheelCollider.isGrounded)
                m_leftWheelCollider.attachedRigidbody.AddForceAtPosition(m_leftWheelCollider.transform.up * (-forceDir * m_antiRollForce), m_leftWheelCollider.transform.position);

            if (m_rightWheelCollider.isGrounded)
                m_rightWheelCollider.attachedRigidbody.AddForceAtPosition(m_rightWheelCollider.transform.up * (forceDir * m_antiRollForce), m_rightWheelCollider.transform.position);

        }

        private void ApplyDownForce()
        {
            if (m_leftWheelCollider.isGrounded)
                m_leftWheelCollider.attachedRigidbody.AddForceAtPosition(leftWheelHit.normal * 
                    (-m_additionalWheelDownForce * m_leftWheelCollider.attachedRigidbody.velocity.magnitude),m_leftWheelCollider.transform.position);

            if (m_rightWheelCollider.isGrounded)
                m_rightWheelCollider.attachedRigidbody.AddForceAtPosition(rightWheelHit.normal *
                    (-m_additionalWheelDownForce * m_rightWheelCollider.attachedRigidbody.velocity.magnitude), m_rightWheelCollider.transform.position);
        }

        private void CorrectStiffness()
        {
            WheelFrictionCurve leftForward = m_leftWheelCollider.forwardFriction;
            WheelFrictionCurve rightForward = m_rightWheelCollider.forwardFriction;

            WheelFrictionCurve leftSideways = m_leftWheelCollider.sidewaysFriction;
            WheelFrictionCurve rightSideways = m_rightWheelCollider.sidewaysFriction;

            leftForward.stiffness = m_baseForwardStiffness + Mathf.Abs(leftWheelHit.forwardSlip) * m_stabilityForwardFactor;
            rightForward.stiffness = m_baseForwardStiffness + Mathf.Abs(rightWheelHit.forwardSlip) * m_stabilityForwardFactor;

            leftSideways.stiffness = m_baseSidewaysStiffness + Mathf.Abs(leftWheelHit.sidewaysSlip) * m_stabilitySidewaysFactor;
            rightSideways.stiffness = m_baseSidewaysStiffness + Mathf.Abs(rightWheelHit.sidewaysSlip) * m_stabilitySidewaysFactor;

            m_leftWheelCollider.forwardFriction = leftForward;
            m_rightWheelCollider.forwardFriction = rightForward;

            m_leftWheelCollider.sidewaysFriction = leftSideways;
            m_rightWheelCollider.sidewaysFriction = rightSideways;
        }

        private void SyncMeshTransform()
        {
            UpdateWheelTransform(m_leftWheelCollider, m_leftWheelMesh);
            UpdateWheelTransform(m_rightWheelCollider, m_rightWheelMesh);
        }

        private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
        {
            Vector3 position;
            Quaternion rotation;

            wheelCollider.GetWorldPose(out position, out rotation);
            wheelTransform.position = position;
            wheelTransform.rotation = rotation;
        }

        #endregion
    }
}
