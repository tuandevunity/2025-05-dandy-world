using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
     private Vector3 originPosition;
    [SerializeField] float delayTime = 0.5f;
    private Rigidbody rb;
 
    void Start()
    {
        originPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        AudioManager _audio = AudioManager.Instance;
        _audio.SpawnAndPlay(transform, _audio.ballSound, isDestroy: false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Preconsts.Tag_Ball_Respawn))
        {
            StartCoroutine(BallRespawn(delayTime));
        }
    }

    private void OnCollisionEnter(Collision other) {
        var gameObject = other.gameObject;
        if (other.gameObject.CompareTag(Preconsts.Tag_Player)) {
            var player = gameObject.GetComponent<PlayerMovement>();
            StartCoroutine(player.Die(3f));
            StartCoroutine(BallRespawn(2));
        }
    }

    IEnumerator BallRespawn(float timeDelay)
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(timeDelay);
        rb.constraints = RigidbodyConstraints.None;
        transform.position = originPosition;
    }

}
