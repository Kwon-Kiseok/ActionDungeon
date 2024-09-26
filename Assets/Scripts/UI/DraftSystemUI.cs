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

    public EnhaceBonus selectedBonusStat;
    public Subject<EnhaceBonus> OnSelectBonusStatSubject = new Subject<EnhaceBonus>();

    private List<int> optionIDList = new List<int>();

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
        _randomDraftSystem.InitIDDictionary(_enhaceBonusDatabase.enhaceBonusDataList);

        OnSelectSubject.Subscribe((_) =>
        {
            CloseUI();
        }).AddTo(this);

        RegisterCloseSubjects();
    }

    private void RegisterCloseSubjects()
    {
        foreach (var option in selectOptionUnits)
        {
            option.OnClickSubject.Subscribe((_) =>
            {
                OnSelectBonusStatSubject.OnNext(option.GetEnhanceBonus());
                option.transform.localScale = Vector3.one;
                CloseUI();
            }).AddTo(this);
        }
    }

    public void PrepareOptions()
    {
        optionIDList.Clear();

        foreach (var option in selectOptionUnits)
        {
            bool isSetOption = false;
            while (isSetOption == false)
            {
                EnhaceBonus randomBonus = _randomDraftSystem.GetRandomBonus(_enhaceBonusDatabase.enhaceBonusDataList);

                if(_randomDraftSystem.enhanceBonusDataIDDictionary[randomBonus.GetID()] == 0)
                {
                    _randomDraftSystem.enhanceBonusDataIDDictionary[randomBonus.GetID()] = 1;
                    optionIDList.Add(randomBonus.GetID());
                    option.SetOptionInfo(randomBonus);
                    isSetOption = true;
                }
            }
        }

        foreach(var ID in optionIDList)
        {
            _randomDraftSystem.enhanceBonusDataIDDictionary[ID] = 0;
        }
    }

    public void CloseUI()
    {
        _panelObject.SetActive(false);
    }
}
