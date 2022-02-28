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
        // ���� �� Ÿ�̸�
        clearTimer += Time.deltaTime;

        CameraMove cameraMove = FindObjectOfType<CameraMove>(); ;
        // ž�� Game Start �ؽ�Ʈ
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
            // ��ư ���� ���ֱ�
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

            // ��ư ���� ���ֱ�
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
            // ��ư ���� ���ֱ�
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

        // ���� ����
        ActiveIndicator indicator = FindObjectOfType<ActiveIndicator>();
        indicator.Inactive();

        // �� ���̵��ξƿ� -> Ending ī�޶� �������� ��������
        if (!resultCamera.activeInHierarchy)
        {
            FadeScene fadeScene = FindObjectOfType<FadeScene>();
            fadeScene.StartFade();
        }

        // ī�޶� ����
        if (timer > 2)
        {
            // ī�޶� ����
            resultCamera.SetActive(true);

            // ���ȭ�� UI �ѱ�
            endingUI.SetActive(true);

            if (endingUI.activeInHierarchy)
            {
                // BGM Off
                GameObject.Find("Main Camera").GetComponent<AudioSource>().enabled = false;
            }

            // �޺����� 
            GameObject sunlight = GameObject.Find("Directional Light");
            Light light = GameObject.Find("Spot Light").GetComponent<Light>();

            if (sunlight != null)
                sunlight.SetActive(false);
            else return;

            // ���̶��� Ű�� 
            light.enabled = true;

            // �ΰ��� UI ����
            TurnOffUI();

            //// UI - ��ư ����
            //GameObject buttons = GameObject.Find("Buttons");
            //if (buttons != null)
            //    buttons.SetActive(false);
            //else return;
            //
            //// UI - ���θ���Ʈ ����
            //GameObject shoppingList = GameObject.Find("ListUI");
            //if (shoppingList != null)
            //    shoppingList.SetActive(false);
            //else return;
            //
            //// UI - ��Ʈ ����
            //GameObject hintUI = GameObject.Find("Hint UI");
            //if (hintUI != null)
            // hintUI.SetActive(false);
            //else return;

            // �ΰ��� UI ����
            //GameObject inGameUI = GameObject.Find("InGame UI");
            //if (inGameUI != null)
            //    inGameUI.SetActive(false);
            //else return;

            // Ÿ�̸� ����
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

        // ���� �� �ε�
        SceneManager.LoadScene($"{sceneName}");
    }

    private void TurnOffUI()
    {
        // UI - ��ư ����
        if (buttons != null)
            buttons.SetActive(false);
        else return;

        // UI - ���θ���Ʈ ����
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

        // UI - ��Ʈ ����
        if (hintUI != null)
            hintUI.SetActive(false);
        else return;

        // ����� ȿ�� ����
        if (getOrangeItem.cabbageAttack != null)
            getOrangeItem.cabbageAttack.SetActive(false);
    }
    
}
