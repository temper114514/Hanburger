// CardManager.cs
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [Header("卡牌池")]
    public List<CardData> allCards;

    [Header("抽取设置")]
    public int drawCount = 5;
    public bool allowDuplicates = false;

    // 抽取结果
    public List<CardData> drawnCards = new List<CardData>();

    // 随机抽取（无重复）
    public void DrawCards()
    {
        if (allowDuplicates)
        {
            drawnCards = Enumerable.Range(0, drawCount)
                .Select(_ => allCards[Random.Range(0, allCards.Count)])
                .ToList();
        }
        else
        {
            // 无重复抽取（Fisher-Yates 洗牌）
            List<CardData> shuffled = new List<CardData>(allCards);
            for (int i = shuffled.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
            }
            drawnCards = shuffled.Take(drawCount).ToList();
        }

        OnCardsDrawn?.Invoke(drawnCards);
    }

    // 事件：通知 UI 或特效系统
    public System.Action<List<CardData>> OnCardsDrawn;
}