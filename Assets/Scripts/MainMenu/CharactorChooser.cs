using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterChooser : MonoBehaviour
{ 
    // Method to choose a character by name and transition to the "EnterPlayerName" scene.
    /*
     * Attach this method to a Unity button's onClick event in the Inspector.
     * Set the 'name' parameter to the desired character's name to be stored in PlayerPrefs.
     */
    public void ChooseCharacterByName (string name)
    {
        // Save the chosen character's name in PlayerPrefs
        PlayerPrefs.SetString("Character", name);

        // Save the PlayerPrefs changes
        PlayerPrefs.Save();

        // Load the "EnterPlayerName" scene to proceed with player name input
        SceneManager.LoadScene("EnterPlayerName");
    }
}