using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Game : MonoBehaviour {
	
	public static Vector3 baseScrollingSpeed = new Vector3(16.0f, 0.0f, 0.0f);
	public static Vector3 baseScrollingDirection = new Vector3(-1.0f, 0.0f, 0.0f);
	public static Vector3 scrollingSpeedIncrease = new Vector3(2.0f, 0.0f, 0.0f);

    public static int NormalDiamondChance = 20; //20%
    public static int RareDiamondChance = 2; //3%
	public static int speedIncreaseTime = 15; //ms
    public static List<Scrolling> scrollingScripts;

    public static Items items;
    public static Characters characters;
    public static Effects effects;

    public static GameObject MainObject;

	void Start ()
    {
        scrollingScripts = new List<Scrolling>();
        MainObject = gameObject;
        items = new Items();
        items.LoadItems();
        characters = new Characters();
        characters.LoadCharacters();
        effects = new Effects();
        effects.LoadEffects();

        //get scrolling scripts
        foreach(Scrolling scr in gameObject.GetComponentsInChildren<Scrolling>())
        {
            scrollingScripts.Add(scr);
        }
        Time.timeScale = 0;
	}

    public static void StopScrolling()
    {
        foreach(Scrolling scr in scrollingScripts)
        {
            scr.Stop();
        }
    }

    public static void SendMagicEffect(MagicEffect effect, Vector3 position)
    {
        if (!effect.loaded)
        {
            return;
        }
        Object eObject = Resources.Load(effect.directory, typeof(GameObject));
        GameObject gObject = Instantiate(eObject, Vector3.zero, Quaternion.identity) as GameObject;
        gObject.transform.position = position;
        gObject.transform.SetParent(MainObject.transform.FindChild("Environment").transform.FindChild("Effects").transform);

        Destroy(gObject, 3); //destroy after 3 seconds
    }

    public static void SendAnimatedText(string text, Color color, Vector3 position)
    {
        Object atObject = Resources.Load("Animated Text/AnimText", typeof(GameObject));
        GameObject gObject = Instantiate(atObject, position, Quaternion.identity) as GameObject;
        gObject.GetComponent<AnimatedText>().Initialize(text, color);
        gObject.transform.SetParent(MainObject.transform.FindChild("UI").transform.FindChild("Canvas").transform.FindChild("AnimatedTexts").transform);

        Destroy(gObject, 2);
    }

}
