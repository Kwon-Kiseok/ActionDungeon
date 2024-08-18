using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using System;

public class ActionButton : MonoBehaviour
{
    [SerializeField] private ActionType actionType;
    [SerializeField] private Button actionButton;

    public Action OnClickEvent;

    private IDisposable disposable;

    void Start()
    {
        disposable = actionButton.onClick.AsObservable().Subscribe(_ => {
            OnClickAction(this.actionType);
        });
    }

    private void OnClickAction(ActionType actionType)
    {
        OnClickEvent?.Invoke();
    }

    void OnDestroy()
    {
        disposable.Dispose();
    }
}
