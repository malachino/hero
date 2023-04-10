using UnityEngine;
using UnityEngine.UI;	
using System.Collections;
using TMPro;

public class HeroBehavior : MonoBehaviour {

	public float kHeroSpeed = 20f;
    private float maxHeroSpeed = 35f;
    private float heroAcceleration = 50f;
    public GameObject mEgg = null;
	private const float kHeroRotateSpeed = 90f/2f; // 90-degrees in 2 seconds
    private float eggReload = .2f;
    private float eggTimer = 0f;
    private bool mouseControls = true;
    public TextMeshProUGUI inputMode;
    public TextMeshProUGUI eggCount;
    public TextMeshProUGUI enemyCount;
    public TextMeshProUGUI enemyDeadCount;
    public TextMeshProUGUI enemyTouchedCount;
    public int eggInt;
    public int enemyInt;
    public int enemyDed;
    public int enemyTouched;

	// Use this for initialization
	void Start () {
        if (mEgg == null)
            mEgg = Resources.Load("Prefabs/Egg") as GameObject;
        inputMode.text = "Mouse";
        eggInt = 0;
        enemyInt = 10;
        enemyDed = 0;
        // GlobalBehavior.sTheGlobalBehavior.heroBeh = this;
        // //enemyCollision.heroBe = this;
	}
	
	// Update is called once per frame
	void Update () {

        ////////////////////////////////MOVEMENT CONTROLS/////////////////////////////////////////////////////////////////////////////
        #region motion control
        //switches controls if m is pressed once
        if(Input.GetKeyDown(KeyCode.M)){
            mouseControls = !mouseControls;
            kHeroSpeed = 20f;
            if(mouseControls){
                inputMode.text = "Mouse";
            }else{
                inputMode.text = "Keyboard";
            }
        }

        if(mouseControls){//mouse controls
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        } else { //keyboard controls
            kHeroSpeed = Mathf.Min(maxHeroSpeed, (kHeroSpeed + Input.GetAxis ("Vertical") * Time.smoothDeltaTime * heroAcceleration));
            if(kHeroSpeed < 0){
                kHeroSpeed = 0;
            }
            transform.position += transform.up * (kHeroSpeed * Time.smoothDeltaTime);
        }

        //rotate happens if mouse or keyboard
        transform.Rotate(Vector3.forward, -1f * Input.GetAxis("Horizontal") * 
                                    (kHeroRotateSpeed * Time.smoothDeltaTime));
        #endregion

        GlobalBehavior.sTheGlobalBehavior.ObjectClampToWorldBound(this.transform);
        

        /////////////////////////////EGG RELOAD//////////////////////////////////////////////////////////////////////////////
        eggTimer -= Time.smoothDeltaTime;
        //Debug.Log("eggtimer ="+eggTimer);

        if (Input.GetKey("space") && eggTimer <= 0f)  // VS. GetKeyDown <<-- even, one per key press
        { // space bar hit
            GameObject e = Instantiate(mEgg) as GameObject;
            eggInt++;
            EggBehavior egg = e.GetComponent<EggBehavior>(); // Shows how to get the script from GameObject
            if (null != egg)
            {
                egg.heroBehav = this;
                e.transform.position = transform.position;
                e.transform.rotation = transform.rotation;
            }
            eggTimer = eggReload;
        }


        //////////////////////TEXT ON SCREEN UPDATES EVERY FRAME//////////////////////////
        eggCount.text = eggInt.ToString();
        enemyCount.text = enemyInt.ToString();
        enemyDeadCount.text = (enemyDed/2).ToString();
        enemyTouchedCount.text = (enemyTouched/2).ToString();
    }
}
