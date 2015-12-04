using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generation : MonoBehaviour {
    Items items;
    Characters characters;

    List<GameObject> tiles = new List<GameObject>();

    //internal use
    Object prefab;

    GameObject ObjectI;
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

	void Start () 
    {
        //let's create the player
        items = new Items();
        characters = new Characters();
        if (items.LoadItems() && characters.LoadCharacters())
        {
			Character character = characters.GetCharacter("Cube");
            prefab = Resources.Load(character.GetDirectory(), typeof(GameObject));
            PlayerObject = Instantiate(prefab) as GameObject;
			PlayerObject.transform.position = character.GetBasePosition();
            PlayerObject.transform.SetParent(gameObject.transform.parent.FindChild("Player").transform);
			player = gameObject.transform.parent.FindChild("Player").GetComponentInChildren<Player>();

			//let's create the initial tiles

			item = items.GetItem("Tile");
			prefab = Resources.Load (item.GetDirectory(), typeof(GameObject));
			ObjectI = ObjectI = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
			ObjectI.transform.position = new Vector3(0, 0, 0) + item.GetBasePosition();
			ObjectI.transform.SetParent(gameObject.transform.FindChild("Tiles").transform);
			tiles.Add (ObjectI);
			//let's calculate how many tiles we need to fill the screen
			Vector2 screenSize = new Vector2(Screen.width, Screen.height);
			tileSize = ObjectI.GetComponent<BoxCollider>().bounds.size;
			//because tile is squared, x&z will have same size
			float screenDiagonal = Mathf.Ceil (Mathf.Sqrt(Mathf.Pow(screenSize.x, 2) + Mathf.Pow(screenSize.y, 2)));
			float tilesCount = Mathf.Ceil (screenDiagonal / tileSize.x);;
			tilesCount = 30.0f; //FIXX!!!!!!!!!!!!!!!!!! too many tiles (game screen)

            for (float i = -Mathf.Ceil(tilesCount / 2); i <= Mathf.Ceil (tilesCount / 2); i++) //initial tiles
            {
				if(i != 0) //avoid first tile
				{
	                item = items.GetItem("Tile");
	                prefab = Resources.Load(item.GetDirectory(), typeof(GameObject));
	                ObjectI = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
	                ObjectI.transform.position = new Vector3(i * tileSize.x, 0, 0) + item.GetBasePosition();
	                ObjectI.transform.SetParent(gameObject.transform.FindChild("Tiles").transform);
					tiles.Add(ObjectI);
				}
            }
        }

	}
	
	// Update is called once per frame
	void Update () 
	{
		lastTile = tiles[tiles.Count - 1];
		distance = lastTile.transform.position;
		if(distance.x <= 30)
		{
			item = items.GetItem("Tile");
			prefab = Resources.Load(item.GetDirectory(), typeof(GameObject));
			ObjectI = Instantiate(prefab) as GameObject;

			int rand = Random.Range (1, 4);
			if(rand == 1 && !lastIsEmpty && player.started)
			{
				ObjectI.transform.position = new Vector3(lastTile.transform.position.x + tileSize.x*4, item.GetBasePosition().y, item.GetBasePosition().z);
				lastIsEmpty = true;			
			}
			else
			{
				ObjectI.transform.position = new Vector3(lastTile.transform.position.x + tileSize.x, item.GetBasePosition().y, item.GetBasePosition().z);
				lastIsEmpty = false;
			}

			ObjectI.transform.SetParent(gameObject.transform.FindChild("Tiles").transform);
			tiles.Add (ObjectI);
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
	}

	Transform GetPlayer()

	{
		foreach(Transform t in gameObject.transform.parent.GetComponentsInChildren<Transform>())
		{
			if(t.tag == "Player")
			{
				return t;
			}
		}
		return null;
	}

}
