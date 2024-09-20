using UnityEngine;
using UniRx;
using System;
using System.Collections.Generic;

public class DraftSystemUI : MonoBehaviour
{
    public Subject<Unit> OnSelectSubject = new Subject<Unit>();
    public List<SelectOptionUnit> selectOptionUnits = new List<SelectOptionUnit>();

    private void Start()
    {
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
        
    }

    private void CloseUI()
    {
        this.gameObject.SetActive(false);
    }
}
