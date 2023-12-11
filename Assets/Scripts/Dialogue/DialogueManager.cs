using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI NpcNameText; // TextMeshProUGUI component for NPC name display

    public TextMeshProUGUI DialogueText; // TextMeshProUGUI component for dialogue text display

    public GameObject ContinueButton; // Button for continue dialogue

    public GameObject NextSceneButton; // Button for go to next scene

    public Animator Animator; // animation for dialogue box

    public Dialogue Dialogue; // The dialogue data for the current conversation

    private Queue<string> _sentences; // the sentences in dialogue

    private string _lastText; // Last displayed text in the dialogue

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the sentence queue
        _sentences = new Queue<string>();

        // Read player data from JSON file 
        List<PlayerEntry> playerList = new List<PlayerEntry>();
        playerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");

        // Get current playerID from PlayerPrefs
        int playerId = PlayerPrefs.GetInt("PlayerID");
        string playerName = playerList[playerId].PlayerName;

        // Start the dialogue with the NPC
        StartDialogue(Dialogue, playerName);
    }

    // Method to start a dialogue
    public void StartDialogue(Dialogue dialogue, string playerName)
    {
        // Animation for dialogue open
        Animator.SetBool("isOpen", true);

        // set NpcName to display
        NpcNameText.text = dialogue.NpcName;

        // Clear the sentence queue
        _sentences.Clear();

        // Iterate through each sentence in the dialogue\
        foreach (string sentence in dialogue.Sentences)
        {
            //replace {player} in dialogue sentence with player current name 
            string formattedSentence = sentence.Replace("{player}", playerName);

            // Enqueue the sentence queue
            _sentences.Enqueue(formattedSentence);
        }

        // Invoke the method to display the next sentence after a delay
        Invoke("DisplayNextSentence", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
         // Check for any key press when the continue button is active then go next sentence
        if(Input.anyKeyDown && ContinueButton.activeSelf)
        {
            DisplayNextSentence();
        }
    }

    // Method to display the next sentence in the dialogue
    public void DisplayNextSentence()
    {
        // Dequeue the next sentence from the queue
        string sentence = _sentences.Dequeue();

        // Set the dialogue text display to the current sentence
        DialogueText.text = sentence;

        // Stop any ongoing coroutine (typing animation)
        StopAllCoroutines();

        // If there are more sentences, start the typing animation coroutine
        if (_sentences.Count > 0)
        {
            StartCoroutine(TypeSentence(sentence));
        }
        else
        {
            // If no more sentences, complete the typing animation and end the dialogue
            StartCoroutine(TypeSentence(sentence));
            _lastText = sentence;
            EndDialogue();
        }
    }

    // Coroutine to simulate typing animation for a sentence
    IEnumerator TypeSentence(string sentence)
    {
        // Clear the dialogue text
        DialogueText.text = "";

        // Iterate through each character in the sentence, gradually revealing the text
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        // If there are no more sentences, activate the next scene button
        if (_sentences.Count == 0)
        {
            NextSceneButton.SetActive(true);
        }
    }

    // Method to end the dialogue and hide continue button
    public void EndDialogue()
    {
        ContinueButton.SetActive(false);
    }

    // Method to go to the next scene
    public void NextScene()
    {
        // Stop any ongoing coroutine (typing animation)
        StopAllCoroutines();

        // Set the dialogue text to the last displayed text
        DialogueText.text = _lastText;

        // Get the index of the next scene and load it
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
