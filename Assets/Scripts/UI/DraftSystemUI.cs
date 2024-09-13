using UnityEngine;
using UniRx;
using System;
using System.Collections.Generic;

public class DraftSystemUI : MonoBehaviour
{
    // 3�� 1���� �ϴ� �ý��۵鿡 �������� ���� UI

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
