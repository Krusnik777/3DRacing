using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(AudioSource))]
    public class WindEffect : MonoBehaviour
    {
        private Car car;
        private AudioSource audioSource;

        private void Start()
        {
            car = transform.root.GetComponent<Car>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            audioSource.volume = car.NormalizedLinearVelocity;
        }
    }
}
