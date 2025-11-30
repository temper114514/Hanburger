// CardUI.cs
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Image cardImage;
    public Text cardNameText;

    private CardData data;
    private bool isSelected = false;

    // 视觉反馈参数
    public Color normalColor = Color.white;
    public Color selectedColor = Color.yellow;
    public float scaleNormal = 1f;
    public float scaleSelected = 1.1f;
    public float animDuration = 0.2f;

    public void Initialize(CardData card)
    {
        data = card;
        cardImage.sprite = card.artwork;
        cardNameText.text = card.cardName;
        Deselect(); // 默认未选中
    }

    public void OnCardClicked()
    {
        isSelected = !isSelected;
        if (isSelected)
            Select();
        else
            Deselect();
    }

    private void Select()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one * scaleSelected, animDuration).setEase(LeanTween.easeOutCubic);
        cardImage.color = selectedCcolor;
    }

    private void Deselect()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one * scaleNormal, animDuration).setEase(LeanTween.easeOutCubic);
        cardImage.color = normalColor;
    }

    // 可选：添加事件回调
    public System.Action<CardUI> OnSelected;
}