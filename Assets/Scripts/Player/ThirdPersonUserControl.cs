using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        public GameObject player;
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

        public bool isTeleported = false;
	    public string portalTag;
	    public float portalDistance = 5.0f;

        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);
            bool m_Jump = Input.GetKey(KeyCode.Space);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v * m_CamForward + h * m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v * Vector3.forward + h * Vector3.right;
            }
#if !MOBILE_INPUT
            // walk speed multiplier
            if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;

            if(isTeleported)
            {
                var portal = GameObject.FindGameObjectWithTag(portalTag);
                var distance = Vector3.Distance(player.transform.position, portal.transform.position);
                if(distance > portalDistance) isTeleported = false;
            }
        }

        private void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("PickupCube"))
		{
			other.gameObject.SetActive(false);
		}

		// Portal Teleportation
		// TODO:Set camera behind the portal after teleporting

		if(!isTeleported)
		{
			if(other.gameObject.CompareTag("Portal_A")){
			// MainScene->PortalA to OrangeScene->PortalOrange
				Teleport(player, GameObject.FindGameObjectWithTag("Portal_Orange"), "OrangeScene");
				portalTag = "Portal_Orange";
			}else if(other.gameObject.CompareTag("Portal_B")){
			// MainScene->PortalB to BlueScene->PortalBlue
				Teleport(player, GameObject.FindGameObjectWithTag("Portal_Blue"),"BlueScene");
				portalTag = "Portal_Blue";
			}else if(other.gameObject.CompareTag("Portal_Orange")){
			// OrangeScene->PortalOrange to MainScene->PortalA 
				Teleport(player, GameObject.FindGameObjectWithTag("Portal_A"), "MainScene");
				portalTag = "Portal_A";
			}else if(other.gameObject.CompareTag("Portal_Blue")){
			// BlueScene->PortalBlue to MainScene->PortalB 
				Teleport(player, GameObject.FindGameObjectWithTag("Portal_B"), "MainScene");
				portalTag = "Portal_B";
			}
		}
		
	}

    //TODO:Might be better to make a TeleportManager thing to manage all the items/player teleportation
	private void Teleport(GameObject go, GameObject dest, String sceneName)
	{ // Teleport a GameObject(usually is player) to a destination
        Scene scene = SceneManager.GetSceneByName(sceneName);
		if(scene.isLoaded){
			SceneManager.MoveGameObjectToScene(go, scene);
        }else{
            // Might need to use a SceneController to deal with the new loaded scene
            // for optimizing memory management. 

            // TODO: Cannot be teleport if the scene is not loaded the first time player entered the portal
            // TODO: The baked light mapping is also a problem if we need SceneManager to load the scene
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
			Scene loadedScene = SceneManager.GetSceneByName(sceneName);
            SceneManager.MoveGameObjectToScene(go, loadedScene);
        }
		player.transform.position = dest.transform.position;

		// TODO:Might be better to detect the speed direction of the player with portal by Mathf.cos
		isTeleported = true;
	}
    }
}
