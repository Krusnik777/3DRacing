using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(AudioSource))]
    public class WheelEffect : MonoBehaviour
    {
        [SerializeField] private WheelCollider[] m_wheels;
        [SerializeField] private ParticleSystem[] m_wheelsSmoke;
        [SerializeField] private float m_forwardSlipLimit;
        [SerializeField] private float m_sidewaysSlipLimit;
        [SerializeField] private GameObject m_skidPrefab;

        private AudioSource m_audio;
        private WheelHit wheelHit;
        private Transform[] skidTrail;

        private void Start()
        {
            skidTrail = new Transform[m_wheels.Length];
            m_audio = GetComponent<AudioSource>();
        }

        private void Update()
        {
            bool isSlip = false;

            for (int i = 0; i < m_wheels.Length; i++)
            {
                m_wheels[i].GetGroundHit(out wheelHit);

                if (m_wheels[i].isGrounded)
                {
                    if (wheelHit.forwardSlip > m_forwardSlipLimit || wheelHit.sidewaysSlip > m_sidewaysSlipLimit)
                    {
                        if (skidTrail[i] == null)
                            skidTrail[i] = Instantiate(m_skidPrefab).transform;

                        if (!m_audio.isPlaying) m_audio.Play();

                        if (skidTrail[i] != null)
                        {
                            skidTrail[i].position = m_wheels[i].transform.position - wheelHit.normal * m_wheels[i].radius;
                            skidTrail[i].forward = -wheelHit.normal;

                            m_wheelsSmoke[i].transform.position = skidTrail[i].position;
                            m_wheelsSmoke[i].Emit(1);
                        }

                        isSlip = true;

                        continue;
                    }
                }

                skidTrail[i] = null;
                m_wheelsSmoke[i].Stop();
            }
            if (isSlip == false) m_audio.Stop();
        }
    }
}
