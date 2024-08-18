using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ExpManager : MonoBehaviour
{
    [SerializeField] private Image expBar;

    public float ExpCurrent { private set; get; }
    public float ExpMax { private set; get; } = 50;

    public void AddExp(float amount)
    {
        float expPrev = ExpCurrent;
        ExpCurrent += amount;

        // UniTask�� DoTween���� ������ ���� �Լ�
        // UpdateExpBarFill(expPrev);
    }

    public void LoadExpData()
    {
        expBar.fillAmount = ExpCurrent / ExpMax;
    }
}
