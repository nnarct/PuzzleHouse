using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawManager : MonoBehaviour
{
    [SerializeField] GameObject GamePanel;

    public ScoreManager scoreManager;
    
    public string PuzzleKey = "Santa";

    public List<JigsawPiece> jigsawPieces; // Drag & drop all 8 pieces in the Inspector

    private List<Vector2> initialPositions;

    private void Start()
    {
        StartCoroutine(CheckPiecesAfterDelay());
    }

    IEnumerator CheckPiecesAfterDelay()
    {
        bool allCorrect = false;

        while (!allCorrect)
        {
            allCorrect = true;

            foreach (JigsawPiece piece in jigsawPieces)
            {
                if (!piece.IsPieceCorrect())
                {
                    allCorrect = false;
                }
            }

            yield return new WaitForSeconds(1f); // Add a delay before the next check
        }

        Correct();
    }

    public void Correct()
    {
        scoreManager.HandleCorrectAnswer(PuzzleKey, GamePanel);
    }

    public void ResetPieces()
    {
        for (int i = 0; i < jigsawPieces.Count; i++)
        {
            jigsawPieces[i].ResetPosition();
        }
    }
}
