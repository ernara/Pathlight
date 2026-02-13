//using UnityEngine;

//public class Boss : MonoBehaviour
//{
//    int hits;

//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Cube"))
//        {
//            other.tag = "Untagged";      
//            hits++;
//            if (hits >= 4) Destroy(gameObject);
//        }
//    }
//}


using UnityEngine;


public class Boss : MonoBehaviour
{
    int hits;

    void OnCollisionEnter(Collision col)
    {
        Spell spell = col.gameObject.GetComponent<Spell>();
        if (spell == null) return;

        hits++;
        //Destroy(col.gameObject);   //sudestroyins spello

        if (hits >= 4)
            Destroy(gameObject);
    }
}


