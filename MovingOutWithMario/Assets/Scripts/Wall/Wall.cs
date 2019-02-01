using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    [System.Serializable]
    public struct WallParts
    {
        [HideInInspector] public SpriteRenderer sprite;
        public SO_WallData wallData;
    }

    public WallParts[] wallParts;

    // Use this for initialization
    void Start () {
        SpriteRenderer[] getSprites = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < wallParts.Length; i++)
        {
            wallParts[i].sprite = getSprites[i];
            wallParts[i].sprite.color = wallParts[i].wallData.defaultColor;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerWallTrigger"))
        {
            for (int i = 0; i < wallParts.Length; i++)
            {
                wallParts[i].sprite.color = wallParts[i].wallData.transparentColor;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerWallTrigger"))
        {
            for (int i = 0; i < wallParts.Length; i++)
            {
                wallParts[i].sprite.color = wallParts[i].wallData.defaultColor;
            }
        }
    }
}
