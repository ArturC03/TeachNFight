using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    private int winner;
    private int loser;
    void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player")){
            loser = other.GetComponent<PlayerCombat>().player;
            Destroy(other.gameObject);
            if (loser == 1){
                winner = 2;
            }
            else{
                winner = 1;
            }
            SceneManager.LoadScene("VictoryScreen");
        }
    }
}
