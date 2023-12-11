using UnityEngine;

public class SkinLoader : MonoBehaviour
{
    public static GameObject selectedPrefab; // Static reference to the selected character's prefab
    
    public GameObject AlanPrefab; // A reference to the Alan prefab assigned in the Inspector

    public GameObject JennyPrefab; // A reference to the Jenny prefab assigned in the Inspector

    public GameObject HumphreyPrefab; // A reference to the Humphrey prefab assigned in the Inspector

    // Awake is called when the script instance is activated
    private void Awake()
    {
        // Get the character choice from PlayerPrefs or default to "Alan" if not found
        string choosenCharacter = PlayerPrefs.GetString("Character", "Alan");

        // Initialize selectedPrefab to null
        GameObject selectedPrefab = null;

        // Choose the character prefab based on the saved choice
        switch (choosenCharacter)
        {
            // Assign AlanPrefab to selectedPrefab if Alan was chosen and AlanPrefab is assigned
            case "Alan":
                selectedPrefab = AlanPrefab != null ? AlanPrefab : selectedPrefab;
                break;

            // Assign JennyPrefab to selectedPrefab if Jenny was chosen and JennyPrefab is assigned
            case "Jenny":
                selectedPrefab = JennyPrefab != null ? JennyPrefab : selectedPrefab;
                break;

            // Assign HumphreyPrefab to selectedPrefab if Humphrey was chosen and HumphreyPrefab is assigned
            case "Humphrey":
                selectedPrefab = HumphreyPrefab != null ? HumphreyPrefab : selectedPrefab;
                break;

            // Log a warning if no character is selected or the character prefab is not assigned
            default:
                Debug.LogWarning("No character selected or character prefab not assigned.");
                break;
        }

        // Check if a valid selectedPrefab exists
        if (selectedPrefab != null)
        {
            // Set the static reference to the selectedPrefab
            SkinLoader.selectedPrefab = selectedPrefab;

            // Instantiate the selectedPrefab at the SkinLoader's position with no rotation
            Instantiate(selectedPrefab, transform.position, Quaternion.identity);
        }

    }
}