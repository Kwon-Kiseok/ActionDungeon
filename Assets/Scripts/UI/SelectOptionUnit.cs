using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class SelectOptionUnit : MonoBehaviour
{
    [SerializeField] private Image optionIconImage;
    [SerializeField] private TextMeshProUGUI optionNameText;

    [SerializeField] private Button iconButton;

    private EnhaceBonus _enhaceBonus;

    private bool _isClick = false;

    public Subject<Unit> OnClickSubject = new Subject<Unit>();

    private void Start()
    {
        iconButton.onClick.AsObservable().Subscribe(_ =>
        {
            if(_isClick == false)
            {
                DoSelectEffect();
            }
        }).AddTo(this);
    }

    public void SetOptionInfo(EnhaceBonus enhaceBonus)
    {
       // 랜덤으로 플레이어 능력치 업그레이드에 대한 내용이 나와야 함
       // 그렇다면 보너스 업그레이드 관련된 클래스가 하나 추가로 적용되어 있어야 하고
       // 기본 스탯 정보 + 보너스 정보를 갱신하며 적용되는게 나을 것 같음 -> BonusStat or EnhanceStat으로 따로 분류해서 관리하는 게 좋을 듯
       _enhaceBonus = enhaceBonus;
       optionNameText.text = enhaceBonus.GetStatName();
    }

    public void DoSelectEffect()
    {
        _isClick = true;
       this.transform.DOScale(1.25f, 1.5f).SetEase(Ease.OutBack).OnComplete(() => {
            _isClick = false;
            OnClickSubject.OnNext(Unit.Default);
       });
    }
}
