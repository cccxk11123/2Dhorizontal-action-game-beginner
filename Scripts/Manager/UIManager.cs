using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;

    private Canvas gameCanvas;

    private void Awake() 
    {
        gameCanvas = FindObjectOfType<Canvas>();    
    }

    private void OnEnable() 
    {
        CharacterEvent.charactertookDamage += CharactertookDamage;
        CharacterEvent.characterHealed += CharacterHealed;    
    }

    private void OnDisable() 
    {
        CharacterEvent.charactertookDamage -= CharactertookDamage;
        CharacterEvent.characterHealed -= CharacterHealed;  
    }

    public void CharactertookDamage(GameObject character, float damage)
    {
        //在被攻击的位置生成
        Vector3 spawanPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawanPosition, Quaternion.identity, gameCanvas.transform)
                                .GetComponent<TMP_Text>();
        tmpText.text = damage.ToString();
    }

    public void CharacterHealed(GameObject character, float healthRestored)
    {
        Vector3 spawanPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawanPosition, Quaternion.identity, gameCanvas.transform)
                                .GetComponent<TMP_Text>();
        tmpText.text = healthRestored.ToString();
    }

    public void OnGameExit(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            #if(UNITY_EDITOR || DEVELOPMENT_BUILD)
                Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            #endif

            #if(UNITY_EDITOR)
                UnityEditor.EditorApplication.isPlaying = false;
            #elif(UNITY_STANDALONE)
                Application.Quit();
            #elif(UNITY_WEBGL)
                SceneManager.LoadSecne("QuitGameScene");
            #endif
        }
    }
}
