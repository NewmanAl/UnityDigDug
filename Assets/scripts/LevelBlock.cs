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

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (currentOverlay != null && other.gameObject.name == "Player")
        {
            currentOverlay.sprite = overlays[1];
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("FUCK ME");
    }
}
