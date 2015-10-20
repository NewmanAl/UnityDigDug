using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

    [SerializeField]
    LevelBlock blockPrefab;
    [SerializeField]
    Dino dinoPrefab;
    [SerializeField]
    TomatoThing tomatoPrefab;
    [SerializeField]
    Player playerPrefab;
    
    public LevelBlock[][] levelBlocks;

    [SerializeField]
    int currentLevel;


	// Use this for initialization
	void Start () {
        //get level sprites
        Sprite[] dirtTiles = new Sprite[4];
        Sprite[] sprites = Resources.LoadAll<Sprite>("sprites/background");
        int j = 0;
        for (int i = 0; i < sprites.Length; i++)
        {
            if(sprites[i].name.StartsWith("dirtTile"))
            {
                dirtTiles[j] = sprites[i];
                j++;
            }
        }


        //create level
        levelBlocks = new LevelBlock[12][];
        for (int i = 0; i < levelBlocks.Length; i++)
            levelBlocks[i] = new LevelBlock[12];
        
        //create level blocks
        for (int y = 0; y < 12; y++)
        {
            
            for (int x = 0; x < 12; x++)
            {
                float xOffset = 0 + (x * 0.16f);
                float yOffset = 0 - (y * 0.16f);
                Vector3 pos = transform.position;
                pos.x = pos.x + xOffset;
                pos.y = pos.y + yOffset;
                pos.z = 0;

                levelBlocks[x][y] = Instantiate<LevelBlock>(blockPrefab);
                levelBlocks[x][y].transform.position = pos;
                levelBlocks[x][y].GetComponent<SpriteRenderer>().sprite = dirtTiles[(y/3)];
            }
            
        }

        //put in dug blocks and monsters depending on level
        switch(currentLevel)
        {
            case 1:
                //dug blocks
                for(int i = 2; i<=5; i++)
                    levelBlocks[1][i].SetOverlay(11);

                for(int i = 8; i<=10; i++)
                    levelBlocks[i][2].SetOverlay(11);

                for(int i = 6; i<=7; i++)
                    levelBlocks[10][i].SetOverlay(11);

                for(int i = 2; i<= 5; i++)
                    levelBlocks[i][9].SetOverlay(11);

                levelBlocks[6][5].SetOverlay(11);

                //monsters
                TomatoThing t = Instantiate<TomatoThing>(tomatoPrefab);
                t.transform.position = new Vector3(levelBlocks[2][9].transform.position.x + 0.08f, levelBlocks[2][9].transform.position.y - 0.08f, 0);

                t = Instantiate<TomatoThing>(tomatoPrefab);
                t.transform.position = new Vector3(levelBlocks[8][2].transform.position.x + 0.08f, levelBlocks[8][2].transform.position.y - 0.08f, 0);

                t = Instantiate<TomatoThing>(tomatoPrefab);
                t.transform.position = new Vector3(levelBlocks[1][2].transform.position.x + 0.08f, levelBlocks[1][2].transform.position.y - 0.08f, 0);

                Dino d = Instantiate<Dino>(dinoPrefab);
                d.transform.position = new Vector3(levelBlocks[10][6].transform.position.x + 0.08f, levelBlocks[10][6].transform.position.y - 0.08f, 0);



                //player
                Player p = Instantiate<Player>(playerPrefab);
                p.transform.position = new Vector3(levelBlocks[6][5].transform.position.x + 0.08f, levelBlocks[6][5].transform.position.y - 0.08f, 0);

                break;

            case 2:
                //dug blocks
                for (int i = 2; i <= 5; i++)
                    levelBlocks[1][i].SetOverlay(11);

                for (int i = 8; i <= 10; i++)
                    levelBlocks[i][2].SetOverlay(11);

                for (int i = 6; i <= 7; i++)
                    levelBlocks[10][i].SetOverlay(11);

                for (int i = 2; i <= 5; i++)
                    levelBlocks[i][9].SetOverlay(11);

                levelBlocks[6][5].SetOverlay(11);

                for (int i = 3; i <= 7; i++)
                    levelBlocks[i][7].SetOverlay(11);

                //monsters
                t = Instantiate<TomatoThing>(tomatoPrefab);
                t.transform.position = new Vector3(levelBlocks[2][9].transform.position.x + 0.08f, levelBlocks[2][9].transform.position.y - 0.08f, 0);
                t.movementSpeed = 0.5f;

                t = Instantiate<TomatoThing>(tomatoPrefab);
                t.transform.position = new Vector3(levelBlocks[8][2].transform.position.x + 0.08f, levelBlocks[8][2].transform.position.y - 0.08f, 0);
                t.movementSpeed = 0.5f;

                t = Instantiate<TomatoThing>(tomatoPrefab);
                t.transform.position = new Vector3(levelBlocks[1][2].transform.position.x + 0.08f, levelBlocks[1][2].transform.position.y - 0.08f, 0);
                t.movementSpeed = 0.5f;

                d = Instantiate<Dino>(dinoPrefab);
                d.transform.position = new Vector3(levelBlocks[10][6].transform.position.x + 0.08f, levelBlocks[10][6].transform.position.y - 0.08f, 0);
                d.movementSpeed = 0.5f;

                d = Instantiate<Dino>(dinoPrefab);
                d.transform.position = new Vector3(levelBlocks[3][7].transform.position.x + 0.08f, levelBlocks[7][7].transform.position.y - 0.08f, 0);
                d.movementSpeed = 0.5f;

                d = Instantiate<Dino>(dinoPrefab);
                d.transform.position = new Vector3(levelBlocks[7][7].transform.position.x + 0.08f, levelBlocks[7][7].transform.position.y - 0.08f, 0);
                d.movementSpeed = 0.5f;



                //player
                p = Instantiate<Player>(playerPrefab);
                p.transform.position = new Vector3(levelBlocks[6][5].transform.position.x + 0.08f, levelBlocks[6][5].transform.position.y - 0.08f, 0);

                break;
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int[] GetTileCoords(LevelBlock l)
    {
        int[] coords = null;

        for (int y = 0; y < 12; y++)
            for (int x = 0; x < 12; x++)
                if (levelBlocks[x][y] == l)
                    coords = new int[] { x, y };

        return coords;
    }
}
