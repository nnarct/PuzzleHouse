using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterChooser : MonoBehaviour
{
    //string[] Charactor = { "Alan" ,"Atanasoff", "Edward", "Humphrey"};
    public void ChooseCharactorByName (string name)
    {
        PlayerPrefs.SetString("charactor", name);
        PlayerPrefs.Save();
        SceneManager.LoadScene("EnterPlayerName");
    }
    
}