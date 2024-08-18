using System;

public enum ActionType
{
    ATTACK,
    DEFENCE,
    DODGE
}

public interface IAction
{
    void Act(Units actUnit, float successRate);
}
