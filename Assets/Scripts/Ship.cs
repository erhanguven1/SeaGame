using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class Ship : MonoBehaviour
{
    public Crew ownerCrew;
    public int gold, food;

    public bool AreWeFighting;
    public Island CurrentIsland;
    public bool can;

    public float speed =.1f;
    void Start()
    {
        
        GameEvents.current.onEnterFight += EnteredFight;
    }
    private void Update()
    {
        if (Input.GetAxis("Vertical") == 0)
        {
            speed = Mathf.Lerp(speed, 0, Time.deltaTime);
        }
        else
        {
            speed += Time.deltaTime * Input.GetAxis("Vertical") * 2;
            //speed *= Math.Abs((int)Input.GetAxis("Vertical"));
            speed = Mathf.Clamp(speed, -10, 10);
        }

        /*if (Input.GetMouseButtonDown(0))
        {
            var g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            g.AddComponent<Rigidbody>();
            g.transform.localScale *= 5;
            g.transform.position = transform.position + transform.forward * 13 +Vector3.up * 2;
            g.GetComponent<Rigidbody>().AddForce(transform.forward * 5000);
            //EnteredFight(this);
        }*/
        if (!can && Input.GetMouseButton(1))
        {
            EnteredFight(this);
        }

        transform.position += transform.right * speed*Time.deltaTime;
        if (GetComponent<Rigidbody>().velocity.magnitude<30)
        {
            //GetComponent<Rigidbody>().AddForce(transform.right * Time.deltaTime * Input.GetAxis("Vertical") * 402);
        }
        if (GetComponent<Rigidbody>().angularVelocity.magnitude<15)
        {
            GetComponent<Rigidbody>().AddTorque(Vector3.up * Time.deltaTime * Input.GetAxis("Horizontal") * 505);
            if (Mathf.Abs(transform.eulerAngles.x)<15)
            {
                GetComponent<Rigidbody>().AddTorque(Vector3.left * Time.deltaTime * Input.GetAxis("Horizontal") * 205);

            }

        }
    }
    private void EnteredFight(Ship ship)
    {
        if (ship == this)
        {
            AreWeFighting = true;
            if (CurrentIsland != null)
                CurrentIsland.dockedShips.Remove(this);
        }
    }

    private void FinishedFight(Ship ship)
    {
        if (ship == this)
        {
            AreWeFighting = false;
            if (CurrentIsland!=null)
            {
                CurrentIsland.dockedShips.Add(this);
            }
            
        }
    }
}
