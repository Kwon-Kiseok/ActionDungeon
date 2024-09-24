using UnityEngine;
using UniRx;
using System;
using System.Collections.Generic;
using Zenject;

public class DraftSystemUI : MonoBehaviour
{
    public Subject<Unit> OnSelectSubject = new Subject<Unit>();
    public List<SelectOptionUnit> selectOptionUnits = new List<SelectOptionUnit>();

    private EnhaceBonusDatabase _enhaceBonusDatabase;
    private RandomDraftSystem _randomDraftSystem;

    [SerializeField] private GameObject _panelObject;
    public GameObject PanelObject => _panelObject;

    [Inject]
    public void Inject(EnhaceBonusDatabase enhaceBonusDatabase, RandomDraftSystem randomDraftSystem)
    {
        _enhaceBonusDatabase = enhaceBonusDatabase;
        _randomDraftSystem = randomDraftSystem;
    }

    private void Start()
    {
        _enhaceBonusDatabase.Load();

        OnSelectSubject.Subscribe((_) =>
        {
            CloseUI();
        }).AddTo(this);

        RegisterCloseSubjects();
    }

    private void RegisterCloseSubjects()
    {
        foreach(var unit in selectOptionUnits)
        {
            unit.OnClickSubject.Subscribe((_) => {
                unit.transform.localScale = Vector3.one;
                CloseUI();
            }).AddTo(this);
        }
    }

    public void PrepareOptions()
    {
        foreach (var unit in selectOptionUnits)
        {
            EnhaceBonus randomBonus = _randomDraftSystem.GetRandomBonus(_enhaceBonusDatabase.enhaceBonusDataList);
            unit.SetOptionInfo(randomBonus);
        }
    }

    private void CloseUI()
    {
        _panelObject.SetActive(false);
    }
}
