using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Global{
	public static int count = 0;
	public static int idCount = 0;
    public static int score = 0;
    public static int roundscore = 0;
    public static bool iszero;
	public static bool[] hitFlag = new bool[] { false, false, false, false, false, false, false, false, false, false, false };                                                                                                                                                                                                                                         
}

public class PinController : MonoBehaviour {

	private int id;
    public Text test;
	public Text textscore;
	//ColorBoxByPose control;
    GameObject get;
    ColorBoxByPose QQ;
    // Use this for initialization
    void Start () {
		Global.idCount++;
        Global.iszero = true;
        get = GameObject.FindGameObjectWithTag("Joint");
        QQ = get.GetComponent<ColorBoxByPose>();
//		id = Global.idCount;
		if( this.transform.position.z < 10.0 ){
			id = 1;
		}
		else if( this.transform.position.z < 11.0 ){
			if (this.transform.position.x < 0 ) {
				id = 2;
			} else {
				id = 3;
			}
		}
		else if( this.transform.position.z < 12 ){
			if (this.transform.position.x < 0 ) {
				id = 4;
			} else if(this.transform.position.x < 1 ){
				id = 5;
			} else {
				id = 6;
			}
		}
		else if( this.transform.position.z < 13.0 ){
			if (this.transform.position.x < -3.0 ) {
				id = 7;
			} else if (this.transform.position.x < -1.0 ){
				id = 8;
			} else if (this.transform.position.x < 2.0 ){
				id = 9;
			} else {
				id = 10;
			}
		}
    }

	// Update is called once per frame
	void Update () {
		textscore = GameObject.FindGameObjectWithTag ("Score" + QQ.round).transform.FindChild ("Text").GetComponent<Text> ();
		if (this.transform.position.y > 0.25 && Global.hitFlag[id]==false)
        {
            Global.hitFlag[id] = true;
			Global.roundscore++;
			Global.score++;
            Global.iszero = false;
            if (QQ.round == 10)
                textscore.text = "" + Global.score;
            else
                textscore.text = "" + Global.roundscore;
        }
        if (Global.iszero == true)
            textscore.text = "" + 0;
        //test.text = "";
		for(int i = 4; i >= 1; i-- ){
			int start = 1;
			for( int c = 1; c <= i-1; c++ ){
				start += c;
			}
			for (int c = 0; c < 4 - i; c++) {
				//test.text += "  ";
			}
			for( int j = 0; j < i; j++ ){
				if (Global.hitFlag [ j + start]) {
					//test.text += "1 ";
				} else {
					//test.text += "0 ";
				}
			}
			//test.text += System.Environment.NewLine;
		}
		if (Input.GetKey (KeyCode.R)) {
			for (int i = 0; i < 11; i++) {
				Global.hitFlag [i] = false;
			}
		}

        if (this.transform.position.y <-5.0f)
        {
            Destroy(this.gameObject);
        }
	}

  //  void OnCollisionEnter(Collision collision)
  //  {
		//if (collision.gameObject.CompareTag("Pin") || collision.gameObject.CompareTag("Player"))
  //      {
		//	Global.hitFlag[id] = true;
  //      }

  //  }
	void OnMouseDown(){
		Global.hitFlag[id] = true;
//		test.text = this.transform.position.x + " " + this.transform.position.z;
	}
}
