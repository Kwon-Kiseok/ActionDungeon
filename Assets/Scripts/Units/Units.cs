using UnityEngine;

public class Units : MonoBehaviour
{
    public string unitName;
    public struct statData
    {
        public float hp;    //ü��
        public float atk;   //���ݷ�
        public float def;   //����
        public float luk;   //��(Ȯ��)

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

        unitRenderer.DoAttackAnim();
    }

    public void Deffence()
    {
        // _stateMachine.SetState(new DeffenceState());
    }

    public void Hit()
    {

    }

    public void Dead()
    {
        
    }
}
