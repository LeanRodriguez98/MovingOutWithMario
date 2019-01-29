using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    public Camera cam;
    public Transform playerTransform;
    public Image loadingCircleImage;
    public Vector2 offset;
   

    // Use this for initialization
    void Start()
    {
        loadingCircleImage.fillAmount = 0;
        loadingCircleImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (loadingCircleImage.IsActive())
        {
            Vector2 barPos = cam.WorldToScreenPoint(playerTransform.position);
            loadingCircleImage.gameObject.transform.position = new Vector2(barPos.x + offset.x, barPos.y + offset.y);
        }
        
    }

    public void EnableLoadingCircle(bool enable)
    {
        loadingCircleImage.gameObject.SetActive(enable);
    }
   
}
