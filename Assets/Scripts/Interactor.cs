using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] GameObject InteractText;
    [SerializeField] GameObject Puzzle;
    public bool IsInRange;
    public bool IsInPuzzle;
    public string PuzzleKey;

    [SerializeField] private AudioSource _source;

    private MovementPlayer _movementPlayer;

    private void Start()
    {
        Puzzle.SetActive(false);
        _movementPlayer = FindObjectOfType<MovementPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerPrefs.GetInt(PuzzleKey, 0) == 0 || PuzzleKey.Length == 0)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                InteractText.gameObject.SetActive(true);
                IsInRange = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InteractText.gameObject.SetActive(false);
            IsInRange = false;
        }
    }

    private void Update()
    {
        if (IsInRange && Input.GetKeyDown(KeyCode.E) && InteractText.gameObject.activeSelf)
        {
            PlayPuzzle();
            _movementPlayer.FreezeMovement();
        }

        if (IsInPuzzle && Input.GetKeyDown(KeyCode.Escape))
        {
            EndInteraction();
            _source.Stop();
        }
    }

    public void PlayPuzzle()
    {
        _source.Play();
        Puzzle.SetActive(true);
        InteractText.gameObject.SetActive(false);
        IsInPuzzle = true;
    }

    public void EndInteraction()
    {
        _source.Play();
        Debug.Log("sound on.");

        Puzzle.SetActive(false);
        _movementPlayer.UnfreezeMovement();

        if (PlayerPrefs.GetInt(PuzzleKey, 0) == 0 || PuzzleKey.Length == 0)
        {
            InteractText.gameObject.SetActive(true);
        }
        IsInPuzzle = false;
    }
}
