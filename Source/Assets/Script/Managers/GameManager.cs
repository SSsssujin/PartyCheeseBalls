using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject gameStartText;
    private GameObject player;

    //private CameraMove cameraMove;
    private GameObject resultCamera;

    private GameObject endingUI;
    private GameObject gameover;

    GetPurpleItem getPurpleItem;
    GetOrangeItem getOrangeItem;
    GetWhiteItem getWhiteItem;

    private float timer = 0;
    private float clearTimer = 0;

    //ActiveIndicator indicator;
    //FadeScene fadeScene;
    [HideInInspector] public bool isDestination = false;

    GameObject hintUI;
    GameObject shoppingList;
    GameObject buttons;
    Text levelText;

    [HideInInspector] public bool isClear = false;
    //Text clearTime;
    //
    //GameObject sunlight;
    //Light light;

    string sceneName; // = SceneManager.GetActiveScene().name;

    // Game Over
    private bool isAllComplete;

    PlayerMovement playerMovement;

    private void Start()
    {
        getPurpleItem = FindObjectOfType<GetPurpleItem>();
        getOrangeItem = FindObjectOfType<GetOrangeItem>();
        getWhiteItem = FindObjectOfType<GetWhiteItem>();

        //indicator = FindObjectOfType<ActiveIndicator>();
        //fadeScene = FindObjectOfType<FadeScene>();
        
        //clearTime = endingUI.GetComponentInChildren<Text>();
        
        //sunlight = GameObject.Find("Directional Light");
        //light = GameObject.Find("Spot Light").GetComponent<Light>();
        
        hintUI = GameObject.Find("Hint UI");
        shoppingList = GameObject.Find("ListUI");
        buttons = GameObject.Find("Buttons");
        
        playerMovement = FindObjectOfType<PlayerMovement>();
        gameStartText = GameObject.Find("GameStart");
        player = GameObject.Find("Players");

        endingUI = GameObject.Find("Ending UI");
        endingUI.SetActive(false);

        resultCamera = GameObject.Find("Ending Camera");
        resultCamera.SetActive(false);

        gameover = GameObject.Find("GameOver");
        gameover.SetActive(false);

        sceneName = SceneManager.GetActiveScene().name;

        levelText = GameObject.Find("Level Text").GetComponent<Text>();
        levelText.text = sceneName;
    }

    private void Update()
    {
        // 엔딩 씬 타이머
        clearTimer += Time.deltaTime;

        CameraMove cameraMove = FindObjectOfType<CameraMove>(); ;
        // 탑뷰 Game Start 텍스트
        if (cameraMove.isStarted)
        {
            gameStartText.SetActive(false);
        }

        ReadyForEnd(); 
    }

    private void ReadyForEnd()
    {
        //if (getPurpleItem.isComplete) Debug.Log("Purple");
        //if (getOrangeItem.isComplete) Debug.Log("Orange");
        //if (getWhiteItem.isComplete) Debug.Log("milk");

        if (getPurpleItem.isComplete && getOrangeItem.isComplete && getWhiteItem.isComplete) isAllComplete = true;
        else isAllComplete = false;
        
        //PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();

        if (isAllComplete && playerMovement.moveNum >= 0)
        {
            EndGame();
        }
        else if (playerMovement.moveNum <= 0 && !isAllComplete)
        {
            // 버튼 먼저 없애기
            if (buttons != null)
                buttons.SetActive(false);
            else return;

            Gameover();
        }
        else return;
    }

    public void EndGame()
    {
        ActiveIndicator indicator = FindObjectOfType<ActiveIndicator>();
        indicator.Active();

        //PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();

        if (playerMovement.agent.destination.x == playerMovement.wayArr[0, 3].position.x && 
            playerMovement.agent.destination.z == playerMovement.wayArr[0, 3].position.z)
        {
            isDestination = true;

            // 버튼 먼저 없애기
            if (buttons != null)
                buttons.SetActive(false);
            else return;
        }

        if (isDestination && playerMovement.moveNum >= 0)
        {
            if (Vector3.Distance(player.transform.position, playerMovement.wayArr[0, 3].position) < 1f)
                 StageClear();
        }
        else if (playerMovement.moveNum == 0)
        {
            // 버튼 먼저 없애기
            if (buttons != null)
                buttons.SetActive(false);
            else return;

            Gameover();
        }

        //Debug.Log(playerMovement.wayArr[0, 3].position);
    }

    private void StageClear()
    {
        timer += Time.deltaTime;
        //Debug.Log(timer);

        isClear = true;

        // 내비 끄기
        ActiveIndicator indicator = FindObjectOfType<ActiveIndicator>();
        indicator.Inactive();

        // 씬 페이드인아웃 -> Ending 카메라 켜져있지 않을때만
        if (!resultCamera.activeInHierarchy)
        {
            FadeScene fadeScene = FindObjectOfType<FadeScene>();
            fadeScene.StartFade();
        }

        // 카메라 변경
        if (timer > 2)
        {
            // 카메라 변경
            resultCamera.SetActive(true);

            // 결과화면 UI 켜기
            endingUI.SetActive(true);

            if (endingUI.activeInHierarchy)
            {
                // BGM Off
                GameObject.Find("Main Camera").GetComponent<AudioSource>().enabled = false;
            }

            // 햇빛끄기 
            GameObject sunlight = GameObject.Find("Directional Light");
            Light light = GameObject.Find("Spot Light").GetComponent<Light>();

            if (sunlight != null)
                sunlight.SetActive(false);
            else return;

            // 스팟라잇 키기 
            light.enabled = true;

            // 인게임 UI 끄기
            TurnOffUI();

            //// UI - 버튼 끄기
            //GameObject buttons = GameObject.Find("Buttons");
            //if (buttons != null)
            //    buttons.SetActive(false);
            //else return;
            //
            //// UI - 쇼핑리스트 끄기
            //GameObject shoppingList = GameObject.Find("ListUI");
            //if (shoppingList != null)
            //    shoppingList.SetActive(false);
            //else return;
            //
            //// UI - 힌트 끄기
            //GameObject hintUI = GameObject.Find("Hint UI");
            //if (hintUI != null)
            // hintUI.SetActive(false);
            //else return;

            // 인게임 UI 끄기
            //GameObject inGameUI = GameObject.Find("InGame UI");
            //if (inGameUI != null)
            //    inGameUI.SetActive(false);
            //else return;

            // 타이머 설정
            Text clearTime = endingUI.GetComponentInChildren<Text>();
            clearTime.text = $"{(int)clearTimer} sec"; //.ToString();
        }
    }

    public void Gameover()
    {
        timer += Time.deltaTime;

        //PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        for (int i = 0; i < playerMovement.animator.Length; i++)
        {
            playerMovement.animator[i].SetTrigger("Dead");
        }

        if (timer > 1.5f)
        {


            gameover.SetActive(true);

            TurnOffUI();
        }
    }

    public void RestartGame()
    {
        //string sceneName = SceneManager.GetActiveScene().name;

        // 현재 씬 로드
        SceneManager.LoadScene($"{sceneName}");
    }

    private void TurnOffUI()
    {
        // UI - 버튼 끄기
        if (buttons != null)
            buttons.SetActive(false);
        else return;

        // UI - 쇼핑리스트 끄기
        if (shoppingList != null)
        {
            Image[] shoppingListImg = shoppingList.GetComponentsInChildren<Image>();
            Text[] shoppingListTxt = shoppingList.GetComponentsInChildren<Text>();

            for (int i = 0; i < shoppingListImg.Length; i++)
            {
                shoppingListImg[i].enabled = false;
            }
            for (int i = 0; i < shoppingListTxt.Length; i++)
            {
                shoppingListTxt[i].enabled = false;
            }
        }
        else return;

        // UI - 힌트 끄기
        if (hintUI != null)
            hintUI.SetActive(false);
        else return;

        // 양배추 효과 끄기
        if (getOrangeItem.cabbageAttack != null)
            getOrangeItem.cabbageAttack.SetActive(false);
    }
    
}
