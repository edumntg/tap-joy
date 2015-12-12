using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;

public class Effects
{

    string file = "Assets/Scripts/Loaders/effects.xml";
    bool loaded = false;
    List<MagicEffect> effects = new List<MagicEffect>();

    public bool LoadEffects()
    {
        MagicEffect effect = null;

        if (File.Exists(file))
        {
            XmlTextReader reader = new XmlTextReader(file);
            while (reader.Read())
            {
                switch (reader.Name.ToString().ToLower())
                {
                    case "effect": //assuming there is more than one item
                        effect = new MagicEffect();
                        if (reader.HasAttributes)
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                switch (reader.Name.ToString())
                                {
                                    case "id":
                                        effect.id = Convert.ToInt32(reader.Value);
                                        break;
                                    case "name":
                                        effect.name = reader.Value;
                                        break;
                                    case "codeName":
                                        effect.codeName = reader.Value;
                                        break;
                                    case "file":
                                        effect.directory = reader.Value;
                                        break;
                                }
                            }
                        }
                        break;

                    case "attribute":
                        if (reader.HasAttributes)
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                effect.attributes.Add(reader.Name.ToString(), reader.Value);
                            }
                        }
                        break;

                    default:
                        //if we get this point, it's mean there is no more attrs to read
                        if (effect != null)
                        {
                            effect.loaded = true;
                            effects.Add(effect);
                        }
                        break;
                }
            }
            reader.Close();
            loaded = true;
        }
        return loaded;

    }

    public MagicEffect GetEffect(string cName)
    {
        return effects.Find(x => x.codeName == cName);
    }
}