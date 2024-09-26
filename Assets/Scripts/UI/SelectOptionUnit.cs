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
        optionNameText.text = "";
       _enhaceBonus = enhaceBonus;
        SetRarityColor(_enhaceBonus.GetRarity());
       optionNameText.text = enhaceBonus.GetStatName();
    }

    private void SetRarityColor(EnhaceBonus.Rarity rarity)
    {
        if(rarity == EnhaceBonus.Rarity.NORMAL)
        {
            optionIconImage.color = Color.white;
        }
        else if(rarity == EnhaceBonus.Rarity.RARE)
        {
            optionIconImage.color = Color.blue;
        }
        else if(rarity == EnhaceBonus.Rarity.UNIQUE)
        {
            optionIconImage.color = Color.magenta;
        }
        else if(rarity == EnhaceBonus.Rarity.LEGENDARY)
        {
            optionIconImage.color = Color.yellow;
        }
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
