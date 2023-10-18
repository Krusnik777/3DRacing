using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UICarEngineIndicator : MonoBehaviour
    {
        [System.Serializable]
        public class EngineIndicatorColor
        {
            public float MaxRpm;
            public Color Color;
        }

        [SerializeField] private EngineIndicatorColor[] m_colors;
        private Image engineRpmIndicator;
        private Car car;

        private void Start()
        {
            car = transform.root.GetComponent<Car>();
            engineRpmIndicator = GetComponent<Image>();
        }

        private void Update()
        {
            if (car)
            {
                engineRpmIndicator.fillAmount = car.EngineRpm / car.EngineMaxRpm;

                for (int i = 0; i < m_colors.Length; i++)
                {
                    if (car.EngineRpm <= m_colors[i].MaxRpm)
                    {
                        engineRpmIndicator.color = m_colors[i].Color;
                        break;
                    }
                }
            }
        }
    }
}
