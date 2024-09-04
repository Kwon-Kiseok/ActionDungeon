using UnityEngine;

public class FailState : IState
{
    private Units _unit;

    public void ChangeUnit(Units unit)
    {
        _unit = unit;

        EnterState();
    }

    public void EnterState()
    {
    }

    public void ExecuteState()
    {
        // �ش� ������ �׼� ����Ʈ�� ���⼭ �÷��ִ� �� ���� ��
        _unit.AddActionPoint(1);
    }

    public void ExitState()
    {
    }
}
