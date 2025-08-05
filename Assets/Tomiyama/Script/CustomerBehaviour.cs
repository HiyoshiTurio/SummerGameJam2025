using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerBehaviour : MonoBehaviour, ICustomer
{
    [SerializeField] [Tooltip("客が食事をする位置")]
    private Transform eatPosition;
    
    [SerializeField] [Tooltip("客が食事をする時間")]
    private float eatTime = 5f;

    [SerializeField] [Tooltip("トラブルから自動回復するまでの時間")]
    private float autoRecoverTime = 10f;

    [SerializeField] [Range(0f, 1f)] [Tooltip("トラブルになる確率")]
    private float troubleChance = 0.1f;
    
    [SerializeField] [Tooltip("生成するアイテムのデータベース")]
    private DeliveryItemDatabase deliveryItemDatabase;

    private GameObject _eatingItem;
    
    private enum CustomerState
    {
        Idle,
        Eating,
        InTrouble,
    }

    private CustomerState _currentState = CustomerState.Idle;

    public bool Take(ItemType itemType)
    {
        if (_currentState == CustomerState.Idle && itemType == ItemType.Food)
        {
            Debug.Log("客が食べ物を受け取りました");
            var itemPrefab = deliveryItemDatabase.GetItemPrefab(itemType);
            if (itemPrefab != null)
            {
                _eatingItem = Instantiate(itemPrefab, eatPosition.position, Quaternion.identity, eatPosition);
            }
            SoundManager.Instance.Play(SoundKey.Okawari);
            StartCoroutine(Eat());
            return true;
        }

        if (_currentState == CustomerState.InTrouble && itemType == ItemType.Drink)
        {
            Debug.Log("客が飲み物を受け取りました");
            _currentState = CustomerState.Idle;
            SoundManager.Instance.Play(SoundKey.PutGruss);
            return true;
        }

        Debug.Log("客はそのアイテムを受け取れませんでした");
        return false;
    }

    private IEnumerator Eat()
    {
        _currentState = CustomerState.Eating;
        yield return new WaitForSeconds(eatTime);

        // 10%の確率でトラブルになる
        var r = Random.Range(0f, 1f);
        if (r < troubleChance)
        {
            Debug.Log("客がトラブルになりました");
            _currentState = CustomerState.InTrouble;

            // トラブルになった場合、一定時間待機してから自動回復
            yield return new WaitForSeconds(autoRecoverTime);
            _currentState = CustomerState.Idle;
            Debug.Log("客が自動回復しました");
            yield break;
        }

        Debug.Log("客が食事を終えました");
        if (_eatingItem != null)
        {
            Destroy(_eatingItem);
            _eatingItem = null;
        }
        _currentState = CustomerState.Idle;
    }

    private void OnDrawGizmos()
    {
        if (eatPosition == null) return;
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(eatPosition.position, 0.5f);
    }
}