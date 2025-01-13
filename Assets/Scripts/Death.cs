using UnityEngine;

public class Death : MonoBehaviour
{
    void OnTriggerExit(Collider other){
        if (other.tag == "Player"){
            Debug.Log("Death");
        }
    }
}
