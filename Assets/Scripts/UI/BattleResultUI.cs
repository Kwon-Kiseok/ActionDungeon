using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class BattleResultUI : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanelObject;
    [SerializeField] private GameObject losePanelObject;

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
            losePanelObject.SetActive(false);
        if(victoryPanelObject.activeSelf)
            victoryPanelObject.SetActive(false);
    }
}
