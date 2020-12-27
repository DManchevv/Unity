using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class TargetExit : MonoBehaviour
{
	public float exitAfterSeconds = 10f; // how long to exist in the world
	public float exitAnimationSeconds = 1f; // should be >= time of the exit animation
    public GameObject newNegative;
	private bool startDestroy = false;
	private float targetTime;
    string scene;
	// Use this for initialization
	void Start ()
	{
		// set the targetTime to be the current time + exitAfterSeconds seconds
		targetTime = Time.time + exitAfterSeconds;
        scene = SceneManager.GetActiveScene().name;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// continually check to see if past the target time
		if (Time.time >= targetTime) {
            if (this.GetComponent<Animator>() == null)
            {
                // no Animator so just destroy right away
                if (this.gameObject.tag == "HardNegative")
                {
                    Vector3 leftPos = transform.position;
                    leftPos.x -= 3;
                    leftPos.y -= 3;

                    Instantiate(newNegative, leftPos, transform.rotation);
                }
                Destroy(gameObject);
            }
            else if (!startDestroy)
            {
                // set startDestroy to true so this code will not run a second time
                startDestroy = true;

                // trigger the Animator to make the "Exit" transition
                this.GetComponent<Animator>().SetTrigger("Exit");

                // Call KillTarget function after exitAnimationSeconds to give time for animation to play
                Invoke("KillTarget", exitAnimationSeconds);
                if (this.gameObject.tag == "HardNegative")
                {
                    Vector3 leftPos = transform.position;
                    leftPos.x -= 3;
                    leftPos.y -= 3;
                    Instantiate(newNegative, leftPos, transform.rotation);
                    if (scene == "Level3")
                    {
                        Vector3 rightPos = transform.position;
                        rightPos.x += 3;
                        rightPos.y += 3;
                        Instantiate(newNegative, rightPos, transform.rotation);
                    }
                }
            }
		}
	}

	// destroy the gameObject when called
	void KillTarget ()
	{
		// remove the gameObject
		Destroy (gameObject);
	}
}
