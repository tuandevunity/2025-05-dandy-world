using UnityEngine;

public class CamPlayer : TickBehaviour
{
    [SerializeField] float sensitivityCam = 15f;
    [SerializeField] Transform camTarget;
    [SerializeField] Vector2 clampCam = new Vector2(-80f, 80f);
    Vector2 tempCamTarget;
    Vector3 velocity;
    FixedTouchField fixedTouchField;
    bool stopRotCam;
    private Vector3 currentRotation;

    protected override void Awake()
    {
        base.Awake();
        fixedTouchField = UIManager.Panel<PanelGamePlay>().FixedTouchField;
    }
    public override void OnLateUpdate()
    {
        base.OnLateUpdate();
        PlayerRotation();
    }
    void PlayerRotation()
    {
        float dSensi = DataManager.SettingsUtility.Profile.Sensivity;
        tempCamTarget += new Vector2(-fixedTouchField.TouchDist.y, fixedTouchField.TouchDist.x) * Time.deltaTime * sensitivityCam * dSensi;
        tempCamTarget.x = Mathf.Clamp(tempCamTarget.x, clampCam.x, clampCam.y);
        var targetEulerAngles = new Vector2(tempCamTarget.x, tempCamTarget.y);
        camTarget.rotation = Quaternion.Euler(targetEulerAngles.x, targetEulerAngles.y, 0);
    }
    public void SetRotCamFirstGame(float rotateCam)
    {
        camTarget.gameObject.SetActive(true);
        SetRotCamPlayer(rotateCam);
    }
    public void SetRotCamPlayer(float rotateCam)
    {
        tempCamTarget.x = 20;
        tempCamTarget.y = rotateCam;
        camTarget.rotation = Quaternion.Euler(20, rotateCam, 0);
    }
    public Transform GetCamTarget()
    {
        return camTarget;
    }
}
