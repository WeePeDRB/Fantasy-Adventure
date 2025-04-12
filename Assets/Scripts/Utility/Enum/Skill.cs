// This show what type the skill is
public enum SkillType
{
    Active,     // This type of skill will be actively triggered by player 
    Passive    // This type of skill will be passively triggered by external factors such as monsters, dropped items, etc.
}

// The skill's area of effect (whether it's single-target or multi-target)
public enum SkillTargetType
{
    AOE,    // Area of Effect – impacts multiple targets within a designated area
    ST      // Single Target – strikes only one specific enemy.
}

// Indicates the special effect of the skill on the target.
public enum SkillCategory
{
    Support,  // Deals damage (can be instant or damage over time)
    Attack,   // Creates a shield, reduces damage
    Defend,   // Buff, healing, speed boost, crowd control...
    Dash,     // A unique type of skill designed to help the player move to a desired 
              // location over a short distance. Depending on the character, 
              // the dash skill may also provide attack, defend, or support capabilities
}