using UnityEngine;

public class SkinLoader : MonoBehaviour
{
    public static GameObject selectedPrefab; // Static reference to the selected character's prefab

    // Assign prefabs in the Inspector for different characters
    public GameObject AlanPrefab;
    public GameObject JennyPrefab;
    public GameObject HumphreyPrefab;

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