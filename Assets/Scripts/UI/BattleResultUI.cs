using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class BattleResultUI : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanelObject;
    [SerializeField] private GameObject losePanelObject;

    public Subject<Unit> OnNextBattleSubject = new Subject<Unit>();
    // public Subject<Unit> OnGameOverSubject = new Subject<Unit>();

    public void BattleResultEnable(Player player)
    {
        if(player.IsAlive)
        {
            VictoryUIEnable();
        }
        else
        {
            LoseUIEnable();
        }
    }

    private void VictoryUIEnable()
    {
        victoryPanelObject.SetActive(true);
        losePanelObject.SetActive(false);
    }

    private void LoseUIEnable()
    {
        losePanelObject.SetActive(true);
        victoryPanelObject.SetActive(false);
    }

    public void ConfirmUI()
    {
        if(losePanelObject.activeSelf)
        {
            losePanelObject.SetActive(false);
            // go to game result or main title scene
            
            // OnGameOverSubject.OnNext(Unit.Default);
        }
        if(victoryPanelObject.activeSelf)
        {
            victoryPanelObject.SetActive(false);
            OnNextBattleSubject.OnNext(Unit.Default);
        }
    }
}
