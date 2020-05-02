using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurvatureController : MonoBehaviour
{
    public List<Material> m;
    public Slider curvatureSlider;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in m)
        {
            item.SetFloat("_Curvature", curvatureSlider.value/1000);
        }
    }
}
