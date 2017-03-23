using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceScript : MonoBehaviour
{
    Image myImageComponent;
    public Sprite Mad;
    public Sprite Sad;
    public Sprite Neutral;
    public Sprite Happy;
    public Sprite Estatic;

    public void Initialize()
    {
        myImageComponent = GetComponent<Image>();
    }

    // Use this for initialization
    void Start()
    {
        myImageComponent = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetImage(int i)
    {
        switch (i)
        {
            case 1:
                myImageComponent.sprite = Mad;
                break;
            case 2:
                myImageComponent.sprite = Sad; 
                break;
            case 3:
                myImageComponent.sprite = Neutral; 
                break;
            case 4:
                myImageComponent.sprite = Happy; 
                break;
            case 5:
                myImageComponent.sprite = Estatic; 
                break;
        }
    }

}
