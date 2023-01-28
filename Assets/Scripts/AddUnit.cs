using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUnit : MonoBehaviour
{
    public Transform Cont;
    private bool isFirst = true;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.TryGetComponent<ActiveUnit>(out _))
        {
            if (isFirst)
            {
                isFirst = false;
                this.gameObject.transform.SetParent(Cont);
                this.gameObject.AddComponent<ActiveUnit>();
                gameObject.GetComponentInChildren<Animator>().SetTrigger("isRuning");
                Cont.gameObject.GetComponent<ChangeUnitPos>().poses.Add(new Vector3(0,0,0));
                Destroy(this);
            }
        }    
    }
}
