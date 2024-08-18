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

        // UniTask나 DoTween으로 게이지 차는 함수
        // UpdateExpBarFill(expPrev);
    }

    public void LoadExpData()
    {
        expBar.fillAmount = ExpCurrent / ExpMax;
    }
}
