using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generation : MonoBehaviour {
    Items items;
    Characters characters;

    List<GameObject> tiles = new List<GameObject>();
    List<GameObject> valuables = new List<GameObject>();

    //internal use
    Object prefab;

    GameObject ObjectI;
    GameObject ValuableObject;
    GameObject PlayerObject;
    GameObject lastTile;

    Item item;

    Vector3 distance;
    Vector3 basePosition;
    Vector3 obstaclePosition;
	Vector3 tileSize;

    Player player;

	bool lastIsEmpty = false;

	float tileYScale = 1.0f;

    int tilesCreated = 0;

	void Start () 
    {
        items = Game.items;
        characters = Game.characters;

        //let's create the player
		Character character = characters.GetCharacter("Cube");
        prefab = Resources.Load(character.GetDirectory(), typeof(GameObject));
        PlayerObject = Instantiate(prefab) as GameObject;
		PlayerObject.transform.position = character.GetBasePosition();
        PlayerObject.transform.SetParent(gameObject.transform.parent.FindChild("Player").transform);
		player = gameObject.transform.parent.FindChild("Player").GetComponentInChildren<Player>();

		//let's create the initial tiles

		item = items.GetItem("Tile");
        tilesCreated++;
		prefab = Resources.Load (item.GetDirectory(), typeof(GameObject));
		ObjectI = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        ObjectI.name = ObjectI.name + tilesCreated;
        ObjectI.transform.localScale = new Vector3(1.0f, tileYScale, 1.0f);
        ObjectI.transform.position = new Vector3(0, 0, 0) + item.GetBasePosition()*tileYScale;
		ObjectI.transform.SetParent(gameObject.transform.FindChild("Tiles").transform);
		tiles.Add (ObjectI);

		//let's calculate how many tiles we need to fill the screen
		Vector2 screenSize = new Vector2(Screen.width, Screen.height);
		tileSize = ObjectI.GetComponent<BoxCollider>().bounds.size;

		//because tile is squared, x&z will have same size
		float screenDiagonal = Mathf.Ceil (Mathf.Sqrt(Mathf.Pow(screenSize.x, 2) + Mathf.Pow(screenSize.y, 2)));
		float tilesCount = Mathf.Ceil (screenDiagonal / tileSize.x);

		tilesCount = 30.0f; //FIXX!!!!!!!!!!!!!!!!!! too many tiles (game screen)

        for (float i = -Mathf.Ceil(tilesCount / 2); i <= Mathf.Ceil (tilesCount / 2); i++) //initial tiles
        {
			if(i != 0) //avoid first tile
			{
	            item = items.GetItem("Tile");
                tilesCreated++;
	            prefab = Resources.Load(item.GetDirectory(), typeof(GameObject));
	            ObjectI = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                ObjectI.name = ObjectI.name + tilesCreated;
                ObjectI.transform.localScale = new Vector3(1.0f, tileYScale, 1.0f);
                ObjectI.transform.position = new Vector3(i * tileSize.x, 0, 0) + item.GetBasePosition()*tileYScale;
	            ObjectI.transform.SetParent(gameObject.transform.FindChild("Tiles").transform);
                tiles.Add(ObjectI);
			}
        }

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		lastTile = tiles[tiles.Count - 1];
		distance = lastTile.transform.position;
		if(distance.x <= 30)
		{
			item = items.GetItem("Tile");
			prefab = Resources.Load(item.GetDirectory(), typeof(GameObject));
            tilesCreated++;
			ObjectI = Instantiate(prefab) as GameObject;
            ObjectI.name = ObjectI.name + tilesCreated;

			int rand = Random.Range (1, 4);
            ObjectI.transform.localScale = new Vector3(1.0f, tileYScale, 1.0f);
            if (rand == 1 && !lastIsEmpty && player.started)
			{
                float sepRand = Random.Range(1.5f, 2.5f);
				ObjectI.transform.position = new Vector3(lastTile.transform.position.x + tileSize.x*2*sepRand, item.GetBasePosition().y*tileYScale, item.GetBasePosition().z);
				lastIsEmpty = true;			
			}
			else
			{
				ObjectI.transform.position = new Vector3(lastTile.transform.position.x + tileSize.x, item.GetBasePosition().y*tileYScale, item.GetBasePosition().z);
				lastIsEmpty = false;
			}
            ObjectI.transform.SetParent(gameObject.transform.FindChild("Tiles").transform);
			tiles.Add (ObjectI);

            //create valuable
            int valuableRand = Random.Range(1, 101);
            if (valuableRand <= Game.NormalDiamondChance && player.started) //30%
            {
                if(valuableRand <= Game.RareDiamondChance)
                {
                    item = items.GetItem("Rare Diamond");
                }
                else
                {
                    item = items.GetItem("Normal Diamond");
                }
                prefab = Resources.Load(item.GetDirectory(), typeof(GameObject));
                ValuableObject = Instantiate(prefab) as GameObject;
                ValuableObject.transform.position = new Vector3(ObjectI.transform.position.x, 0, 0);
                ValuableObject.transform.SetParent(gameObject.transform.FindChild("Valuables").transform);
                valuables.Add(ValuableObject);
            }
		}

		//let's delete tiles outside of camera
		foreach(GameObject it in tiles)
		{
			distance = -it.transform.position;
			if(distance.x >= 30 && tiles.Count > 30)
			{
				Destroy(it);
				tiles.Remove(it);
				break;
			}
		}

        foreach(GameObject it in valuables)
        {
            if (it != null)
            {
                distance = -it.transform.position;
                if (distance.x >= 30 && valuables.Count > 9)
                {
                    Destroy(it);
                    valuables.Remove(it);
                    break;
                }
            }
        }
    }

}
