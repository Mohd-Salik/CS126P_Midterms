using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//VARIABLE REFERENCES
	[Header("REFERENCES")]
	public Transform trans;
	public Transform modelTrans;

	[Header("GOAL AND START")]
	public Transform food;
	public Transform startPoint;

	[Header("ENEMY AI REFERENCES")]
	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	public GameObject enemy4;
	public GameObject enemy5;

	[Header("GAME MODE REFERENCES")]
	public static bool game_win = false;
	public static bool stop_movement = false;
	public static bool start = false;

	[Tooltip("LEVEL 0 - TUTORIAL\n LEVEL 1-3 RACE MODE")]
	public static int level = 0; 
	public CharacterController characterController;

	//CHARACTER CONTROLLER CODES FROM SIR
	//Movement
	[Header("MOVEMENT")]
	[Tooltip("Units moved per second at maximum speed.")]
	public float movespeed = 24;
	[Tooltip("Time, in seconds, to reach maximum speed.")]
	public float timeToMaxSpeed = .26f;
	private float VelocityGainPerSecond { get { return movespeed / timeToMaxSpeed; } }
	[Tooltip("Time, in seconds, to go from maximum speed to stationary.")]
	public float timeToLoseMaxSpeed = .2f;
	private float VelocityLossPerSecond { get { return movespeed / timeToLoseMaxSpeed; } }
	[Tooltip("Multiplier for momentum when attempting to move in a direction opposite the current traveling direction (e.g. trying to move right when already moving left).")]
	public float reverseMomentumMultiplier = 2.2f;
	//Death and Respawning
	[Header("Death and Respawning")]
	[Tooltip("How long after the player's death, in seconds, before they are respawned?")]
	public float respawnWaitTime = 0.5f;
	private Quaternion spawnRotation;
	private bool dead = false;

	//CHARACTER AND ENEMY LOCATION VARIABLES
	private Vector3 movementVelocity = Vector3.zero;
	private Vector3 spawnPoint;
	private Vector3 foodLocation;
	private Vector3 enemyLocation1;
	private Vector3 enemyLocation2;
	private Vector3 enemyLocation3;
	private Vector3 enemyLocation4;
	private Vector3 enemyLocation5;

	private void Movement(){
		//FORWARD AND BACKWARD MOVEMENT
		//If the player just entered the level do not move
		if (stop_movement != true){
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
				transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
				if (movementVelocity.z >= 0){
					movementVelocity.z = Mathf.Min(movespeed,movementVelocity.z + VelocityGainPerSecond * Time.deltaTime);
				}
				else{
					movementVelocity.z = Mathf.Min(0,movementVelocity.z + VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
				}
			}

			//If S or the down arrow key is held:
			else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
				if (movementVelocity.z > 0){
					movementVelocity.z = Mathf.Max(0,movementVelocity.z - VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
				}
				else{
					movementVelocity.z = Mathf.Max(-movespeed,movementVelocity.z - VelocityGainPerSecond * Time.deltaTime);
				}
			}
			else{
				if (movementVelocity.z > 0){
					movementVelocity.z = Mathf.Max(0,movementVelocity.z - VelocityLossPerSecond * Time.deltaTime);
				}
				else{
					movementVelocity.z = Mathf.Min(0,movementVelocity.z + VelocityLossPerSecond * Time.deltaTime);
				}
			}

			//RIGHT AND LEFT MOVEMENT:
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
				if (movementVelocity.x >= 0){
					movementVelocity.x = Mathf.Min(movespeed,movementVelocity.x + VelocityGainPerSecond * Time.deltaTime);
				}
				else{
					movementVelocity.x = Mathf.Min(0,movementVelocity.x + VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
				}
			}
			else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
				if (movementVelocity.x > 0){
					movementVelocity.x = Mathf.Max(0,movementVelocity.x - VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
				}
				else{
					movementVelocity.x = Mathf.Max(-movespeed,movementVelocity.x - VelocityGainPerSecond * Time.deltaTime);
				}
			}
			else{
				if (movementVelocity.x > 0){
					movementVelocity.x = Mathf.Max(0,movementVelocity.x - VelocityLossPerSecond * Time.deltaTime);
				}
				else{
					movementVelocity.x = Mathf.Min(0,movementVelocity.x + VelocityLossPerSecond * Time.deltaTime);
				}
			}
			if (movementVelocity.x != 0 || movementVelocity.z != 0){
				characterController.Move(movementVelocity * Time.deltaTime);
				modelTrans.rotation = Quaternion.Slerp(modelTrans.rotation,Quaternion.LookRotation(movementVelocity),.18F);
			}
		}
	}

	//RESPAWN CODE FROM WEEK12
	public void Die(){
		if (!dead){
			dead = true;
			Invoke("Respawn",respawnWaitTime);
			movementVelocity = Vector3.zero;
			enabled = false;
			characterController.enabled = false;  
			modelTrans.gameObject.SetActive(false);
		}
	}
	public void Respawn(){
		dead = false;
		trans.position = spawnPoint;
		food.position = foodLocation;
		enemy1.transform.position = enemyLocation1;
		enemy2.transform.position = enemyLocation2;
		enemy3.transform.position = enemyLocation3;
		enemy4.transform.position = enemyLocation4;
		enemy5.transform.position = enemyLocation5;
		modelTrans.rotation = spawnRotation;
		enabled = true;
		characterController.enabled = true;
		modelTrans.gameObject.SetActive(true);
	}

	//INITIALIZATION BUG FIX SHORTCUTS
	public void deactivateEverything(){
		RefreshAgent.refresh = true;
		stop_movement = true;
		enemy1.SetActive(false);
		enemy2.SetActive(false);
		enemy3.SetActive(false);
		enemy4.SetActive(false);
		enemy5.SetActive(false);
	}
	public void activateEverything(){
		RefreshAgent.refresh = false;
		FoodTrigger.food_eaten = false;
		EnemyAI.enemy_win = false;
		game_win = false;
		start = false;
		stop_movement = false;
		level = 0;
		RaceStatus.timer = 5000f;
		enemy1.SetActive(true);
		enemy2.SetActive(true);
		enemy3.SetActive(true);
		enemy4.SetActive(true);
		enemy5.SetActive(true);
	}


	
	//STATUS CHEKING WHILE ON RACE
	public void checkProgress(){
		if (FoodTrigger.food_eaten == true){
			deactivateEverything();
			if (EnemyAI.enemy_win == true){
				Debug.Log("BACTERIA GOT TO THE FOOD FIRST");
				restart();
			}
			else if(RaceStatus.count_down != true){
				Debug.Log("5 SECONDS HAS PASSED");
				restart();
			}
			else{
				Debug.Log("CONGRATULATIONS!");
				restart();
			}
			
		}
	}

	//INPUT CONTROL FUNCTIONS
	public void restart(){
		if (Input.GetKeyDown(KeyCode.Space)){
			Die();
			activateEverything();
		}
	}
	public void levelInput(){
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			Debug.Log ("level 1 selected");
			food.position = new Vector3(0f, 4f, 300f);
			startPoint.position = new Vector3(0f, 0f, 100f);
			level = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2)){
			Debug.Log ("level 2 selected");
			food.position = new Vector3(185f, 4f, 703f);
			startPoint.position = new Vector3(135f, 0f, 510f);
			level = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3)){
			Debug.Log ("level 3 selected");
			food.position = new Vector3(-118f, 4f, 497f);
			startPoint.position = new Vector3(-82f, 0f, 354f);
			level = 3;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4)){
			Debug.Log ("level 4 selected");
			food.position = new Vector3(-126f, 4f, 841f);
			startPoint.position = new Vector3(-73f, 0f, 570f);
			level = 4;
		}
	}
	public void beginLevel(){
		if (level == 1){
			enemy1.transform.position = new Vector3(5f, 0f, 103f);
			enemy2.transform.position = new Vector3(-10f, 0f, 105f);
			enemy3.SetActive(false);
			enemy4.SetActive(false);
			enemy5.SetActive(false);
		}
		else if (level == 2){
			enemy1.transform.position = new Vector3(145f, 0f, 517f);
			enemy2.transform.position = new Vector3(119f, 0f, 524f);
			enemy3.transform.position = new Vector3(115f, 0f, 512f);
			enemy4.SetActive(false);
			enemy5.SetActive(false);
		}
		else if (level == 3){
			enemy1.transform.position = new Vector3(-92f, 0f, 350f);
			enemy2.transform.position = new Vector3(-78f, 0f, 360f);
			enemy3.transform.position = new Vector3(-103f, 0f, 360f);
			enemy4.transform.position = new Vector3(-60f, 0f, 360f);
			enemy5.SetActive(false);
		}
		else if (level == 4){
			enemy1.transform.position = new Vector3(-86f, 0f, 580f);
			enemy2.transform.position = new Vector3(-21f, 0f, 585f);
			enemy3.transform.position = new Vector3(-64f, 0f, 580f);
			enemy4.transform.position = new Vector3(-105f, 0f, 575f);
			enemy5.transform.position = new Vector3(-92f, 0f, 560f);
		}

		if (Input.GetKeyDown(KeyCode.Space)){
			stop_movement = false;
			start = true;
		}
	}

	//SET DEFUALT POSITION
	void Start(){
		spawnPoint = trans.position;
		spawnRotation = modelTrans.rotation;
		foodLocation = food.position;
		enemyLocation1 = enemy1.transform.position;
		enemyLocation2 = enemy2.transform.position;
		enemyLocation3 = enemy3.transform.position;
		enemyLocation4 = enemy4.transform.position;
		enemyLocation5 = enemy5.transform.position;
	}

	//MAIN GAME FLOW
	private void Update(){
		Movement();
		levelInput();
		if (stop_movement == true){
			beginLevel();
		}
		checkProgress();
	}
}
