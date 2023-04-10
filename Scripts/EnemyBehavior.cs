using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	
	public float mSpeed = 20f;
	public int health = 4;
	public HeroBehavior heroBeha = null;
		
	// Use this for initialization
	void Start () {
		NewPosition();
		NewDirection();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += (mSpeed * Time.smoothDeltaTime) * transform.up;
		GlobalBehavior globalBehavior = GameObject.Find ("GameManager").GetComponent<GlobalBehavior>();
		
		GlobalBehavior.WorldBoundStatus status =
			globalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds);
			
		if (status != GlobalBehavior.WorldBoundStatus.Inside) {
			//Debug.Log("collided position: " + this.transform.position);
			NewDirection();
		}
	}

	public void onEggHIt(){
		health--;
		if (health <= 0)
        {
            NewPosition();
			NewDirection();
			health = 4;
            if (heroBeha != null)
                heroBeha.enemyDed+= 2;
        }	
	}

	public void onPlayerHit(){
		heroBeha.enemyTouched++; 
		NewPosition();
		NewDirection();
		health = 4;
        if (heroBeha != null){
            heroBeha.enemyDed++;
        }	
	}

	// New direction will be something completely random!
	private void NewDirection() {
		Vector2 v = Random.insideUnitCircle;
		transform.up = new Vector3(v.x, v.y, 0.0f);
	}

	private void NewPosition() {
		float randomX = Random.Range(0.9f * GlobalBehavior.sTheGlobalBehavior.WorldMin.x, 0.9f * GlobalBehavior.sTheGlobalBehavior.WorldMax.x);
		float randomY = Random.Range(0.9f * GlobalBehavior.sTheGlobalBehavior.WorldMin.y, 0.9f * GlobalBehavior.sTheGlobalBehavior.WorldMax.y);
        transform.position = new Vector3(randomX, randomY, 0.0f);
	}
	
}
