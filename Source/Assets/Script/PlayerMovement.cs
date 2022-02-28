using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // ��ư ã��
    public Button[] buttons;
    private float buttonTimer = 0f;
    private AudioSource buttonSound;

    // �÷��̾� ��ġ �� ������ ����
    private Transform player;
    public  Transform[,] wayArr = new Transform[4, 4];
    private int index1, index2;
    private bool isMoving = false;

    [HideInInspector] public bool           isExit;
    [HideInInspector] public NavMeshAgent   agent;
    [HideInInspector] public Animator[]     animator;

    public LayerMask layerMask;

    // �̵�Ƚ�� ����
    public int moveNum = 20;
    private Text moveNumText;

    //private GameManager gameManager;
    //private bool isGameOver;

    public enum Direction
    {
        UP,
        RIGHT,
        LEFT,
        DOWN,
    }

    [HideInInspector]
    public Direction currDirection = Direction.UP;

    private void Start()
    {
        buttonSound = GetComponent<AudioSource>();
        animator = GetComponentsInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Players").transform;
        moveNumText = GameObject.Find("MoveNum Text").GetComponent<Text>();
        //gameManager = FindObjectOfType<GameManager>();

        #region WayPoint �迭
        wayArr[0, 0] = GameObject.Find("1").transform;
        wayArr[0, 1] = GameObject.Find("2").transform;
        wayArr[0, 2] = GameObject.Find("3").transform;
        wayArr[0, 3] = GameObject.Find("4").transform;

        wayArr[1, 0] = GameObject.Find("5").transform;
        wayArr[1, 1] = GameObject.Find("6").transform;
        wayArr[1, 2] = GameObject.Find("7").transform;
        wayArr[1, 3] = GameObject.Find("8").transform;

        wayArr[2, 0] = GameObject.Find("9").transform;
        wayArr[2, 1] = GameObject.Find("10").transform;
        wayArr[2, 2] = GameObject.Find("11").transform;
        wayArr[2, 3] = GameObject.Find("12").transform;

        wayArr[3, 0] = GameObject.Find("13").transform;
        wayArr[3, 1] = GameObject.Find("14").transform;
        wayArr[3, 2] = GameObject.Find("15").transform;
        wayArr[3, 3] = GameObject.Find("16").transform;
        #endregion

        //transform.position = wayArr[3, 1].position; // + new Vector3 (0f, 10f, 0f);
        player.position = wayArr[3, 1].position; // + new Vector3 (0f, 10f, 0f);
        index1 = 3;
        index2 = 1;

        //Debug.Log(wayArr[3, 1].position);
    }

    private void Update()
    {
        // move Num ���� ����
        if (moveNum < 0) moveNum = 0;

        // �̵�Ƚ�� �������ֱ�
        moveNumText.text = moveNum.ToString();

        // �ⱸ(ī����)�� �ִ��� �Ǵ�
        if (Vector3.Distance(player.position, wayArr[0, 3].position) < 1f) isExit = true;
        else isExit = false;

        // �÷��̾� ������ �Ǵ�
        if (agent.velocity != Vector3.zero)
        {
            isMoving = true;
        }
        if (agent.velocity == Vector3.zero)
        { 
            isMoving = false;
        }

        // �����̰� ������ �ִϸ��̼� ���
        if (isMoving)
        {
            for (int i = 0; i < animator.Length; i++)
            {
                animator[i].SetBool("IsWalking", true);
            }
        }
        if (!isMoving)
        {
            for (int i = 0; i < animator.Length; i++)
            {
                animator[i].SetBool("IsWalking", false);
            }
        }

        // animator ����
        // 0 �ϴû�
        // 1 ����
        // 2 ������

        // ���� ��ư ����
        DirectionKeyManager();

        // ��ֹ� �浹
        ObstacleRay();

        // �÷��̾� �ڵ� �̵�
        //PlayerAutoMove();
    }



    public void UpButton()
    {
        if (Time.time - buttonTimer >= 0.3f)
        {
            //buttonSound.Play();

            moveNum -= 1;
            //timer = 0;

            switch (currDirection)
            {
                case Direction.UP:
                    index1 -= 1;
                    currDirection = Direction.UP;
                    break;

                case Direction.LEFT:
                    index2 -= 1;
                    currDirection = Direction.LEFT;
                    break;

                case Direction.RIGHT:
                    index2 += 1;
                    currDirection = Direction.RIGHT;
                    break;

                case Direction.DOWN:
                    index1 += 1;
                    currDirection = Direction.DOWN;
                    break;
            }
            agent.SetDestination(wayArr[index1, index2].position);
            //Debug.Log($"index1 {index1}, Index2 {index2}");

            buttonTimer = Time.time;
        }
    }

    public void LeftButton()
    {
        if (Time.time - buttonTimer >= 0.3f)
        {
            //buttonSound.Play();

            moveNum -= 1;
            //timer = 0;

            switch (currDirection)
            {
                case Direction.UP:
                    index2 -= 1;
                    currDirection = Direction.LEFT;
                    break;

                case Direction.LEFT:
                    index1 += 1;
                    currDirection = Direction.DOWN;
                    break;

                case Direction.RIGHT:
                    index1 -= 1;
                    currDirection = Direction.UP;
                    break;

                case Direction.DOWN:
                    index2 += 1;
                    currDirection = Direction.RIGHT;
                    break;
            }
            agent.SetDestination(wayArr[index1, index2].position);
            //Debug.Log($"index1 {index1}, Index2 {index2}");

            buttonTimer = Time.time;
        }
    }

    public void RightButton()
    {
        if (Time.time - buttonTimer >= 0.3f)
        {
            //buttonSound.Play();

            moveNum -= 1;
            //timer = 0;

            switch (currDirection)
            {
                case Direction.UP:
                    index2 += 1;
                    currDirection = Direction.RIGHT;
                    break;
                case Direction.LEFT:
                    index1 -= 1;
                    currDirection = Direction.UP;
                    break;
                case Direction.RIGHT:
                    index1 += 1;
                    currDirection = Direction.DOWN;
                    break;
                case Direction.DOWN:
                    index2 -= 1;
                    currDirection = Direction.LEFT;
                    break;
            }
            agent.SetDestination(wayArr[index1, index2].position);
            //Debug.Log($"index1 {index1}, Index2 {index2}");

            buttonTimer = Time.time;
        }
    }

    private void DirectionKeyManager()
    {
        // ��ư Ȱ��ȭ
        if (isMoving)
        {
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(false);
            }
        }
        if (!isMoving)
        {
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(true);
            }
        }

        // ��ư0 ����
        // ��ư1 ����
        // ��ư2 ������
        if (currDirection == Direction.UP)
        {
            if (index1 == 0) buttons[1].gameObject.SetActive(false);
            if (index2 == 0) buttons[0].gameObject.SetActive(false);
            if (index2 == 3) buttons[2].gameObject.SetActive(false);
        }
        if (currDirection == Direction.LEFT)
        {
            if (index1 == 0) buttons[2].gameObject.SetActive(false);
            if (index2 == 0) buttons[1].gameObject.SetActive(false);
            if (index1 == 3) buttons[0].gameObject.SetActive(false);
        }
        if(currDirection == Direction.RIGHT)
        {
            if (index1 == 0) buttons[0].gameObject.SetActive(false);
            if (index1 == 3) buttons[2].gameObject.SetActive(false);
            if (index2 == 3) buttons[1].gameObject.SetActive(false);
        }
        if (currDirection == Direction.DOWN)
        {
            if (index2 == 0) buttons[2].gameObject.SetActive(false);
            if (index1 == 3) buttons[1].gameObject.SetActive(false);
            if (index2 == 3) buttons[0].gameObject.SetActive(false);
        }
    }


    // ��ư0 ����
    // ��ư1 ����
    // ��ư2 ������
    private void ObstacleRay()
    {
        RaycastHit hitInfo;

        // 1. ������ �˻�
        if (Physics.Raycast(transform.position, transform.right, out hitInfo, 15f, layerMask))
        {
            //Debug.Log(hitInfo.collider.name);
            buttons[2].gameObject.SetActive(false);
        }
        // 2. ���� �˻�
        if (Physics.Raycast(transform.position, -transform.right, out hitInfo, 15f, layerMask))
        {
            buttons[0].gameObject.SetActive(false);
        }
        // 3. ���� �˻�
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 15f, layerMask))
        {
            buttons[1].gameObject.SetActive(false);
        }

        
    }

    #region �ڵ��̵� �ߴ���
    /*
    private void PlayerAutoMove()
    {
        if (timer > 2.5f)
        {
            if (currDirection == Direction.UP)
            {
                if (index1 == 0)
                {
                    index2 -= 1; 
                    currDirection = Direction.RIGHT;
                }
                else
                {
                    index1 -= 1;
                    currDirection = Direction.UP;
                }

                timer = 0;
            }

            if (currDirection == Direction.RIGHT)
            {
                if (index2 == 3)
                {
                    index1 += 1;
                    currDirection = Direction.DOWN;
                }
                else
                {
                    index2 += 1;
                    currDirection = Direction.RIGHT;
                }

                timer = 0;
            }

            if (currDirection == Direction.LEFT)
            {
                if (index2 == 0)
                {
                    index1 -= 1;
                    currDirection = Direction.UP;
                }
                else
                {
                    index2 -= 1;
                    currDirection = Direction.LEFT;
                }

                timer = 0;
            }

            if (currDirection == Direction.DOWN)
            {
                if (index1 == 3)
                {
                    index2 -= 1;
                    currDirection = Direction.LEFT;
                }
                else 
                {
                    index1 += 1;
                    currDirection = Direction.DOWN;
                }

                timer = 0;
            }
            agent.SetDestination(wayArr[index1, index2].position);
            //timer = 0;
        }
    }
    */
    #endregion
}
