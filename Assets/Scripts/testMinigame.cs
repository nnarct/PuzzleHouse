using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMinigame : MonoBehaviour
{
    [SerializeField] GameObject GamePanel;

    public void ButtonOrderPanelClose()
    {
        GamePanel.SetActive(false);
    }

    public void ButtonOrderPanelOpen()
    {
        GamePanel.SetActive(true);
    }
}
