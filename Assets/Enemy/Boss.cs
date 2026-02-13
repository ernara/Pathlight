//using UnityEngine;


//public class Boss : MonoBehaviour
//{
//    int hits;

//    void OnCollisionEnter(Collision col)
//    {
//        Spell spell = col.gameObject.GetComponent<Spell>();
//        if (spell == null) return;

//        hits++;
//        //Destroy(col.gameObject);   //sudestroyins spello

//        if (hits >= 4)
//            Destroy(gameObject);
//    }
//}


using UnityEngine;

public class Boss : MonoBehaviour
{
    int hits;

    void OnCollisionEnter(Collision col)
    {
        HandleSpellHit(col.gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        HandleSpellHit(col.gameObject);
    }

    void HandleSpellHit(GameObject obj)
    {
        Spell spell = obj.GetComponent<Spell>();
        if (spell == null) return;

        hits++;
        if (hits >= 4)
            Destroy(gameObject);
    }
}



