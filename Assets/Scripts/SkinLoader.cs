using UnityEngine;

public class SkinLoader : MonoBehaviour
{
    public static GameObject selectedPrefab;

    public GameObject AlanPrefab; // Assign prefabs in the Inspector for different characters
    public GameObject JennyPrefab;
    public GameObject HumphreyPrefab;

    private void Awake()
    {
        string choosenCharacter = PlayerPrefs.GetString("Character", "Alan");

        GameObject selectedPrefab = null;

        switch (choosenCharacter)
        {
            case "Alan":
                selectedPrefab = AlanPrefab != null ? AlanPrefab : selectedPrefab;
                break;
            case "Jenny":
                selectedPrefab = JennyPrefab != null ? JennyPrefab : selectedPrefab;
                break;
            case "Humphrey":
                selectedPrefab = HumphreyPrefab != null ? HumphreyPrefab : selectedPrefab;
                break;
            default:
                Debug.LogWarning("No character selected or character prefab not assigned.");
                break;
        }

        if (selectedPrefab != null)
        {
            SkinLoader.selectedPrefab = selectedPrefab;
            Instantiate(selectedPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No player prefab assigned for the chosen character.");
        }
    }
}

