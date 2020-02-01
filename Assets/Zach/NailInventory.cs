using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NailInventory : MonoBehaviour
{
    [SerializeField]
    public Image[] images;
    int count = 0;

    public void Add(){
        if(count == images.Length)
            return;
            print("enabled");
        images[count].enabled = true;
        count++;
    }
    public void Subtract(){
        if(count==0)
            return;
        images[count-1].enabled = false;
        count--;
    }
}
