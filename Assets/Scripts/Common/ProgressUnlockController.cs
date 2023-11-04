using System;
using UnityEngine;

namespace Racing
{
    public class ProgressUnlockController : MonoBehaviour, IDependency<GameCompletion>
    {
        public enum UnlockType
        {
            PassedRacesAmount,
            PassedCertainRace
        }

        [Serializable]
        private class UnlockCondition
        {
            public UnlockType Type;
            public int RacesAmount;
            public string RaceKey;
        }

        [Serializable]
        private class Race
        {
            public UIRaceButton RaceButton;
            public UnlockCondition UnlockCondition;
        }

        [Serializable]
        private class Season
        {
            public UISelectableButton SeasonButton;
            public int PassedRacesToProgress;
            public Race[] Races;
        }

        private GameCompletion m_gameCompletion;
        public void Construct(GameCompletion gameCompletion) => m_gameCompletion = gameCompletion;

        [SerializeField] private Season[] m_Seasons;

        private void Start()
        {
            foreach (var season in m_Seasons)
            {
                if (season.PassedRacesToProgress > m_gameCompletion.PassedRacesAmount)
                {
                    season.SeasonButton.SetInteractible(false);
                    season.SeasonButton.SetLocker(season.PassedRacesToProgress);
                }
                else
                {
                    season.SeasonButton.SetInteractible(true);
                    season.SeasonButton.SetLockerOff();
                }

                foreach (var race in season.Races)
                {
                    if (race.UnlockCondition.Type == UnlockType.PassedRacesAmount)
                    {
                        if (race.UnlockCondition.RacesAmount > m_gameCompletion.PassedRacesAmount)
                        {
                            race.RaceButton.SetInteractible(false);
                            race.RaceButton.SetLocker(race.UnlockCondition.RacesAmount);
                        }
                        else
                        {
                            race.RaceButton.SetInteractible(true);
                            race.RaceButton.SetLockerOff();
                        }
                    }

                    if (race.UnlockCondition.Type == UnlockType.PassedCertainRace)
                    {
                        if (!m_gameCompletion.TryGetRaceStatus(race.UnlockCondition.RaceKey))
                        {
                            race.RaceButton.SetInteractible(false);
                            race.RaceButton.SetLocker("Clear Prev. Race");
                        }
                        else
                        {
                            race.RaceButton.SetInteractible(true);
                            race.RaceButton.SetLockerOff();
                        }
                    }
                }
            }
        }


    }
}
