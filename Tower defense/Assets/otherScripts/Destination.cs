using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Destination : MonoBehaviour
{
    public int TroopsCount = 0;
    public TextMeshProUGUI TroopCountText;
    private void Update()
    {
        TroopCountText.text = TroopsCount.ToString(); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BaseCollision")
        {
            TroopsCount++;
            Debug.Log("Troops came");
        }
    }

 
}
