using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class UnitRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }
    [SerializeField] private Animator animator;
    [SerializeField] private UnitStatUI unitStatUI;
    
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

    public UnitStatUI GetUnitStatUI()
    {
        return unitStatUI;
    }

    public void DoAttackAnim()
    {
        animator.SetTrigger("Attack_Trigger");
    }

    public void DoDodgeAnim()
    {
        spriteRenderer.transform.DOLocalMoveX(-0.65f, 0.2f).SetEase(Ease.OutBack).OnComplete(async () => { 
        await UniTask.Delay(200);
        spriteRenderer.transform.DOLocalMoveX(0f, 0.2f).SetEase(Ease.InBack);
        });
    }
}
