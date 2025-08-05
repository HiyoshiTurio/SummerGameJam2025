using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerIconDatabase", menuName = "ScriptableObjects/CustomerIconDatabase", order = 1)]
public class CustomerIconDatabase : ScriptableObject
{
    [Serializable]
    public struct StateIconPair
    {
        public CustomerState state;
        public Sprite sprite;
    }
    [SerializeField] private StateIconPair[] stateIcons;

    private readonly Dictionary<CustomerState, Sprite> _dict = new();

    private void OnValidate()
    {
        _dict.Clear();
        foreach (var pair in stateIcons)
        {
            var stateIcon = pair.state;
            var sprite = pair.sprite;

            _dict.Add(stateIcon, sprite);
        }
    }

    public Sprite GetSprite(CustomerState state)
    {
        return _dict[state];
    }
}
