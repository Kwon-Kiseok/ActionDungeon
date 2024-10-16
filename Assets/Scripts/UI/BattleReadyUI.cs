using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class BattleReadyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemyNameText;
    [SerializeField] private Image bgImage;
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
        ReadyAnimation();

        await UniTask.WaitForSeconds(5f);
        CloseUI();
    }

    public void CloseUI()
    {
        enemyIntroduceObject.SetActive(false);
        touchGuardImage.enabled = false;
    }

    private void ReadyAnimation() 
    {
        bgImage.DOFade(0.0f, 0f);
        enemyPortraitImage.DOFade(0.0f, 0f);
        enemyNameText.DOFade(0.0f, 0f);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(bgImage.DOFade(1.0f, 1f))
        .Join(enemyPortraitImage.DOFade(1.0f, 1f))
        .Join(enemyNameText.DOFade(1.0f, 1f))
        .OnComplete(async () => {
            await UniTask.WaitForSeconds(2f);
            CloseAnimation();
        });

        sequence.Play();
    }

    private void CloseAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(bgImage.DOFade(0.0f, 1f))
        .Join(enemyPortraitImage.DOFade(0.0f, 1f))
        .Join(enemyNameText.DOFade(0.0f, 1f));

        sequence.Play();
    }

}
