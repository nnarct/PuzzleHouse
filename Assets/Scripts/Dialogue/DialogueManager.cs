using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI NpcNameText;
    public TextMeshProUGUI DialogueText;
    public GameObject ContinueButton;
    public GameObject NextSceneButton;

    private Queue<string> _sentences;

    public Animator Animator;
    public Dialogue Dialogue;

    private string _lastText;

    // Start is called before the first frame update
    void Start()
    {
        _sentences = new Queue<string>();

        List<PlayerEntry> playerList = new List<PlayerEntry>();
        playerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");
        int playerId = PlayerPrefs.GetInt("PlayerID");
        string playerName = playerList[playerId].PlayerName;

        StartDialogue(Dialogue, playerName);
    }

    public void StartDialogue(Dialogue dialogue, string playerName)
    {
        Animator.SetBool("isOpen", true);
        NpcNameText.text = dialogue.NpcName;

        _sentences.Clear();

        foreach (string sentence in dialogue.Sentences)
        {
            string formattedSentence = sentence.Replace("{player}", playerName);
            _sentences.Enqueue(formattedSentence);
        }


        Invoke("DisplayNextSentence", 1.0f);
    }

    public void DisplayNextSentence()
    {
        string sentence = _sentences.Dequeue();
        DialogueText.text = sentence;
        StopAllCoroutines();

        if (_sentences.Count > 0)
        {
            StartCoroutine(TypeSentence(sentence));
        }
        else
        {
            StartCoroutine(TypeSentence(sentence));
            _lastText = sentence;
            EndDialogue();

        }


    }

    IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        if (_sentences.Count == 0)
        {

            NextSceneButton.SetActive(true);
        }
    }

    public void EndDialogue()
    {

        Debug.Log("End");
        ContinueButton.SetActive(false);

    }

    public void NextScene()
    {
        StopAllCoroutines();
        DialogueText.text = _lastText;

        //Animator.SetBool("isOpen", false);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
