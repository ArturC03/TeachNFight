using UnityEngine;

public class Grito : MonoBehaviour
{
    public CircleCollider2D colliderGrito;
    public bool canAttack = true;
    public int player;
    public GameObject grito;
    void Update()
    {
        //Debug.Log(isAttacking);
		if (player == 1){
			if (Input.GetKeyDown(KeyCode.I) && canAttack)
			{
				AttackGrito();
			}
			else
			{
				//Disable animator
			}   
		}
    }
    void AttackGrito(){
        Instantiate(grito, transform.position, Quaternion.identity);
        while (Input.GetKey(KeyCode.I)){

        }
    }
}
