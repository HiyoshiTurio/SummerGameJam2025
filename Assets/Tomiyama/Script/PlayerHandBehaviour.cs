using UnityEngine;

public class PlayerHandBehaviour : MonoBehaviour
{
    [SerializeField] [Tooltip("プレイヤーの手のTransform")]
    private Transform playerHand;

    [SerializeField] [Tooltip("SphereCastでヒット判定を行うレイヤー")]
    private LayerMask interactiveLayer;

    [SerializeField] [Tooltip("アイテムを拾うためのキー")]
    private KeyCode pickUpKey = KeyCode.LeftShift;

    [SerializeField] [Tooltip("SphereCastの半径")]
    private float sphereRadius;

    [SerializeField] [Tooltip("生成するアイテムのデータベース")]
    private DeliveryItemDatabase deliveryItemDatabase;

    [SerializeField] [Tooltip("アイテムを渡したときのスコア")]
    private int scorePerItem = 100;

    [SerializeField] [Tooltip("トラブルを解決したときのスコア")]
    private int scorePerTrouble = 50;

    [SerializeField]
    private Player player;

    private ItemType _currentItem = ItemType.None;
    private GameObject _holdingObject;

    private void Update()
    {
        if (Input.GetKeyDown(pickUpKey))
        {
            Debug.Log("Pressed key");
            PerformSphereCast();
        }
    }

    private void PerformSphereCast()
    {
        var origin = transform.position;
        var direction = transform.forward;
        var distance = Vector3.Distance(origin, playerHand.position);

        var hasHit = Physics.SphereCast(origin, sphereRadius, direction, out var hit, distance,
            interactiveLayer);

        if (hasHit)
        {
            // ヒットしたオブジェクトに対する処理
            var target = hit.collider.gameObject;
            if (!IsInLayerMask(target.layer, interactiveLayer)) return;
            if (target.TryGetComponent(out ICustomer customer) && customer.Take(_currentItem))
            {
                Debug.Log("アイテムを渡しました: " + _currentItem);
                GameManager.Instance.AddScore(_currentItem == ItemType.Drink ? scorePerTrouble : scorePerItem, player);
                SoundManager.Instance.Play(SoundKey.ScoreUp);
                _currentItem = ItemType.None;
                if (_holdingObject != null)
                {
                    Destroy(_holdingObject);
                    _holdingObject = null;
                }
            }

            if (_currentItem != ItemType.None) return;
            if (target.TryGetComponent(out IGenerator foodGenerator) && foodGenerator.TryTakeItem(out var itemType))
            {
                _currentItem = itemType;
                var itemPrefab = deliveryItemDatabase.GetItemPrefab(itemType);
                _holdingObject = Instantiate(itemPrefab, playerHand.position, transform.rotation, playerHand);
                Debug.Log("アイテムを取得: " + itemType);
            }
        }
        else
        {
            Debug.Log("何もヒットしませんでした");
        }
    }

    private void OnDrawGizmos()
    {
        if (playerHand == null) return;
        var origin = transform.position;
        var direction = transform.forward;
        var distance = Vector3.Distance(origin, playerHand.position);

        // ヒットした場合、色を変更する
        Gizmos.color = Physics.SphereCast(origin, sphereRadius, direction, out _, distance, interactiveLayer)
            ? Color.blue
            : Color.red;

        Gizmos.DrawWireSphere(origin, sphereRadius);
        Gizmos.DrawRay(origin, direction * distance);
        Gizmos.DrawWireSphere(origin + direction * distance, sphereRadius);
    }

    private static bool IsInLayerMask(int layer, LayerMask layerMask) => (layerMask.value & (1 << layer)) != 0;
}