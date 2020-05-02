using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Island : MonoBehaviour
{

    public int gold, food, human;
    public List<Ship> dockedShips = new List<Ship>();

    public TextMeshPro IslandInfoText;

    void OnShipDocked()
    {
        int dockedCount = dockedShips.Count - 1;
        if (dockedCount<0)
        {
            return;
        }

        if (gold > 0)
        {
            float r = Random.value * dockedCount;

            dockedShips[0].gold += r < .5f ? gold-- : 0;
            if (r >= .5f)
            {
                dockedShips[1].gold += gold / gold--;

            }
        }

        if (food > 0)
        {
            float r = Random.value * dockedCount;

            dockedShips[0].food += r < .5f ? food / food-- : 0;
            if (r >= .5f)
            {
                dockedShips[1].food += food--;

            }
        }

        if (human > 0)
        {
            float r = Random.value * dockedCount;

            dockedShips[0].ownerCrew.memberCount += r < .5f ? human / human-- : 0;
            if (r >= .5f)
            {
                dockedShips[1].ownerCrew.memberCount += human--;

            }
        }

        IslandInfoText.text = "gold: " + gold + "\nfood: " + food + "\nman: " + human;

        Invoke("OnShipDocked", 1.5f);

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ship")
        {
            other.GetComponent<Ship>().CurrentIsland = this;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ship")
        {
            Ship who = other.GetComponent<Ship>();
            if (!dockedShips.Contains(who))
            {
                dockedShips.Add(who);
            }
            if (dockedShips.Count == 1)
            {
                Invoke("OnShipDocked", 1.5f);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ship")
        {
            if (dockedShips.Contains(other.GetComponent<Ship>()))
            {
                dockedShips.Remove(other.GetComponent<Ship>());
                other.GetComponent<Ship>().CurrentIsland = null;
            }
        }
    }

}
