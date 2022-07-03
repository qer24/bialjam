using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class LevelEndPanel : Singleton<LevelEndPanel>
{
    [SerializeField] private LeaderboardServerBridge leaderboards;
    
    [Space]
    [SerializeField] private TextMeshProUGUI levelNameText;
    [SerializeField] private GameObject continueButton, resetButton;
    [SerializeField] private TMP_InputField nicknameInput;
    [SerializeField] private GameObject enterNicnkamePanel, leaderBoardPanel;
    [SerializeField] private TextMeshProUGUI leaderboardText;

    private const string NicknameString = "Nick";

    private FirstPersonController player;
    private Vector3 startPos;

    public override void Awake()
    {
        keepAlive = false;
        base.Awake();
        
        player = GameObject.FindWithTag("Player").GetComponentInChildren<FirstPersonController>();
        startPos = player.transform.position;
        
        gameObject.SetActive(false);
    }

    public void OpenEndPanel()
    {
        gameObject.SetActive(true);
        
        nicknameInput.text = PlayerPrefs.GetString(NicknameString);

        var currentLevel = SceneManager.GetActiveScene().buildIndex;
        levelNameText.text = $"Level {currentLevel + 1} Ukończony";

        var isLastLevel = currentLevel + 1 >= SceneManager.sceneCountInBuildSettings;
        continueButton.SetActive(!isLastLevel);
        resetButton.SetActive(isLastLevel);
        
        enterNicnkamePanel.SetActive(true);
        leaderBoardPanel.SetActive(false);
    }

    public void EnterScore()
    {
        PlayerPrefs.SetString(NicknameString, nicknameInput.text);
        StartCoroutine(EnterScoreCoroutine());
    }

    private IEnumerator EnterScoreCoroutine()
    {
        var sendTask = leaderboards.SendUserValue(PlayerPrefs.GetString(NicknameString), TimerManager.instance.timer);
        enterNicnkamePanel.SetActive(false);

        while (!sendTask.IsCompleted)
        {
            yield return null;
        }

        var getTask = leaderboards.RequestUserEntry(PlayerPrefs.GetString(NicknameString));
        
        while (!getTask.IsCompleted)
        {
            yield return null;
        }
        
        var userEntry = getTask.Result;
        var userPlacement = userEntry.position;

        var getAdjTask = leaderboards.RequestEntries(Mathf.Max(userPlacement - 1, 0), 3);
        
        while (!getAdjTask.IsCompleted)
        {
            yield return null;
        }
        var adjEntries = getAdjTask.Result;
        var leaderboardString = new StringBuilder();

        for (int i = 0; i < adjEntries.Count; i++)
        {
            var isPlayer = string.Equals(userEntry.name, adjEntries[i].name, StringComparison.CurrentCultureIgnoreCase);
            var score = adjEntries[i].GetValueAsFloat();
            
            if (isPlayer)
            {
                leaderboardString.Append($"<b>{adjEntries[i].position}. {adjEntries[i].name} - {score.ToTime("00:00.000")}</b>");
            }
            else
            {
                leaderboardString.Append($"{adjEntries[i].position}. {adjEntries[i].name} - {score.ToTime("00:00.000")}");
            }

            if ( i != 2) leaderboardString.Append("\n");
        }

        leaderboardText.text = leaderboardString.ToString();

        leaderBoardPanel.SetActive(true);
    }

    public void Restart()
    {
        RestartManager.instance.Restart();
        
        gameObject.SetActive(false);
    }
    
    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }
}