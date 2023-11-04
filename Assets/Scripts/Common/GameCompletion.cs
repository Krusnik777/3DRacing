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

        public void LoadResult(string RaceKey, ref float time)
        {
            foreach (var race in m_completionData.RacesScore)
            {
                if (race.RaceKey == RaceKey)
                {
                    time = race.BestTime;
                    break;
                }
            }
        }

        public void SaveResult(string RaceKey, float time)
        {
            foreach (var race in m_completionData.RacesScore)
            {
                if (race.RaceKey == RaceKey)
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

        public bool TryGetRaceStatus(string RaceKey)
        {
            foreach (var race in m_completionData.RacesScore)
            {
                if (race.RaceKey == RaceKey)
                {
                    return race.Passed;
                }
            }

            return false;
        }

        #endregion

        #region Private

        private void Awake()
        {
            Saver<CompletionData>.TryLoad(Filename, ref m_completionData);
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
