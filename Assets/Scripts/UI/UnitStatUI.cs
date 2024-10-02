using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;

public class UnitStatUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI defText;
    [SerializeField] private TextMeshProUGUI lukText;
    [SerializeField] private TextMeshProUGUI apText;

    public void SetStatus(Units.statData status, bool isDaysDeBuff)
    {
        hpText.text = string.Format($"HP: {status.hp.ToString()}");
        atkText.text = string.Format($"ATK: {status.atk.ToString()}");
        defText.text = string.Format($"DEF: {status.def.ToString()}");

        if(isDaysDeBuff)
        {
            lukText.color = Color.red;
            lukText.text = string.Format($"LUK: {(status.luk * 0.5f).ToString()}");
        }
        else
        {
            lukText.color = Color.white;
            lukText.text = string.Format($"LUK: {status.luk.ToString()}");
        }
    }

    public void SetAPUI(int actionPoint)
    {
        apText.text = string.Format($"AP: {actionPoint.ToString()}");
    }
}
