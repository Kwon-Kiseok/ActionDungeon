using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using DG.Tweening;

public class SelectOptionUnit : MonoBehaviour
{
    [SerializeField] private Image optionIconImage;
    [SerializeField] private TextMeshProUGUI optionNameText;

    [SerializeField] private Button iconButton;

    private void Start()
    {
        iconButton.onClick.AsObservable().Subscribe(_ =>
        {
            DoSelectEffect();
        }).AddTo(this);
    }

    // 매개변수로 옵션에 대한 정보를 받아오고 옵션 아이템 UI에 설정을 하는 방식
    public void SetOptionInfo()
    {
       
    }

    // 해당 옵션을 선택할 때 연출 함수
    public void DoSelectEffect()
    {
       this.transform.DOPunchScale(transform.localScale, 1f);
    }
}
