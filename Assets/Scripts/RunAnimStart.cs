using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimStart : MonoBehaviour
{
    public ActiveUnit[] units;

    public void StartRunAnim()
    {
        units = FindObjectsOfType<ActiveUnit>();
        for(int i = 0; i < this.units.Length; i++)
        {
            this.units[i].gameObject.GetComponentInChildren<Animator>().SetTrigger("isRuning");
        }
    }
}
