using UnityEngine;

namespace Racing
{
    public class ClearRaceMarker : MonoBehaviour, IDependency<GameCompletion>
    {
        [SerializeField] private UISelectableButtonContainer[] m_racingButtonsContainers;

        private GameCompletion m_gameCompletion;
        public void Construct(GameCompletion gameCompletion) => m_gameCompletion = gameCompletion;

        private void Start()
        {
            foreach(var container in m_racingButtonsContainers)
            {
                var raceButtons = container.GetComponentsInChildren<UIRaceButton>();

                foreach (var raceButton in raceButtons)
                {
                    if (m_gameCompletion.TryGetRaceStatus(raceButton.RaceKey, out float raceBestTime))
                    {
                        raceButton.SetMark(raceBestTime);
                    }
                    else
                    {
                        raceButton.SetMarkOff();
                    }
                }
            }
        }
    }
}
