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
            
            /*
            
            //determine appropriate overlay
            Level l = GameObject.Find("Level").GetComponent<Level>();
            int[] myPos = l.GetTileCoords(this);

            Player.Direction dir = other.gameObject.GetComponent<Player>().facing;

            bool dugRight, dugLeft, dugUp, dugDown;
            dugRight = dugLeft = dugUp = dugDown = false;

            if (myPos[0] != 0)
                dugLeft = l.levelBlocks[myPos[0] - 1][myPos[1]].dug;

            if (myPos[0] != 11)
                dugRight = l.levelBlocks[myPos[0] + 1][myPos[1]].dug;

            if (myPos[1] != 0)
                dugUp = l.levelBlocks[myPos[0]][myPos[1] - 1].dug;

            if (myPos[1] != 11)
                dugDown = l.levelBlocks[myPos[0]][myPos[1] + 1].dug;

            switch (dir)
            { 
                case Player.Direction.right:
                    if (!dugRight && !dugUp && !dugDown)//end of tunnel
                        currentOverlay.sprite = overlays[7];

                    if (!dugRight && dugUp && !dugDown)//corner up
                        currentOverlay.sprite = overlays[2];

                    if (!dugRight && !dugUp && dugDown)//corner down
                        currentOverlay.sprite = overlays[8];

                    if (!dugRight && dugUp && dugDown)//up down
                        currentOverlay.sprite = overlays[13];

                    if (dugRight && !dugUp & !dugDown)// straight
                        currentOverlay.sprite = overlays[6];

                    if (dugRight && dugUp && !dugDown) // 
                        currentOverlay.sprite = overlays[15];

                    if (dugRight && !dugUp && dugDown)
                        currentOverlay.sprite = overlays[14];

                    if (dugRight && dugUp && dugDown)
                        currentOverlay.sprite = overlays[11];

                    break;

                case Player.Direction.left:
                    if (!dugLeft && !dugUp && !dugDown) //end of tunnel
                        currentOverlay.sprite = overlays[4];

                    if (!dugLeft && dugUp && !dugDown) //corner up
                        currentOverlay.sprite = overlays[5];

                    if (!dugLeft && !dugUp && dugDown) //corner down
                        currentOverlay.sprite = overlays[3];

                    if (!dugLeft && dugUp && dugDown) //up down
                        currentOverlay.sprite = overlays[12];

                    if (dugLeft && !dugUp && !dugDown) //straight
                        currentOverlay.sprite = overlays[6];

                    if (dugLeft && dugUp && !dugLeft)
                        currentOverlay.sprite = overlays[15];

                    if (dugLeft && !dugUp && dugDown)
                        currentOverlay.sprite = overlays[14];

                    if (dugLeft && dugUp && dugDown)
                        currentOverlay.sprite = overlays[11];

                    break;

                case Player.Direction.up:

                    if (!dugUp && !dugLeft && !dugRight)//tunnel end
                        currentOverlay.sprite = overlays[1];

                    if (!dugUp && dugLeft && !dugRight) //corner left
                        currentOverlay.sprite = overlays[8];

                    if (!dugUp && !dugLeft && dugRight) //corner right
                        currentOverlay.sprite = overlays[3];

                    if(!dugUp && dugLeft && dugRight) // 
                        currentOverlay.sprite = overlays[14];

                    if(dugUp && !dugLeft && !dugRight) // up down
                        currentOverlay.sprite = overlays[0];

                    if (dugUp && dugLeft && !dugRight)
                        currentOverlay.sprite = overlays[13];

                    if (dugUp && !dugLeft && dugRight)
                        currentOverlay.sprite = overlays[12];

                    if (dugUp && dugLeft && dugRight)
                        currentOverlay.sprite = overlays[11];

                    break;

                case Player.Direction.down:

                    if (!dugDown && !dugLeft && !dugDown) //end tunnel
                        currentOverlay.sprite = overlays[9];

                    if (!dugDown && dugLeft && !dugRight) //corner left
                        currentOverlay.sprite = overlays[2];

                    if (!dugDown && !dugLeft && dugRight) //corner right
                        currentOverlay.sprite = overlays[5];

                    if (!dugDown && dugLeft && dugRight)
                        currentOverlay.sprite = overlays[15];

                    if (dugDown && !dugLeft && !dugRight) // up down
                        currentOverlay.sprite = overlays[0];

                    if (dugDown && dugLeft && !dugRight)
                        currentOverlay.sprite = overlays[13];

                    if (dugDown && !dugLeft && dugRight)
                        currentOverlay.sprite = overlays[12];

                    if (dugDown && dugLeft && dugRight)
                        currentOverlay.sprite = overlays[11];

                    break;
            }*/

            currentOverlay.sprite = overlays[11];


            dug = true;
        }
    }

}
