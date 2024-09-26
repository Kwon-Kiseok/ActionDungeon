using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;

public class ActionEnhanceBonus : MonoBehaviour
{
    [SerializeField] private DraftSystemUI draftSystemUI;
    [SerializeField] private SuccessCountUI successCountUI;

    // 3 Success Action => 1 Enhance Bonus
    private int _successCount = 0;
    public int SuccessCount => _successCount;

    private const int maxCount = 3;

    private Player _player;

    private void Start() {
        successCountUI.DeActivateIconImages();
    }

    public void SetPlayer(Player player)
    {
        _player = player;

        draftSystemUI.OnSelectBonusStatSubject.Subscribe((bonus) => {
            _player.AddBonusStatData(bonus.GetStatData());
            _player.OnSelectBonusStatSubject.OnNext(Unit.Default);
        }).AddTo(this);
    }

    public void IncreaseSuccessCount()
    {
        _successCount++;
        successCountUI.ActivateIconImage(_successCount);
        if(CheckSuccessCount())
        {
            _successCount = 0;
            ActivateDraftUI();
        }
    }

    private bool CheckSuccessCount()
    {
        return _successCount >= 3;
    }

    private void ActivateDraftUI()
    {
        draftSystemUI.PrepareOptions();
        draftSystemUI.PanelObject.SetActive(true);
        successCountUI.DeActivateIconImages();
    }
}
