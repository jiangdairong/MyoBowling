using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

// Change the material when certain poses are made with the Myo armband.
// Vibrate the Myo armband when a fist pose is made.
public class ColorBoxByPose : MonoBehaviour
{
    public Text stateText;
    private Rigidbody rb;
    // Myo game object to connect with.
    // This object must have a ThalmicMyo script attached.
    public GameObject myo = null;
    public GameObject joint;
    private bool releaseBall = false;

    // Materials to change to when poses are made.
    public Material waveInMaterial;
    public Material waveOutMaterial;
    public Material doubleTapMaterial;
    public Text textscore;

    private Vector3 lastBallPos;
    private int frameCount = 0;
    private bool isset;

	public int round=1;

    public Animation sweep;
    // The pose from the last update. This is used to determine if the pose has changed
    // so that actions are only performed upon making them rather than every frame during
    // which they are active.
    private Pose _lastPose = Pose.Unknown;
    GameObject test;
    GameObject pin_manager;
    GameObject JJ;
    GameObject player;
    Transform act;
    Vector3 Pos;
    float[] pos_x;
    float[] pos_z;


    void Start()
    {
        pin_manager = GameObject.FindGameObjectWithTag("Pin_Manager");
        JJ = GameObject.FindGameObjectWithTag("JJ");
        pos_x = new float[10];
        pos_z = new float[10];
        isset = false;

        pos_x[0] = 4.03f;
        pos_x[1] = 2.73f;
        pos_x[2] = 5.33f;
        pos_x[3] = 1.44f;
        pos_x[4] = 4.12f;
        pos_x[5] = 6.52f;
        pos_x[6] = 0.22f;
        pos_x[7] = 2.72f;
        pos_x[8] = 5.31f;
        pos_x[9] = 7.83f;

        pos_z[0] = 10.71f;
        pos_z[1] = 11.96f;
        pos_z[2] = 11.96f;
        pos_z[3] = 13.11f;
        pos_z[4] = 13.11f;
        pos_z[5] = 13.11f;
        pos_z[6] = 14.45f;
        pos_z[7] = 14.45f;
        pos_z[8] = 14.45f;
        pos_z[9] = 14.45f;

        for (int i = 0; i < 10; i++)
        {
            test = Instantiate(Resources.Load("pin") as GameObject) as GameObject;
            test.transform.SetParent(pin_manager.transform);
            test.GetComponent<Transform>().localPosition = new Vector3(pos_x[i], 0.0f, pos_z[i]);
            test.SetActive(true);
        }

        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Joint");
        act = player.GetComponent<Transform>();
        Pos = new Vector3(1.96f,4.13f,-14.52f);

    }
    // Update is called once per frame.
    void Update ()
    {
        // Access the ThalmicMyo component attached to the Myo game object.
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

        if (Input.GetKeyDown(KeyCode.I)) {
            round++;
            //Global.iszero = false;

            for (int i = 0; i < 10;i++ )
            {
                test = Instantiate(Resources.Load("pin") as GameObject) as GameObject;
                test.transform.SetParent(pin_manager.transform);
                test.GetComponent<Transform>().localPosition = new Vector3(pos_x[i], 0.0f, pos_z[i]);
                test.SetActive(true);

            }
            for (int i = 0; i < 11; i++)
            {
                Global.hitFlag[i] = false;
            }

            //test.GetComponent<RectTransform>().sizeDelta = new Vector2(320.0f, 100.0f);

            //test.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);

        }
       

        if (act.transform.position.z > 7.0f && isset == false)
        {
            isset = true;
            StartCoroutine(load());
        }

        /*if (already == true) {
            round++;
            already = false;
            for (int i = 0; i < 10; i++)
            {
                test = Instantiate(Resources.Load("pin") as GameObject) as GameObject;
                test.transform.SetParent(pin_manager.transform);
                test.GetComponent<Transform>().localPosition = new Vector3(pos_x[i], 0.0f, pos_z[i]);

                test.SetActive(true);
            }
        }*/

        //stateText.text = (thalmicMyo.accelerometer * 100).ToString();
        var a = joint.transform.FindChild("Sphere");
        //stateText.text = GameObject.Find("Box").transform.position.ToString() + "";
        // Check if the pose has changed since last update.
        // The ThalmicMyo component of a Myo game object has a pose property that is set to the
        // currently detected pose (e.g. Pose.Fist for the user making a fist). If no pose is currently
        // detected, pose will be set to Pose.Rest. If pose detection is unavailable, e.g. because Myo
        // is not on a user's arm, pose will be set to Pose.Unknown.
        if (thalmicMyo.pose != _lastPose) {
            _lastPose = thalmicMyo.pose;

            
            // Vibrate the Myo armband when a fist is made.
            if (thalmicMyo.pose == Pose.FingersSpread) {
                thalmicMyo.Vibrate (VibrationType.Medium);

                rb.useGravity = true;
                if (!releaseBall)
                {
                    var temp = joint.transform.FindChild("Sphere");
                    temp.transform.parent = null;
                    releaseBall = true;

                    Vector3 thisAcc = thalmicMyo.accelerometer;

                    
                    Vector3 direction = temp.transform.position - lastBallPos;
                    stateText.text = direction.y.ToString() + "";
                    if (direction.y > 1)
                    {
                        direction.y = 0.5f;
                    }
                    Vector3 add;
                    add.x = 0.0f;
                    add.y = 0.0f;
                    add.z = 10.0f;
                    rb.AddForce(direction * 5000 + add * 10000);

                }


                ExtendUnlockAndNotifyUserAction (thalmicMyo);

            // Change material when wave in, wave out or double tap poses are made.
            } else if (thalmicMyo.pose == Pose.WaveIn) {
                GetComponent<Renderer>().material = waveInMaterial;

                ExtendUnlockAndNotifyUserAction (thalmicMyo);
            } else if (thalmicMyo.pose == Pose.WaveOut) {
                GetComponent<Renderer>().material = waveOutMaterial;

                ExtendUnlockAndNotifyUserAction (thalmicMyo);
            } else if (thalmicMyo.pose == Pose.DoubleTap) {
                GetComponent<Renderer>().material = doubleTapMaterial;

                ExtendUnlockAndNotifyUserAction (thalmicMyo);
            }
        }
        if (frameCount % 10 == 0)
        {
            lastBallPos = a.transform.position;
        }
        frameCount++;
        if(frameCount > 1000)
        {
            frameCount -= 1000;
        }
    }

    // Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
    // recognized.
    void ExtendUnlockAndNotifyUserAction (ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard) {
            myo.Unlock (UnlockType.Timed);
        }

        myo.NotifyUserAction ();
    }

	IEnumerator load(){
		yield return new WaitForSeconds (5);
		for (int i = 0; i < 11; i++) {
			Global.hitFlag [i] = true;
		}
        Global.roundscore = 0;
		sweep.Play ();
        yield return new WaitForSeconds(10);
        round++;
        //Global.iszero = false;
            for (int i = 0; i < 10; i++)
            {
                test = Instantiate(Resources.Load("pin") as GameObject) as GameObject;
                test.transform.SetParent(pin_manager.transform);
                test.GetComponent<Transform>().localPosition = new Vector3(pos_x[i], 0.0f, pos_z[i]);
                test.SetActive(true);

            }
            for (int i = 0; i < 11; i++)
            {
                Global.hitFlag[i] = false;
            }
            releaseBall = false;
            isset = false;
            this.transform.SetParent( JJ.transform );
            Vector3 localPos = GameObject.Find("Box").transform.position - GameObject.Find("Stick").transform.position;
            localPos = localPos.normalized;
            
            this.transform.position = GameObject.Find("Box").transform.position + localPos * 0.8f;
            
            rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;            
        
	}

}
