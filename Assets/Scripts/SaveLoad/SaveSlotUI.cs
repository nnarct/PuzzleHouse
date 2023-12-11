using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SaveSlotUI : MonoBehaviour
{

    [SerializeField] GameObject PlayerSlotPrefab;

    [SerializeField] GameObject PlayerSlotDelPrefab;

    [SerializeField] GameObject NewGameButton;

    [SerializeField] Transform ElementWrapper;

    [SerializeField] Transform ElementWrapperDel;

    string Filename = "PlayerData.json";

    List<GameObject> uiElements = new List<GameObject>();

    List<GameObject> uiElementsDel = new List<GameObject>();
    
    public List<PlayerEntry> PlayerList = new List<PlayerEntry>();

    // Start is called before the first frame update
    void Start()
    {
        PlayerList = FileHandler.ReadListFromJSON<PlayerEntry>(Filename);
        _updateUI();
    }

    private void _updateUI()
    {
        for (int i = 0; i < PlayerList.Count; i++)
        {
            PlayerEntry el = PlayerList[i];

            if (el != null)
            {
                if (i >= uiElements.Count)
                {
                    // instantiate new entry
                    var inst = Instantiate(PlayerSlotPrefab, Vector3.zero, Quaternion.identity);
                    var instDel = Instantiate(PlayerSlotDelPrefab, Vector3.zero, Quaternion.identity);
                    inst.transform.SetParent(ElementWrapper, false);
                    instDel.transform.SetParent(ElementWrapperDel, false);

                    uiElements.Add(inst);
                    uiElementsDel.Add(instDel);
                }

                // write or overwrite name & points
                uiElements[i].name = i.ToString();
                uiElementsDel[i].name = i.ToString();
                var texts = uiElements[i].GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = el.PlayerName;
            }
        }
        if (PlayerList.Count < 3)
        {
            var inst = Instantiate(NewGameButton, Vector3.zero, Quaternion.identity);
            inst.transform.SetParent(ElementWrapper, false);
            uiElements.Add(inst);
            
        }
        
    }

}
