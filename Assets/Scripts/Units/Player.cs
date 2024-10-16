using UnityEngine;
using UniRx;

public class Player : Units
{
    [SerializeField] private Transform _transform;

    public Subject<Unit> ActionSuccessSubject = new Subject<Unit>();
    public Subject<Unit> OnSelectBonusStatSubject = new Subject<Unit>();
}
