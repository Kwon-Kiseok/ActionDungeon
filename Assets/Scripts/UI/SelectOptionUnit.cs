using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using DG.Tweening;

public class SelectOptionUnit : MonoBehaviour
{
    [SerializeField] private Image optionIconImage;
    [SerializeField] private TextMeshProUGUI optionNameText;

    [SerializeField] private Button iconButton;

    private void Start()
    {
        iconButton.onClick.AsObservable().Subscribe(_ =>
        {
            DoSelectEffect();
        }).AddTo(this);
    }

    // �Ű������� �ɼǿ� ���� ������ �޾ƿ��� �ɼ� ������ UI�� ������ �ϴ� ���
    public void SetOptionInfo()
    {
       
    }

    // �ش� �ɼ��� ������ �� ���� �Լ�
    public void DoSelectEffect()
    {
       this.transform.DOPunchScale(transform.localScale, 1f);
    }
}
