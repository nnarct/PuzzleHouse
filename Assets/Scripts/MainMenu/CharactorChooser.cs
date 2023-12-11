using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterChooser : MonoBehaviour
{ 
    public void ChooseCharacterByName (string name)
    {
        PlayerPrefs.SetString("Character", name);
        PlayerPrefs.Save();
        SceneManager.LoadScene("EnterPlayerName");
    }
}