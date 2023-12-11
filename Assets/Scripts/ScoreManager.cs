using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Reflection;


public class ScoreManager : MonoBehaviour
{
    
    public TMP_Text ScoreText;// Reference to the TextMeshProUGUI for displaying the score

    public int Stage; // Stage identifier
    
    public List<PuzzleKeysData> PuzzleKeys; // List of PuzzleKeysData for managing puzzles
    
    public GameObject CorrectPanel; // Reference to the CorrectPanel GameObject
    
    private int _maxScore; // Maximum possible score
    
    private bool _isOpenCorrectPanel = false; // Flag to check if the CorrectPanel is open

    private string _puzzleKey; // Stores the puzzle key for the correct answer

    private MovementPlayer _movementPlayer;// Reference to the MovementPlayer script

    [SerializeField] private AudioSource _openCorrectPanelSound; // Audio source for the correct panel sound

    [SerializeField] private AudioSource _clickKeySound; // Audio source for the key click sound

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Load player data from the file to PlayerPrefs
        LoadFileToPlayerPrefs();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Find the MovementPlayer script in the scene
        _movementPlayer = FindObjectOfType<MovementPlayer>();

        // Check for null references
        if (ScoreText == null)
        {
            Debug.LogError("Error! Cannot find reference of ScoreText.");
        } 
        if(Stage == 0)
        {
            Debug.LogError("Error! Cannot find reference of Stage.");
        } 
        if(PuzzleKeys.Count == 0)
        {
            Debug.LogError("Error! Cannot find reference of PuzzleKeys.");
        }
        if(CorrectPanel == null)
        {
            Debug.LogError("Error! Cannot find reference of CorrectPanel.");
        };

        // Set the maximum score based on the number of PuzzleKeys
        _maxScore = PuzzleKeys.Count;

        // Load player data from the file to PlayerPrefs
        LoadFileToPlayerPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the score display
        UpdateScoreDisplay();

        // Check for input when the CorrectPanel is open
        if (_isOpenCorrectPanel)
        {
            if (Input.anyKeyDown)
            {
                // Play the key click sound and manage the score
                _clickKeySound.Play();
                Invoke("ManageScore", 0.5f);
            }
        }
    }

    // Subscribe to the CorrectAnswerEvent when the ScoreManager is enabled
    private void OnEnable()
    {
        QuizManager.CorrectAnswerEvent += HandleCorrectAnswer;
    }

    // Unsubscribe from the CorrectAnswerEvent when the ScoreManager is disabled
    private void OnDisable()
    {
        QuizManager.CorrectAnswerEvent -= HandleCorrectAnswer;
    }

    // Handle the correct answer event by showing the CorrectPanel
    public void HandleCorrectAnswer(string puzzleKey, GameObject puzzlePanel)
    {
        // Check for null parameters
        if (puzzlePanel == null)
        {
            Debug.LogError("Parameter puzzlePanel cannot be null.");
            return;
        }

        // Set the puzzle key and show the CorrectPanel
        if (!string.IsNullOrEmpty(puzzleKey))
        {
            // Store the puzzle key for future reference
            _puzzleKey = puzzleKey;

            // Hide the puzzle panel
            puzzlePanel.SetActive(false);

            // Activate the CorrectPanel to provide feedback
            CorrectPanel.SetActive(true);

            // Set the flag to indicate that the CorrectPanel is open
            _isOpenCorrectPanel = true;

            // Play the correct panel opening sound
            _openCorrectPanelSound.Play();

            // Delay changing the CorrectPanel status for smoother animations
            Invoke("DelayCorrectPanelStatus", 2f);
        }
        else
        {
            Debug.LogError("Error! Puzzle Key not found.");
        }
    }

    // Manage the score by updating PlayerPrefs and UI
    private void ManageScore()
    {
        // Deactivate the CorrectPanel to hide it from the UI
        CorrectPanel.SetActive(false);

        // Set the flag to indicate that the CorrectPanel is closed
        _isOpenCorrectPanel = false;

        // Update PlayerPrefs to mark the current puzzle as solved
        PlayerPrefs.SetInt(_puzzleKey, 1);

        // Save PlayerPrefs to persist the changes
        PlayerPrefs.Save();

        // Update the specific stage field for the puzzle key
        UpdateStage1Field(_puzzleKey, 1);

        // Update the score display in the UI
        UpdateScoreDisplay();

        // Unfreeze player movement to allow further interaction
        _movementPlayer.UnfreezeMovement();
    }

    // Update the score display based on PlayerPrefs values
    private void UpdateScoreDisplay()
    {
        // Initialize the current score to zero
        int currentScore = 0;

        // Calculate the current score based on PuzzleKeys
        for (int i = 0; i < _maxScore; i++)
        {
            // Check for missing puzzle key
            if (string.IsNullOrEmpty(PuzzleKeys[i].Key))
            {
                // Log an error and return
                Debug.LogError("Error! Puzzle Key not found.");
                return;
            }

            // Get the point value from PlayerPrefs
            int point = PlayerPrefs.GetInt(PuzzleKeys[i].Key, 0);
            
            // Add the point value to the current score
            currentScore += point;
        }

        // Update the TextMeshProUGUI score display
        ScoreText.text = currentScore.ToString() + "/" + _maxScore.ToString();

        // Update the PlayerPrefs score based on the stage
        if (Stage == 1)
        {
            PlayerPrefs.SetInt("Stage1_score", currentScore);
        }
        else if (Stage == 2)
        {
            PlayerPrefs.SetInt("Stage2_score", currentScore);
        }

        // Save PlayerPrefs
        PlayerPrefs.Save();
    }

    // Delay setting CorrectPanel status to allow animations
    public void DelayCorrectPanelStatus()
    {
        _isOpenCorrectPanel = true;
        Invoke("ManageScore", 4f);
    }

    // Load data from the file to PlayerPrefs
    void LoadFileToPlayerPrefs()
    {
        List<PlayerEntry> playerList = new List<PlayerEntry>();

        // Read player data from the JSON file
        playerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");

        // Get the player ID from PlayerPrefs
        int playerId = PlayerPrefs.GetInt("PlayerID");

        // Set the character choice in PlayerPrefs
        PlayerPrefs.SetString("Character", playerList[playerId].CharacterChoose);

        // Iterate through PuzzleKeysData and set PlayerPrefs values
        foreach (var puzzleData in PuzzleKeys)
        {
            string puzzleKey = puzzleData.Key;
            int isSolved = 0;

            // Check the stage and get the corresponding puzzle status
            if (Stage == 1)
            {
                // Get the field info for the puzzle key
                FieldInfo fieldInfo = typeof(Stage1).GetField(puzzleKey);

                // Get the value of the field for the current player
                isSolved = (int)fieldInfo.GetValue(playerList[playerId].Stage1);
            }
            else if (Stage == 2)
            {
                // Get the field info for the puzzle key
                FieldInfo fieldInfo = typeof(Stage2).GetField(puzzleKey);

                // Get the value of the field for the current player
                isSolved = (int)fieldInfo.GetValue(playerList[playerId].Stage2);
            }

            // Set PlayerPrefs value for the puzzle key
            PlayerPrefs.SetInt(puzzleKey, isSolved);
        }

        // Save PlayerPrefs
        PlayerPrefs.Save();
    }

    void UpdateStage1Field(string fieldName, int value)
    {
        // Read player data from the JSON file
        List<PlayerEntry> playerList = new List<PlayerEntry>();

        // Read player data from the JSON file
        playerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");

        // Get the player ID from PlayerPrefs
        int playerId = PlayerPrefs.GetInt("PlayerID");

        // Get the stage data for the current player
        Stage1 stage1 = playerList[playerId].Stage1;
        Stage2 stage2 = playerList[playerId].Stage2;

        // Get the field info for the puzzle key
        FieldInfo fieldInfo;

        // Update the field value based on the stage
        if(Stage == 1)
        {
            // Get the field info for the puzzle key
            fieldInfo = typeof(Stage1).GetField(fieldName);
            // Get the value of the field for the current player
            fieldInfo.SetValue(stage1, value);
        }
        else if(Stage == 2)
        {
            // Get the field info for the puzzle key
            fieldInfo = typeof(Stage2).GetField(fieldName);
            // Get the value of the field for the current player
            fieldInfo.SetValue(stage2, value);
        }
        else
        {
            // Log an error if the field is not found
            UnityEngine.Debug.LogError($"Field not found or not writable: {fieldName}");
        }

        // Save the updated stage data back to the player list
        FileHandler.SaveToJSON<PlayerEntry>(playerList, "PlayerData.json");
    }

}

[System.Serializable]
public class PuzzleKeysData
{
    public string Key;
}
