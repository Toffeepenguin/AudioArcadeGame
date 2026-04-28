
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMOD;
using FMOD.Studio;
using System.Collections;

public class VAG_FPSController : MonoBehaviour
{
    InputSubscription GetInput;
    Camera PlayerCamera;
    CharacterController characterController;

    Animator anim;

    Rigidbody rb;



    [Header("Functional Options")]
    public bool CanMove = true;
    public bool IsInteracting = false;


    [Header("Movement Parameters")]
    float MovementSpeed = 5f;
    Vector3 MovementDirection;

    float Gravity = 30.0f;


    [Header("Look Parameters")]
    float Sensitivity = 0.1f;
    float RotationX = 0;
    float ViewAgnle = 85f;



    [Header("Headbob Parameters")]
    float BobbingSpeed = 15f;
    float BobbingAmount = 0.05f;
    float CamPosY = 1.75f;
    float timer;

    [Header("Footstep Parameters")]
    //[SerializeField] AudioSource SFXSTEPPlayer;
    float StepsRateSpeed = 0.5f;
    float StepTimer;
    //[SerializeField] AudioClip FootStep;



    [Header("Interaction")]
    [SerializeField] Transform[] ArcadePositions;


    float InteractionRange = 2f;
    [SerializeField] LayerMask InteractablesLayer;
    [SerializeField] GameObject CrossHair;
    
    Transform CurrentArcade;
   
    Vector2 CurrentInput;
    bool ArcadeIsDetected;
    VCA vca;

      //[SerializeField][Range(-100f, 10f)] 
        private float vcaVolume = -80f;
        
        private float volume;



    [Header("UI HANDLER")]
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject PauseMenu;
    bool GameIsPause;
    bool MainMenuActive;
    bool MenuMusicMuted;
    //[SerializeField] AudioSource MenuMusicSFX;




    void Awake()
    {
        GetInput = GetComponent<InputSubscription>();
        PlayerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        
        

      



    }

    private void Start()
    {
        Time.timeScale = 1f;
       
        if (PlayerPrefs.GetInt("GAMEWASLAUNCHED") == 0)
        {
            
            vca = FMODUnity.RuntimeManager.GetVCA("vca:/Arcade");
            //vca.setVolume(0f);
           
            MainMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            CanMove = false;
            //MenuMusicSFX.Play();

            PlayerPrefs.SetInt("GAMEWASLAUNCHED", 1);
            PlayerPrefs.Save();
        }








        CrossHair.SetActive(false);
        
        if (PlayerPrefs.GetInt("GameMachineID") == 0)
        {
            transform.position = ArcadePositions[0].position;
        }
        else
        {
                transform.position = ArcadePositions[PlayerPrefs.GetInt("GameMachineID")].position;
                transform.rotation = ArcadePositions[PlayerPrefs.GetInt("GameMachineID")].rotation;

                anim.SetBool("ReturnedToArcade", true);
                CanMove = false;
                IsInteracting = true;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
        }








        StartCoroutine(setVCA());
    }

    IEnumerator setVCA()
    {
        volume = Mathf.Pow(10.0f, vcaVolume / 20f);
        vca.setVolume(volume);
        yield return new WaitForSeconds(0.0f);
    }


    void Update()
    {
    //volume = Mathf.Pow(10.0f, vcaVolume / 20f);
   //vca.setVolume(volume);
    

        if (CanMove)
        {
            HandleMovementInput();
            
            
            HandleMouseLook();

            HandleHeadBob();
          
            HandleFootsteps();

            ApplyFinalMovements();
        }
        HandleInteraction();
    }

    private void HandleMovementInput()
    {
        CurrentInput = new Vector2(GetInput.NormalizedMovementInput.x, GetInput.NormalizedMovementInput.y) * MovementSpeed;

        float moveDirectionY = MovementDirection.y;
        MovementDirection = (transform.TransformDirection(Vector3.forward) * CurrentInput.y) + (transform.TransformDirection(Vector3.right) * CurrentInput.x);
        MovementDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        RotationX -= GetInput.MouseDeltaInput.y * Sensitivity;
        RotationX = Mathf.Clamp(RotationX, -ViewAgnle, ViewAgnle);
        PlayerCamera.transform.localRotation = Quaternion.Euler(RotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, GetInput.MouseDeltaInput.x * Sensitivity, 0);
    }

   


    private void HandleHeadBob()
    {
        if (!characterController.isGrounded) 
        {
            return;
        }

        if (Mathf.Abs(MovementDirection.x) > 0.1f || Mathf.Abs(MovementDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * BobbingSpeed;
            PlayerCamera.transform.localPosition = new Vector3(PlayerCamera.transform.localPosition.x, CamPosY + Mathf.Sin(timer) *  BobbingAmount, PlayerCamera.transform.localPosition.z);
        }

    }

    private void HandleInteraction()
    {
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out RaycastHit hit, InteractionRange, InteractablesLayer))
        {
            UnityEngine.Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.forward * InteractionRange, Color.yellow);

            if(!CurrentArcade && !IsInteracting)
            CrossHair.SetActive(true);
           

            if (GetInput.EInput && !CurrentArcade && !IsInteracting)
            {
                CrossHair.SetActive(false);

                Invoke("SetInteractions", 1f);

                anim.SetBool("ZoomInAnim", true);

                CurrentArcade = hit.collider.gameObject.transform.GetChild(0).GetComponent<Transform>();


                transform.SetParent(CurrentArcade);

                PlayerCamera.transform.localRotation = Quaternion.Euler(CurrentArcade.rotation.x, 0, 0);
                transform.rotation = CurrentArcade.rotation;
                transform.position = CurrentArcade.position;
                GetComponent<FMODUnity.StudioEventEmitter>().Play();

                


                CanMove = false;

            }

        }
        else
        {
            CrossHair.SetActive(false);
            
        }


        if (GetInput.MenuInput && IsInteracting && !GameIsPause)
        {

            QuitMachine();
        }

        if (GetInput.MenuInput && !IsInteracting)
        {
            if (!GameIsPause)
            {
                PAUSEGame();
            }
            else
            {
                RESUMEBUTTON();
            }

        }



    }

    void SetInteractions()
    {
        IsInteracting = true;
        //replace with custom Cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitMachine()
    {


        anim.SetBool("ReturnedToArcade", false);

        anim.SetBool("ZoomInAnim", false);
        Invoke("ResetInteractions", 1f);

        //replace with custom Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        PlayerPrefs.SetInt("GameMachineID", 0);
        PlayerPrefs.Save();

    }

    void ResetInteractions()
    {
        IsInteracting = false;
        CanMove = true;
        CrossHair.SetActive(true);
        //RotationX = 0;
        if (transform.parent != null)
        {
            transform.SetParent(null);
        }
        CurrentArcade = null;


    }
    



    private void HandleFootsteps()
    {
        if (!characterController.isGrounded) 
        { 
            return; 
        }
        if (CurrentInput == Vector2.zero)
        {
            return;
        }

        StepTimer -= Time.deltaTime;

        if (StepTimer <= 0)
        {
            float rng = Random.Range(0.5f, 1.5f);

            //SFXSTEPPlayer.PlayOneShot(FootStep);
            //SFXSTEPPlayer.pitch = rng;

            StepTimer = 0.35f;
        }
    }

    private void ApplyFinalMovements()
    {
        if (!characterController.isGrounded)
        {
            MovementDirection.y -= Gravity * Time.deltaTime;
        }

        characterController.Move(MovementDirection * Time.deltaTime);
    }



    //------------------------
    // UI STUFF
    //------------------------



    public void PLAYBUTTON()
    {
        //close Menu
        Time.timeScale = 1f;
        MainMenu.SetActive(false);
        CanMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //MenuMusicSFX.Pause();
        FMODUnity.RuntimeManager.PlayOneShot("event:/Arcade/UI/Menu Select");
        //FMODUnity.RuntimeManager.GetVCA("Arcade");
        //VCA = FMODUnity.RuntimeManager.GetVCA("Arcade");
        
        vcaVolume = 0f;
        volume = Mathf.Pow(10.0f, vcaVolume / 20f);
        vca.setVolume(volume);
        


    }

    public void RESUMEBUTTON()
    {
        //Unpause
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameIsPause = false;

        CanMove = true ;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Arcade/UI/Menu Resume");
        vcaVolume = 0f;
        volume = Mathf.Pow(10.0f, vcaVolume / 20f);
        vca.setVolume(volume);

    }

    public void PAUSEGame()
    {
        //Unpause
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameIsPause = true;

        CanMove = false;

        FMODUnity.RuntimeManager.PlayOneShot("event:/Arcade/UI/Menu Pause");
        vcaVolume = -80f;
        volume = Mathf.Pow(10.0f, vcaVolume / 20f);
        vca.setVolume(volume);
    }

    public void QUITBUTTON()
    {
        Time.timeScale = 1f;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Arcade/UI/Menu Select");

        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        UnityEngine.Debug.Log("Quit function Test");

        PlayerPrefs.SetInt("GAMEWASLAUNCHED", 0);
        PlayerPrefs.SetInt("GameMachineID", 0);
        


        PlayerPrefs.Save();
    }

}
