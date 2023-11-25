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

    private void Start()
    {
        CorrectPanel.SetActive(false);
    }

    void Update()
    {
        bool isAllCorrect = false;

        foreach (JigsawPiece piece in jigsawPieces)
        {
            if (!piece.IsPieceCorrect())
            {
                isAllCorrect = false;
                break; // Break the loop if any piece is incorrect
            }
        }

        if (isAllCorrect)
        {
            // All pieces are in correct positions
            CorrectPanel.SetActive(true);
            Invoke("ClosePanel", 1f);
            //Debug.Log("All pieces are correct!");
        }
    }

    public void ClosePanel()
    {
        GamePanel.SetActive(false);
    }
}
