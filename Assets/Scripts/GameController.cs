using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public delegate void score();
    public static event score onNewStage, onScoreMultiply;

    public GameObject EndCanvas, PlayerInterface;

    public static bool bEnd;
    public static int userScore, stage, gameSpeedMultiplier;

    private int stageScoreMultiplier;
    private bool bStage, bCityBonus;

    private void Awake()
    {
        Missile.onEnemyMissileDestroy += EnemyMissileDestroy;
        Missile.onEnemyCruiseMissileDestroy += EnemyCruiseMissileDestroy;
    }

    private void Start()
    {
        bEnd = false;
        bStage = false;
        bCityBonus = false;

        userScore = 0;
        stage = 0;

        stageScoreMultiplier = 1;
        gameSpeedMultiplier = 1;

        Debug.Log(stage);
    }

    private void Update()
    {
        EndGame();

        if (!bEnd)
            StartCoroutine("NewStage");
    }

    private void EnemyMissileDestroy()
    {
        userScore += 25 * stageScoreMultiplier;
        onScoreMultiply?.Invoke();
    }

    private void EnemyCruiseMissileDestroy()
    {
        userScore += 125 * stageScoreMultiplier;
        onScoreMultiply?.Invoke();
    }

    private void BonusForCities()
    {
        userScore += Cities.valCities * 100 * stageScoreMultiplier;
        onScoreMultiply?.Invoke();
    }

    private void StageScoreMultiplier()
    {
        if (stage == 1 || stage == 2)
            stageScoreMultiplier = 1;
        else if (stage == 3 || stage == 4)
            stageScoreMultiplier = 2;
        else if (stage == 5 || stage == 6)
            stageScoreMultiplier = 3;
        else if (stage == 7 || stage == 8)
            stageScoreMultiplier = 4;
        else if (stage == 9 || stage == 10)
            stageScoreMultiplier = 5;
        else if (stage >= 11)
            stageScoreMultiplier = 6;
    }

    private void GameSpeedMultiplier()
    {
        gameSpeedMultiplier = 1 / stage;
    }

    private void EndGame()
    {
        if (Cities.valCities <= 0)
            bEnd = true;

        if (bEnd)
        {
            EndCanvas.SetActive(true);
            PlayerInterface.SetActive(false);
        }
        else
        {
            PlayerInterface.SetActive(true);
        }
    }

    private void OnDisable()
    {
        Missile.onEnemyMissileDestroy -= EnemyMissileDestroy;
        Missile.onEnemyCruiseMissileDestroy -= EnemyCruiseMissileDestroy;
    }

    IEnumerator NewStage()
    {
        if (!bStage)
        {
            bStage = true;
            stage += 1;
            onNewStage?.Invoke();
            StageScoreMultiplier();

            if (bCityBonus)
                BonusForCities();
            else
                bCityBonus = true;

            yield return new WaitForSeconds(7);
            bStage = false;
        }
    }
}
