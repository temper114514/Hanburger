// DrawEffectManager.cs
using System.Collections;
using UnityEngine;

public class DrawEffectManager : MonoBehaviour
{
    public GameObject cardPrefab; // 带 CardUI 的 prefab
    public Transform drawArea;
    public float spawnDelay = 0.1f;
    public Vector3 spawnFromPosition = new Vector3(0, 10, 0);

    private ObjectPool cardPool;

    void Start()
    {
        cardPool = new ObjectPool(cardPrefab, 10, transform);
        FindObjectOfType<CardManager>().OnCardsDrawn += PlayDrawEffect;
    }

    public void PlayDrawEffect(List<CardData> cards)
    {
        // 清理旧卡牌
        foreach (Transform child in drawArea)
            cardPool.ReturnObject(child.gameObject);

        StartCoroutine(SpawnCardsOneByOne(cards));
    }

    IEnumerator SpawnCardsOneByOne(List<CardData> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            GameObject cardObj = cardPool.GetObject();
            cardObj.transform.SetParent(drawArea, false);
            cardObj.transform.localPosition = new Vector3(i * 120 - (cards.Count - 1) * 60, 0, 0);
            cardObj.transform.localScale = Vector3.zero;

            // 初始化卡牌数据
            var cardUI = cardObj.GetComponent<CardUI>();
            cardUI.Initialize(cards[i]);
            cardUI.OnSelected = (ui) => { /* 处理选中逻辑 */ };

            // 特效：从上方飞入 + 缩放
            cardObj.transform.position = spawnFromPosition;
            LeanTween.move(cardObj, cardObj.transform.position, 0.3f)
                .setEase(LeanTween.easeOutBack);
            LeanTween.scale(cardObj, Vector3.one, 0.3f)
                .setEase(LeanTween.easeOutElastic);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}