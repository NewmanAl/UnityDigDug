using UnityEngine;
using System.Collections;

public class LevelBlock : MonoBehaviour {

    Sprite[] overlays;
    SpriteRenderer currentOverlay;

    public bool dug;
    
    // Use this for initialization
	void Start () {
        
        Sprite[] sprites = Resources.LoadAll<Sprite>("sprites/background");

        overlays = new Sprite[16];
        int j = 0;
        for(int i =0; i<sprites.Length; i++)
            if (sprites[i].name.StartsWith("tunnelOverlay"))
            {
                overlays[j] = sprites[i];
                j++;
            }

        currentOverlay = transform.FindChild("TunnelOverlay").gameObject.GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (currentOverlay != null && other.gameObject.name == "Player")
        {
            //determine appropriate overlay
            Level l = GameObject.Find("Level").GetComponent<Level>();
            int[] myPos = l.GetTileCoords(this);

            Player.Direction dir = other.gameObject.GetComponent<Player>().facing;

            bool dugRight, dugLeft, dugUp, dugDown;
            dugRight = dugLeft = dugUp = dugDown = false;

            if (myPos[0] != 0)
                dugRight = l.levelBlocks[myPos[0] - 1][myPos[1]].dug;

            if (myPos[0] != 11)
                dugRight = l.levelBlocks[myPos[0] + 1][myPos[1]].dug;

            if (myPos[1] != 0)
                dugUp = l.levelBlocks[myPos[0]][myPos[1] - 1].dug;

            if (myPos[1] != 11)
                dugDown = l.levelBlocks[myPos[0]][myPos[1] + 1].dug;

            switch (dir)
            { 
                //case Player.Direction.right:



            }


            
            currentOverlay.sprite = overlays[1];
            dug = true;
        }
    }

}
