using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(AudioSource))]
    public class GearShiftSound : MonoBehaviour
    {
        private Car car;
        private AudioSource audioSource;

        private void Start()
        {
            car = transform.root.GetComponent<Car>();
            audioSource = GetComponent<AudioSource>();

            car.EventOnGearChanged += OnGearChanged;
        }

        private void OnGearChanged(string obj)
        {
            audioSource.Play();
        }
    }
}
