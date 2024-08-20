using UnityEngine;
using UniRx;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class Units : MonoBehaviour
{
    public string unitName;
    public struct statData
    {
        public float hp;    //체력
        public float atk;   //공격력
        public float def;   //방어력
        public float luk;   //운(확률)

        public statData(float hp, float atk, float def, float luk)
        {
            this.hp = hp;
            this.atk = atk;
            this.def = def;
            this.luk = luk;
        }
    }
    [SerializeField] private statData _statData;
    public UnitRenderer unitRenderer;

    private StateMachine _stateMachine;
    public StateMachine StateMachine => _stateMachine;

    [SerializeField] private CharacterController _characterController;
    public CharacterController CharacterController => _characterController;

    private Transform _actionPosition;
    private Vector3 _unitPosition;

    public virtual void Init(string unitName, statData statData) 
    {
        _statData = statData;
    }

    void Awake()
    {
        if(_stateMachine is null)
        {
            _stateMachine = new StateMachine(this);
        }
    }

    public void SetUnitPosition(Vector3 unitPosition)
    {
        _unitPosition = unitPosition;
    }

    public void SetActionPosition(Transform actionPosition)
    {
        _actionPosition = actionPosition;
    }

    public statData GetStatData()
    {
        return _statData;
    }

    public void Attack(Units targetUnit)
    {
        AttackState atkState = new AttackState();
        _stateMachine.SetState(atkState);
        atkState.SetTargetUnit(targetUnit);

        this.transform.DOMove(_actionPosition.position, 0.3f).OnComplete(async () =>
        {
            unitRenderer.DoAttackAnim();
            await UniTask.Delay(1000);
            this.transform.DOMove(_unitPosition, 0.25f);
        });
    }

    public void Defence()
    {
        // _stateMachine.SetState(new DefenceState());
        this.transform.DOMove(_actionPosition.position, 0.3f).OnComplete(async () =>
        {
            // unitRenderer.DoAttackAnim();
            await UniTask.Delay(1000);
            this.transform.DOMove(_unitPosition, 0.25f);
        });
    }

    public void Dodge()
    {
        this.transform.DOMove(_actionPosition.position, 0.3f).OnComplete(async () =>
        {
            unitRenderer.DoDodgeAnim();
            await UniTask.Delay(600);
            this.transform.DOMove(_unitPosition, 0.25f);
        });
    }

    public void Hit()
    {

    }

    public void Dead()
    {
        
    }
}
