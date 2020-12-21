using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum nodeType {Tree};
[CreateAssetMenu(fileName = "Resource Node", menuName = "ScriptableObjects/Resource Node/Resource Node")]
public class ResourceNode : ScriptableObject
{
    [SerializeField]
    private string nodeName;
    [SerializeField]
    private GameObject nodePrefab;
    [SerializeField]
    public float nodeBaseHealth;
    [SerializeField]
    private nodeType node;
    [SerializeField]
    public Item[] outputItems;

}
