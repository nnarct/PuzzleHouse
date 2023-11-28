using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawManager : MonoBehaviour
{
    [SerializeField]
    GameObject CorrectPanel;
    [SerializeField]
    GameObject GamePanel;
    
    public List<JigsawPiece> jigsawPieces; // Drag & drop all 8 pieces in the Inspector

    private List<Vector2> initialPositions;

    private void Start()
    {
        CorrectPanel.SetActive(false);
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
                    //Debug.Log("Piece is incorrect.");
                }
            }

            yield return new WaitForSeconds(1f); // Add a delay before the next check
        }

        // All pieces are in correct positions
        CorrectPanel.SetActive(true);
        Invoke("ClosePanel", 1f);
    }

    public void ClosePanel()
    {
        GamePanel.SetActive(false);
    }

    public void ResetPieces()
    {
        for (int i = 0; i < jigsawPieces.Count; i++)
        {
            jigsawPieces[i].ResetPosition();
        }
    }
}
