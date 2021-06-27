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

    public float hackCooldown;
    private float cooldownTracker;

    public float fadeOutDuration;

    private Enemy targetEnemy;
    private Door targetDoor;

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

        isHacking = false;
        hackPos = 0;
        hackTime = 0;
        hackDuration = 0;
        cooldownTracker = 0;
    }

    // Update is called once per frame
    private void Update() {
        if (!TimeManager.isPause && isHacking) {
            hackTimePass += Time.deltaTime;
            if (hackPos < hackList.Length && Input.GetKeyDown(hackList[hackPos])) {
                hi.Press(hackPos);
                hackPos++;
                if (hackPos == hackList.Length) {
                    HackSuccess();
                }
            }
            if (hackTimePass > hackDuration) {
                Debug.Log("EndTime");
                HackFailed();
            }
        }
        if (cooldownTracker > 0) {
            cooldownTracker -= Time.deltaTime;
        } else if (cooldownTracker < 0) {
            cooldownTracker = 0;
        }
        PressHack();
    }

    private void PressHack() {
        if (Input.GetKeyDown(KeyCode.Mouse1) && hackableList.Count != 0 && cooldownTracker == 0) {
            Vector2 mousePosition = mc.ScreenToWorldPoint(Input.mousePosition);
            GameObject nearestObject = null;
            foreach (GameObject go in hackableList) {
                if ((nearestObject == null || Vector2.Distance(mousePosition, go.transform.position) < Vector2.Distance(mousePosition, nearestObject.transform.position)) && InScreen(go)) {
                    nearestObject = go;
                }
            }
            if (nearestObject != null) {
                if (nearestObject.GetComponent<Enemy>() != null) {
                    nearestObject.GetComponent<Enemy>().StartHack();
                } else if (nearestObject.GetComponent<Door>() != null) {
                    nearestObject.GetComponent<Door>().StartHack();
                }
            }
        }
    }

    private bool InScreen(GameObject go) {
        Vector3 screenPoint = mc.WorldToViewportPoint(go.transform.position);
        return screenPoint.z > -10 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
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

    public void HackSuccess() {
        if (isHacking) {
            if (targetEnemy != null) {
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

    public bool PressOtherKeys(KeyCode kc) {
        return false;
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