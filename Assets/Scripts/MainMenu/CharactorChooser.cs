using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterChooser : MonoBehaviour
{ 
    //string[] Charactor = { "Alan" ,"Atanasoff", "Edward", "Humphrey"};
    public void ChooseCharacterByName (string name)
    {
        PlayerPrefs.SetString("character", name);
        PlayerPrefs.Save();
        SceneManager.LoadScene("EnterPlayerName");
    }

    //string choosenCharacter = PlayerPrefs.GetString("character");
}