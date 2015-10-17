using UnityEngine;
using System.Collections;

public class LevelBlock : MonoBehaviour {

    Sprite[] overlays;
    SpriteRenderer currentOverlay;

    public bool dug;
    
    // Use this for initialization
	void Start () {
        
        Sprite[] sprites = Resources.LoadAll<Sprite>("sprites/background");

        overlays = new Sprite[11];
        int j = 0;
        for(int i =0; i<sprites.Length; i++)
            if (sprites[i].name.StartsWith("tunnelOverlay"))
            {
                overlays[j] = sprites[i];
                j++;
            }

        currentOverlay = transform.FindChild("TunnelOverlay").gameObject.GetComponent<SpriteRenderer>();

        Debug.Log(transform.position.x);
        Debug.Log(GetComponent<SpriteRenderer>().sprite.bounds.size.x);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
