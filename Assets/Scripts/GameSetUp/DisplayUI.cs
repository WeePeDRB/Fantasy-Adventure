using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUI : MonoBehaviour
{
    // Character stats
    private SO_Character characterInfo;
  

    // Skill panel
    [SerializeField] private Button dashSkill;
    [SerializeField] private Button specialSkill;
    [SerializeField] private Button ultimateSkill;

    // Skill description
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;

    // Character stat
    [SerializeField] private TextMeshProUGUI characterClass;
    [SerializeField] private TextMeshProUGUI characterHealth;
    [SerializeField] private TextMeshProUGUI characterSpeed;
    [SerializeField] private TextMeshProUGUI characterAmor;

    
    // Display suitable UI when change character 
    private void ChangeSkillImage()
    {
        dashSkill.image.sprite = characterInfo.dashSkill.skillSprite;
        specialSkill.image.sprite = characterInfo.specialSkill.skillSprite;
        ultimateSkill.image.sprite = characterInfo.ultimateSkill.skillSprite; 
    }
    private void ChangeCharacterStat()
    {

        characterClass.text = characterInfo.characterClass;
        characterHealth.text = characterInfo.maxHealth.ToString();
        characterSpeed.text = characterInfo.speed.ToString();
        characterAmor.text = characterInfo.maxAmor.ToString();
    }

    // Display skill information on click
    private void OnClickDashSkill()
    {
        skillName.text = characterInfo.dashSkill.skillName;
        skillDescription.text = characterInfo.dashSkill.skillDescription;
    }
    private void OnClickSpecialSkill()
    {
        skillName.text = characterInfo.specialSkill.skillName;
        skillDescription.text = characterInfo.specialSkill.skillDescription;
    }
    private void OnClickUltimateSkill()
    {
        skillName.text = characterInfo.ultimateSkill.skillName;
        skillDescription.text = characterInfo.ultimateSkill.skillDescription;
    }

    // Handler for event
    private void OnChangeCharacterHandler(object sender, CharacterSelection.CharacterInfo info)
    {
        characterInfo = info.characterData;
        ChangeSkillImage();
        ChangeCharacterStat();
    }

    //
    private void Start()
    {
        CharacterSelection.OnChangeCharacter += OnChangeCharacterHandler;
    }
}
