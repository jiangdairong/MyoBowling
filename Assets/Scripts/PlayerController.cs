using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	private Rigidbody rb;
	public float speed;
	public Text myText;
    public Animation anim1;
	public Animation anim2;
	public Animation anim3;
	public Animation anim4;
	public Animation anim5;
	public Animation anim6;
	public int count=0;
	public float ver1 ;
	public float hor1 ;
	public Vector3 orig; 

    // Use this for initialization
    void Start () {
//		rb = GetComponent<Rigidbody> ();
//		ver1 = Random.Range (30.0f,50.0f);
//		hor1 = Random.Range(-0.15f,0.15f);
//		Vector3 vect = new Vector3 (hor1, 0, ver1);
//		rb.AddForce (vect * speed);
//		StartCoroutine (SequenceStart ());
//		StopCoroutine (SequenceStart ());
//		StartCoroutine (SequenceCont ());
//		StopCoroutine (SequenceCont ());

		orig =  this.transform.position;

    }
    void FixedUpdate(){

//		if(this.transform.position.y<-100){
//			this.transform.position = orig;
//		}

		if (Input.GetKey (KeyCode.R)) {
			SceneManager.LoadScene ("mainScene");  
        }

        if (Input.GetKey ("escape")) {
			Application.Quit ();
		}



	}
//
//	IEnumerator SequenceStart()
//	{
//		yield return new WaitForSeconds(2);        
//		anim1.Play ();
//		anim6.Play ();
//		yield return new WaitForSeconds(2);
//		anim2.Play ();
//		yield return new WaitForSeconds(1);        
//		anim3.Play ();
//		anim4.Play ();
//		yield return new WaitForSeconds(2);        
//		anim5.Play ();
//
//	}
//	IEnumerator SequenceDelay()
//	{
//		for (float i = 0; i <= 30; i += Time.deltaTime) {
//			yield return new WaitForSeconds (10);
//		}
//	}
//	IEnumerator SequenceCont()
//	{
//		for (float i = 0; i <= 50; i += Time.deltaTime) {
//
//			yield return new WaitForSeconds (20);
//			anim1.Play ();
//			yield return new WaitForSeconds (20);
//			anim2.Play ();
//			yield return new WaitForSeconds (20);
//			anim3.Play ();
//			yield return new WaitForSeconds (20);
//			anim4.Play ();
//			yield return new WaitForSeconds (20);
//			anim5.Play ();
//			yield return new WaitForSeconds (20);
//			anim6.Play ();
//		}
//	}
}
