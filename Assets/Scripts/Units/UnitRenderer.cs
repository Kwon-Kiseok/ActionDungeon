using UnityEngine;

public class UnitRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
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
}
