using UnityEngine;
using UniRx;
using System;
using System.Collections.Generic;

public class DraftSystemUI : MonoBehaviour
{
    public Subject<Unit> OnSelectSubject = new Subject<Unit>();
    public List<SelectOptionUnit> selectOptionUnits = new List<SelectOptionUnit>();

    private EnhaceBonusDatabase _enhaceBonusDatabase;

    [SerializeField] private GameObject _panelObject;
    public GameObject PanelObject => _panelObject;

    private void Start()
    {
        _enhaceBonusDatabase = new EnhaceBonusDatabase();
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
            int randomIndex = UnityEngine.Random.Range(0, _enhaceBonusDatabase.enhaceBonusDataList.Count);
            EnhaceBonus randomBonus = _enhaceBonusDatabase.enhaceBonusDataList[randomIndex];
            unit.SetOptionInfo(randomBonus);
        }
    }

    private void CloseUI()
    {
        _panelObject.SetActive(false);
    }
}
