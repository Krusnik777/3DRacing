using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UIRaceResultPanel : MonoBehaviour, IDependency<RaceResultTime>
    {
        [SerializeField] private GameObject m_resultsPanel;
        [SerializeField] private GameObject m_recordTimeObject;
        [SerializeField] private GameObject m_newRecordObject;
        [SerializeField] private Text m_playerCurrentTimeText;
        [SerializeField] private Text m_standardTimeText;
        [SerializeField] private Text m_recordTimeText;

        private RaceResultTime m_raceResultTime;
        public void Construct(RaceResultTime raceResultTime) => m_raceResultTime = raceResultTime;

        private UISelectableButtonContainer buttonContainer;

        private void Start()
        {
            buttonContainer = GetComponent<UISelectableButtonContainer>();

            m_raceResultTime.EventOnUpdatedResults += OnUpdatedResults;

            m_resultsPanel.SetActive(false);
        }

        private void Update()
        {
            if (m_resultsPanel.activeInHierarchy) ControlPauseMenu();
        }

        private void OnDestroy()
        {
            m_raceResultTime.EventOnUpdatedResults -= OnUpdatedResults;
        }

        private void OnUpdatedResults()
        {
            m_resultsPanel.SetActive(true);

            m_playerCurrentTimeText.text = StringTime.SecondToTimeString(m_raceResultTime.CurrentTime);
            m_standardTimeText.text = StringTime.SecondToTimeString(m_raceResultTime.GoldTime);

            if (!m_raceResultTime.RecordWasSet)
            {
                m_newRecordObject.SetActive(false);
                m_recordTimeObject.SetActive(false);
            }
            else
            {
                if (m_raceResultTime.CurrentTime <= m_raceResultTime.PlayerRecordTime)
                {
                    SetRecordObjectsByResult(true);
                }
                else
                {
                    SetRecordObjectsByResult(false);
                }
            }
        }

        private void SetRecordObjectsByResult(bool newRecordWasSet)
        {
            m_newRecordObject.SetActive(newRecordWasSet);
            m_recordTimeObject.SetActive(!newRecordWasSet);

            if (!newRecordWasSet) m_recordTimeText.text = StringTime.SecondToTimeString(m_raceResultTime.PlayerRecordTime);
        }

        private void ControlPauseMenu()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                buttonContainer.SelectNext();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                buttonContainer.SelectPrevious();
            }

            if (Input.GetButton("Submit"))
            {
                buttonContainer.ActivateButton();
            }
        }
    }
}
