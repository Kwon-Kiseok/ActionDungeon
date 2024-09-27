using UnityEngine;
using Zenject;
using UniRx;

public class TurnClockSystem : MonoBehaviour
{
    [SerializeField] private TurnCountUI daysCountUI;
    [SerializeField] private TurnCountUI nightCountUI;

    private int _turnCount = 5;
    private bool _isDays = true;
    public bool IsDays => _isDays;

    private BattleManager _battleManager;

    [Inject]
    public void Inject(BattleManager battleManager)
    {
        _battleManager = battleManager;
    }

    private void Start() {
        TurnCount();
        
        nightCountUI.DisableUI();
    }

    private void TurnCount()
    {
        if(_battleManager != null)
        {
            _battleManager.OnActionSubject.Subscribe((_) => {
                
                if(_isDays)
                {
                    daysCountUI.CheckTurnUI(_turnCount);
                }
                else
                {
                    nightCountUI.CheckTurnUI(_turnCount);
                }
                _turnCount--;
                
                if(_turnCount <= 0)
                {
                    _isDays = !_isDays;
                    if(_isDays == true)
                    {
                        daysCountUI.ResetTurnUI();
                    }
                    else
                    {
                        nightCountUI.ResetTurnUI();
                    }
                    _turnCount = 5;
                }
            }).AddTo(this);
        }
    }
}
