using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public float distance = 35f;
    [SerializeField] int damage = 25;
    [SerializeField] GameObject door;
    [SerializeField] GameObject gunMesh;
    Vector3 _positionOrigin;
    bool attachedPlayer;
    bool stopGun;

    Coroutine shootingCoroutine;
    // Update is called once per framedamage
    void Start()
    {
        _positionOrigin = transform.position;
        Debug.Log("gun");
        Debug.Log(_positionOrigin);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Preconsts.Tag_Player)) {
            if (attachedPlayer) return;
            StartGun();
            var player = other.gameObject.GetComponent<PlayerMovement>();
            SetPosGun(player.posGun.transform);
            Enemy enemy = FindNearEnemy();
            if (enemy == null) {
                Debug.Log("khong tim thay quai vat");
                return;
            }
            enemy.StoreGun(this);
            shootingCoroutine = StartCoroutine(CallShootMultipleTimes(
                () => {
                    enemy.TakeDamage(damage);
                }
            ));
            
        }
    }

    private void SetPosGun(Transform targetPos) {
        transform.SetParent(targetPos);
        transform.localPosition = new Vector3(0.28f,-0.067f,0.052f);
        transform.localRotation = Quaternion.Euler(3.249f,250.416f,177.712f);
    }

    private Enemy FindNearEnemy()
    {
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in allTargets)
        {
            float dist = Vector3.Distance(transform.position, obj.transform.position);
            Debug.Log(obj.name);
            Debug.Log("dist" + dist);
            if (dist <= distance)
            {
                return obj.GetComponent<Enemy>();
            }
        }
        return null;
    }

    
    IEnumerator CallShootMultipleTimes(Action shootCallback)
    {
        
        while (!stopGun)
        {
            yield return new WaitForSeconds(2f); 
            shootCallback?.Invoke();
        }
    }

    public void StartGun() {
        AudioManager.Instance.SpawnAndPlay(transform, AudioManager.Instance.reloadBullet);
        gunMesh.SetActive(true);
        stopGun = false;
        attachedPlayer = true;
    }

    public void ResetGun() {
        StopCoroutine(shootingCoroutine);
        gunMesh.SetActive(false);
        stopGun = true;
        attachedPlayer = false;
        transform.parent = null;
        transform.position = _positionOrigin;
    }

    public void CompleteGun() {
        ResetGun();
        door.SetActive(false);
    }

    
}
