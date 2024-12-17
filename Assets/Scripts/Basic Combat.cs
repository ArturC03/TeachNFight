using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicCombat : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Attack");
            
        }
    }

    
}
