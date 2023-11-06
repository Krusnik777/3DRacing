using System;
using UnityEngine;

namespace Racing
{
    public class GameCompletion : MonoBehaviour
    {
        public const string Filename = "GameCompletion.dat";

        [Serializable]
        private class RaceScore
        {
            public string RaceKey;
            public float BestTime = 0;
            public bool Passed = false;
        }

        [Serializable]
        private class CompletionData
        {
            public RaceScore[] RacesScore;
            public int PassedAmount = 0;
        }

        [SerializeField] private CompletionData m_completionData;

        public int PassedRacesAmount => m_completionData.PassedAmount;

        #region Public

        public void LoadResult(string raceKey, ref float time)
        {
            foreach (var race in m_completionData.RacesScore)
            {
                if (race.RaceKey == raceKey)
                {
                    time = race.BestTime;
                    break;
                }
            }
        }

        public void SaveResult(string raceKey, float time)
        {
            foreach (var race in m_completionData.RacesScore)
            {
                if (race.RaceKey == raceKey)
                {
                    race.BestTime = time;
                    if (race.Passed == false)
                    {
                        race.Passed = true;
                        UpdatePassedAmount();
                    }
                    Saver<CompletionData>.Save(Filename, m_completionData);
                    break;
                }
            }
        }

        public bool TryGetRaceStatus(string raceKey)
        {
            foreach (var race in m_completionData.RacesScore)
            {
                if (race.RaceKey == raceKey)
                {
                    return race.Passed;
                }
            }

            return false;
        }

        public bool TryGetRaceStatus(string raceKey, out float bestTime)
        {
            foreach (var race in m_completionData.RacesScore)
            {
                if (race.RaceKey == raceKey)
                {
                    bestTime = race.BestTime;
                    return race.Passed;
                }
            }
            bestTime = 0;
            return false;
        }

        #endregion

        #region Private

        private void Awake()
        {
            Saver<CompletionData>.TryLoad(Filename, ref m_completionData);

            FileHandler.EventOnReset += OnReset;
        }

        private void OnDestroy()
        {
            FileHandler.EventOnReset -= OnReset;
        }

        private void OnReset(string filename)
        {
            if (filename == Filename)
            {
                foreach (var race in m_completionData.RacesScore)
                {
                    race.BestTime = 0;
                    race.Passed = false;
                }
                m_completionData.PassedAmount = 0;
            }
        }

        private void UpdatePassedAmount()
        {
            var currentAmount = m_completionData.PassedAmount;

            m_completionData.PassedAmount = 0;

            foreach (var passed in m_completionData.RacesScore)
            {
                if (passed.Passed) m_completionData.PassedAmount++;
            }

            if (currentAmount < m_completionData.PassedAmount) Saver<CompletionData>.Save(Filename, m_completionData);
        }

        #endregion
    }
}
