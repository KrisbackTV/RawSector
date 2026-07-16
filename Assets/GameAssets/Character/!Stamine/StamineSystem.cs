using System;
using UnityEngine;
using UnityEngine.UI;

public class StamineSystem : MonoBehaviour, IStaminable, IStaminaRegenerator
{
    private EnemyConfigSO enemyData;

    private float currentStamine = 100f;
    private bool canRegenerate = false;


    [Header("Глобальные события (для Игрока/Босса")]
    public FloatEvent OnMaxStamine;
    public FloatEvent OnCurrentStamine;

    //Свойства
    public event Action<float, float> OnStamineChangeLocal;

    public float CurrentStamine
    {
        get; private set;
        ////get
        ////{
        ////    if (enemyData == null)
        ////    {
        ////        Debug.Log("enemyData == null");
        ////        return currentStamine;
        ////    }

        ////    //if (enemyData is PlayerDataSO)
        ////    //{
        ////    //    return enemyData.currentStamine;
        ////    //}
        ////    //else
        ////    //{
        ////    //    return currentStamine;
        ////    //}
        ////}
        //set
        //{
        //    if (enemyData == null)
        //    {
        //        currentStamine = value;
        //        return;
        //    }

        //    //if (enemyData is PlayerDataSO) enemyData.currentStamine = value;
        //    //else currentStamine = value;
        //}
    }

    public void Initialize(EnemyConfigSO enemyDataSO)
    {
        if (enemyDataSO == null) return;

        enemyData = enemyDataSO;

        if (enemyData is PlayerConfigSo)
        {
            currentStamine = enemyData.maxStamine;
            Debug.Log("Данные взяты из SO");
        }

        canRegenerate = true;
    }

    private void Update()
    {
        if(enemyData == null) return;

        if (canRegenerate && CurrentStamine < enemyData.maxStamine)
        {
            HealStamine(enemyData.regenRateStamine * Time.deltaTime);
        }
    }


    public void TakeStamine(float damage)
    {
        CurrentStamine = Mathf.Max(0, CurrentStamine - damage);

        NotifiChanges();
    }

    public void HealStamine(float amount)
    {
        CurrentStamine = Mathf.Min(enemyData.maxStamine, CurrentStamine + amount);

        NotifiChanges();
    }


    private void NotifiChanges()
    {
        if (enemyData == null) return;

        OnStamineChangeLocal?.Invoke(CurrentStamine, enemyData.maxStamine);

        if(OnMaxStamine != null) OnMaxStamine.Raise(enemyData.maxStamine);
        if (OnCurrentStamine != null) OnCurrentStamine.Raise(CurrentStamine);
    }

    public void SetRegenerate(bool regenerate) => canRegenerate = regenerate;
}
