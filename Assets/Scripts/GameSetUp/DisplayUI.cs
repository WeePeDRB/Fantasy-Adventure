using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUI : MonoBehaviour
{
    // Character stats
    private SO_Hero heroData;
  

    // Skill panel
    // Buttons
    [SerializeField] private Button dashSkill;
    [SerializeField] private Button specialSkill;
    [SerializeField] private Button ultimateSkill;
    // Color outline
    [SerializeField] private Image dashSkillImage;
    [SerializeField] private Image specialSkillImage;
    [SerializeField] private Image ultimateSkillImage;
    // Skill key
    [SerializeField] private TextMeshProUGUI dashSkillKey;
    [SerializeField] private TextMeshProUGUI specialSkillKey;
    [SerializeField] private TextMeshProUGUI ultimatetSkillKey;

    // Skill description
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;

    // Character stat
    [SerializeField] private TextMeshProUGUI characterClass;
    [SerializeField] private TextMeshProUGUI characterHealth;
    [SerializeField] private TextMeshProUGUI characterSpeed;
    [SerializeField] private TextMeshProUGUI characterAmor;

    // Color
    private Color unselectedColor = new Color(73,73,73);
    private Color selectedColor = new Color(212, 174, 0);
    
    // Display suitable UI when change character 
    private void ChangeSkillImage()
    {
        dashSkill.image.sprite = heroData.dashSkill.skillSprite;
        specialSkill.image.sprite = heroData.specialSkill.skillSprite;
        ultimateSkill.image.sprite = heroData.ultimateSkill.skillSprite; 
    }
    private void ChangeCharacterStat()
    {

        characterClass.text = heroData.heroClass;
        characterHealth.text = heroData.maxHealth.ToString();
        characterSpeed.text = heroData.speed.ToString();
        characterAmor.text = heroData.maxAmor.ToString();
    }

    // Display skill information on click
    public void OnClickDashSkill()
    {
        // Display Skill Information
        skillName.text = heroData.dashSkill.skillName;
        skillDescription.text = heroData.dashSkill.skillDescription;

        // Display Color for Skills
        dashSkillImage.color = selectedColor;
        specialSkillImage.color = unselectedColor;
        ultimateSkillImage.color = unselectedColor;

        // Display color for key
        dashSkillKey.color = selectedColor;
        specialSkillKey.color = unselectedColor;
        ultimatetSkillKey.color = unselectedColor;
    }
    public void OnClickSpecialSkill()
    {
        // Display Skill Information
        skillName.text = heroData.specialSkill.skillName;
        skillDescription.text = heroData.specialSkill.skillDescription;
        
        // Display Color for Skills
        dashSkillImage.color = unselectedColor;
        specialSkillImage.color = selectedColor;
        ultimateSkillImage.color = unselectedColor;

        // Display color for key
        dashSkillKey.color = unselectedColor;
        specialSkillKey.color = selectedColor;
        ultimatetSkillKey.color = unselectedColor;
    }
    public void OnClickUltimateSkill()
    {
        // Display Skill Information
        skillName.text = heroData.ultimateSkill.skillName;
        skillDescription.text = heroData.ultimateSkill.skillDescription;

        // Display Color for Skills
        dashSkillImage.color = unselectedColor;
        specialSkillImage.color = unselectedColor;
        ultimateSkillImage.color = selectedColor;
        
        // Display color for key
        dashSkillKey.color = unselectedColor;
        specialSkillKey.color = unselectedColor;
        ultimatetSkillKey.color = selectedColor;
    }

    // Handler for event
    private void OnChangeCharacterHandler(object sender, CharacterSelection.HeroData data)
    {
        heroData = data.heroData;
        ChangeSkillImage();
        ChangeCharacterStat();
        OnClickDashSkill();
    }

    //
    private void Start()
    {
        CharacterSelection.OnChangeHero += OnChangeCharacterHandler;
    }
}
