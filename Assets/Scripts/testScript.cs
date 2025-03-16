using UnityEngine;

public class testScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2D) {
        print(collider2D.gameObject.name + " entered collider"); 
    }

    private void OnTriggerExit2D(Collider2D collider2D) {
        print(collider2D.gameObject.name + " exited collider"); 
    }
}
