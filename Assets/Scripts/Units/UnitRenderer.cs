using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using TMPro;

public class UnitRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI failText;
    
    public void SetUnitSprite(Sprite sprite)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
        }
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    public void DoAttackAnim()
    {
        animator.SetTrigger("Attack_Trigger");
    }

    public void DoDefenceAnim()
    {

    }

    public void DoDodgeAnim()
    {
        float dodgeDistance = -0.65f;
        if(spriteRenderer.flipX)
        {
            dodgeDistance *= -1;
        }

        spriteRenderer.transform.DOLocalMoveX(dodgeDistance, 0.2f).SetEase(Ease.OutBack).OnComplete(async () => { 
        await UniTask.Delay(200);
        spriteRenderer.transform.DOLocalMoveX(0f, 0.2f).SetEase(Ease.InBack);
        });
    }

    public void DoHitAnim()
    {
        // 흰색 or 붉은색 + 반투명으로 이미지가 깜빡 거리는 연출이 들어가면 좋을 것 같음
        // ex. 록맨 피격 연출
        
        spriteRenderer.color = Color.red;

        Sequence seq = DOTween.Sequence();

        seq.Append(spriteRenderer.DOFade(0.2f, 0.1f))
            .Append(spriteRenderer.DOFade(1f, 0.1f))
            .SetLoops(3)
            .OnComplete(() => {
                spriteRenderer.color = Color.white;
            });

        seq.Play();
    }


    public void DoActionFail()
    {
        FailTextAnim();
    }

    private void FailTextAnim()
    {
        Sequence seq = DOTween.Sequence();

        failText.color = new Color(failText.color.r, failText.color.g, failText.color.b, 1);

        seq.Append(failText.transform.DOLocalMoveY(0.2f, 0.5f).SetEase(Ease.OutBack))
           .Join(failText.DOFade(0.0f, 0.45f))
           .OnComplete(() =>
           {
               failText.transform.localPosition = Vector3.zero;
           });

        seq.Play();
    }
}
