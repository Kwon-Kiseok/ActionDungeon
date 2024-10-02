using UnityEngine;
using UniRx;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using TMPro;

public class UnitRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI failText;

    public Subject<Unit> DeathAnimationEndSubject = new Subject<Unit>();
    public Subject<Unit> DeleteUnitSubject = new Subject<Unit>();

    [SerializeField] private ParticleSystem defenceParticle;
    [SerializeField] private ParticleSystem hitParticle;

    private void Start()
    {
        this.DeathAnimationEndSubject.Subscribe(async (_) =>
        {
            await UniTask.WaitForSeconds(2f);
            DeleteUnitSubject.OnNext(Unit.Default);
        }).AddTo(this);
    }

    public void SetUnitSprite(Sprite sprite)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
        }

        animator.SetBool("IsAlive", true);
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
        if(defenceParticle.isPlaying)
        {
            defenceParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        defenceParticle.Play();
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
        if (hitParticle.isPlaying)
        {
            hitParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        hitParticle.Play();

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

    public void DoDeathAnim()
    {
        animator.SetBool("IsAlive", false);
    }

    public void DeathAnimationEndTrigger()
    {
        DeathAnimationEndSubject.OnNext(Unit.Default);
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
