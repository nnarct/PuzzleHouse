using UnityEngine;

public class SkinLoader : MonoBehaviour
{
    public GameObject alanPrefab; // Assign prefabs in the Inspector for different characters
    public GameObject jennyPrefab;
    public GameObject humphreyPrefab;

    private void Awake()
    {
        string choosenCharacter = PlayerPrefs.GetString("character");

        switch (choosenCharacter)
        {
            case "Alan":
                Instantiate(alanPrefab, transform);
                break;
            case "Jenny":
                Instantiate(jennyPrefab, transform);
                break;
            case "Humphrey":
                Instantiate(humphreyPrefab, transform);
                break;
            default:
                Debug.LogWarning("No character selected or character prefab not assigned.");
                break;
        }
    }
}
