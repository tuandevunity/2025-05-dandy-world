using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int ID;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DataManager.GamePlayUtility.Profile.OnChangedCheckPointInMap(ID, () => {
                Debug.Log("Checkpoint updated!");
                var _audio = AudioManager.Instance;
                _audio.SpawnAndPlay(transform, _audio.checkpointSound, 1.2f);
                UIManager.ShowPanel<PanelInforCheckpoint>(1f);
                // âm thanh eff..
                //DataManager.GamePlayUtility.Save(); // Lưu luôn sau khi chạm
            });
        }
    }
}
