using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoSingleton<LightController>
{

    public Canvas candleCanvas;
    public GameObject Light;

    // Start is called before the first frame update
    void Start()
    {
        LightOn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightOn()
    {
        Light.SetActive(false);
        candleCanvas.gameObject.SetActive(false);
    }

    public void LightOff()
    {
        Light.SetActive(true);
        candleCanvas.gameObject.SetActive(true);
    }
}
