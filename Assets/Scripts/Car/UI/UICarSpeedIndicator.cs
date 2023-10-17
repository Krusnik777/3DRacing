using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UICarSpeedIndicator : MonoBehaviour
    {
        private Car car;
        private Text speedText;

        private void Start()
        {
            car = transform.root.GetComponent<Car>();
            speedText = GetComponent<Text>();
            speedText.text = "0";
        }

        private void Update()
        {
            if (car) speedText.text = ((int)car.LinearVelocity).ToString();
        }
    }
}
