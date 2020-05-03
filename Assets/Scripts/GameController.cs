using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public enum FireType { Side, Front }
    public FireType fireType;

    public Text FiretypeText;
    public Image cooldownSlider;
    public Slider shipHealthSlider;

    float cooldownSpeed;
    public int firedSideCount;

    public float shipHealth;

    // Start is called before the first frame update
    void Start()
    {
        cooldownSpeed = (int)fireType + 1;
        cooldownSlider.fillAmount = 1;
    }

    public void OnClickFireType()
    {
        fireType = (GameController.FireType)(((int)fireType + 1) % 2);
        FiretypeText.text = fireType.ToString();
        cooldownSpeed = (int)fireType + 1;
        cooldownSlider.fillAmount = 1;
        print(cooldownSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownSlider.fillAmount <= 1)
        {
            cooldownSlider.fillAmount += Time.deltaTime * cooldownSpeed;
        }
        shipHealthSlider.value = shipHealth;
    }
}
