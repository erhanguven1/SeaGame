using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Photon.Pun;
public class Ship : MonoBehaviourPun
{
    public Crew ownerCrew;
    public int gold, food;

    public bool AreWeFighting;
    public Island CurrentIsland;
    public bool can;

    public float speed =.1f;

    public GameController gameController;
    public GameObject shipCollider;

    public List<GameObject> cannons = new List<GameObject>();

    void Start()
    {
        if (!photonView.IsMine)
        {
            this.enabled = false;
        }
        gameController = FindObjectOfType<GameController>();
        GameEvents.current.onEnterFight += EnteredFight;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (photonView.IsMine&&collision.tag == "Cannon")
        {
            Destroy(collision.gameObject);
            gameController.shipHealth -= gameController.fireType == GameController.FireType.Front ? 15 : 7.5f;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gameController.OnClickFireType();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<Rigidbody>().AddForce((transform.right + transform.up/5) * 4000);
        }


        if (Input.GetAxis("Vertical") == 0)
        {
            speed = Mathf.Lerp(speed, 0, Time.deltaTime);
        }
        else
        {
            speed += Time.deltaTime * Input.GetAxis("Vertical") * 2;
            speed = Mathf.Clamp(speed, -10, 10);
        }

        if (!can && Input.GetMouseButton(1))
        {
            EnteredFight(this);
        }

        transform.position += transform.right * speed*Time.deltaTime;
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
