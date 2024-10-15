using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using Cysharp.Threading.Tasks;

public class BattleReadyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemyNameText;
    [SerializeField] private Image enemyPortraitImage;
    [SerializeField] private Image touchGuardImage;
    [SerializeField] private GameObject enemyIntroduceObject;

    private const string PortraitPath = "Unit_Portrait/";

    public void SetEnemyInfoUI(Enemy enemy)
    {
        StringBuilder sb = new StringBuilder();
        string path = sb.Append(PortraitPath).Append(enemy.unitName).ToString();

        enemyNameText.text = enemy.unitName;
        AddressableSpriteLoader.SetSprite(enemyPortraitImage, path);
    }

    public async void IntroduceNextEnemy()
    {
        enemyIntroduceObject.SetActive(true);
        touchGuardImage.enabled = true;

        // *인트로 연출 추가*
        await UniTask.WaitForSeconds(2f);
        CloseUI();
    }

    public void CloseUI()
    {
        enemyIntroduceObject.SetActive(false);
        touchGuardImage.enabled = false;
    }

}
