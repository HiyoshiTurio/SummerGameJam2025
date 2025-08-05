using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    
    [SerializeField]
    private Material[] customerMaterials;
    
    [SerializeField]
    private SkinnedMeshRenderer meshRenderer;
    
    [SerializeField] [Tooltip("生成するアイテムのデータベース")]
    private DeliveryItemDatabase deliveryItemDatabase;

    [SerializeField]
    private CustomerIconDatabase customerIconDatabase;


    [SerializeField] private Image customerImage;

    private GameObject _eatingItem;

    private CustomerState _currentState = CustomerState.Idle;

    private CustomerState CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            UpdateCustomerStatus(_currentState);
        }
    }

    private void Start()
    {
        if (customerMaterials.Length > 0 && meshRenderer != null)
        {
            // ランダムにマテリアルを設定
            var randomIndex = Random.Range(0, customerMaterials.Length);
            meshRenderer.material = customerMaterials[randomIndex];
        }
        UpdateCustomerStatus(CurrentState);
    }

    private void UpdateCustomerStatus(CustomerState customerState)
    {
        Debug.Log("吹き出し更新：" + customerState);
        if (customerImage == null) return;
        var sprite = customerIconDatabase.GetSprite(customerState);
        customerImage.sprite = sprite;
        customerImage.enabled = true;

        switch (customerState)
        {
            case CustomerState.Idle:
                if (_eatingItem != null)
                {
                    Destroy(_eatingItem);
                }
                break;
            case CustomerState.Eating:
                var itemPrefab = deliveryItemDatabase.GetItemPrefab(ItemType.Food);
                if (itemPrefab != null)
                {
                    _eatingItem = Instantiate(itemPrefab, eatPosition.position, Quaternion.identity, eatPosition);
                }
                break;
        }
    }

    public bool Take(ItemType itemType)
    {
        if (CurrentState == CustomerState.Idle && itemType == ItemType.Food)
        {
            Debug.Log("客が食べ物を受け取りました");
            SoundManager.Instance.Play(SoundKey.Okawari);
            StartCoroutine(Eat());
            return true;
        }

        if (CurrentState == CustomerState.InTrouble && itemType == ItemType.Drink)
        {
            Debug.Log("客が飲み物を受け取りました");
            Destroy(_eatingItem);
            CurrentState = CustomerState.Idle;
            SoundManager.Instance.Play(SoundKey.PutGruss);
            return true;
        }

        Debug.Log("客はそのアイテムを受け取れませんでした");
        return false;
    }

    private IEnumerator Eat()
    {
        CurrentState = CustomerState.Eating;
        yield return new WaitForSeconds(eatTime);

        // 確率でトラブルになる
        var r = Random.Range(0f, 1f);
        if (r <= troubleChance)
        {
            Debug.Log("客がトラブルになりました");
            CurrentState = CustomerState.InTrouble;

            // トラブルになった場合、一定時間待機してから自動回復
            yield return new WaitForSeconds(autoRecoverTime);
            CurrentState = CustomerState.Idle;
            Debug.Log("客が自動回復しました");
            yield break;
        }

        Debug.Log("客が食事を終えました");
        CurrentState = CustomerState.Idle;
    }
    private void OnDrawGizmos()
    {
        if (eatPosition == null) return;
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(eatPosition.position, 0.5f);
    }
}

public enum CustomerState
{
    Idle,
    Eating,
    InTrouble,
}