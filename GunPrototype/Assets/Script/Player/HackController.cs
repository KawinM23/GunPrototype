using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackController : MonoBehaviour {
    private Camera mc;
    private TimeManager tm;
    private CanvasGroup hackGroup;
    private HackInterface hi;
    private HackTimer ht;
    [SerializeField] private GameObject markPrefab;

    public bool isHacking = false;

    private List<GameObject> hackableList;

    private static KeyCode[] randomList = new KeyCode[4] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
    private KeyCode[] hackList;
    private int hackPos;
    private int hackTime;
    private float hackDuration;
    private float hackTimePass;

    Vector2 mousePosition;
    GameObject hackTarget;
    private int hackDis = 100;
    public LayerMask groundLayer;

    public float hackCooldown;
    private float cooldownTracker;

    public Color UnselectedColor;
    public Color SelectedColor;

    public float fadeOutDuration;

    private Enemy targetEnemy;
    private Door targetDoor;

    [Header("Sounds")]
    [SerializeField] AudioSource hackSource;
    [SerializeField] AudioClip glassShattered;
    [SerializeField] AudioClip upSound;
    [SerializeField] AudioClip downSound;
    [SerializeField] AudioClip leftSound;
    [SerializeField] AudioClip rightSound;
    private float hackSoundScale = 0.7f;


    // Start is called before the first frame update
    private void Start() {
        hackableList = new List<GameObject>();

        mc = GameObject.Find("Main Camera").GetComponent<Camera>();
        tm = GameObject.Find("Manager").GetComponent<TimeManager>();
        hackGroup = GameObject.Find("HackGroup").GetComponent<CanvasGroup>();
        hi = GameObject.Find("HackInterface").GetComponent<HackInterface>();
        ht = GameObject.Find("HackTimer").GetComponent<HackTimer>();

        hi.HideInterface();
        ht.HideTimer();

        hackTarget = null;
        isHacking = false;
        hackPos = 0;
        hackTime = 0;
        hackDuration = 0;
        cooldownTracker = 0;
    }

    // Update is called once per frame
    private void Update() {
        if (!TimeManager.isPause) {
            hackSource.UnPause();
            if(isHacking){
                hackTimePass += Time.deltaTime;
                if (hackPos < hackList.Length) {
                    PressHackCheck();
                    if (hackPos == hackList.Length) {
                        HackSuccess();
                    }
                }
                if (hackTimePass > hackDuration) {
                    Debug.Log("EndTime");
                    HackFailed();
                }
            }
        }else{
            hackSource.Pause();
        }
        if (cooldownTracker > 0) {
            cooldownTracker -= Time.deltaTime;
        } else if (cooldownTracker < 0) {
            cooldownTracker = 0;
        }
        
        PressHack();
    }

    private void PressHack() {
        if(!PlayerController.Instance.die){
            if(hackableList.Count != 0){
                mousePosition = mc.ScreenToWorldPoint(Input.mousePosition);
                foreach (GameObject go in hackableList) {
                    if(hackTarget==null && InScreen(go)){
                        hackTarget = go;
                        hackTarget.transform.Find("HackMarkPrefab(Clone)").GetComponent<SpriteRenderer>().color = SelectedColor;
                    } else if (hackTarget!=null && go!=null && hackTarget!=go && (Vector2.Distance(mousePosition, go.transform.position) < Vector2.Distance(mousePosition, hackTarget.transform.position)) && InScreen(go)) {
                        if(hackTarget.transform.Find("HackMarkPrefab(Clone)")!=null){
                           hackTarget.transform.Find("HackMarkPrefab(Clone)").GetComponent<SpriteRenderer>().color = UnselectedColor; 
                        }
                        hackTarget = go;
                        hackTarget.transform.Find("HackMarkPrefab(Clone)").GetComponent<SpriteRenderer>().color = SelectedColor;
                    }
                }
            } else {
                hackTarget = null;
            }

            if (hackTarget != null && cooldownTracker == 0 && Input.GetKeyDown(KeyCode.Mouse1) && SeeHackTarget()) {
                if (hackTarget.GetComponent<Enemy>() != null) {
                    hackTarget.GetComponent<Enemy>().StartHack();
                } else if (hackTarget.GetComponent<Door>() != null) {
                    hackTarget.GetComponent<Door>().StartHack();
                }
            }
        }
    }

    private bool InScreen(GameObject go) {
        Vector3 screenPoint = mc.WorldToViewportPoint(go.transform.position);
        return screenPoint.z > -10 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    private bool SeeHackTarget() {
        Vector2 endPoint = hackTarget.transform.position + (hackTarget.transform.position - gameObject.transform.position).normalized * hackDis;
        RaycastHit2D hit = Physics2D.Linecast(gameObject.transform.position, endPoint, groundLayer);
        Debug.DrawRay(gameObject.transform.position, (gameObject.transform.position - hackTarget.transform.position).normalized * hackDis, Color.blue);

        if (hit.collider != null && hit.collider.gameObject == hackTarget) {
            Debug.DrawRay(gameObject.transform.position, gameObject.transform.position - hackTarget.transform.position, Color.green);
            return true;
        }
        return false;
    }

    public void StartEnemyHack(Enemy enemy, int size, float duration) {
        if (!isHacking) {
            isHacking = true;
            targetEnemy = enemy;
            targetDoor = null;

            hackList = RandomList(size);

            hackDuration = duration * tm.slowdownFactor;
            hackTimePass = 0f;

            hackGroup.alpha = 1;
            hi.ShowHackList(hackList);
            ht.ShowTimer();
            tm.DoSlowmotion();
        }
    }

    public void StartDoorHack(Door door, int size, float duration) {
        if (!isHacking) {
            isHacking = true;
            targetEnemy = null;
            targetDoor = door;

            hackList = RandomList(size);

            hackDuration = duration * tm.slowdownFactor;
            hackTimePass = 0f;

            hackGroup.alpha = 1;
            hi.ShowHackList(hackList);
            ht.ShowTimer();
            tm.DoSlowmotion();
        }
    }

    private void PressHackCheck(){
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)){
            if(Input.GetKeyDown(hackList[hackPos])){
                hi.Press(hackPos);
                PlayPressSound(hackPos);
                hackPos++;
            }else{
                hackTimePass += hackDuration/5f;
            }
        }
    }

    public void HackSuccess() {
        if (isHacking) {
            if (targetEnemy != null) {
                hackSource.PlayOneShot(glassShattered, hackSoundScale);
                targetEnemy.BreakShield();
            } else if (targetDoor != null) {
                targetDoor.OpenDoor();
            }
            EndHack();
        }
    }

    public void HackFailed() {
        if (isHacking) {
            EndHack();
        }
    }

    public void EndHack() {
        if (isHacking) {
            tm.hacking = false;
            isHacking = false;
            hackPos = 0;
            hackTime++;
            cooldownTracker = hackCooldown;
            if (targetEnemy != null) {
                targetEnemy.EndHack();
            } else if (targetDoor != null) {
                targetDoor.EndHack();
            }

            StartCoroutine(HideHackGroup());
        }
    }

    void PlayPressSound(int hackPos) {
        switch (GetKeyNumber(hackPos)) {
            case 1:
                hackSource.PlayOneShot(upSound, hackSoundScale);
                break;
            case 2:
                hackSource.PlayOneShot(leftSound, hackSoundScale);
                break;
            case 3:
                hackSource.PlayOneShot(downSound, hackSoundScale);
                break;
            case 4:
                hackSource.PlayOneShot(rightSound, hackSoundScale);
                break;
            default:
                break;
        }
    }

    private IEnumerator HideHackGroup() {
        while (hackGroup.alpha != 0) {
            hackGroup.alpha -= (1f / fadeOutDuration) * Time.unscaledDeltaTime;
            yield return null;
        }
        hi.HideInterface();
        ht.HideTimer();
    }

    public KeyCode[] RandomList(int size) {
        KeyCode[] rl = new KeyCode[size];
        for (int i = 0; i < size; i++) {
            rl[i] = randomList[Random.Range(0, randomList.Length)];
        }
        return rl;
    }

    public int GetKeyNumber(int index) {
        KeyCode kc = hackList[index];
        if (kc == KeyCode.W) {
            return 1;
        } else if (kc == KeyCode.A) {
            return 2;
        } else if (kc == KeyCode.S) {
            return 3;
        } else if (kc == KeyCode.D) {
            return 4;
        } else {
            return 0;
        }
    }

    public float GetTimePercentage() {
        return (hackDuration - hackTimePass) / hackDuration;
    }

    public float GetCooldownPercentage() {
        return 1 - (cooldownTracker / hackCooldown);
    }

    public IEnumerator AddToHackableList(GameObject go) {
        while (hackableList == null) {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        if (hackableList != null && !hackableList.Contains(go)) {
            GameObject hackMark = Instantiate(markPrefab);
            hackableList.Add(go);
            hackMark.transform.SetParent(go.transform);
            hackMark.transform.localPosition = new Vector3(0, 0, 0);

            yield return null;
        }
    }

    public void RemoveFromHackableList(GameObject go) {
        if (hackableList.Contains(go)) {
            hackableList.Remove(go);
            Destroy(go.transform.Find("HackMarkPrefab(Clone)").gameObject);
            return;
        }
    }

}