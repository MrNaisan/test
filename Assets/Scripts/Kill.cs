using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    public int killType;
    public ParticleSystem particle;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody obj))
        {
            particle.Play();
            obj.isKinematic = false;
            obj.gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
            obj.gameObject.transform.SetParent(null);
            if(killType == 1)
            {
                obj.AddExplosionForce(5, this.gameObject.transform.position, 5, 5);
            }
            Destroy(obj.GetComponent<ActiveUnit>());
            StartCoroutine(delObj(obj.gameObject));
        }    
    }

    IEnumerator delObj(GameObject obj)
    {
        if(killType == 1)
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(obj);
    }
}
