using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TargetBehavior : MonoBehaviour
{
    
    // target impact on game
    public int scoreAmount = 0;
	public float timeAmount = 0.0f;
    public GameObject newNegative;
	// explosion when hit?
	public GameObject explosionPrefab;
    private string scene;
    // when collided with another gameObject
    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
    }
    void OnCollisionEnter (Collision newCollision)
	{
		// exit if there is a game manager and the game is over
		if (GameManager.gm) {
			if (GameManager.gm.gameIsOver)
				return;
		}

		// only do stuff if hit by a projectile
		if (newCollision.gameObject.tag == "Projectile") {
			if (explosionPrefab) {
				// Instantiate an explosion effect at the gameObjects position and rotation
				Instantiate (explosionPrefab, transform.position, transform.rotation);
			}

			// if game manager exists, make adjustments based on target properties
			if (GameManager.gm) {
				GameManager.gm.targetHit (scoreAmount, timeAmount);
			}
				
            if ((this.gameObject.tag == "HardNegative" || this.gameObject.tag == "Negative") && scene != "Level1")
            {
                Vector3 leftPos = transform.position;
                leftPos.x -= 3;
                leftPos.y -= 3;

                Vector3 rightPos = transform.position;
                rightPos.x += 3;
                rightPos.y += 3;

                Instantiate(newNegative, leftPos, transform.rotation);
                Instantiate(newNegative, rightPos, transform.rotation);
            }
            if (GameManager.gm){
                if (this.gameObject.tag == "Ammo")
                {
                    GameManager.gm.ammoUpdate(GameManager.gm.getAmmo() + 2);
                }
            }
			// destroy the projectile
			Destroy (newCollision.gameObject);
				
			// destroy self
			Destroy (gameObject);
		}
	}
}
