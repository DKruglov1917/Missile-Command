using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    public TMPro.TextMeshProUGUI userScoreTxt, ammoTxt, stageTxt;
    public Animator scoreAnimator, stageAnimator, cameraAnimator;

    private int intBackgroundColor;

    private void Start()
    {
        GameController.onNewStage += StageShake;
        GameController.onScoreMultiply += ScoreShake;
        Missile.onCityDestroy += CameraShake;
    }

    private void FixedUpdate()
    {
        userScoreTxt.text = GameController.userScore.ToString();
        ammoTxt.text = Ammunition.curAmmo.ToString();
        stageTxt.text = "Stage: " + GameController.stage.ToString();
    }

    private void ScoreShake()
    {
        scoreAnimator.SetTrigger("shake");
    }

    private void StageShake()
    {
        stageAnimator.SetTrigger("shake");
    }

    private void CameraShake()
    {
        cameraAnimator.SetTrigger("shake");
    }

    private void OnDisable()
    {
        GameController.onNewStage -= StageShake;
        GameController.onScoreMultiply -= ScoreShake;
        Missile.onCityDestroy -= CameraShake;
    }
}
