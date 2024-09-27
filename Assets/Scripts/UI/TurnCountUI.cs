using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TurnCountUI : MonoBehaviour
{
    [SerializeField] private List<Image> countImageList = new List<Image>();

    public void CheckTurnUI(int turnCount)
    {
        countImageList[countImageList.Count - turnCount].enabled = false;
    }

    public void ResetTurnUI()
    {
        foreach(var image in countImageList)
        {
            image.enabled = true;
        }
    }

    public void DisableUI()
    {
        foreach (var image in countImageList)
        {
            image.enabled = false;
        }
    }
}
