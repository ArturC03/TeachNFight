using Unity.VisualScripting;
using UnityEngine;

public class Death : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player")){
            Destroy(other.gameObject);
        }
    }
}
