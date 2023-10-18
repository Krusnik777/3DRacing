using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UIGearboxIndicator : MonoBehaviour
    {
        private Car car;
        private Text gearboxIndexText;

        private void Start()
        {
            car = transform.root.GetComponent<Car>();
            gearboxIndexText = GetComponent<Text>();
            gearboxIndexText.text = "0";

            car.EventOnGearChanged += OnGearChanged;
        }

        private void OnGearChanged(string gearName)
        {
            gearboxIndexText.text = gearName;
        }

        private void OnDestroy()
        {
            car.EventOnGearChanged -= OnGearChanged;
        }
    }
}
