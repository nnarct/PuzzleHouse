using UnityEngine;

public class SkinLoader : MonoBehaviour
{
    public GameObject AlanPrefab; // Assign prefabs in the Inspector for different characters
    public GameObject JennyPrefab;
    public GameObject HumphreyPrefab;

    private void Awake()
    {
        string choosenCharacter = PlayerPrefs.GetString("Character", "Alan");

        switch (choosenCharacter)
        {
            case "Alan":
                Instantiate(AlanPrefab, transform);
                break;
            case "Jenny":
                Instantiate(JennyPrefab, transform);
                break;
            case "Humphrey":
                Instantiate(HumphreyPrefab, transform);
                break;
            default:
                Instantiate(AlanPrefab, transform);
                break;
        }
    }
}
