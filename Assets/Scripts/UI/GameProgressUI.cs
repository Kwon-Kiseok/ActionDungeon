using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class GameProgressUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killedEnemyCountText;

    private int _killedEnemyCount = 0;

    private void Start() {
        UpdateKilledCountText();
    }

    public void AddKilledEnemyCount(int count)
    {
        _killedEnemyCount += count;
        UpdateKilledCountText();
    }

    private void UpdateKilledCountText()
    {
        StringBuilder sb = new StringBuilder();
        killedEnemyCountText.text = sb.Append(_killedEnemyCount.ToString("N0")).Append(" Killed").ToString();
    }
}
