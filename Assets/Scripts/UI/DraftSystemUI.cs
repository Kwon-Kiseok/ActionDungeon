using UnityEngine;
using UniRx;
using System;
using System.Collections.Generic;

public class DraftSystemUI : MonoBehaviour
{
    // 3중 1택을 하는 시스템들에 공통으로 사용될 UI

    public Subject<Unit> OnSelectSubject = new Subject<Unit>();
    public List<SelectOptionUnit> selectOptionUnits = new List<SelectOptionUnit>();

    private void Start()
    {
        OnSelectSubject.Subscribe((_) =>
        {
            CloseUI();
        }).AddTo(this);
    }

    public void PrepareOptions()
    {

    }

    private void CloseUI()
    {
        this.gameObject.SetActive(false);
    }
}
