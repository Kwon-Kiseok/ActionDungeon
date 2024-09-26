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
       _enhaceBonus = enhaceBonus;
       optionNameText.text = enhaceBonus.GetStatName();
    }

    public EnhaceBonus GetEnhanceBonus()
    {
        return _enhaceBonus;
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
