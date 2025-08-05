using System.Collections;
using UnityEngine;

public class FoodGenerator : MonoBehaviour, IGenerator
{
    [SerializeField] [Tooltip("食材の生成間隔")]
    private float reloadTime = 2f;

    [SerializeField] [Tooltip("食材を生成する位置")]
    private Transform itemSpawnPoint;

    [SerializeField] [Tooltip("生成するアイテムの種類")]
    private ItemType generateItemType = ItemType.Food;

    [SerializeField] [Tooltip("生成するアイテムのデータベース")]
    private DeliveryItemDatabase deliveryItemDatabase;

    private bool _hasFood;

    private void Start()
    {
        var itemPrefab = deliveryItemDatabase.GetItemPrefab(generateItemType);
        Instantiate(itemPrefab, itemSpawnPoint.position, itemSpawnPoint.rotation, itemSpawnPoint);
        _hasFood = false;
        itemSpawnPoint.gameObject.SetActive(false);

        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        // スロットが開くまで待機する。
        yield return new WaitUntil(() => !_hasFood);

        // スロットが開いたら一定時間待機してから生成
        yield return new WaitForSeconds(reloadTime);
        Debug.Log("アイテムが生成されました");
        SoundManager.Instance.Play(SoundKey.RestockSoba);
        itemSpawnPoint.gameObject.SetActive(true);
        _hasFood = true;
    }

    /// <summary>
    /// 食材を取得する処理。
    /// プレイヤーはこれを呼び出して食材を取得する。
    /// </summary>
    /// <returns>取得できた場合はtrueを返し、そうでなければfalseを返す。</returns>
    public bool TryTakeItem(out ItemType itemType)
    {
        if (_hasFood)
        {
            _hasFood = false;
            itemSpawnPoint.gameObject.SetActive(false);
            itemType = generateItemType;
            StartCoroutine(Generate());
            Debug.Log("アイテムを取得: " + itemType);
            return true;
        }

        itemType = ItemType.None;
        Debug.Log("取得可能なアイテムがありません");
        return false;
    }
}

public enum ItemType
{
    None,
    Food,
    Drink
}