using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string displayName;
    public Sprite sprite;
}