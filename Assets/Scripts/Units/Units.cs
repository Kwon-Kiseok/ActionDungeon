using UnityEngine;
using UniRx;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class Units : MonoBehaviour
{
    public string unitName;
    public struct statData
    {
        public float hp;   
        public float atk;   
        public float def;   
        public float luk;

        public string desc;

        public statData(float hp, float atk, float def, float luk, string desc = null)
        {
            this.hp = hp;
            this.atk = atk;
            this.def = def;
            this.luk = luk;
            this.desc = desc;
        }
    }
    [SerializeField] private statData _statData;
    private statData _bonusStatData;

    private int _actionPoint = 0;
    public int ActionPoint => _actionPoint;

    private bool _isAlive = true;
    public bool IsAlive => _isAlive;

    public UnitRenderer unitRenderer;

    private StateMachine _stateMachine;
    public StateMachine StateMachine => _stateMachine;

    [SerializeField] private CharacterController _characterController;
    public CharacterController CharacterController => _characterController;

    public Subject<Unit> OnActionEnd = new Subject<Unit>();

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

        unitRenderer.DeleteUnitSubject.Subscribe((_) =>
        {
            if (IsAlive == false)
            {
                Destroy(this);
            }
        }).AddTo(this);
    }

    public void SetUnitPosition(Vector3 unitPosition)
    {
        _unitPosition = unitPosition;
    }

    public void SetActionPosition(Transform actionPosition)
    {
        _actionPosition = actionPosition;
    }

    public statData GetOriginStatData()
    {
        return _statData;
    }

    public statData GetBonusStatData()
    {
        return _bonusStatData;
    }

    public void AddBonusStatData(statData newBonusStat)
    {
        _bonusStatData.hp += newBonusStat.hp;
        _bonusStatData.atk += newBonusStat.atk;
        _bonusStatData.def += newBonusStat.def;
        _bonusStatData.luk += newBonusStat.luk;
    }

    public statData GetTotalStatData()
    {
        statData totalStat = new statData(
            _statData.hp + _bonusStatData.hp,
            _statData.atk + _bonusStatData.atk,
            _statData.def + _bonusStatData.def,
            _statData.luk + _bonusStatData.luk,
            null
            );
        return totalStat;
    }

    public void AddActionPoint(int addPoint)
    {
        _actionPoint += addPoint;
        if(_actionPoint > 3)
        {
            _actionPoint = 3;
        }
    }

    public void UseActionPoint()
    {
        _actionPoint = 0;
    }

    public void Attack(Units targetUnit)
    {
        AttackState atkState = new AttackState();
        _stateMachine.SetState(atkState);
        atkState.SetTargetUnit(targetUnit);

        this.transform.DOMove(_actionPosition.position, 0.3f).OnComplete(async () =>
        {
            unitRenderer.DoAttackAnim();
            _stateMachine.DoExecuteState();
            OnActionEnd.OnNext(Unit.Default);

            if (IsAlive)
            {
                await UniTask.Delay(1000);
                this.transform.DOMove(_unitPosition, 0.25f);
            }
        });
    }

    public void Defence()
    {
        DefenceState defenceState = new DefenceState();
        _stateMachine.SetState(defenceState);

        this.transform.DOMove(_actionPosition.position, 0.3f).OnComplete(async () =>
        {
            unitRenderer.DoDefenceAnim();
            _stateMachine.DoExecuteState();
            OnActionEnd.OnNext(Unit.Default);

            if (IsAlive)
            {
                await UniTask.Delay(1000);
                this.transform.DOMove(_unitPosition, 0.25f);
            }
        });
    }

    public void Dodge()
    {
        DodgeState dodgeState = new DodgeState();
        _stateMachine.SetState(dodgeState);

        this.transform.DOMove(_actionPosition.position, 0.3f).OnComplete(async () =>
        {
            unitRenderer.DoDodgeAnim();
            _stateMachine.DoExecuteState();
            OnActionEnd.OnNext(Unit.Default);
            await UniTask.Delay(600);
            this.transform.DOMove(_unitPosition, 0.25f);
        });
    }

    public void ActionFail()
    {
        FailState failState = new FailState();
        _stateMachine.SetState(failState);

        this.transform.DOMove(_actionPosition.position, 0.3f).OnComplete(async () =>
        {
            unitRenderer.DoActionFail();
            _stateMachine.DoExecuteState();
            OnActionEnd.OnNext(Unit.Default);

            if (IsAlive)
            {
                await UniTask.Delay(1000);
                this.transform.DOMove(_unitPosition, 0.25f);
            }
        });
    }

    public void Hit(float hitDamage)
    {
        _statData.hp -= hitDamage;
        if(_statData.hp <= 0)
        {
            Dead();
            return;
        }

        if(StateMachine.CurrentState is DefenceState)
        {
            unitRenderer.DoDefenceAnim();
        }
        else
        {
            unitRenderer.DoHitAnim();
            unitRenderer.DamageTextAnim(hitDamage);
        }
    }

    public void Dead()
    {
        _isAlive = false;
        Debug.Log(unitName + " is Dead");
        unitRenderer.DoDeathAnim();
    }
}
