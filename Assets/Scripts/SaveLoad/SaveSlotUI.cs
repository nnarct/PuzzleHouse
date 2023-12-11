using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SaveSlotUI : MonoBehaviour
{
    [SerializeField] public GameObject PlayerSlotPrefab; // Reference to the prefab for the player slot

    [SerializeField] public GameObject PlayerSlotDelPrefab; // Reference to the prefab for the player slot delete button

    [SerializeField] public GameObject NewGameButton; // Reference to the new game button

    [SerializeField] public Transform ElementWrapper; // Reference to the wrapper for player slots

    [SerializeField] public Transform ElementWrapperDel; // Reference to the wrapper for player slot delete buttons
    
    public List<PlayerEntry> PlayerList = new List<PlayerEntry>(); // List to store player entries

    private List<GameObject> uiElements = new List<GameObject>();  // Lists to store UI elements for player slots button

    private List<GameObject> uiElementsDel = new List<GameObject>();  // Lists to store UI elements for delete player button
    
    private string filename = "PlayerData.json"; // file name  player data

    // Start is called before the first frame update
    void Start()
    {
        // Read player data from JSON file and update the UI
        PlayerList = FileHandler.ReadListFromJSON<PlayerEntry>(filename);
        _updateUI();
    }

    // method to update the UI select sever based on player data
    private void _updateUI()
    {
        // print all PlayerList in player data
        for (int i = 0; i < PlayerList.Count; i++)
        {
            PlayerEntry el = PlayerList[i];

            if (el != null)
            {
                if (i >= uiElements.Count)
                {
                    // Instantiate new entry and delete button
                    var inst = Instantiate(PlayerSlotPrefab, Vector3.zero, Quaternion.identity);
                    var instDel = Instantiate(PlayerSlotDelPrefab, Vector3.zero, Quaternion.identity);
                    inst.transform.SetParent(ElementWrapper, false);
                    instDel.transform.SetParent(ElementWrapperDel, false);

                    // Add instantiated objects to the lists
                    uiElements.Add(inst);
                    uiElementsDel.Add(instDel);
                }

                // Write or overwrite name in the UI
                uiElements[i].name = i.ToString();
                uiElementsDel[i].name = i.ToString();
                var texts = uiElements[i].GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = el.PlayerName;
            }
        }

        // If the number of player slots is less than 3, instantiate a new game button
        if (PlayerList.Count < 3)
        {
            var inst = Instantiate(NewGameButton, Vector3.zero, Quaternion.identity);
            inst.transform.SetParent(ElementWrapper, false);
            uiElements.Add(inst);
        }
    }
}
