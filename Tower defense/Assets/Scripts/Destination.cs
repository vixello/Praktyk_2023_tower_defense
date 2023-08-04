using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Destination : MonoBehaviour
{
    public int troopsCount = 0;
    public TextMeshProUGUI troopCountText;
    private void Update()
    {
        troopCountText.text = troopsCount.ToString(); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BaseCollision")
        {
            troopsCount++;
            Debug.Log("Troops came");
        }
    }

 
}
