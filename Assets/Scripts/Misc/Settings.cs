using UnityEngine;

public static class Settings
{
  
    // Fader Parameters
    public const float fadeInSeconds = 0.35f;
    public const float fadeOutSeconds = 0.35f;
    public const float targetAlpha = 0.45f;


    // Player Movement 
    public const float runningSpeed = 5.333f;
    public const float walkingSpeed = 2.666f;

    // Inventory
    public static int playerInitialInventoryCapacity = 24;
    public static int playerMaximumInventoryCapacity = 48;

    // Player Animation Parameters
    public static int xInput;
    public static int yInput;
    public static int isWalking;
    public static int isRunning;
    public static int toolEffect;
    public static int isUsingToolUp;
    public static int isUsingToolDown;
    public static int isUsingToolRight;
    public static int isUsingToolLeft;
    public static int isSwingingToolUp;
    public static int isSwingingToolDown;
    public static int isSwingingToolRight;
    public static int isSwingingToolLeft;
    public static int isLiftingToolUp;
    public static int isLiftingToolDown;
    public static int isLiftingToolRight;
    public static int isLiftingToolLeft;
    public static int isPickingUp;
    public static int isPickingDown;
    public static int isPickingRight;
    public static int isPickingLeft;

    // Shared Animation Parameters
    public static int idleUp;
    public static int idleDown;
    public static int idleRight;
    public static int idleLeft;

    // Tools
    public const string HoeingTool = "HoeingTool";
    public const string ChoppingTool = "ChoppingTool";
    public const string BreakingTool = "BreakingTool";
    public const string ReapingTool = "ReapingTool";
    public const string WateringTool = "WateringTool";
    public const string CollectingTool = "CollectingTool";

    // static constructor
    static Settings()
    {
        // Player Animation Parameters
        xInput = Animator.StringToHash("xInput");
        yInput = Animator.StringToHash("yInput");
        isWalking = Animator.StringToHash("isWalking");
        isRunning = Animator.StringToHash("isRunning");
        toolEffect = Animator.StringToHash("toolEffect");
        isUsingToolUp = Animator.StringToHash("isUsingToolUp");
        isUsingToolDown = Animator.StringToHash("isUsingToolDown");
        isUsingToolRight = Animator.StringToHash("isUsingToolRight");
        isUsingToolLeft = Animator.StringToHash("isUsingToolLeft");
        isSwingingToolUp = Animator.StringToHash("isSwingingToolUp");
        isSwingingToolDown = Animator.StringToHash("isSwingingToolDown");
        isSwingingToolRight = Animator.StringToHash("isSwingingToolRight");
        isSwingingToolLeft = Animator.StringToHash("isSwingingToolLeft");
        isLiftingToolUp = Animator.StringToHash("isLiftingToolUp");
        isLiftingToolDown = Animator.StringToHash("isLiftingToolDown");
        isLiftingToolRight = Animator.StringToHash("isLiftingToolRight");
        isLiftingToolLeft = Animator.StringToHash("isLiftingToolLeft");
        isPickingUp = Animator.StringToHash("isPickingUp");
        isPickingDown = Animator.StringToHash("isPickingDown");
        isPickingRight = Animator.StringToHash("isPickingRight");
        isPickingLeft = Animator.StringToHash("isPickingLeft");
        // Shared Animation Parameters
        idleUp = Animator.StringToHash("idleUp");
        idleDown = Animator.StringToHash("idleDown");
        idleRight = Animator.StringToHash("idleRight");
        idleLeft = Animator.StringToHash("idleLeft");
    }
}
