using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class SuccessCountUI : MonoBehaviour
{
    [SerializeField] private List<Image> successIconImages = new List<Image>();

    public void ActivateIconImage(int count)
    {
        int iconIndex = count - 1;
        successIconImages[iconIndex].enabled = true;
        successIconImages[iconIndex].transform.localScale = Vector3.zero;
        successIconImages[iconIndex].transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }

    public void ActivateIconImages(int count)
    {
        for(int i = count; i > 0; i--)
        {
            ActivateIconImage(i);
        }
    }

    public void DeActivateIconImages()
    {
        foreach(var image in successIconImages)
        {
            image.enabled = false;
        }
    }
}
