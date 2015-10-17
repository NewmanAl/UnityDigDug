﻿using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

    [SerializeField]
    LevelBlock blockPrefab;
    
    public LevelBlock[][] levelBlocks;


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
                float xOffset = -1.075f + (x * 0.16f);
                float yOffset = 0.636f + (y * 0.16f);
                Vector3 pos = transform.position;
                pos.x = pos.x + xOffset;
                pos.y = pos.y + yOffset;

                levelBlocks[x][y] = ((GameObject)Instantiate(blockPrefab, pos, Quaternion.identity)).GetComponent<LevelBlock>();
                levelBlocks[x][y].GetComponent<SpriteRenderer>().sprite = dirtTiles[(y/3)];
            }
        }
        
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}