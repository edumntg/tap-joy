using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item
{
    public int id;
    public string name;
    public string description;
    public string fileDir;
    public int value;
    public Vector3 basePosition;
    public GameObject gameObject;
    public Dictionary<string, string> attributes;

    public Item()
    {
        basePosition = Vector3.zero;
        attributes = new Dictionary<string, string>();
    }

    public int GetId() { return id; }
    public string GetName() { return name; }
    public string GetDecription() { return description; }
    public string GetDirectory() { return fileDir; } 
    public int GetValue() { return value; }
    public bool HasAttribute(string name)
    {
        return attributes.ContainsKey(name);
    }
    public string GetAttribute(string name)
    {
        return attributes[name];
    }
    public Vector3 GetBasePosition() {return basePosition;}

    public GameObject GetGameObject() { return gameObject; }

}
