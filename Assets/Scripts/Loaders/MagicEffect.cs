using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MagicEffect {

    public int id;
    public string name;
    public string codeName;
    public string directory;
    public bool loaded = false;
    public Dictionary<string, string> attributes;

    public MagicEffect()
    {
        attributes = new Dictionary<string, string>();
    }


}
